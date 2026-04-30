# ECommerce API — SOLID Principles & Design Patterns in .NET

A self-directed .NET Core project built to practise and demonstrate clean software engineering principles in a realistic API context. This is not a production application — it is a deliberate learning exercise in writing maintainable, well-structured backend code from scratch.

---

## Purpose

When working on enterprise projects, it is easy to follow existing patterns without deeply understanding the *why* behind them. This project was built to close that gap — every structural decision here was made intentionally, with the goal of understanding how SOLID principles and design patterns affect code quality, testability, and extensibility in practice.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 10) |
| Language | C# |
| Database | SQLite via Entity Framework Core |
| Authentication | JWT Bearer |
| Logging | Serilog (console sink) |
| API Docs | Swagger / Swashbuckle |
| Caching | In-Memory Cache |

---

## Architecture

The project is structured using a layered architecture with clear separation of concerns:

```
ECommerceAPI/
├── Controllers/       # HTTP layer — receives requests, returns responses
├── Services/          # Business logic — IProductService / ProductService
├── Data/              # EF Core DbContext and data access
├── Models/            # Domain entities
├── Middleware/        # Cross-cutting concerns (exception handling)
└── Migrations/        # EF Core database migrations
```

---

## SOLID Principles Applied

### Single Responsibility Principle
Each class has one reason to change. Controllers handle HTTP concerns only — they do not contain business logic. Business logic lives exclusively in the Services layer. The `ExceptionMiddleware` handles error formatting and nothing else.

### Open/Closed Principle
The service layer is structured so new product behaviours can be added by extending, not modifying, existing classes. New implementations of `IProductService` can be introduced without touching existing code.

### Liskov Substitution Principle
`ProductService` fully implements `IProductService` and can be substituted anywhere the interface is expected — including in tests — without changing behaviour.

### Interface Segregation Principle
Service interfaces are kept focused and specific to their domain. Controllers depend only on the interfaces they need, not on concrete implementations or unrelated methods.

### Dependency Inversion Principle
All dependencies are injected via the built-in .NET DI container (`builder.Services.AddScoped<IProductService, ProductService>()`). Controllers and services depend on abstractions, not concrete classes.

---

## Design Patterns Used

- **Repository Pattern** — data access is abstracted away from business logic, keeping the service layer persistence-agnostic
- **Strategy Pattern** — interchangeable business logic implementations via interfaces
- **Dependency Injection** — all dependencies wired through the .NET service container, enabling loose coupling and testability
- **Middleware Pipeline** — custom `ExceptionMiddleware` intercepts unhandled exceptions globally, returning consistent error responses without try/catch blocks scattered across controllers

---

## Key Features

- **JWT Authentication** — endpoints protected with Bearer token authentication; token validation configured in `Program.cs`
- **Global Exception Handling** — custom middleware catches all unhandled exceptions and returns structured error responses
- **Structured Logging** — Serilog configured at startup, logs piped through the host for consistent output across the application lifecycle
- **In-Memory Caching** — `IMemoryCache` registered and available for performance-sensitive read operations
- **Swagger UI** — full API documentation auto-generated and browsable at `/swagger`
- **EF Core Migrations** — database schema managed via code-first migrations

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run locally

```bash
git clone https://github.com/madangoud/Ecommerce_SOLID.git
cd Ecommerce_SOLID

# Apply database migrations
dotnet ef database update

# Run the application
dotnet run
```

Once running, open your browser at:
- **Swagger UI:** `https://localhost:{port}/swagger`

> **Note:** The JWT secret key in `Program.cs` is hardcoded for local development purposes only. In a production application this would be stored in environment variables or Azure Key Vault.

---

## What I Would Do Differently in Production

- Move the JWT secret to environment variables / Azure Key Vault (flagged in a code comment)
- Add unit tests using xUnit and Moq against the service interfaces
- Replace SQLite with SQL Server and add proper connection string management
- Introduce a proper DTO layer to avoid exposing domain models directly from the API

---

## Author

**Madan Burra** — Full Stack .NET Developer  
[linkedin.com/in/madan-goud-090a551a9](https://linkedin.com/in/madan-goud-090a551a9)
