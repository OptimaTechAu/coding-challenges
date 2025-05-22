import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TaskListComponent } from './task-list.component';
import { TaskService } from '../../services';
import { Priority, Task } from '../../models';
import { of } from 'rxjs';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let taskServiceSpy: jasmine.SpyObj<TaskService>;

  const mockTasks: Task[] = [
    {
      id: '1',
      title: 'Submit report',
      description: 'Submit quarterly report to management',
      completed: false,
      priority: Priority.High,
      createdAt: new Date(),
    },
    {
      id: '2',
      title: 'Clean desk',
      description: 'Organize and clean desk area',
      completed: false,
      priority: Priority.Low,
      createdAt: new Date(),
    },
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('TaskService', [
      'getFilteredTasks',
      'toggleTaskCompletion',
      'deleteTask',
      'togglePriorityFilter',
      'getSelectedPriorities',
      'getDateRange',
      'getSortDirection',
      'clearPriorityFilters',
      'clearDateFilter',
      'clearAllFilters',
    ]);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FontAwesomeModule],
      declarations: [TaskListComponent],
      providers: [{ provide: TaskService, useValue: spy }],
    }).compileComponents();

    taskServiceSpy = TestBed.inject(TaskService) as jasmine.SpyObj<TaskService>;
    taskServiceSpy.getFilteredTasks.and.returnValue(of(mockTasks));
    taskServiceSpy.getSelectedPriorities.and.returnValue([]);
    taskServiceSpy.getDateRange.and.returnValue({
      startDate: null,
      endDate: null,
    });
    taskServiceSpy.getSortDirection.and.returnValue('desc');
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display tasks from the task service', () => {
    expect(taskServiceSpy.getFilteredTasks).toHaveBeenCalled();
  });

  it('should toggle task completion when toggleTaskCompletion is called', () => {
    const taskId = '1';
    component.toggleTaskCompletion(taskId);
    expect(taskServiceSpy.toggleTaskCompletion).toHaveBeenCalledWith(taskId);
  });

  it('should delete a task when deleteTask is called', () => {
    const taskId = '1';
    component.deleteTask(taskId);
    expect(taskServiceSpy.deleteTask).toHaveBeenCalledWith(taskId);
  });

  it('should toggle priority filter when togglePriorityFilter is called', () => {
    component.togglePriorityFilter(Priority.High);
    expect(taskServiceSpy.togglePriorityFilter).toHaveBeenCalledWith(
      Priority.High
    );
  });

  it('should clear priority filters when clearPriorityFilters is called', () => {
    component.clearPriorityFilters();
    expect(taskServiceSpy.clearPriorityFilters).toHaveBeenCalled();
  });

  it('should return the correct priority class', () => {
    expect(component.getPriorityClass(Priority.High)).toBe('priority-high');
    expect(component.getPriorityClass(Priority.Medium)).toBe('priority-medium');
    expect(component.getPriorityClass(Priority.Low)).toBe('priority-low');
  });

  // Example test scenario from requirements
  it('should check if priority is selected', () => {
    taskServiceSpy.getSelectedPriorities.and.returnValue([Priority.High]);
    component.selectedPriorities = [Priority.High];

    expect(component.isPrioritySelected(Priority.High)).toBeTrue();
    expect(component.isPrioritySelected(Priority.Low)).toBeFalse();
  });
});
