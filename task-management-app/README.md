# Task Management Application - Angular Coding Test

This repository contains my implementation of a task management application as part of a coding challenge. The application allows users to create, manage, filter, and sort tasks with a responsive interface and theme switching capabilities.

## Technology Stack

- **Frontend Framework**: Angular 15.2.6
- **Language**: TypeScript
- **Node.js Version**: 18.12.1
- **Styling**: SCSS with CSS Variables for theming
- **UI Components**: Font Awesome icons
- **Testing**: Jasmine and Karma
- **Containerization**: Docker

## Setup Instructions

### Option 1: Manual Setup

1. Ensure Node.js v18.12.1 and npm are installed
2. Clone this repository
3. Navigate to the project directory
4. Install dependencies:

```bash
npm install
```

5. Start the development server:

```bash
npm start
```

The application will be available at [http://localhost:4200](http://localhost:4200)

### Option 2: Using Docker

1. Ensure Docker is installed on your machine
2. Clone this repository
3. Navigate to the project directory
4. Build and run with Docker:

```bash
# Build and run in one command
npm run docker:start

# Or individual steps
npm run docker:build
npm run docker:run

# To stop and remove the container
npm run docker:stop
```

The application will be available at [http://localhost:4200](http://localhost:4200)

## Component Structure

The application follows a modular component structure:

- **AppComponent**: Main container component
- **TaskListComponent**: Displays the list of tasks and contains filtering functionality
- **TaskFormComponent**: Handles task creation with a form
- **ThemeSwitcherComponent**: Allows switching between light and dark themes

### Services

- **TaskService**: Handles all task operations and filtering logic

Due to time constraints, I did not separate the filter functionality into a dedicated service. In a production application, I would extract filter logic into a separate FilterService for better separation of concerns.

## Design Decisions & Assumptions

1. **Single Page Application**: The app is designed as a single page without routing, assuming it's a focused task management tool.

2. **In-Memory Data**: All data is stored in-memory using Angular services, assuming this is a frontend-only demonstration without backend persistence.

3. **Responsive Design**: The UI is fully responsive using Flexbox and media queries, tested on both desktop and mobile viewports.

4. **Theme Support**: Implemented using CSS variables for easy theme switching without requiring component reloading.

5. **Accessibility**: Made a conscious effort to ensure the app is keyboard navigable and uses proper ARIA attributes.

## Completed Requirements

### Core Features

- ✅ Create and manage tasks with title, description, and priority levels
- ✅ Mark tasks as complete/incomplete
- ✅ Delete tasks
- ✅ Clear visual indication of task priorities and completion status

### Filtering & Sorting

- ✅ Filter tasks by priority (High, Medium, Low)
- ✅ Filter tasks by date range
- ✅ Sort tasks by creation date (newest/oldest first)
- ✅ Clear indication of active filters
- ✅ Ability to reset/clear all filters

### UI/UX

- ✅ Responsive design that works on mobile and desktop
- ✅ Clean, modern UI with good visual hierarchy
- ✅ Theme switching between light and dark modes
- ✅ Visual feedback for user actions
- ✅ Accessible UI with keyboard navigation support

## Running Tests

```bash
npm run test
```

## Running Linting

```bash
npm run lint
```
