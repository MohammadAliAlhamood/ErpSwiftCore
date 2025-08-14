# ErpSwiftCore

**A Modern, Multi-Tenant Cloud ERP System built with .NET 9, Razor Pages, and Clean Architecture**

---

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-blue?logo=dotnet" alt=".NET 9"/>
  <img src="https://img.shields.io/badge/Architecture-Clean%20Architecture-brightgreen" alt="Clean Architecture"/>
  <img src="https://img.shields.io/badge/Platform-Razor%20Pages-blueviolet" alt="Razor Pages"/>
  <img src="https://img.shields.io/badge/Multi--Tenant-Yes-orange" alt="Multi-Tenant"/>
  <img src="https://img.shields.io/badge/License-MIT-green" alt="MIT License"/>
</p>

---

## Overview

ErpSwiftCore is a next-generation, full-featured ERP system designed for modern businesses and organizations. It offers robust multi-tenancy, a clean and scalable architecture, and a beautiful, responsive UI built with Razor Pages. The system is engineered for extensibility, security, and high performance, leveraging the latest .NET 9 technologies and best practices (Clean Architecture, DDD, CQRS, MediatR, EF Core).

---

## Key Features & Strengths

- **Clean Architecture:** Strict separation of concerns (Domain, Application, Infrastructure, Web) for maintainability, testability, and scalability.
- **Multi-Tenancy:** Each company (tenant) is fully isolated, with dynamic context management.
- **Enterprise-Grade Security:** JWT authentication, advanced authorization, data protection, and Identity support.
- **Highly Extensible:** Easily add new modules (CRM, Billing, Inventory, Financials, etc.) thanks to modular design.
- **RESTful API & Razor Pages UI:** Powerful APIs with Swagger documentation and a modern, user-friendly web interface.
- **Advanced Data Management:** Soft delete, restore, hard delete, and full audit trail support.
- **Reliability:** Automated migrations & seeding, advanced error logging, and validation.
- **Modern Tooling:** AutoMapper, MediatR, FluentValidation, Quartz (scheduling), Redis (caching), and more.
- **Localization & Customization:** Architecture supports multi-language and easy UI customization.
- **Premium UX:** Responsive design, professional Bootstrap templates, notifications, DataTables, Toastr, SweetAlert2, TinyMCE, and more.

---

## Project Structure

- **ErpSwiftCore.API:** RESTful API layer (ASP.NET Core Web API) with Swagger and JWT support.
- **ErpSwiftCore.Web:** User interface (Razor Pages) with authentication, operations management, and interactive dashboards.
- **ErpSwiftCore.Application:** Business logic (CQRS, MediatR, Validation).
- **ErpSwiftCore.Domain:** Entities and domain services.
- **ErpSwiftCore.Persistence:** Data access (EF Core, Repositories, Multi-Tenant).
- **ErpSwiftCore.Infrastructure:** Infrastructure services (Validation, Security, Notifications).
- **ErpSwiftCore.SharedKernel:** Shared code (Base Entities, Utilities).
- **ErpSwiftCore.TenantManagement:** Tenant and user context management.
- **ErpSwiftCore.Notifications:** Notification system.

---

## Requirements

- .NET 9 SDK
- SQL Server (or any EF Core-supported database)
- Redis (optional for caching)
- Node.js (for frontend asset management, optional)
- Modern IDE (Visual Studio 2022+, Rider, VS Code, etc.)

---

## Quick Start

### 1. Clone the Repository
git clone https://github.com/your-org/ErpSwiftCore.git
cd ErpSwiftCore
### 2. Configure the Database

- Set your connection string in `ErpSwiftCore.API/appsettings.json` (`DefaultConnection` key).
- Migrations and seeding run automatically on first launch.

### 3. Run the API
cd ErpSwiftCore.API
dotnet run- Default: `https://localhost:7000`
- Swagger: `https://localhost:7000/swagger`

### 4. Run the Web UI (Razor Pages)
cd ../ErpSwiftCore.Web
dotnet run- Default: `https://localhost:7025`

### 5. Login & Explore

- Use the default admin credentials (auto-created on first run).
- Explore the dashboard, manage products, customers, invoices, inventory, financial accounts, and more.

---

## Usage Examples

- **Create a New Order:** Via the web UI or API.
- **Manage Branches, Customers, Suppliers:** Intuitive, responsive interfaces.
- **Financial & Cash Flow Reports:** Real-time, customizable reports.
- **Inventory & Transfers:** Full support for logistics operations.
- **Notifications & Alerts:** Flexible, extensible notification system.

---

## Documentation & Extensibility

- **Swagger UI:** Full API documentation and live testing.
- **AutoMapper Profiles:** Easy mapping between entities and DTOs.
- **FluentValidation:** Advanced data validation.
- **Modular Additions:** Add new modules (Billing, HR, CRM, etc.) with minimal effort.

---

## Why ErpSwiftCore?

- **Enterprise-Ready:** Robust, scalable architecture for large and medium businesses.
- **Security & Reliability:** High security standards, full auditing, multi-tenancy.
- **Easy Customization:** Adapt to any sector or special requirements.
- **Modern UX:** Clean, intuitive, and responsive user experience.
- **Community & Support:** Clean code, full documentation, and easy contribution.

---

## Screenshots

> Add screenshots of the dashboard, order management, reports, etc. (optional)

---

## Contributing

We welcome contributions! Please read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting a pull request.

---

## License

ErpSwiftCore is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

## Contact

- **Project Page:** [https://github.com/your-org/ErpSwiftCore](https://github.com/your-org/ErpSwiftCore)
- **Support:** [MohammadAlhamood20031010@gmail.com](mailto:MohammadAlhamood20031010@gmail.com)

---

**ErpSwiftCore — The Future of Enterprise Management Systems!**

---

Want to customize or integrate ErpSwiftCore with your systems? Contact us for tailored solutions.

---

*If you need this README in Arabic or want to add more sections (screenshots, demo video, etc.), let us know!*