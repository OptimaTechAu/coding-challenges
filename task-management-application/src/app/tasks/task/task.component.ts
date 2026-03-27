import { Component, EventEmitter, Input, Output } from '@angular/core';

import { Task } from '../../../model/task.model';

@Component({
  selector: 'app-task',
  imports: [],
  standalone: true,
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {
  @Input() task!: Task;
  @Output() taskComplete: EventEmitter<number> = new EventEmitter<number>();

  onCompleteTask() {
    this.taskComplete.emit(this.task.id);
  }
}
