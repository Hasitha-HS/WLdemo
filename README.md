# Waterlily Product Management App

ASP.NET Core MVC application for the Waterlily Labs C# web development assessment.

## Requirements

- .NET 10 SDK
- Microsoft SQL Server (the default connection uses `localhost`)
- Visual Studio or PowerShell
- `dotnet-ef` tool

## Project Structure

- `WLdemo/` - main ASP.NET Core MVC application
- `WLdemo.Tests/` - MSTest project with Moq tests
- `WLdemo/Models/` - product and report models
- `WLdemo/Repositories/` - database access layer
- `WLdemo/Services/` - business logic and caching layer
- `WLdemo/Controllers/` - MVC controllers
- `WLdemo/Views/` - Product and Report pages
- `WLdemo/Migrations/` - EF Core code-first migrations and stored procedure setup
- `Scripts/SeedDummyData.sql` - optional manual demo data script

## Setup

Open PowerShell or another terminal in the solution root, where `WLdemo.slnx` is located.

Install the EF Core command-line tool if it is not already installed:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.14
```

Restore the solution:

```powershell
dotnet restore .\WLdemo.slnx
```

## Database Setup

The application uses Microsoft SQL Server. The default connection in `WLdemo/appsettings.json` is:

```text
Server=localhost;Database=WaterlilyProductDb;Trusted_Connection=True;TrustServerCertificate=True
```

If SQL Server uses a named instance, update the `Server` value. For example:

```text
Server=localhost\SQLEXPRESS;Database=WaterlilyProductDb;Trusted_Connection=True;TrustServerCertificate=True
```

The connection string can also be supplied through the `ConnectionStrings__DefaultConnection` environment variable. Copy `.env.example` as a reference and set the variable in the terminal session before running EF commands:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=localhost;Database=WaterlilyProductDb;Trusted_Connection=True;TrustServerCertificate=True"
```

ASP.NET Core reads double underscores as configuration sections. Do not commit a real `.env` file.

If this is a new checkout, apply the migrations:

```powershell
dotnet ef database update --project .\WLdemo\WLdemo.csproj
```

The initial migration creates the `Products` table and the following stored procedures:

- `sp_GetAveragePricePerCategory`
- `sp_GetHighestStockValueCategory`

The database update creates the database, tables, and stored procedures. Testers normally only need to run the update command once on a new checkout. The `database drop` command is only for resetting a local demo database and should not be used if it contains data that needs to be kept.

## Optional Demo Data

The application does not seed data automatically. If sample products are needed for testing the reports, run `Scripts/SeedDummyData.sql` after the database has been created.

Using `sqlcmd` with the default SQL Server instance:

```powershell
sqlcmd -S localhost -E -i .\Scripts\SeedDummyData.sql
```

For SQL Express, use:

```powershell
sqlcmd -S localhost\SQLEXPRESS -E -i .\Scripts\SeedDummyData.sql
```

The script is safe to run more than once and inserts sample products only when they are not already present.

## Run the Application

From the solution root:

```powershell
dotnet run --project .\WLdemo\WLdemo.csproj
```

Alternatively, open the solution in Visual Studio and press `Ctrl+F5`.

Use the HTTPS URL displayed by the application, for example:

```text
https://localhost:7208
```

## Application Pages

- Product management: `/Product`
- Reports dashboard: `/Report`
- Home page: `/`

The Product page supports creating, reading, updating, and deleting products through jQuery AJAX requests. The Reports page displays the average price by category and the category with the highest total stock value.

## Run Tests

The tests use MSTest and Moq and isolate `ProductService` from the database through a mocked repository.

If the test project is not already in the solution, run this from the solution root:

```powershell
dotnet sln .\WLdemo.slnx add .\WLdemo.Tests\WLdemo.Tests.csproj
```

Run the test project:

```powershell
dotnet test .\WLdemo.Tests\WLdemo.Tests.csproj
```

Build the application:

```powershell
dotnet build .\WLdemo\WLdemo.csproj
```

## Assessment Requirement Mapping

- ASP.NET Core MVC - MVC controllers and Razor views
- Product CRUD - `ProductController`, `ProductService`, and `ProductRepository`
- Repository and service patterns - separate repository and service layers
- Code-first database - EF Core `ApplicationDbContext` and migrations
- MSSQL - SQL Server provider and configurable connection string
- Stored procedures - category average price and highest stock value calculations
- Caching - five-minute cache and long-lived cache with invalidation after writes
- Delegates - `OperationCallback` for CRUD operation feedback
- Bootstrap and jQuery - Bootstrap layout/modal UI and jQuery AJAX requests
- Unit testing - MSTest and Moq tests for ProductService behavior

## Manual UI Test Checklist

1. Open `/Product`.
2. Add a product and confirm it appears in the table.
3. Edit the product and confirm the values update.
4. Delete the product and confirm it is removed.
5. Add products in multiple categories.
6. Open `/Report` and confirm both report calculations display.
7. Refresh the pages and confirm the application continues to load cached data correctly.
