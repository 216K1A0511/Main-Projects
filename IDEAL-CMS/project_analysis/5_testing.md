# 5. Testing Methodologies

## 5.1 Architecture & Strategy Overview
To maintain absolute stability in a monolithic, deeply relational college ERP ecosystem, the system relies on separated testing nodes mapped strictly across the three core layers: The ASP.NET Core Engine, the PostgreSQL persistence vectors, and the UI-level interactions.

## 5.2 Unit Testing the MediatR Pipeline (Backend)
- **Focus:** Validating internal Application layer logic entirely separated from the database.
- **Tooling:** Uses `xUnit` and `Moq`.
- **Methodology:** Every Handle function tied to MediatR requires an isolation test. 
- **Example Scenario:** Creating a test payload asserting that if a Student generates a "Gate Pass Request" object spanning past their `TermEndDate`, the business logic handler `GatePassCommandHandler` must successfully calculate the timestamp mismatch and throw a `ValidationException` without ever interacting with the DB.

## 5.3 Database Interaction Tests (Infrastructure)
- **Focus:** Validating that complex Entity Framework LINQ queries translate efficiently into correct SQL.
- **Tooling:** In-Memory Database objects tracking generated DBContext structures.
- **Methodology:** Generating artificial parent/child objects in a local memory array and ensuring querying the hierarchy matches expected behavior.
- **Example Scenario:** Creating a mock `Programs` array containing 5 `Courses`, each containing 30 `Students`, and executing an asynchronous Average GPA computation function over the framework.

## 5.4 Cross-Layer Integration Testing (API/Frontend)
- **Focus:** Ensuring the Vanilla JavaScript modules can correctly map to the backend endpoints under real network environments.
- **Tooling:** Postman Collections for API hitting, and potentially basic automated tools mapping the DOM elements for JS.
- **Methodology:** Systematically validating the `index.html` login gateway matrix, verifying that injecting incorrect Role Enums blocks HTML routing asynchronously from the API endpoints, successfully tripping the "Red Alert" wrappers.
