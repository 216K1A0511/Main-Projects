# 1. System Login & Routing Process

**Entry File:** `index.html`

## Overview
The Login Gateway is the central authentication point for the entire **IDEAL-CMS** ecosystem. It dynamically routes users to their respective isolated dashboards based on the selected Role Type.

## Working Process Flow
1. **Access Gateway**: The user arrives at `index.html`.
2. **Role Selection**: The user selects their specific institutional role from the dropdown (`Main Administrator`, `Principal / Director`, `Head of Department`, `Faculty / Employee`, `Student`, `Parent / Guardian`, `Hostel Warden`, `Placement Coordinator`).
3. **Authentication**: The user provides their ID/Username and secure password.
4. **Processing**: Upon clicking "Secure Login", the system verifies the credentials. A loader triggers, preventing multiple clicks.
5. **Gateway Routing**: 
   - Admin routes to `admin-dashboard.html`
   - Principal routes to `principal-dashboard.html`
   - HOD routes to `hod-dashboard.html`
   - Faculty routes to `employee-dashboard.html`
   - Student routes to `student-dashboard.html`
   - Parent routes to `parent-dashboard.html`
   - Warden routes to `warden-dashboard.html`
   - Placement Coordinator routes to `placement-coordinator-dashboard.html`
