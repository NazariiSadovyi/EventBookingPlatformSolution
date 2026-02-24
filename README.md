# EventBookingPlatformSolution

Event booking platform built with **.NET 8** and **ASP.NET Core Web API**, structured using **Clean Architecture** principles.

---

## 🏗 Architecture Overview

# 🧠 Design Principles Used

- Clean Architecture
- Dependency Inversion
- Domain-Driven Design
- Explicit EF Configurations
- Layered validation
- The solution follows Clean Architecture with clear separation of concerns:
  - API → Application → Domain
  - API → Infrastructure

### Projects

- **EBP.API**
  - ASP.NET Core Web API
  - Controllers
  - Swagger configuration
  - Authentication / Authorization
  - Dependency Injection configuration
  - Composition root

- **EBP.Application**
  - Use Cases (Commands / Queries)
  - Business workflows
  - DTOs
  - Interfaces (abstractions for infrastructure)
  - Validation logic

- **EBP.Domain**
  - Entities
  - Value Objects
  - Enums
  - Domain rules
  - Domain exceptions

- **EBP.Infrastructure**
  - EF Core DbContext
  - Entity configurations
  - Migrations
  - Repository implementations
  - External services

- **EBP.Application.UnitTests**
  - Unit tests for Application layer

---

## ⚙️ Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- JWT Authentication
- Docker & Docker Compose

---

## 🚀 How to Run the Project

You can run the project either with Docker (recommended) or locally.

---

# 🐳 Option 1 — Run with Docker (Recommended)

### Requirements

- Docker Desktop installed

### Step 1 — From repository root folder

```bash
docker compose up --build
```

This will start:
- SQL Server 2022 container
- API container

### Step 2 — Access API

```bash
https://localhost:8443/swagger
```
