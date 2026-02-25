import { Component, ChangeDetectionStrategy } from '@angular/core';
import { ThemeService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  title = 'Task Management';

  constructor(private themeService: ThemeService) {}

  getCurrentYear(): number {
    return new Date().getFullYear();
  }
}
