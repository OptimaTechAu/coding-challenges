import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Priority, Task } from '../../models';
import { DateFilterRange, SortDirection, TaskService } from '../../services';
import {
  ICONS,
  PRIORITY_CLASSES,
  DEFAULT_DATE_RANGE,
  DEFAULT_SORT_DIRECTION,
  DATE_FORMATS,
} from '../../constants/index';
import { formatDateToIsoString } from '../../utils/date.utils';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TaskListComponent implements OnInit {
  tasks$!: Observable<Task[]>;
  selectedPriorities: Priority[] = [];
  dateRange: DateFilterRange = DEFAULT_DATE_RANGE;
  sortDirection: SortDirection = DEFAULT_SORT_DIRECTION;
  isFiltersExpanded = false;

  Priority = Priority;
  dateFilterForm!: FormGroup;
  DATE_FORMATS = DATE_FORMATS;

  // Icons
  icons = ICONS;

  constructor(private taskService: TaskService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.tasks$ = this.taskService.getFilteredTasks();
    this.dateFilterForm = this.fb.group({
      startDate: [null],
      endDate: [null],
    });
    this.selectedPriorities = this.taskService.getSelectedPriorities();
    this.dateRange = this.taskService.getDateRange();
    this.sortDirection = this.taskService.getSortDirection();

    // Initialize form values
    this.dateFilterForm.setValue({
      startDate: this.formatDateForInput(this.dateRange.startDate),
      endDate: this.formatDateForInput(this.dateRange.endDate),
    });

    // If there are active filters, expand the filter section
    if (this.isAnyFilterActive()) {
      this.isFiltersExpanded = true;
    }
  }

  toggleFiltersVisibility(): void {
    this.isFiltersExpanded = !this.isFiltersExpanded;
  }

  toggleTaskCompletion(taskId: string): void {
    this.taskService.toggleTaskCompletion(taskId);
  }

  deleteTask(taskId: string): void {
    this.taskService.deleteTask(taskId);
  }

  isPrioritySelected(priority: Priority): boolean {
    return this.selectedPriorities.includes(priority);
  }

  togglePriorityFilter(priority: Priority): void {
    this.taskService.togglePriorityFilter(priority);
    this.selectedPriorities = this.taskService.getSelectedPriorities();
  }

  clearPriorityFilters(): void {
    this.taskService.clearPriorityFilters();
    this.selectedPriorities = [];
  }

  applyDateFilter(): void {
    const formValues = this.dateFilterForm.value;
    const dateRange: DateFilterRange = {
      startDate: formValues.startDate ? new Date(formValues.startDate) : null,
      endDate: formValues.endDate ? new Date(formValues.endDate) : null,
    };

    this.taskService.setDateRange(dateRange);
    this.dateRange = this.taskService.getDateRange();
  }

  clearDateFilter(): void {
    this.dateFilterForm.reset();
    this.taskService.clearDateFilter();
    this.dateRange = DEFAULT_DATE_RANGE;
  }

  toggleSortDirection(): void {
    this.taskService.toggleSortDirection();
    this.sortDirection = this.taskService.getSortDirection();
  }

  clearAllFilters(): void {
    this.taskService.clearAllFilters();
    this.selectedPriorities = [];
    this.dateRange = DEFAULT_DATE_RANGE;
    this.dateFilterForm.reset();
  }

  isAnyFilterActive(): boolean {
    return (
      this.selectedPriorities.length > 0 ||
      this.dateRange.startDate !== null ||
      this.dateRange.endDate !== null
    );
  }

  getPriorityClass(priority: Priority): string {
    return PRIORITY_CLASSES[priority] || '';
  }

  private formatDateForInput(date: Date | null): string | null {
    return formatDateToIsoString(date);
  }
}
