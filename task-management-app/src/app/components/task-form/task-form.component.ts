import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Priority } from '../../models';
import { TaskService } from '../../services';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TaskFormComponent implements OnInit {
  taskForm!: FormGroup;
  priorities = Object.values(Priority);
  formSubmitted = false;

  constructor(private fb: FormBuilder, private taskService: TaskService) {}

  ngOnInit(): void {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)],
      priority: [Priority.Medium, Validators.required],
    });
  }

  get title() {
    return this.taskForm.get('title');
  }
  get description() {
    return this.taskForm.get('description');
  }

  onSubmit(): void {
    this.formSubmitted = true;

    if (this.taskForm.valid) {
      this.taskService.addTask({
        title: this.taskForm.value.title,
        description: this.taskForm.value.description,
        priority: this.taskForm.value.priority,
        completed: false,
      });

      this.taskForm.reset({ priority: Priority.Medium });
      this.formSubmitted = false;
    }
  }
}
