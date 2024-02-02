# EConsulting

A full-stack e-consulting platform built with **ASP.NET Core 6 MVC**. The platform lets clients browse and purchase consulting services across multiple domains (IT, Business, Agro, Sport), manage orders, and communicate in real time with staff — all backed by a comprehensive admin panel.

---

## Features

### Client Side
- Browse consulting products by category (IT, Business, Agro, Sport)
- Product detail pages with descriptions and pricing
- User registration with **email confirmation** (SendGrid)
- Login / logout with cookie-based authentication
- Place and track orders
- Real-time **conference chat** with staff

### Admin Panel (`/admin`)
- Dashboard with key statistics
- Full **CRUD** for products and categories
- User management with role assignment (User / Moderator / Admin / SuperAdmin)
- Email template management and bulk campaigns
- Live currency rate management (Fixer.io API)
- Send **push notifications** to all connected users
- Real-time alert feed showing admin activity

### Real-Time (SignalR)
| Hub | Purpose |
|-----|---------|
| `AlertMessageHub` | Push notifications to all clients |
| `OnlineUserHub` | Track who is currently online |
| `StaffUsersViewHub` | Admin view of active staff sessions |
| `ChatHub` | Conference-style real-time chat |

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core 6 MVC |
| ORM | Entity Framework Core 6 |
| Database | SQL Server (MSSQL) |
| Real-time | SignalR |
| Auth | Cookie Authentication + BCrypt |
| Email | SendGrid API / NETCore.MailKit |
| Currency | Fixer.io REST API |
| Frontend | Bootstrap 4, jQuery, ApexCharts |

---

## Project Structure

```
EConsult/
├── Areas/Admin/          # Admin panel (controllers, views, view models)
├── Controllers/          # Client-side controllers
├── Database/
│   ├── Models/           # Entity models (User, Product, Category, Order, ...)
│   ├── Configurations/   # EF Core Fluent API configs
│   └── EConsultDbContext.cs
├── Hubs/                 # SignalR hubs (Chat, Alert, OnlineUser, StaffView)
├── Migrations/           # EF Core migrations
├── Services/
│   ├── Abstracts/        # Service interfaces
│   └── Concretes/        # Service implementations
├── ViewModels/           # Request / response view models
├── Contracts/            # Shared enums and DTOs
└── wwwroot/              # Static assets (CSS, JS, images)
```

---

## Getting Started

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server (local or remote)
- A [SendGrid](https://sendgrid.com) account (free tier works)
- A [Fixer.io](https://fixer.io) API key (free tier works)

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/VagifPashayev/EConsulting.git
cd EConsulting
```

**2. Configure secrets**

Edit `EConsult/appsettings.Development.json` with your values:
```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER;Database=e-consulting;Integrated Security=True;"
  },
  "EmailSettings": {
    "Email": "your@email.com",
    "Username": "E-Consulting",
    "ApiKey": "YOUR_SENDGRID_API_KEY"
  },
  "CurrencyApiBase": "http://data.fixer.io/api/latest",
  "CurrencyApiKey": "YOUR_FIXER_API_KEY"
}
```

**3. Apply database migrations**
```bash
cd EConsult
dotnet ef database update
```

**4. Run the application**
```bash
dotnet run
```

The app will be available at `https://localhost:5001`.

---

## Database Schema

| Table | Description |
|-------|-------------|
| `Users` | Platform users with role-based access |
| `Products` | Consulting services / offerings |
| `Categories` | Service categories (IT, Business, Agro, Sport) |
| `CategoryProducts` | Many-to-many join for product categories |
| `Orders` | Client orders with status tracking |
| `AlertMessages` | Real-time notification records |
| `EmailMessages` | Email template storage |
| `UserActivations` | Email confirmation tokens |

---

## Configuration Reference

| Key | Description |
|-----|-------------|
| `ConnectionStrings:Default` | SQL Server connection string |
| `EmailSettings:Email` | Sender email address |
| `EmailSettings:ApiKey` | SendGrid API key |
| `CurrencyApiBase` | Fixer.io base URL |
| `CurrencyApiKey` | Fixer.io API key |

---

## License

This project is for portfolio and educational purposes.
