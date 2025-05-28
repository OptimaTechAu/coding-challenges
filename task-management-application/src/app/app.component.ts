import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { MOCK_TASKS } from '../mock/mock-tasks';
import { RouterOutlet } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, TasksComponent],
  standalone: true,
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'task-management-application';
  tasks = MOCK_TASKS;
}
