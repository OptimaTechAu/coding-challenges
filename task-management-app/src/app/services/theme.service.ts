import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { DEFAULT_THEME, THEME_STORAGE_KEY } from '../constants/theme.constants';

export type Theme = 'light' | 'dark';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private themeSubject = new BehaviorSubject<Theme>(DEFAULT_THEME);

  constructor() {
    const savedTheme = localStorage.getItem(THEME_STORAGE_KEY) as Theme;
    if (savedTheme) {
      this.themeSubject.next(savedTheme);
      this.applyTheme(savedTheme);
    }
  }

  getTheme(): Observable<Theme> {
    return this.themeSubject.asObservable();
  }

  setTheme(theme: Theme): void {
    localStorage.setItem(THEME_STORAGE_KEY, theme);
    this.themeSubject.next(theme);
    this.applyTheme(theme);
  }

  private applyTheme(theme: Theme): void {
    document.body.className = '';
    document.body.classList.add(`theme-${theme}`);
  }
}
