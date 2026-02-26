# 4. Database Schema & Relations

## 4.1 Global Environment Constraints
- **Engine:** PostgreSQL containerized over Linux, communicating with the backend framework via heavily optimized Entity Framework (EF) Core connections.
- **Primary Design Paradigm:** Uses highly normalized tables focused heavily on One-to-Many and Many-to-Many foreign key connections between isolated conceptual boundaries.

## 4.2 Core Entity Relational Examples

### 4.2.1 The Academic Vector (Faculty -> Student Mapping)
The structure prevents hard-coding a Faculty directly to a Student, instead mapping via flexible intermediary tables.
- **`Programs` Table:** E.g., B.Tech Computer Science (Parent Node)
- **`Courses` Table:** E.g., Advanced Data Structures (Child Node of Program)
- **`ClassSections` Table:** E.g., Section A. (Has `FacultyID` Foreign Key).
- **`StudentEnrollments` J-Table:** Maps specific `StudentID` keys against specific `ClassSectionID` keys.

### 4.2.2 The Attendance/Assessment Vector (Heavy Traffic Nodes)
- **`AttendanceRecords` Table:** Links deeply against `ClassSectionID`, `StudentID` and `Date`. Includes boolean columns (`IsPresent`, `IsLeaveMapped`).
- **`StudentGrades` Table:** Maps `StudentID` against `CourseID` and a specific assessment identifier (e.g., "Midterm 1").

### 4.2.3 The Infrastructure/Occupancy Vector (Warden/Hostel Mapping)
- **`HostelBlocks` Table:** The parent node (e.g., "Mens Block A").
- **`HostelRooms` Table:** Maps to the Block via Foreign Key. Contains constraints like `MaxBeds`.
- **`RoomAllocations` J-Table:** A constantly shifting, history-tracked relation mapping a `StudentID` to a `RoomID` along with an active boolean tracker (`IsCurrentlyOccupied`).

## 4.3 Database Best Practices
- Every entity explicitly derives from a `BaseEntity` object containing automated, global columns: `Id` (GUID string), `CreatedAt` (Timestamp), `CreatedBy` (String), `LastModifiedAt` (Timestamp).
- Extreme reliance on cascading soft-deletes via the `IsDeleted` boolean column. Hard-deleting records in highly relational SQL grids breaks critical historical data streams (e.g., "Deleting" a faculty member would break grading records for students who graduated two years ago).
