import { Component, EventEmitter, Output } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { NgForm } from '@angular/forms';
import { Task } from '../../../model/task.model';

@Component({
  selector: 'app-new-task',
  imports: [FormsModule],
  standalone: true,
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.css'
})
export class NewTaskComponent {
  @Output() addingTaskCancelled = new EventEmitter<void>();
  @Output() addingTask = new EventEmitter<Task>();
  enteredTitle = '';
  enteredDescription = '';
  enteredPriority = 'high';

  onAddTask() {
    if (this.enteredTitle.trim() === '' || this.enteredDescription.trim() === '') {
      return;
    }
    this.addingTask.emit({
      id: Math.floor(Math.random() * 1000000), // Generate a random ID
      title: this.enteredTitle,
      description: this.enteredDescription,
      priority: this.enteredPriority,
      completed: false
    } as Task);
    // Reset the form fields after adding the task
    this.enteredTitle = '';
    this.enteredDescription = '';
    this.enteredPriority = 'high';
  }

  onSubmit(taskForm: NgForm) {
    if (taskForm.invalid) {
      return;
    }
    this.onAddTask();
  }

  onCancel() {
    this.addingTaskCancelled.emit();
  }
}
