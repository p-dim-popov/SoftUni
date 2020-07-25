/*
Part I – Queries for SoftUni Database
*/
USE [SoftUni]
GO

-- Problem 1. Find Names of All Employees by First Name
SELECT [FirstName],
       [LastName]
FROM [Employees]
WHERE [FirstName] LIKE '[Ss][Aa]%'

-- Problem 2. Find Names of All employees by Last Name
SELECT [FirstName],
       [LastName]
FROM [Employees]
WHERE [LastName] LIKE '%ei%'

-- Problem 3. Find First Names of All Employees
SELECT [FirstName]
FROM [Employees]
WHERE [DepartmentID] = 3
   OR [DepartmentID] = 10
    AND [HireDate] BETWEEN '1995' AND '2005'

-- Problem 4. Find All Employees Except Engineers
SELECT [FirstName],
       [LastName]
FROM [Employees]
WHERE [JobTitle] NOT LIKE '%[Ee]ngineer%'

-- Problem 5. Find Towns with Name Length
SELECT [Name]
FROM [Towns]
WHERE LEN(Name) BETWEEN 5 AND 6
ORDER BY [Name] ASC

-- Problem 6. Find Towns Starting With
SELECT [TownID], [Name]
FROM [Towns]
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name] ASC

-- Problem 7. Find Towns Not Starting With
SELECT [TownID], [Name]
FROM [Towns]
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name] ASC

-- Problem 8.Create View Employees Hired After 2000 Year
GO

CREATE /*OR ALTER*/ VIEW V_EmployeesHiredAfter2000 AS
SELECT [FirstName],
       [LastName]
FROM [Employees]
WHERE [HireDate] > '2001'

GO

-- Problem 9. Length of Last Name
SELECT [FirstName],
       [LastName]
FROM [Employees]
WHERE LEN(LastName) = 5

-- Problem 10. Rank Employees by Salary
SELECT [EmployeeID],
       [FirstName],
       [LastName],
       [Salary],
       DENSE_RANK() OVER
           (PARTITION BY [Salary] ORDER BY [EmployeeID])
           AS [Rank]
FROM [Employees]
WHERE [Salary] BETWEEN 10000 AND 50000
ORDER BY [Salary] DESC

-- Problem 11. Find All Employees with Rank 2 *
SELECT *
FROM (SELECT [EmployeeID],
             [FirstName],
             [LastName],
             [Salary],
             DENSE_RANK() OVER
                 (PARTITION BY [Salary] ORDER BY [EmployeeID])
                 AS [Rank]
      FROM [Employees]
      WHERE [Salary] BETWEEN 10000 AND 50000
) AS E_WithRanks
WHERE [Rank] = 2
ORDER BY [Salary] DESC

-- or
WITH EmployeesWithRanks AS (
    SELECT [EmployeeID],
           [FirstName],
           [LastName],
           [Salary],
           DENSE_RANK() OVER
               (PARTITION BY [Salary] ORDER BY [EmployeeID])
               AS [Rank]
    FROM [Employees]
    WHERE [Salary] BETWEEN 10000 AND 50000
)
SELECT *
FROM [EmployeesWithRanks]
WHERE [Rank] = 2
ORDER BY [Salary] DESC

/*
Part II – Queries for Geography Database
*/
USE [Geography]
GO

-- Problem 12. Countries Holding ‘A’ 3 or More Times
SELECT [CountryName] AS [Country Name],
       [IsoCode]     AS [ISO Code]
FROM [Countries]
WHERE [CountryName] LIKE '%[Aa]%[Aa]%[Aa]%'
ORDER BY [IsoCode]

-- Problem 13. Mix of Peak and River Names
SELECT [PeakName],
       [RiverName],
       LOWER([PeakName]) +
       SUBSTRING(LOWER(RiverName), 2, LEN(RiverName) - 1) AS [Mix]
FROM [Peaks], [Rivers]
-- FROM [Countries]
-- JOIN [MountainsCountries] [MC]
-- ON [Countries].[CountryCode] = [MC].[CountryCode]
-- JOIN [CountriesRivers]    [CR]
-- ON [Countries].[CountryCode] = [CR].[CountryCode]
-- JOIN [Mountains]          [M]
-- ON [MC].[MountainId] = [M].[Id]
-- JOIN [Peaks]              [P]
-- ON [M].[Id] = [P].[MountainId]
-- JOIN [Rivers]             [R2]
-- ON [CR].RiverId = [R2].[Id]
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
ORDER BY [Mix]

/*
Part III – Queries for Diablo Database
*/
USE [Diablo]
GO

-- Problem 14. Games from 2011 and 2012 year
SELECT TOP 50 [Name],
              FORMAT([Start], 'yyyy-MM-dd') AS [Start]
FROM [Games]
-- WHERE [Start] BETWEEN '2011' AND '2013'
-- WHERE YEAR([Start]) = '2011' OR YEAR([Start]) = '2012'
WHERE YEAR([Start]) >= '2011'
  AND YEAR([Start]) <= '2012'
ORDER BY [Start],
         [Name]

-- Problem 15. User Email Providers
SELECT [Username],
       (
           SELECT [value]
           FROM (
               SELECT [value],
                      row_number()
                              OVER (ORDER BY current_timestamp) AS [row]
               FROM STRING_SPLIT([Users].[Email], '@')
           ) AS SplittedString
           WHERE [row] = 2
       ) AS [Email Provider]
FROM [Users]
ORDER BY [Email Provider],
         [Username]

-- or

SELECT [Username],
       (SELECT REPLACE(
                       PARSENAME(
                               REPLACE(
                                       REPLACE([Users].[Email], '.', '/'), -- replace dot with slash because PARSENAME parses strings by dot
                                       '@', '.'), -- parse it like 'username.mail/com'
                               1), -- get the first from right so: 'mail/com'
                       '/', '.') -- and replace back the comma with a dot so 'mail/com' becomes 'mail.com'
       )
           AS [Email Provider]
FROM [Users]
ORDER BY [Email Provider],
         [Username]

-- Problem 16. Get Users with IPAddress Like Pattern
SELECT [Username], [IpAddress] AS [IP Address]
FROM [Users]
WHERE [IpAddress] LIKE '___.1%.%.___'
ORDER BY [Username]

-- Problem 17. Show All Games with Duration and Part of the Day
SELECT [Name],
       CASE
           WHEN CAST([Start] AS TIME) BETWEEN '00:00:00' AND '12:00:00'
               THEN 'Morning'
           WHEN CAST([Start] AS TIME) BETWEEN '12:00:00' AND '18:00:00'
               THEN 'Afternoon'
           ELSE 'Evening'
           END AS [Part of the Day],
       CASE
           WHEN [Duration] IS NULL
               THEN 'Extra Long'
           WHEN [Duration] > 6
               THEN 'Long'
           WHEN [Duration] BETWEEN 4 AND 6
               THEN 'Short'
           WHEN [Duration] BETWEEN 0 AND 3
               THEN 'Extra Short'
           END AS [Duration]
FROM [Games]
ORDER BY [Name],
         [Duration]

/*
Part IV – Date Functions Queries
*/
-- Abstract database and tables are used here :D

-- Problem 18. Orders Table
SELECT [ProductName],
       [OrderDate],
       DATEADD(DAY, 3, [OrderDate])   AS [Pay Due],
       DATEADD(MONTH, 1, [OrderDate]) AS [Deliver Due]
FROM [Orders]

-- Problem 19. People Table
DROP TABLE IF EXISTS [People]
CREATE TABLE [People]
(
    [Id]        INT PRIMARY KEY IDENTITY (1,1),
    [Name]      VARCHAR(30) NOT NULL,
    [Birthdate] DATE        NOT NULL
)
DROP TABLE IF EXISTS [People]

SELECT [Name],
       DATEDIFF(YEAR, [Birthdate], CURRENT_TIMESTAMP),
       DATEDIFF(MONTH, [Birthdate], CURRENT_TIMESTAMP),
       DATEDIFF(DAY, [Birthdate], CURRENT_TIMESTAMP),
       DATEDIFF(MINUTE, [Birthdate], CURRENT_TIMESTAMP)
FROM [People]