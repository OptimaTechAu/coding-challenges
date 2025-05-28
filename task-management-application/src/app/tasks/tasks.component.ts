import { Component, Input, SimpleChanges } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { NewTaskComponent } from './new-task/new-task.component';
import { Task } from '../../model/task.model';
import { TaskComponent } from './task/task.component';

@Component({
  selector: 'app-tasks',
  imports: [TaskComponent, NewTaskComponent, FormsModule],
  standalone: true,
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent {
  @Input() tasks!: Task[];
  public selectedPriority: string = 'high';
  public filteredTasks: Task[] = [];
  public isAddingTask: boolean = false;

  private _internalTasks: Task[] = [];

  ngOnInit() {
    this.syncInternalTasks();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['tasks']) {
      this.syncInternalTasks();
    }
  }

  private syncInternalTasks() {
    this._internalTasks = [...this.tasks];
    this.applyFilter();
  }

  private applyFilter() {
    if (this.selectedPriority) {
      this.filteredTasks = this._internalTasks.filter(
        task => task.priority.toLowerCase() === this.selectedPriority.toLowerCase()
      );
    } else {
      this.filteredTasks = [...this._internalTasks];
    }
  }

  onCompleteTask(taskId: number) {
    this._internalTasks = this._internalTasks.map(t =>
      t.id === taskId ? { ...t, completed: !t.completed } : t
    );
    this.applyFilter();
  }

  onStartAddTask() {
    this.isAddingTask = true;
  }

  onCancelAddTask() {
    this.isAddingTask = false;
  }

  onAddTask(newTask: Task) {
    this._internalTasks = [...this._internalTasks, newTask];
    this.isAddingTask = false;
    this.applyFilter();
  }

  onPriorityChange() {
    this.applyFilter();
  }
}
