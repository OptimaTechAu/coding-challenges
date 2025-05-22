import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Theme, ThemeService } from '../../services';
import { Observable } from 'rxjs';
import { ICONS } from '../../constants/index';

@Component({
  selector: 'app-theme-switcher',
  templateUrl: './theme-switcher.component.html',
  styleUrls: ['./theme-switcher.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ThemeSwitcherComponent implements OnInit {
  currentTheme$!: Observable<Theme>;
  icons = ICONS;

  constructor(private themeService: ThemeService) {}

  ngOnInit(): void {
    this.currentTheme$ = this.themeService.getTheme();
  }

  toggleTheme(currentTheme: Theme): void {
    const newTheme: Theme = currentTheme === 'light' ? 'dark' : 'light';
    this.themeService.setTheme(newTheme);
  }
}
