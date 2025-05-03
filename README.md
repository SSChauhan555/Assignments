# RaftLabsAssignment

This is a .NET Core solution developed as part of a test assignment for RaftLabs. It demonstrates consumption of an external REST API (`https://reqres.in/api`) using `HttpClient`, with support for pagination, user detail retrieval, and integration/unit testing using xUnit.

---

##  Project Structure

RaftLabsAssignment.sln
├── RaftLabsAssignment.Core
│ ├── Models
│ │ └── User.cs
│ └── Services
│ └── ExternalUserService.cs
├── RaftLabsAssignment.ConsoleApp
│ └── Program.cs
├── RaftLabsAssignment.Tests
│ └── ExternalUserServiceTests.cs

## 🏗️ Build
Open a terminal in the root of the solution:
dotnet build

## 🏗️ Run
cd RaftLabsAssignment.ConsoleApp
dotnet run

## 🏗️ Test
dotnet test

## 🏗️ Detailed Output
dotnet test --logger:"console;verbosity=detailed"

 **Design Decisions**
HttpClient + Dependency Injection: ExternalUserService uses HttpClient injected via constructor for clean separation and testability.

Pagination Handling: GetAllUsersAsync() automatically iterates over paged results to return a flattened user list.

Models: Basic User model maps the response structure. JSON parsing is case-insensitive.

Testing with xUnit: All service logic is covered via integration-style tests using real API responses.

Configuration: API base URL is injected via IConfiguration to support environment-based switching and easier test setup.

 **Notes**
You must be connected to the internet while running the tests, as the service fetches live data from the public ReqRes API.

If you're running the solution in VS Code, ensure the C# extension is installed for IntelliSense and debugging support.


