USE [WaterlilyProductDb];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name = N'Demo Laptop' AND Category = N'Electronics')
BEGIN
	INSERT INTO dbo.Products (Name, Category, Price, Stock)
	VALUES (N'Demo Laptop', N'Electronics', 1200.00, 10);
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name = N'Demo Keyboard' AND Category = N'Electronics')
BEGIN
	INSERT INTO dbo.Products (Name, Category, Price, Stock)
	VALUES (N'Demo Keyboard', N'Electronics', 75.00, 25);
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name = N'Demo Desk' AND Category = N'Furniture')
BEGIN
	INSERT INTO dbo.Products (Name, Category, Price, Stock)
	VALUES (N'Demo Desk', N'Furniture', 250.00, 8);
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name = N'Demo Chair' AND Category = N'Furniture')
BEGIN
	INSERT INTO dbo.Products (Name, Category, Price, Stock)
	VALUES (N'Demo Chair', N'Furniture', 150.00, 15);
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name = N'Demo Notebook' AND Category = N'Stationery')
BEGIN
	INSERT INTO dbo.Products (Name, Category, Price, Stock)
	VALUES (N'Demo Notebook', N'Stationery', 12.50, 100);
END;

SELECT Id, Name, Category, Price, Stock
FROM dbo.Products
WHERE Name LIKE N'Demo %'
ORDER BY Id;
GO
