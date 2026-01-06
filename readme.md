```markdown
# M_Marketplace

---

## Features 

**Comprehensive E-commerce Platform** – Built for both B2B and B2C markets
**Role-Based Access Control** – Secure vendor, customer, and admin experiences
**Stripe Integration** – Seamless payments with webhooks and Stripe Connect
**Vendor Onboarding** – Complete vendor profile management with Stripe account linking
**Cart & Checkout System** – Full shopping cart functionality with order creation
**Sales Analytics** – Detailed vendor sales reporting and analytics
**Email Activation** – Secure user registration with email verification
**RESTful API** – Clean, well-documented API endpoints for all operations

---

## Tech Stack

### Core Technologies
- **Language**: C# (.NET 8.0)
- **Framework**: ASP.NET Core Web API
- **Database**: Entity Framework Core (SQL Server)
- **Authentication**: JWT with role-based authorization
- **Payment Processing**: Stripe Connect for vendor payouts

### Key Components
- **Domain Layer**: Clean architecture with domain entities and value objects
- **Application Layer**: Use cases implementing business logic
- **Infrastructure Layer**: Repositories, services, and external integrations
- **Presentation Layer**: RESTful API controllers

### Additional Tools
- **Dependency Injection**: Built-in .NET DI container
- **Configuration**: Environment variables and appsettings.json

---

## Installation

### Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [PostgreSQL](https://www.postgresql.org/download/)
- [Stripe Account](https://dashboard.stripe.com/register) (for payment processing)
- [Git](https://git-scm.com/downloads)

### Quick Start

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/M_Marketplace.git
   cd M_Marketplace
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Set up environment variables**:
   Create a `.env` file in the `src/M_API` directory with the following variables:
   ```
   ASPNETCORE_ENVIRONMENT=Development
   ConnectionStrings__Default=Your_Connection_String_Here
   Jwt:SecretKey=Your_Jwt_Secret_Key_Here
   Stripe:SecretKey=Your_Stripe_Secret_Key
   Stripe:WebhookSecret=Your_Stripe_Webhook_Secret
   EmailSettings__Host=Your_SMTP_Host
   EmailSettings__Port=587
   EmailSettings__Username=Your_SMTP_Username
   EmailSettings__Password=Your_SMTP_Password
   EmailSettings__From=Your_Email_Address
   ```

4. **Apply database migrations**:
   ```bash
   dotnet ef database update
   ```

5. **Run the application**:
   ```bash
   dotnet run --project src/M_API/M_API.csproj
   ```
---

## Usage

### Basic API Endpoints

#### Authentication
- **Login**: `POST /api/auth/login`
  ```csharp
  // Example request
  var loginDto = new LoginDto { Email = "user@example.com", Password = "password123" };
  var response = await client.PostAsJsonAsync("/api/auth/login", loginDto);
  ```

#### Vendor Operations
- **Create Vendor Profile**: `POST /api/vendor-profiles`
  ```csharp
  var vendorDto = new CreateVendorProfileDto {
      UserId = Guid.NewGuid(),
      FirstName = "John",
      LastName = "Doe",
      CompanyName = "Tech Solutions Inc.",
      Cnpj = "12345678901234",
      // ... other required fields
  };
  var response = await client.PostAsJsonAsync("/api/vendor-profiles", vendorDto);
  ```

#### Customer Operations
- **Create Customer Profile**: `POST /api/customer-profiles`
  ```csharp
  var customerDto = new CreateCustomerProfileDto {
      UserId = Guid.NewGuid(),
      FirstName = "Jane",
      LastName = "Smith",
      Phone = "+1234567890",
      Street = "123 Main St",
      City = "New York",
      // ... other required fields
  };
  var response = await client.PostAsJsonAsync("/api/customer-profiles", customerDto);
  ```

#### Cart Operations
- **Add Item to Cart**: `POST /api/cart/items`
  ```csharp
  var cartItemDto = new AddCartItemDto { ProductId = Guid.NewGuid(), Quantity = 2 };
  var response = await client.PostAsJsonAsync("/api/cart/items", cartItemDto);
  ```

- **Checkout**: `POST /api/checkout`
  ```csharp
  var orderId = Guid.NewGuid();
  var response = await client.PostAsJsonAsync("/api/checkout", orderId);
  var checkoutUrl = response.Result.Value<string>("checkoutUrl");
  ```

### Stripe Webhook Handling

The application includes a webhook endpoint for Stripe events:
- **Webhook Endpoint**: `POST /api/webhooks/stripe`
  This endpoint processes payment events and updates order statuses accordingly.

---

## Project Structure

```
M_Marketplace/
├── src/
│   ├── M_API/                  # Main API project
│   │   ├── API/                # Controllers and routes
│   │   ├── Application/        # Business logic and DTOs
│   │   │   ├── DTOs/           # Data Transfer Objects
│   │   │   ├── Services/       # External services (Email, Stripe)
│   │   │   ├── Security/       # Authentication and authorization
│   │   │   └── UseCases/       # Business use cases
│   │   ├── Domain/             # Core domain models and repositories
│   │   ├── Infrastructure/     # Data access and external integrations
│   │   ├── Program.cs          # Entry point
│   │   └── appsettings.json     # Configuration files
│   └── tests/                  # Unit and integration tests
├── .gitignore
├── README.md                   # This file
└── LICENSE                     # License information
```

---

## 🔧 Configuration

### Environment Variables

| Variable                     | Description                                                                 | Example Value                          |
|------------------------------|-----------------------------------------------------------------------------|----------------------------------------|
| `ASPNETCORE_ENVIRONMENT`     | Environment (Development/Production)                                         | `Development`                          |
| `ConnectionStrings__Default` | Database connection string                                                  | `Server=localhost;Database=M_Marketplace;Trusted_Connection=True;` |
| `Jwt:SecretKey`              | Secret key for JWT token generation                                          | `YourStrongSecretKeyHere`              |
| `Stripe:SecretKey`           | Stripe API secret key                                                      | `sk_test_abc123`                       |
| `Stripe:WebhookSecret`       | Stripe webhook signing secret                                               | `whsec_abc123`                        |
| `EmailSettings__Host`        | SMTP host for email notifications                                           | `smtp.example.com`                     |
| `EmailSettings__Port`        | SMTP port                                                                   | `587`                                  |
| `EmailSettings__Username`     | SMTP username                                                              | `your-email@example.com`               |
| `EmailSettings__Password`     | SMTP password                                                              | `your-email-password`                  |
| `EmailSettings__From`        | Email address for outgoing emails                                           | `noreply@m-marketplace.com`            |

### Database Configuration

The application uses Entity Framework Core for database operations. Ensure your `appsettings.json` or `appsettings.Development.json` includes the appropriate connection string.

Example for SQL Server:
```json
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\MSSQLLocalDB;Database=M_Marketplace;Trusted_Connection=True;"
  }
}
```


