import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { TaskFormComponent } from './task-form.component';
import { TaskService } from '../../services';
import { Priority } from '../../models';

describe('TaskFormComponent', () => {
  let component: TaskFormComponent;
  let fixture: ComponentFixture<TaskFormComponent>;
  let taskServiceSpy: jasmine.SpyObj<TaskService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('TaskService', ['addTask']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [TaskFormComponent],
      providers: [{ provide: TaskService, useValue: spy }],
    }).compileComponents();

    taskServiceSpy = TestBed.inject(TaskService) as jasmine.SpyObj<TaskService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with default values', () => {
    expect(component.taskForm.get('title')?.value).toBe('');
    expect(component.taskForm.get('description')?.value).toBe('');
    expect(component.taskForm.get('priority')?.value).toBe(Priority.Medium);
  });

  it('should mark form as invalid when title is empty', () => {
    component.taskForm.patchValue({
      title: '',
      description: 'Test description',
      priority: Priority.Low,
    });

    expect(component.taskForm.valid).toBeFalse();
  });

  it('should mark form as valid with required fields', () => {
    component.taskForm.patchValue({
      title: 'Test Task',
      description: 'Test description',
      priority: Priority.Low,
    });

    expect(component.taskForm.valid).toBeTrue();
  });

  it('should add a new task when form is submitted with valid data', () => {
    // Set up form with valid data
    component.taskForm.patchValue({
      title: 'New Task',
      description: 'Task description',
      priority: Priority.High,
    });

    // Submit form
    component.onSubmit();

    // Verify that addTask was called with correct data
    expect(taskServiceSpy.addTask).toHaveBeenCalledWith({
      title: 'New Task',
      description: 'Task description',
      priority: Priority.High,
      completed: false,
    });
  });

  it('should reset the form after successful submission', () => {
    // Set up the form with valid data
    component.taskForm.patchValue({
      title: 'New Task',
      description: 'Task description',
      priority: Priority.High,
    });

    // Reset form spy
    spyOn(component.taskForm, 'reset').and.callThrough();

    // Submit form
    component.onSubmit();

    // Verify form was reset
    expect(component.taskForm.reset).toHaveBeenCalled();
  });

  it('should not submit if the form is invalid', () => {
    // Set up form with invalid data (missing title)
    component.taskForm.patchValue({
      title: '',
      description: 'Task description',
      priority: Priority.High,
    });

    // Submit form
    component.onSubmit();

    // Verify addTask was not called
    expect(taskServiceSpy.addTask).not.toHaveBeenCalled();
  });
});
