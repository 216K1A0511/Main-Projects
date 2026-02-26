# 3. Backend Logic & Architecture

## 3.1 Architectural Foundation: Clean Architecture
The IDEAL-CMS strictly utilizes modern Clean Architecture principles based in ASP.NET Core 8.0 to ensure a highly decoupled, heavily scalable backend server capable of operating independently of the frontend UI.

## 3.2 The Four Layers
### 3.2.1 `IDEAL.ERP.Domain` (Core Entity Matrix)
- **Role:** Deepest layer, entirely independent of the database or any external dependencies (No EF Core, no third-party libraries).
- **Contents:** Houses the core system Entities (e.g., `Student`, `Faculty`, `RoleEnum`), custom Exceptions, and foundational Value Objects.

### 3.2.2 `IDEAL.ERP.Application` (Business Rules)
- **Role:** Implements the CQRS (Command Query Responsibility Segregation) pattern utilizing MediatR.
- **Contents:** Handles all business logic logic. Translates data coming in into specific DTOs (Data Transfer Objects). Uses FluentValidation to verify data states before the logic operates (e.g., ensuring an 'Absent' status doesn't contradict a 'Leave' tracking status).

### 3.2.3 `IDEAL.ERP.Infrastructure` (Data Persistence)
- **Role:** Connects the business logic to the external world.
- **Contents:** Houses the Entity Framework (EF) Core configurations mapping the domain models to exact PostgreSQL SQL syntax. Connects any external dependency APIs (email service abstractions, third-party loggers).

### 3.2.4 `IDEAL.ERP.Api` (Web Presentation)
- **Role:** The outermost layer facing the frontend HTML files.
- **Contents:** Contains the minimal API controllers and middleware routing logic. Injects dependencies into the other layers on spin-up.

## 3.3 Core Processing Logic: Security & Requests
- Follows the RESTful standard for cross-communication via JSON payloads between the HTML/JS frontend and the ASP.NET Core controllers.
- Requires heavily configured global Action Filters operating over the MediatR pipelines to intercept unauthorized actions seamlessly (e.g., a query arriving from a 'Student' endpoint trying to mutate 'Admin HR' data).
- Relies critically on Serilog infrastructure writing complex structured text logs asynchronously to prevent pipeline blocking during massive concurrent inputs (e.g. 500 faculty submitting attendance at exactly 8:05 AM).
