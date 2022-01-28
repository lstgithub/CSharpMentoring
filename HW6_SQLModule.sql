SELECT OrderID, ShippedDate, ShipVia FROM Orders WHERE Orders.ShippedDate >= CONVERT(date, '1998-05-06') AND Orders.ShipVia >= 2;  -- 1.1 - 1

SELECT OrderID, CASE WHEN Orders.ShippedDate IS NULL THEN 'Not shipped' END AS DeliveryStatus FROM Orders WHERE Orders.ShippedDate IS NULL; -- 1.1 - 2

SELECT ContactName, Country FROM Customers WHERE Customers.Country IN ('Canada', 'USA') ORDER BY Customers.Country, Customers.ContactName ASC; -- 1.2 - 1

SELECT ContactName, Country FROM Customers WHERE Customers.Country NOT IN ('Canada', 'USA') ORDER BY Customers.ContactName ASC; -- 1.2 - 2

SELECT DISTINCT OrderID FROM [Order Details] WHERE [Order Details].Quantity BETWEEN 3 AND 10; -- 1.3 - 1

SELECT CustomerID, Country FROM Customers WHERE Customers.Country LIKE '[B-G]%' ORDER BY Customers.Country ASC; -- 1.3 - 2 !

SELECT * FROM Products WHERE Products.ProductName LIKE 'cho%olade'; -- 1.4 - 1

SELECT ROUND(SUM((UnitPrice * Quantity) - ((UnitPrice * Quantity) * Discount)), 0) AS TotalWithDiscount FROM [Order Details]; -- 2.1 - 1

SELECT COUNT(CASE WHEN Orders.ShippedDate IS NULL THEN 'Not shipped' END) AS NotDeliveredYet FROM Orders; -- 2.1 - 2

SELECT DISTINCT YEAR(ShippedDate) AS AggregatedYear, COUNT(CustomerID) AS AggregatedOrders FROM Orders WHERE ShippedDate IS NOT NULL GROUP BY YEAR(ShippedDate); -- 2.2 - 1

SELECT CONCAT(Employees.LastName, ' ', Employees.FirstName) AS Name, COUNT(Orders.EmployeeID) AS TotalOrdersForSeller FROM Orders JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID GROUP BY Orders.EmployeeID, Employees.FirstName, Employees.LastName ORDER BY TotalOrdersForSeller DESC; -- 2.2 - 2

SELECT CONCAT(Employees.LastName, ' ', Employees.FirstName) AS WesternEmployeeName
	FROM Territories
	JOIN Region ON Region.RegionID = Territories.RegionID
	JOIN EmployeeTerritories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
	JOIN Employees ON EmployeeTerritories.EmployeeID = Employees.EmployeeID
	WHERE Region.RegionDescription = 'Western'
	GROUP BY Region.RegionDescription, Employees.FirstName, Employees.LastName; -- 2.3 - 1

SELECT Customers.ContactName, COUNT(Orders.ShippedDate) AS TotalOrders FROM Customers LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID GROUP BY Customers.ContactName ORDER BY TotalOrders ASC; -- 2.3 - 2