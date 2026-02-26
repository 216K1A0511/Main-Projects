# 6. Opening Dashboards & User Navigation

## 6.1 The Standard Login Process (Recommended)
The **IDEAL-CMS** is designed around a unified portal gateway. To access any dashboard, users should start at the central gateway:

1. **Open the Login Gateway**: Navigate to the root directory and open `index.html` in your web browser. 
2. **Select Role**: Use the dropdown menu to select the specific institutional role (e.g., "Student", "Faculty / Employee", "Main Administrator").
3. **Authenticate**: Enter the corresponding Username and Password credentials.
4. **Dynamic Routing**: Upon clicking "Secure Login", the system verifies the credentials. A successful verification automatically routes the session to the designated isolated dashboard file (e.g., `student-dashboard.html`) without manual URL entry.

## 6.2 Bypassing Authentication (Development/Preview Mode)
During development or UI testing, you can bypass the login gateway and open specific dashboards directly in your browser. Since the project uses Vanilla HTML/JS, you simply need to execute the HTML file.

### Available Dashboard URLs:
- **Admin**: `admin-dashboard.html`
- **Principal/Director**: `principal-dashboard.html`
- **Head of Department (HOD)**: `hod-dashboard.html`
- **Faculty/Employee**: `employee-dashboard.html`
- **Student**: `student-dashboard.html`
- **Parent/Guardian**: `parent-dashboard.html`
- **Hostel Warden**: `warden-dashboard.html`
- **Placement Coordinator**: `placement-coordinator-dashboard.html`

### How to Open Directly
1. **Using File Explorer (Windows/Mac)**: Navigate to the project root folder and simply double-click the desired `.html` file. This will open it in your default web browser (Chrome, Edge, Safari).
2. **Using VS Code Live Server**: If you are editing the code in Visual Studio Code, right-click the desired `.html` file in the explorer pane and select "Open with Live Server". This is the recommended method for developers as it prevents local CORS policy blocks when fetching JSON or importing modules.
3. **Using Command Line (Windows PowerShell)**: 
   ```powershell
   # Opens the Admin Dashboard in your default browser
   Start-Process "admin-dashboard.html"
   ```

## 6.3 Post-Login Context Switching (Admin/Principal Only)
Certain high-level roles have the ability to explicitly change their operational "context" while inside their dashboard without needing to logout and log back in.
- **The Context Dropdown**: Located in the top navigation bar of `admin-dashboard.html` and `principal-dashboard.html`.
- **Function**: Selecting a different branch (e.g., switching from "Computer Science Wing" to "Mechanical Wing") dynamically triggers an immediate data-refresh script, updating all `Chart.js` graphs and KPI matrices on the dashboard to reflect the new branch's data, without performing a full page reload.
