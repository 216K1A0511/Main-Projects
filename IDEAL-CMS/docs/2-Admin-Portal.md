# 2. Administrative Portal Working Process

## Overview
The **Admin Portal** is isolated for top-level configuration, management, and oversight of the entire institution. The Admin sets up structural logic that dictates how the other portals behave.

## Working Process & Files involved

1. **`admin-dashboard.html`**
   - **Role:** High-level oversight.
   - **Process:** Acts as the landing page. Features a live context-switcher (Institution Dropdown) that dynamically recalculates all UI charts and KPIs across different institutional branches without a page reload.

2. **`admin-setup-college.html`**
   - **Role:** Core institutional parameters.
   - **Process:** Admin configures the fundamental components (Academic terms, Program curriculums, Assessment timelines, and Extracurricular structures).

3. **`admin-hr.html` & `admin-hr-grievances.html`**
   - **Role:** Human Resources Control.
   - **Process:** Used to add new Faculty/Employees, configure payroll/leaves, accept/reject resignations, print ID Cards dynamically, and moderate internal grievances reported by staff or students.

4. **`admin-admissions.html`**
   - **Role:** Enrollment lifecycle.
   - **Process:** Manages incoming student requests, enrollment pipelines, assigning roll numbers, and configuring tuition fee structures.

5. **`admin-communication.html` & `admin-online-users.html`**
   - **Role:** Connectivity.
   - **Process:** Send system-wide broadcast updates and manage active user sessions.

6. **`admin-management.html`**
   - **Role:** Utilities.
   - **Process:** Managing modules related to general policies, tutors, and system permissions.
