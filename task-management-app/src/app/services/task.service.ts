import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { DateFilterRange, Priority, SortDirection, Task } from '../models';
import { DEFAULT_DATE_RANGE, DEFAULT_SORT_DIRECTION } from '../constants/index';
import { TASKS_INITIAL_DATA } from '../constants/task.constants';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private initialTasks: Task[] = TASKS_INITIAL_DATA;

  private tasksSubject = new BehaviorSubject<Task[]>(this.initialTasks);
  private filteredTasksSubject = new BehaviorSubject<Task[]>(this.initialTasks);

  // Filtering state
  private selectedPriorities = new Set<Priority>();
  private dateRange: DateFilterRange = DEFAULT_DATE_RANGE;

  // Sorting state
  private sortDirection: SortDirection = DEFAULT_SORT_DIRECTION;

  constructor() {
    // Initialize with default values
    this.applyFiltersAndSort();
  }

  getTasks(): Observable<Task[]> {
    return this.tasksSubject.asObservable();
  }

  getFilteredTasks(): Observable<Task[]> {
    return this.filteredTasksSubject.asObservable();
  }

  getSelectedPriorities(): Priority[] {
    return Array.from(this.selectedPriorities);
  }

  getDateRange(): DateFilterRange {
    return this.dateRange;
  }

  getSortDirection(): SortDirection {
    return this.sortDirection;
  }

  addTask(task: Omit<Task, 'id' | 'createdAt'>): void {
    const newTask: Task = {
      ...task,
      id: Date.now().toString(),
      createdAt: new Date(),
    };

    const updatedTasks = [...this.tasksSubject.value, newTask];
    this.tasksSubject.next(updatedTasks);
    this.applyFiltersAndSort();
  }

  updateTask(updatedTask: Task): void {
    const tasks = this.tasksSubject.value;
    const index = tasks.findIndex((task) => task.id === updatedTask.id);

    if (index !== -1) {
      const updatedTasks = [
        ...tasks.slice(0, index),
        updatedTask,
        ...tasks.slice(index + 1),
      ];

      this.tasksSubject.next(updatedTasks);
      this.applyFiltersAndSort();
    }
  }

  toggleTaskCompletion(taskId: string): void {
    const tasks = this.tasksSubject.value;
    const taskIndex = tasks.findIndex((task) => task.id === taskId);

    if (taskIndex !== -1) {
      const task = tasks[taskIndex];
      const updatedTask = { ...task, completed: !task.completed };

      this.updateTask(updatedTask);
    }
  }

  deleteTask(taskId: string): void {
    const tasks = this.tasksSubject.value;
    const updatedTasks = tasks.filter((task) => task.id !== taskId);

    this.tasksSubject.next(updatedTasks);
    this.applyFiltersAndSort();
  }

  togglePriorityFilter(priority: Priority): void {
    if (this.selectedPriorities.has(priority)) {
      this.selectedPriorities.delete(priority);
    } else {
      this.selectedPriorities.add(priority);
    }
    this.applyFiltersAndSort();
  }

  clearPriorityFilters(): void {
    this.selectedPriorities.clear();
    this.applyFiltersAndSort();
  }

  setDateRange(dateRange: DateFilterRange): void {
    this.dateRange = dateRange;
    this.applyFiltersAndSort();
  }

  clearDateFilter(): void {
    this.dateRange = { startDate: null, endDate: null };
    this.applyFiltersAndSort();
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.applyFiltersAndSort();
  }

  setSortDirection(direction: SortDirection): void {
    this.sortDirection = direction;
    this.applyFiltersAndSort();
  }

  clearAllFilters(): void {
    this.selectedPriorities.clear();
    this.dateRange = { startDate: null, endDate: null };
    this.applyFiltersAndSort();
  }

  private applyFiltersAndSort(): void {
    let filteredTasks = this.tasksSubject.value;

    // Apply priority filters if any are selected
    if (this.selectedPriorities.size > 0) {
      filteredTasks = filteredTasks.filter((task) =>
        this.selectedPriorities.has(task.priority)
      );
    }

    // Apply date range filter if set
    if (this.dateRange.startDate || this.dateRange.endDate) {
      filteredTasks = filteredTasks.filter((task) => {
        const taskDate = new Date(task.createdAt);

        // Check if task date is after start date (if set)
        if (this.dateRange.startDate) {
          const startDate = new Date(this.dateRange.startDate);
          startDate.setHours(0, 0, 0, 0);
          if (taskDate < startDate) return false;
        }

        // Check if task date is before end date (if set)
        if (this.dateRange.endDate) {
          const endDate = new Date(this.dateRange.endDate);
          endDate.setHours(23, 59, 59, 999);
          if (taskDate > endDate) return false;
        }

        return true;
      });
    }

    // Sort by date
    filteredTasks = [...filteredTasks].sort((a, b) => {
      const dateA = new Date(a.createdAt).getTime();
      const dateB = new Date(b.createdAt).getTime();
      return this.sortDirection === 'asc'
        ? dateA - dateB // Oldest first
        : dateB - dateA; // Newest first
    });

    this.filteredTasksSubject.next(filteredTasks);
  }
}
