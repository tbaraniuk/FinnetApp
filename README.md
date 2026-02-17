# FinnetApp

The way from **monolith** to **microservices**

## Project Overview

FinnetApp is an ASP.NET Core Web API project demonstrating a modular monolith architecture. The application is built with .NET 9.0 and uses PostgreSQL as the database.

## Technologies & Libraries

| Category | Library |
|----------|---------|
| **Framework** | ASP.NET Core 9.0 |
| **Database** | PostgreSQL with Entity Framework Core 9.0 |
| **ORM** | Dapper 2.1.66 |
| **Validation** | FluentValidation 12.0.0 |
| **Messaging** | MediatR 12.5.0 |
| **API Documentation** | Swashbuckle.AspNetCore 9.0.6, Microsoft.AspNetCore.OpenApi 9.0.0 |

## Prerequisites

- .NET 9.0 SDK
- PostgreSQL 15+
- Docker (optional, for running PostgreSQL)

## How to Run

### 1. Start PostgreSQL

Using Docker:
```sh
docker run -d --name postgres -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_DB=fitnet -p 5432:5432 postgres
```

Or use a local PostgreSQL instance with the following credentials:
- Host: localhost:5432
- Database: fitnet
- Username: postgres
- Password: mysecretpassword

### 2. Run the Application

```sh
cd Chapter-1-initial-architecture/Src/Fitnet
dotnet restore
dotnet run
```

### 3. Access the Application

- API: http://localhost:5013
- Swagger UI: http://localhost:5013/swagger

## Configuration

Database connection strings are configured in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Passes": "Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword",
    "Contracts": "Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword",
    "Reports": "Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword",
    "Offers": "Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword"
  }
}
```

## Modules

- **Contracts** - Contract management functionality
- **Passes** - Pass/Gym membership management
- **Reports** - Reporting features
- **Offers** - Special offers module

## Testing

```sh
# Run unit tests
dotnet test --project Fitnet.UnitTests

# Run integration tests
dotnet test --project Fitnet.IntegrationTests

# Run architecture tests
dotnet test --project Fitnet.ArchitectureTests
```
