import { TestBed } from '@angular/core/testing';
import { TaskService, DateFilterRange } from './task.service';
import { Priority } from '../models';
import { take } from 'rxjs/operators';

describe('TaskService', () => {
  let service: TaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should initially return all tasks', (done) => {
    service
      .getTasks()
      .pipe(take(1))
      .subscribe((tasks) => {
        expect(tasks.length).toBeGreaterThan(0);
        done();
      });
  });

  it('should initially not have any priority filters active', () => {
    expect(service.getSelectedPriorities().length).toBe(0);
  });

  it('should add a new task', (done) => {
    const initialTaskCount = service['tasksSubject'].value.length;

    const newTask = {
      title: 'New Test Task',
      description: 'Test Description',
      priority: Priority.Medium,
      completed: false,
    };

    service.addTask(newTask);

    service
      .getTasks()
      .pipe(take(1))
      .subscribe((tasks) => {
        expect(tasks.length).toBe(initialTaskCount + 1);

        const addedTask = tasks.find((t) => t.title === newTask.title);
        expect(addedTask).toBeTruthy();
        expect(addedTask?.description).toBe(newTask.description);
        expect(addedTask?.priority).toBe(newTask.priority);
        expect(addedTask?.completed).toBe(newTask.completed);
        expect(addedTask?.id).toBeTruthy();
        expect(addedTask?.createdAt).toBeInstanceOf(Date);
        done();
      });
  });

  it('should toggle task completion status', (done) => {
    service
      .getTasks()
      .pipe(take(1))
      .subscribe((initialTasks) => {
        const targetTask = initialTasks[0];
        const initialStatus = targetTask.completed;

        service.toggleTaskCompletion(targetTask.id);

        service
          .getTasks()
          .pipe(take(1))
          .subscribe((updatedTasks) => {
            const updatedTask = updatedTasks.find(
              (t) => t.id === targetTask.id
            );
            expect(updatedTask?.completed).toBe(!initialStatus);
            done();
          });
      });
  });

  it('should delete a task', (done) => {
    service
      .getTasks()
      .pipe(take(1))
      .subscribe((initialTasks) => {
        expect(initialTasks.length).toBeGreaterThan(0);
        const initialCount = initialTasks.length;
        const taskToDelete = initialTasks[0];

        service.deleteTask(taskToDelete.id);

        service
          .getTasks()
          .pipe(take(1))
          .subscribe((updatedTasks) => {
            expect(updatedTasks.length).toBe(initialCount - 1);
            expect(
              updatedTasks.find((t) => t.id === taskToDelete.id)
            ).toBeUndefined();
            done();
          });
      });
  });

  it('should toggle priority filter', (done) => {
    // Initial state - no priorities selected
    expect(service.getSelectedPriorities().length).toBe(0);

    // Toggle high priority on
    service.togglePriorityFilter(Priority.High);
    expect(service.getSelectedPriorities()).toContain(Priority.High);

    // Get filtered tasks - should only include high priority
    service
      .getFilteredTasks()
      .pipe(take(1))
      .subscribe((filteredTasks) => {
        expect(
          filteredTasks.every((t) => t.priority === Priority.High)
        ).toBeTrue();

        // Toggle high priority off
        service.togglePriorityFilter(Priority.High);
        expect(service.getSelectedPriorities().length).toBe(0);

        done();
      });
  });

  it('should clear priority filters', () => {
    service.togglePriorityFilter(Priority.High);
    service.togglePriorityFilter(Priority.Medium);
    expect(service.getSelectedPriorities().length).toBe(2);

    service.clearPriorityFilters();
    expect(service.getSelectedPriorities().length).toBe(0);
  });

  it('should set and clear date range filter', () => {
    const dateRange: DateFilterRange = {
      startDate: new Date('2023-01-01'),
      endDate: new Date('2023-12-31'),
    };

    service.setDateRange(dateRange);
    expect(service.getDateRange()).toEqual(dateRange);

    service.clearDateFilter();
    expect(service.getDateRange().startDate).toBeNull();
    expect(service.getDateRange().endDate).toBeNull();
  });

  it('should toggle sort direction', () => {
    expect(service.getSortDirection()).toBe('desc'); // Default is desc

    service.toggleSortDirection();
    expect(service.getSortDirection()).toBe('asc');

    service.toggleSortDirection();
    expect(service.getSortDirection()).toBe('desc');
  });

  it('should clear all filters', () => {
    // Set up some filters
    service.togglePriorityFilter(Priority.High);
    service.setDateRange({
      startDate: new Date(),
      endDate: null,
    });

    service.clearAllFilters();

    // Check all filters were cleared
    expect(service.getSelectedPriorities().length).toBe(0);
    expect(service.getDateRange().startDate).toBeNull();
    expect(service.getDateRange().endDate).toBeNull();
  });
});
