/*
[Gringotts] DB
*/

USE [Gringotts]
GO

-- Problem 1.Records’ Count
SELECT  
    COUNT(*) AS [Count]
FROM [dbo].[WizzardDeposits]

-- Problem 2.Longest Magic Wand
SELECT  
    MAX([WizzardDeposits].[MagicWandSize])
FROM [dbo].[WizzardDeposits]

-- Problem 3.Longest Magic Wand per Deposit Groups
SELECT  
    [WizzardDeposits].[DepositGroup], 
    MAX([WizzardDeposits].[MagicWandSize]) AS [LongestMagicWand]
FROM [WizzardDeposits]
GROUP BY 
    [WizzardDeposits].[DepositGroup]

-- Problem 4.* Smallest Deposit Group per Magic Wand Size
GO
WITH [CTE_AveragesPerGroup]
	AS (SELECT  
		   AVG([WizzardDeposits].[MagicWandSize]) AS [AvgMagicWandSize], 
		   [WizzardDeposits].[DepositGroup]
	    FROM [WizzardDeposits]
	    GROUP BY 
		   [WizzardDeposits].[DepositGroup])
	SELECT TOP 2  
	    [DepositGroup]
	FROM [CTE_AveragesPerGroup]
	ORDER BY 
	    [AvgMagicWandSize] ASC
GO

-- Problem 5.Deposits Sum
SELECT  
    [WizzardDeposits].[DepositGroup], 
    SUM([WizzardDeposits].[DepositAmount]) AS [TotalSum]
FROM [WizzardDeposits]
GROUP BY 
    [WizzardDeposits].[DepositGroup]

-- Problem 6.Deposits Sum for Ollivander Family
SELECT   
    [WizzardDeposits].[DepositGroup], 
    SUM([WizzardDeposits].[DepositAmount]) AS [TotalSum]
FROM  [WizzardDeposits]
WHERE [WizzardDeposits].[MagicWandCreator] = 'Ollivander family'
GROUP BY 
    [WizzardDeposits].[DepositGroup]

-- Problem 7.Deposits Filter
GO
WITH CTE_DepOlivBelow150000
	AS (SELECT   
		   [WizzardDeposits].[DepositGroup], 
		   SUM([WizzardDeposits].[DepositAmount]) AS [TotalSum]
	    FROM  [WizzardDeposits]
	    WHERE [WizzardDeposits].[MagicWandCreator] = 'Ollivander family'
	    GROUP BY 
		   [WizzardDeposits].[DepositGroup])
	SELECT   
	    *
	FROM  [CTE_DepOlivBelow150000]
	WHERE [CTE_DepOlivBelow150000].[TotalSum] <= 150000
	ORDER BY 
	    [TotalSum] DESC
	GO

-- Problem 8. Deposit Charge
SELECT  
    [WizzardDeposits].[DepositGroup], 
    [WizzardDeposits].[MagicWandCreator], 
    MIN([WizzardDeposits].[DepositCharge]) AS [MinDepositCharge]
FROM [WizzardDeposits]
GROUP BY 
    [WizzardDeposits].[DepositGroup], 
    [WizzardDeposits].[MagicWandCreator]
ORDER BY 
    [WizzardDeposits].[MagicWandCreator] ASC, 
    [WizzardDeposits].[DepositGroup] ASC

-- Problem 9.Age Groups
WITH CTE_AgeGroups
	AS (SELECT  
		   CASE
			  WHEN [WizzardDeposits].[Age] BETWEEN 0 AND 10
				 THEN '[1-10]'
			  WHEN [WizzardDeposits].[Age] BETWEEN 11 AND 20
				 THEN '[11-20]'
			  WHEN [WizzardDeposits].[Age] BETWEEN 21 AND 30
				 THEN '[21-30]'
			  WHEN [WizzardDeposits].[Age] BETWEEN 31 AND 40
				 THEN '[31-40]'
			  WHEN [WizzardDeposits].[Age] BETWEEN 41 AND 50
				 THEN '[41-50]'
			  WHEN [WizzardDeposits].[Age] BETWEEN 51 AND 60
				 THEN '[51-60]'
			  ELSE '[61+]'
		   END AS [AgeGroup]
	    FROM [WizzardDeposits])
	SELECT  
	    [CTE_AgeGroups].[AgeGroup], 
	    COUNT([CTE_AgeGroups].[AgeGroup]) AS [WizardCount]
	FROM [CTE_AgeGroups]
	GROUP BY 
	    [CTE_AgeGroups].[AgeGroup]

-- Problem 10.First Letter
SELECT DISTINCT   
    LEFT([WizzardDeposits].[FirstName], 1) AS [FirstLetter]
FROM  [WizzardDeposits]
WHERE [WizzardDeposits].[DepositGroup] = 'Troll Chest'
--
GO 
WITH CTE_FirstLetter
	AS (SELECT   
		   LEFT([WizzardDeposits].[FirstName], 1) AS [FirstLetter], 
		   [WizzardDeposits].[FirstName]
	    FROM  [WizzardDeposits]
	    WHERE [WizzardDeposits].[DepositGroup] = 'Troll Chest')
	SELECT  
	    [CTE_FirstLetter].[FirstLetter], 
	    COUNT([CTE_FirstLetter].[FirstName])
	FROM [CTE_FirstLetter]
	GROUP BY 
	    [CTE_FirstLetter].[FirstLetter]
GO

-- Problem 11.Average Interest 
SELECT   
    [WizzardDeposits].[DepositGroup], 
    [WizzardDeposits].[IsDepositExpired], 
    AVG([WizzardDeposits].[DepositInterest])
FROM  [WizzardDeposits]
WHERE [WizzardDeposits].[DepositStartDate] > '19850101'
GROUP BY 
    [WizzardDeposits].[DepositGroup], 
    [WizzardDeposits].[IsDepositExpired]
ORDER BY 
    [WizzardDeposits].[DepositGroup] DESC, 
    [WizzardDeposits].[IsDepositExpired] ASC

-- Problem 12.* Rich Wizard, Poor Wizard
WITH CTE_RichPoor
	AS (SELECT       
		   [WD].[DepositAmount] -
	    (
		   SELECT   
			  [W].[DepositAmount]
		   FROM   [WizzardDeposits] AS [W]
		   WHERE [W].[Id] = [WD].[Id] + 1
	    ) AS [Diff]
	    FROM [WizzardDeposits] AS [WD])
	SELECT  
	    SUM([CTE_RichPoor].[Diff])
	FROM [CTE_RichPoor]

/*
[Softuni] DB
*/
USE [SoftUni]
GO

-- Problem 13.Departments Total Salaries
SELECT  
    [Employees].[DepartmentID], 
    SUM([Employees].[Salary]) AS [TotalSalary]
FROM [Employees]
GROUP BY 
    [Employees].[DepartmentID]
ORDER BY 
    [Employees].[DepartmentID]

-- Problem 14.Employees Minimum Salaries
SELECT   
    [Employees].[DepartmentID], 
    MIN([Employees].[Salary]) AS [MinimumSalary]
FROM  [Employees]
WHERE [Employees].[DepartmentID] IN(2, 5, 7)
AND [Employees].[HireDate] > '20000101'
GROUP BY 
    [Employees].[DepartmentID]

-- Problem 15.Employees Average Salaries
SELECT   
    *
INTO 
    [EmployeesSalaryMoreThan30000]
FROM  [Employees]
WHERE [Employees].[Salary] > 30000
--
DELETE FROM [EmployeesSalaryMoreThan30000]
WHERE         
    [EmployeesSalaryMoreThan30000].[ManagerID] = 42
--
UPDATE [EmployeesSalaryMoreThan30000]
  SET  
	 [EmployeesSalaryMoreThan30000].[Salary]+=5000
WHERE    
    [EmployeesSalaryMoreThan30000].[DepartmentID] = 1
--
SELECT  
    [EmployeesSalaryMoreThan30000].[DepartmentID], 
    AVG([EmployeesSalaryMoreThan30000].[Salary]) AS [AverageSalary]
FROM [EmployeesSalaryMoreThan30000]
GROUP BY 
    [EmployeesSalaryMoreThan30000].[DepartmentID]

-- Problem 16.Employees Maximum Salaries
GO
WITH CTE_EmployeesMaximumSalaries
	AS (SELECT  
		   [Employees].[DepartmentID], 
		   MAX([Employees].[Salary]) AS [MaxSalary]
	    FROM [Employees]
	    GROUP BY 
		   [Employees].[DepartmentID])
	SELECT   
	    *
	FROM  [CTE_EmployeesMaximumSalaries]
	WHERE [CTE_EmployeesMaximumSalaries].[MaxSalary] NOT BETWEEN 30000 AND 70000
    GO
--
SELECT  
    [Employees].[DepartmentID], 
    MAX([Employees].[Salary]) AS [MaxSalary]
FROM [Employees]
GROUP BY 
    [Employees].[DepartmentID]
HAVING MAX([Employees].[Salary]) NOT BETWEEN 30000 AND 70000

-- Problem 17.Employees Count Salaries
SELECT   
    COUNT(*)
FROM  [Employees]
WHERE [Employees].[ManagerID] IS NULL
	 AND [Employees].[Salary] IS NOT NULL

-- Problem 18.*3rd Highest Salary
WITH CTE_18_1
	AS (SELECT  
		   [Employees].[Salary], 
		   [Employees].[DepartmentID], 
		   RANK() OVER(PARTITION BY [Employees].[DepartmentID]
		   ORDER BY 
		   [Employees].[Salary] DESC) AS [SalaryRank]
	    FROM [Employees])
	SELECT   
	    [CTE_18_1].[DepartmentID], 
	    [CTE_18_1].[Salary]
	FROM  [CTE_18_1]
	WHERE [CTE_18_1].[SalaryRank] = 3