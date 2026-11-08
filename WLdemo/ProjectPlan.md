Project Plan - Waterlily Labs C# Assessment - H H Senevirathne

1. Requirement Understanding
The goal is to build an ASP.NET Core MVC web application for managing products with CRUD features and a reporting dashboard. 
The system requires two main pages: Product List and Reports. 
Product data consists of Product ID, Name, Category, Price, and Stock. 
Calculations for average category price and the highest stock value category must be computed using MSSQL Stored Procedures. 
Business logic and data access must follow the Service and Repository pattern. 
The Service layer will use caching (5-minute expiration and permanent cache until manual invalidation) and delegate callbacks for operation feedback. 
UI scripting will use Bootstrap and jQuery AJAX. 
MSTest with Moq will verify Service layer CRUD operations.

2. Implementation Steps
Step 1: Setup ASP.NET Core MVC solution structure and EF Core packages for MSSQL.
Step 2: Create Product entity model and configure ApplicationDbContext.
Step 3: Add initial EF Core migration and database creation scripts.
Step 4: Create Stored Procedures for category average price and highest stock value category.
Step 5: Build generic MemoryCache service implementing Cached and CachedLong policies.
Step 6: Implement ProductRepository handling standard CRUD and SQL stored procedure execution.
Step 7: Implement ProductService with delegate callback parameters and cache invalidation.
Step 8: Build ProductController and MVC views for Product management and Reports.
Step 9: Add Bootstrap layout and jQuery AJAX scripts for dynamic CRUD and modal popups.
Step 10: Create MSTest project with Moq unit tests for ProductService methods.

3. Issues Identified During Development

4. Pseudo Code

