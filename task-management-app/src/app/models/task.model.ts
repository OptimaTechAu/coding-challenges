export enum Priority {
  Low = 'Low',
  Medium = 'Medium',
  High = 'High',
}

export interface Task {
  id: string;
  title: string;
  description: string;
  completed: boolean;
  priority: Priority;
  createdAt: Date;
}
