
/*
SoftUni Database
*/

USE [SoftUni];
GO

-- Problem 1.Employee Address
SELECT TOP 5  
    [Employees].[EmployeeID], 
    [Employees].[JobTitle], 
    [Employees].[AddressID], 
    [Addresses].[AddressText]
FROM [Employees]
INNER JOIN [Addresses]
	ON [Employees].[AddressID] = [Addresses].[AddressID]
ORDER BY 
    [AddressID] ASC;

-- Problem 2.Addresses with Towns
SELECT TOP 50  
    [Employees].[FirstName], 
    [Employees].[LastName], 
    [Towns].[Name], 
    [Addresses].[AddressText]
FROM [dbo].[Employees]
INNER JOIN [dbo].[Addresses]
	ON [dbo].[Employees].[AddressID] = [dbo].[Addresses].[AddressID]
INNER JOIN [dbo].[Towns]
	ON [dbo].[Addresses].[TownID] = [dbo].[Towns].[TownID]
ORDER BY 
    [FirstName] ASC, 
    [LastName] ASC

-- Problem 3.Sales Employee
SELECT   
    [dbo].[Employees].[EmployeeID], 
    [dbo].[Employees].[FirstName], 
    [dbo].[Employees].[LastName], 
    [dbo].[Departments].[Name] AS [DepartmentName]
FROM  [dbo].[Employees]
INNER JOIN [dbo].[Departments]
	 ON [dbo].[Employees].[DepartmentID] = [dbo].[Departments].[DepartmentID]
WHERE [dbo].[Departments].[Name] = 'Sales'
ORDER BY 
    [dbo].[Employees].[EmployeeID] ASC

-- Problem 4.Employee Departments
SELECT TOP 5   
    [dbo].[Employees].[EmployeeID], 
    [dbo].[Employees].[FirstName], 
    [dbo].[Employees].[Salary], 
    [dbo].[Departments].[Name] AS [DepartmentName]
FROM  [dbo].[Employees]
INNER JOIN [dbo].[Departments]
	 ON [dbo].[Employees].[DepartmentID] = [dbo].[Departments].[DepartmentID]
WHERE [dbo].[Employees].[Salary] > 15000
ORDER BY 
    [dbo].[Employees].[DepartmentID] ASC

-- Problem 5.Employees Without Project
SELECT TOP 3   
    [dbo].[Employees].[EmployeeID], 
    [dbo].[Employees].[FirstName]
FROM  [dbo].[Employees]
LEFT JOIN [dbo].[EmployeesProjects]
	 ON [dbo].[Employees].[EmployeeID] = [dbo].[EmployeesProjects].[EmployeeID]
WHERE [dbo].[EmployeesProjects].[ProjectID] IS NULL
ORDER BY 
    [dbo].[Employees].[EmployeeID] ASC

-- Problem 6.Employees Hired After
SELECT   
    [dbo].[Employees].[FirstName], 
    [dbo].[Employees].[LastName], 
    [dbo].[Employees].[HireDate], 
    [dbo].[Departments].[Name] AS [DeptName]
FROM  [dbo].[Employees]
INNER JOIN [dbo].[Departments]
	 ON [dbo].[Employees].[DepartmentID] = [dbo].[Departments].[DepartmentID]
WHERE [dbo].[Departments].[Name] IN('Sales', 'Finance')
	 AND [dbo].[Employees].[HireDate] > '19990101'
ORDER BY 
    [dbo].[Employees].[HireDate] ASC

-- Problem 7.Employees with Project
SELECT TOP 5   
    [dbo].[Employees].[EmployeeID], 
    [dbo].[Employees].[FirstName], 
    [dbo].[Projects].[Name] AS [ProjectName]
FROM  [dbo].[Employees]
INNER JOIN [dbo].[EmployeesProjects]
	 ON [dbo].[Employees].[EmployeeID] = [dbo].[EmployeesProjects].[EmployeeID]
INNER JOIN [dbo].[Projects]
	 ON [dbo].[EmployeesProjects].[ProjectID] = [dbo].[Projects].[ProjectID]
WHERE [dbo].[Projects].[StartDate] > '20020813'
	 AND [dbo].[Projects].[EndDate] IS NULL
ORDER BY 
    [dbo].[Employees].[EmployeeID] ASC

-- Problem 8.Employee 24
SELECT   
    [dbo].[Employees].[EmployeeID], 
    [dbo].[Employees].[FirstName],
    CASE
	   WHEN [dbo].[Projects].[StartDate] > '2005'
		  THEN NULL
	   ELSE [dbo].[Projects].[Name]
    END AS [ProjectName]
FROM  [dbo].[Employees]
INNER JOIN [dbo].[EmployeesProjects]
	 ON [dbo].[Employees].[EmployeeID] = [dbo].[EmployeesProjects].[EmployeeID]
INNER JOIN [dbo].[Projects]
	 ON [dbo].[EmployeesProjects].[ProjectID] = [dbo].[Projects].[ProjectID]
WHERE [dbo].[Employees].[EmployeeID] = 24

-- Problem 9.Employee Manager
SELECT   
    [E].[EmployeeID], 
    [E].[FirstName], 
    [E].[ManagerID], 
    [M].[FirstName] AS [ManagerName]
FROM  [dbo].[Employees] AS [E]
INNER JOIN [dbo].[Employees] AS [M]
	 ON [E].[ManagerID] = [M].[EmployeeID]
WHERE E.[ManagerID] IN(3, 7)
ORDER BY 
    E.[EmployeeID] ASC

-- Problem 10.Employee Summary
SELECT TOP 50  
    [E].[EmployeeID], 
    [E].[FirstName] + ' ' + [E].[LastName] AS [EmployeeName], 
    [M].[FirstName] + ' ' + [M].[LastName] AS [ManagerName], 
    [dbo].[Departments].[Name] AS             [DepartmentName]
FROM [dbo].[Employees] AS [E]
INNER JOIN [dbo].[Employees] AS [M]
	ON [E].[ManagerID] = [M].[EmployeeID]
INNER JOIN [dbo].[Departments]
	ON [E].[DepartmentID] = [dbo].[Departments].[DepartmentID]
ORDER BY 
    [E].[EmployeeID] ASC

-- Problem 11.Min Average Salary
SELECT TOP 1          
(
    SELECT   
	   AVG([dbo].[Employees].[Salary])
    FROM   [dbo].[Employees]
    WHERE [dbo].[Employees].[DepartmentID] = [dbo].[Departments].[DepartmentID]
) AS [AverageSalary]
FROM [dbo].[Departments]
ORDER BY 
    [AverageSalary] ASC

-- better with group by
SELECT TOP 1  
    AVG([Salary]) AS [AverageSalary]
FROM [dbo].[Employees]
GROUP BY 
    [dbo].[Employees].[DepartmentID]
ORDER BY 
    [AverageSalary] ASC

/*
Geography DB
*/

USE [Geography]
GO

-- Problem 12.Highest Peaks in Bulgaria
SELECT   
    [dbo].[MountainsCountries].[CountryCode], 
    [dbo].[Mountains].[MountainRange], 
    [dbo].[Peaks].[PeakName], 
    [dbo].[Peaks].[Elevation]
FROM  [dbo].[Peaks]
INNER JOIN [dbo].[Mountains]
	 ON [dbo].[Peaks].[MountainId] = [dbo].[Mountains].[Id]
INNER JOIN [dbo].[MountainsCountries]
	 ON [dbo].[Mountains].[Id] = [dbo].[MountainsCountries].[MountainId]
WHERE [dbo].[Peaks].[Elevation] > 2835
	 AND [dbo].[MountainsCountries].[CountryCode] = 'BG'
ORDER BY 
    [dbo].[Peaks].[Elevation] DESC

-- Problem 13.Count Mountain Ranges
SELECT  
    [dbo].[MountainsCountries].[CountryCode], 
    COUNT([dbo].[Mountains].[MountainRange]) AS [MountainRanges]
FROM [dbo].[Mountains]
INNER JOIN [dbo].[MountainsCountries]
	ON [dbo].[Mountains].[Id] = [dbo].[MountainsCountries].[MountainId]
GROUP BY 
    [dbo].[MountainsCountries].[CountryCode]
HAVING [dbo].[MountainsCountries].[CountryCode] IN('BG', 'RU', 'US')

-- Problem 14.Countries with Rivers
SELECT TOP 5   
    [dbo].[Countries].[CountryName], 
    [dbo].[Rivers].[RiverName]
FROM  [dbo].[Countries]
LEFT OUTER JOIN [dbo].[CountriesRivers]
	 ON [dbo].[Countries].[CountryCode] = [dbo].[CountriesRivers].[CountryCode]
LEFT OUTER JOIN [dbo].[Rivers]
	 ON [dbo].[CountriesRivers].[RiverId] = [dbo].[Rivers].[Id]
INNER JOIN [dbo].[Continents]
	 ON [dbo].[Countries].[ContinentCode] = [dbo].[Continents].[ContinentCode]
WHERE [dbo].[Continents].[ContinentName] = 'Africa'
ORDER BY 
    [dbo].[Countries].[CountryName] ASC

-- Problem 15.*Continents and Currencies
-- TODO: ...
--
--
-- Problem 16.Countries without any Mountains
SELECT   
    COUNT(*)
FROM  [dbo].[Countries]
LEFT OUTER JOIN [dbo].[MountainsCountries]
	 ON [dbo].[Countries].[CountryCode] = [dbo].[MountainsCountries].[CountryCode]
WHERE [dbo].[MountainsCountries].[MountainId] IS NULL

-- Problem 17.Highest Peak and Longest River by Country
SELECT TOP 5  
    [C].[CountryName], 
    MAX([P].[Elevation]) AS [HighestPeakElevation], 
    MAX([R].[Length]) AS    [LongestRiverLength]
FROM [dbo].[Countries] AS [C]
FULL OUTER JOIN [dbo].[MountainsCountries] AS [MC]
	ON [C].[CountryCode] = [MC].[CountryCode]
FULL OUTER JOIN [dbo].[Mountains] AS [M]
	ON [MC].[MountainId] = [M].[Id]
FULL OUTER JOIN [dbo].[Peaks] AS [P]
	ON [M].[Id] = [P].[MountainId]
FULL OUTER JOIN [dbo].[CountriesRivers] AS [CR]
	ON [C].[CountryCode] = [CR].[CountryCode]
FULL OUTER JOIN [dbo].[Rivers] AS [R]
	ON [CR].[RiverId] = [R].[Id]
GROUP BY 
    [C].[CountryName]
ORDER BY 
    [HighestPeakElevation] DESC, 
    [LongestRiverLength] DESC, 
    [C].[CountryName] ASC

-- Problem 18.* Highest Peak Name and Elevation by Country
-- TODO: ...
--
--