# 1. Requirements Analysis

## 1.1 Project Objectives
The IDEAL Corporate College Management System (IDEAL-CMS) is an end-to-end Enterprise Resource Planning (ERP) application. The platform provides a dynamic and highly-isolated ecosystem designed explicitly for academic administration, faculty operations, student analytics, extracurricular management, and infrastructure observability.

## 1.2 Identified Stakeholders & User Roles
The logic requires 8 heavily isolated, yet cross-communicating user domains:
1. **Main Administrator** - Configuration mapping and highest-level approvals (HR/Admissions).
2. **Principal / Director** - Macro-analytics viewer and final auditor.
3. **Head of Department (HOD)** - Curriculum supervision and workload allocator for specific branches.
4. **Faculty / Employee** - Micro-level driver of academic variables (attendance, lesson plans, grading).
5. **Student** - Primary end-user interacting with mapped academic workflows.
6. **Parent / Guardian** - Isolated, read-only analytics consumer based on their Ward's metadata.
7. **Hostel Warden** - Administrator of physical infrastructural assets and geospatial occupancy logic.
8. **Placement Coordinator** - External-recruitment conduit and training orchestrator.

## 1.3 Key Features & Functional Requirements
- **Dynamic Role-Based Login:** An `index.html` gateway determining the correct isolated view via drop-down and matching credentials.
- **Academic Setup Lifecycle:** From Admin-created Academic terms -> HOD mapping -> Faculty lesson plans -> Student interaction.
- **HR Syncing Lifecycle:** Leave approvals, grievances workflow, dynamically tracked employee resignations terminating server permissions.
- **Physical Tracking Ecosystem:** Connecting Student UI "Gate Pass Requests" to the Warden UI physical logic and displaying real-time occupancy.
- **Internal Messaging Matrix:** Direct UI messaging allowing Parents -> Faculty messaging, but restricting Student -> Principal messaging.

## 1.4 Non-Functional Requirements
- **Scalability:** Requires robust dependency architectures capable of parsing thousands of simultaneous, concurrent API hits at the 8 AM timestamp for faculty attendance roll-calls.
- **High Availability:** Ensure database redundancy so that the login gateway and student portals remain functional under extreme load.
- **Data Isolation:** Clean code logic to prevent a Faculty UI hit inadvertently changing an Admin table.
- **Responsive Web Design:** Needs intuitive, multi-platform HTML layouts that render perfectly in browsers using standard Javascript arrays.
