# TaskManagementApplication

This project contains my implementation of a task management application as part of a coding challenge. The application allows users to create, manage, filter tasks with a simple responsive interface.

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.13.

## Development server

To prepare the local development environment:

1. Ensure Node.js v18.19.1 and npm are installed

2. Install all the dependencies

```bash
npm install
```

3. To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Linting

To lint your code using ESLint, run:

```bash
ng lint
```
> **Note:** If your `package.json` contains `"lint": "ng lint"`, update it to:
> ```json
> "lint": "eslint '**/*.{ts,html}'"
> ```

## Completed Requirements

### Core Features
- ✅ Create and manage tasks with title, description, and priority levels
- ✅ Mark tasks as complete/completed
### Filtering & Sorting
- ✅ Filter tasks by priority (High, Medium, Low)
- ✅ Indication of active filters
### UI/UX
- ✅ Responsive design that works on mobile and desktop
- ✅ Clean, modern UI with good visual hierarchy
- ✅ Simple form validation


## Solution Design

For a detailed overview of the application's architecture, design decisions, trade-offs, and best practices, please refer to the [SolutionDesign.md](./SolutionDesign.md) file in the project root.  
This document covers:
- Page layout and user flow
- Angular and Node.js version requirements
- Project structure and component breakdown
- State management approach
- Trade-offs and design patterns
- Potential improvements and additional considerations


## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
