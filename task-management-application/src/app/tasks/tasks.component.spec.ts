import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormsModule } from '@angular/forms';
import { NewTaskComponent } from './new-task/new-task.component';
import { Task } from '../../model/task.model';
import { TaskComponent } from './task/task.component';
import { TasksComponent } from './tasks.component';

describe('TasksComponent', () => {
  let fixture: ComponentFixture<TasksComponent>;
  let component: TasksComponent;

  const initialTasks: Task[] = [
    { id: 1, title: 'Task 1', description: 'Desc 1', priority: 'high', completed: false },
    { id: 2, title: 'Task 2', description: 'Desc 2', priority: 'medium', completed: false },
    { id: 3, title: 'Task 3', description: 'Desc 3', priority: 'low', completed: false }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TasksComponent, TaskComponent, NewTaskComponent, FormsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(TasksComponent);
    component = fixture.componentInstance;
    component.tasks = [...initialTasks];
    component.selectedPriority = 'high';
    fixture.detectChanges();
  });

  it('should display only high priority tasks when filtered by high', () => {
    component.selectedPriority = 'high';
    fixture.detectChanges();

    expect(component.filteredTasks.length).toBe(1);
    expect(component.filteredTasks[0].priority).toBe('high');
    // Optionally, check the DOM
    const taskTitles = fixture.nativeElement.querySelectorAll('app-task h2');
    expect(taskTitles.length).toBe(1);
    expect(taskTitles[0].textContent).toContain('Task 1');
  });

  it('should add a new high priority task and show it in the filtered list', () => {
    const newTask: Task = {
      id: 4,
      title: 'Submit report',
      description: 'Submit the quarterly report',
      priority: 'high',
      completed: false
    };
    component.onAddTask(newTask);
    component.selectedPriority = 'high';
    fixture.detectChanges();

    // The filtered list should now include the new task
    expect(component.filteredTasks.some(t => t.title === 'Submit report')).toBeTrue();
    // Optionally, check the DOM
    const taskTitles = fixture.nativeElement.querySelectorAll('app-task h2');
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const titles = Array.from(taskTitles).map((el: any) => el.textContent);
    expect(titles).toContain('Submit report');
  });

  it('should show a visual indication that a filter is applied', () => {
    // This assumes you add a CSS class or text when a filter is active
    // For example, in your template: <span *ngIf="selectedPriority">Filtered by: {{selectedPriority}}</span>
    component.selectedPriority = 'high';
    fixture.detectChanges();

    // Simulate a visual indicator in the DOM
    // Add this to your template for the test to pass:
    // <span class="filter-indicator" *ngIf="selectedPriority">Filtered by: {{selectedPriority}}</span>
    const indicator = fixture.nativeElement.querySelector('.filter-indicator');
    expect(indicator).toBeTruthy();
    expect(indicator.textContent).toContain('Filtered by: high');
  });

  it('should show all tasks when default filter is applied', () => {
    component.selectedPriority = '';
    fixture.detectChanges();

    expect(component.filteredTasks.length).toBe(1);
  });
});
