# IDEAL ERP - Comprehensive College Management System

This project implements a high-performance, scalable, and fully integrated Enterprise Resource Planning (ERP) system tailored for higher education institutions. **IDEAL-CMS** provides dedicated, isolated portals for every stakeholder in the college ecosystem, ensuring secure, role-based access and streamlined workflows. The backend is built on a robust foundation using **ASP.NET Core 8.0** and **PostgreSQL**.

## 🌟 Comprehensive Portals & Workflows

The system features a dynamic central Login Gateway (`index.html`) that intelligently routes users to one of 8 dedicated institutional portals:

### 1. 🛡️ Admin Portal (`admin-*.html`)
Top-level configuration, management, and oversight of the entire institution.
- **Setup College:** Configure academic terms, program curriculums, assessment timelines, and structures.
- **Human Resources:** Manage faculty/employees, payroll, leaves, resignations, ID cards, and grievances.
- **Admissions:** Handle enrollment lifecycles, assign roll numbers, and manage fee structures.
- **Operations:** Live context-switcher across institutional branches, system-wide broadcast updates, and general policies.

### 2. 🏛️ Principal / Director Portal (`principal-*.html`)
Ultimate viewer for academic execution and operational health.
- **Command Center:** 10,000-foot view of top-performing cohorts, struggling departments, and HR metrics.
- **Academic Analytics:** Deep matrices for time table structuring, faculty workload balancing, and result auditing.

### 3. 📚 HOD (Head of Department) Portal (`hod-*.html`)
Supervisory logic over specific academic departments.
- **Workload Allocation:** Assign subjects to specific faculties.
- **Academic Approvals:** Approve/Reject lesson plans, review and publish final grades.
- **Monitoring:** Track syllabus completion across all batches and view department performance snapshots.

### 4. 👨‍🏫 Faculty / Employee Portal (`employee-*.html`)
Primary driver of academic inputs.
- **Course Management:** Create lesson plans, upload resources/assignments, and configure deadlines.
- **Classroom Operations:** Quick-Mark attendance, log detailed student grades, and view daily lecture schedules.
- **HR Sync:** Dedicated resignation workflow syncing directly to the Admin portal.

### 5. 🎓 Student Portal (`student-*.html`)
Interactive view for the student's academic and extracurricular trajectory.
- **Academics:** Download lesson plans, submit assignments, view time tables, and track GPA/Attendance.
- **Extracurriculars:** Browse clubs, register for hackathons/events, and view galleries.
- **Placements:** Opt-in for campus recruitment drives, upload resumes, and track selection statuses.
- **Requests:** Apply for leaves, hostel gate passes, physical documents, and raise grievances.

### 6. 👪 Parent / Guardian Portal (`parent-dashboard.html`)
Isolated, read-only analytics view of the specific ward's trajectory.
- **Tracking:** View precise attendance heatmaps and mid/final term grades.
- **Fees & Engagement:** Track outstanding fee structures, monitor registered events, and communicate with instructors.

### 7. 🏢 Hostel Warden Portal (`warden-*.html`)
Autonomous management of physical assets and residency operations.
- **Infrastructural Setup:** Architect blocks, rooms, bed capacity, and roll-call time slots.
- **Occupancy Management:** Allocate rooms to students/staff with a visual map of active occupancy.
- **Attendance & Operations:** Run daily block attendance, manage gate pass requests, and handle active complaints.

### 8. 💼 Placement Coordinator Portal (`placement-coordinator-dashboard.html`)
Bridge for students transitioning into professional environments.
- **Drive Creation:** Design corporate recruitment events with specific thresholds (e.g., cutoff GPA).
- **Application Processing:** Review student resumes and manage interview rounds (Aptitude, Tech, HR).
- **Skill Training:** Setup seminars and track student completion of soft-skills/coding bootcamps.

---

## 🚀 Tech Stack

- **Frontend UI**: Vanilla JS, HTML5, CSS3 
- **Backend Framework**: ASP.NET Core 8.0
- **Database**: PostgreSQL (with Entity Framework Core)
- **Architecture**: Clean Architecture / Modular Monolith
- **Communication**: MediatR (CQRS Pattern)
- **Validation**: FluentValidation
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI

## 📂 Project Structure

### Frontend Structure
- `/` - Root web pages containing all HTML views (`.html`) for every role, plus the master `index.html` login gateway.
- `docs/` - Contains comprehensive markdown documentation detailing the workflows and processes for each individual portal.

### Backend Structure
- `src/IDEAL.ERP.Domain`: Core business logic, entities, and value objects.
- `src/IDEAL.ERP.Application`: Interfaces, DTOs, Handlers (MediatR), and Business Logic.
- `src/IDEAL.ERP.Infrastructure`: Persistence (EF Core), External Services, and System dependencies.
- `src/IDEAL.ERP.Api`: Web API controllers, Middleware, and Entry point.

---

## 🛠️ Getting Started

### 1. Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### 2. Setup Database
Run the following command to start PostgreSQL and pgAdmin via Docker Compose:
```bash
docker-compose up -d
```

### 3. Running the Backend API
Navigate to the API project and execute:
```bash
cd src/IDEAL.ERP.Api
dotnet run
```
Access the Swagger UI at: `https://localhost:7001/swagger`

### 4. Running the Frontend
Simply open `index.html` in any modern web browser to access the Login Gateway.

---

## 🛡️ Best Practices & Architecture
- **Strict Clean Architecture**: Dependencies point inwards.
- **Fail-Fast Validation**: Using FluentValidation for robust request modeling.
- **Global Exception Handling**: Centralized error management to ensure stability.
- **Auditing**: Automatic database tracking with `CreatedAt` and `LastModifiedAt`.

---

## 🛠️ Troubleshooting & Common Issues

During setup or development, you might encounter some common issues across the Frontend, Backend, or Database layers. Here are some quick solutions:

### Frontend
**Q: The page styling looks broken or scripts aren't running.**
* **Solution:** Since this project uses Vanilla JS/CSS without a bundler, ensure you are loading the static files over a local web server (like VS Code Live Server) rather than directly opening the file `file://...`. Some browsers block local CORS requests for imported scripts/fonts.

**Q: Clicking "Login" doesn't do anything.**
* **Solution:** The frontend logic relies on standard JavaScript event listeners. Check the browser's developer console (F12) for any JavaScript errors. Also, ensure you have selected a specific role from the dropdown before clicking "Login".

### Backend (ASP.NET Core 8.0)
**Q: `dotnet run` throws a "port already in use" error.**
* **Solution:** The API defaults to ports 7001 (HTTPS) and 5001 (HTTP). If another process is using these ports, you can modify them in the `launchSettings.json` file inside `src/IDEAL.ERP.Api/Properties/`.

**Q: I get a "Connection Refused" error when the API tries to talk to the database.**
* **Solution:** Ensure the PostgreSQL Docker container is actively running using `docker ps`. Verify that the connection string inside `src/IDEAL.ERP.Api/appsettings.Development.json` matches your Docker configuration (Host, Port, Username, Password).

### Database (PostgreSQL & EF Core)
**Q: The tables aren't created in PostgreSQL.**
* **Solution:** Entity Framework Core migrations handle schema creation. Ensure you've applied the migrations after starting the database. Run `dotnet ef database update` from the API directory.

**Q: Database connection fails due to authentication.**
* **Solution:** Check the `docker-compose.yml` file to see the expected `POSTGRES_USER` and `POSTGRES_PASSWORD` values, and ensure they perfectly match the connection string in your backend appsettings.
