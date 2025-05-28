import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Task } from '../../../model/task.model';
import { TaskComponent } from './task.component';

describe('TaskComponent', () => {
  let component: TaskComponent;
  let fixture: ComponentFixture<TaskComponent>;

  const mockTask: Task = {
    id: 1,
    title: 'Test Task',
    description: 'Test Description',
    priority: 'high',
    completed: false
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskComponent);
    component = fixture.componentInstance;
    component.task = mockTask;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
