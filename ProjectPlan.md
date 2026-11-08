Project Plan - Waterlily Labs C# Assessment - H H Senevirathne

Requirement Understanding
The goal is to build an ASP.NET Core MVC web application for managing products with CRUD features and a reporting dashboard.
The system requires two main pages: Product List and Reports.
Product data consists of Product ID, Name, Category, Price, and Stock.
Calculations for average category price and the highest stock value category must be computed using MSSQL Stored Procedures.
Business logic and data access must follow the Service and Repository pattern.
The Service layer will use caching (5-minute expiration and permanent cache until manual invalidation) and delegate callbacks for operation feedback.
UI scripting will use Bootstrap and jQuery AJAX.
MSTest with Moq will verify Service layer CRUD operations.

Implementation Steps
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

Issues Identified During Development

Cache Stale Data Risk: Executing CRUD actions without explicit cache management leaves outdated records in memory.
Mitigation: Implemented central cache invalidation in ProductService that clears product and report cache keys immediately following any database modification.

EF Core Raw SQL Stored Procedure Result Mapping: Mapping stored procedure results to C# classes requires configuration when models lack explicit primary keys.
Mitigation: Added keyless entity mapping (HasNoKey()) in ApplicationDbContext for CategoryAvgPriceViewModel and HighestStockValueCategoryViewModel.

Database Null/Aggregation Exceptions: Empty tables or categories without price data could cause null reference issues during execution.
Mitigation: Handled null checks explicitly in stored procedures using CAST/AVG operations and checked return objects prior to view binding.

Pseudo Code

A. Generic Memory Cache Wrapper (Cached & CachedLong)
FUNCTION GetOrCreate(cacheKey, dataFactory, expirationTime):
IF cache contains entry for cacheKey THEN
RETURN cached value
ELSE
data = EXECUTE dataFactory()
IF expirationTime HAS VALUE THEN
SET cache options WITH expirationTime (Cached policy - 5 mins)
ELSE
SET cache options WITHOUT expiration (CachedLong policy - Permanent)
END IF
STORE data IN cache FOR cacheKey
RETURN data
END IF
END FUNCTION

B. Product Service CRUD Action with Delegates & Cache Invalidation
FUNCTION CreateProduct(productModel, callbackDelegate):
TRY
VALIDATE productModel fields
CALL ProductRepository.Add(productModel)
CALL ProductRepository.Save()

    // Cache Invalidation
    CALL MemoryCacheService.Remove("products_all_cache")
    CALL MemoryCacheService.Remove("reports_avg_price_cache")
    CALL MemoryCacheService.Remove("reports_highest_stock_cache")
    
    INVOKE callbackDelegate("Product created successfully.", true)
CATCH Exception ex
    INVOKE callbackDelegate("Error: " + ex.Message, false)
END TRY


END FUNCTION

C. SQL Stored Procedures for Reporting
PROCEDURE sp_GetAveragePricePerCategory:
SELECT Category, CAST(AVG(Price) AS DECIMAL(18,2)) AS AveragePrice
FROM Products
GROUP BY Category
END PROCEDURE

PROCEDURE sp_GetHighestStockValueCategory:
SELECT TOP 1 Category, SUM(Price * Stock) AS TotalStockValue
FROM Products
GROUP BY Category
ORDER BY TotalStockValue DESC
END PROCEDURE
