# 7. Hostel Warden Portal Working Process

## Overview
The **Hostel Warden Portal** is an autonomous wing responsible for defining spatial constraints on physical assets (buildings, rooms, beds), mapping human resources into them, and monitoring daily active occupancy.

## Working Process & Files involved

1. **`warden-dashboard.html`**
   - **Role:** Real-time occupancy health.
   - **Process:** Highlights macro-counts like Active Complaints, Total Hostel Population broken down by Blocks (visualized via dynamic donut chart), and incoming Gate Pass requests.

2. **`warden-hostel-ops.html`**
   - **Role:** Core infrastructural operations.
   - **Process:** 
     - **Setup:** Architecting the actual physical logic of the Institution Hostels (Adding Floors, Configuring Attendance Roll-Call time slots, generating specific Room IDs with bed capacity constraints).
     - **Registrations:** Pulling a Student/Staff record from the central database (Admin origin) and allocating specific Rooms/Beds to them over time. Provides a visual map of who is currently sleeping where.
     - **Attendance Tracking:** Allows the warden to run through entire blocks dynamically marking them as Present/Absent/On-Leave against specific time markers, and generating excel reports.
