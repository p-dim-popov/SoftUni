

USE [master]
GO

CREATE DATABASE [Service]
GO

USE [Service]
GO

--1.Create---------------------------------------------------------------------

CREATE TABLE [Users]
(
    [Id]        INT IDENTITY PRIMARY KEY,
    [Username]  VARCHAR(30)
    UNIQUE NOT NULL,
    [Password]  VARCHAR(50) NOT NULL,
    [Name]      VARCHAR(50),
    [Birthdate] DATETIME,
    [Age]       INT CHECK([Age] BETWEEN 14 AND 110),
    [Email]     VARCHAR(50) NOT NULL
)

CREATE TABLE [Departments]
(
    [Id]   INT IDENTITY PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE [Employees]
(
    [Id]           INT IDENTITY PRIMARY KEY,
    [FirstName]    VARCHAR(25),
    [LastName]     VARCHAR(25),
    [Birthdate]    DATETIME,
    [Age]          INT CHECK([age] BETWEEN 18 AND 110),
    [DepartmentId] INT FOREIGN KEY REFERENCES [Departments](
    [Id])
)

CREATE TABLE [Categories]
(
    [Id]           INT IDENTITY PRIMARY KEY,
    [Name]         VARCHAR(50) NOT NULL,
    [DepartmentId] INT FOREIGN KEY REFERENCES [Departments](
    [Id]) NOT NULL
)

CREATE TABLE [Status]
(
    [Id]    INT IDENTITY PRIMARY KEY,
    [label] VARCHAR(30) NOT NULL
)

CREATE TABLE [Reports]
(
    [Id]          INT IDENTITY PRIMARY KEY,
    [CategoryId]  INT FOREIGN KEY REFERENCES [Categories](
    [Id]) NOT NULL,
    [StatusId]    INT FOREIGN KEY REFERENCES [Status](
    [Id]) NOT NULL,
    [OpenDate]    DATETIME NOT NULL,
    [CloseDate]   DATETIME,
    [Description] VARCHAR(200) NOT NULL,
    [UserId]      INT FOREIGN KEY REFERENCES [Users](
    [Id]) NOT NULL,
    [EmployeeId]  INT FOREIGN KEY REFERENCES [Employees](
    [Id])
)

--2.Insert-------------------------------------------------------------

INSERT INTO [Employees]
(
    [Employees].[FirstName],
    [Employees].[LastName],
    [Employees].[Birthdate],
    [Employees].[DepartmentId]
)
VALUES
    (
    'Marlo', 'O''Malley', '1958-09-21', 1
    ),
    (
    'Niki', 'Stanaghan', '1969-11-26', 4
    ),
    (
    'Ayrton', 'Senna', '1960-03-21', 9
    ),
    (
    'Ronnie', 'Peterson', '1944-02-14', 9
    ),
    (
    'Giovanna', 'Amati', '1959-07-20', 5
    )

INSERT INTO [Reports]
(
    [Reports].[CategoryId],
    [Reports].[StatusId],
    [Reports].[OpenDate],
    [Reports].[CloseDate],
    [Reports].[Description],
    [Reports].[UserId],
    [Reports].[EmployeeId]
)
VALUES
    (
    1, 1, '2017-04-13', NULL, 'Stuck Road on Str.133', 6, 2
    ),
    (
    6, 3, '2015-09-05', '2015-12-06', 'Charity trail running', 3, 5
    ),
    (
    14, 2, '2015-09-07', NULL, 'Falling bricks on Str.58', 5, 2
    ),
    (
    4, 3, '2017-07-03', '2017-07-06', 'Cut off streetlight on Str.11', 1, 1
    )

--3.Update------------------------------------------------------------------

UPDATE [Reports]
  SET
	 [Reports].[CloseDate] = GETDATE()
WHERE
    [Reports].[CloseDate] IS NULL

--4.Delete--------------------------------------------------------------------

DELETE FROM [Reports]
WHERE
    [Reports].[StatusId] = 4

--5.Unassigned Reports---------------------------------------------------------

SELECT
    [Reports].[Description],
    format([Reports].[OpenDate], 'dd-MM-yyyy')
FROM  [Reports]
WHERE [Reports].[EmployeeId] IS NULL
ORDER BY
    [Reports].[OpenDate] ASC,
    [Reports].[Description] ASC

--6.Reports & Categories------------------------------------------------------

SELECT
    [Reports].[Description],
    [Categories].[Name]
FROM [Reports]
JOIN [Categories]
	ON [Reports].[CategoryId] = [Categories].[Id]
ORDER BY
    [Reports].[Description] ASC,
    [Categories].[Name] ASC

--7.Most Reported Category----------------------------------------------------

SELECT TOP 5
    [Categories].[Name],
    COUNT([Reports].[Id]) AS [RepNum]
FROM [Categories]
JOIN [Reports]
	ON [Reports].[CategoryId] = [Categories].[Id]
GROUP BY
    [Categories].[Name]
ORDER BY
    [RepNum] DESC,
    [Categories].[Name] ASC

--8.Birthday Report------------------------------------------------------------

SELECT
    [Users].[Username],
    [Categories].[Name]
FROM  [Reports]
JOIN [Users]
	 ON [Reports].[UserId] = [Users].[Id]
JOIN [Categories]
	 ON [Reports].[CategoryId] = [Categories].[Id]
WHERE format([Users].[Birthdate], 'dd-MM') = format([Reports].[OpenDate], 'dd-MM')
ORDER BY
    [Username] ASC,
    [Categories].[Name] ASC

--9.Users per Employee-------------------------------------------------------------

SELECT
    [Employees].[FirstName] + ' ' + [Employees].[LastName] AS [FullName],
    COUNT([Users].[Id]) AS                                    [UsersCount]
FROM [Employees]
LEFT JOIN [Reports]
	ON [Employees].[Id] = [Reports].[EmployeeId]
LEFT JOIN [Users]
	ON [Reports].[UserId] = [Users].[Id]
GROUP BY
    [Employees].[FirstName],
    [Employees].[LastName]
ORDER BY
    [UsersCount] DESC,
    [FullName] ASC

--10.Full Info--------------------------------------------------------------------

SELECT
    CASE
	   WHEN [Employees].[FirstName] IS NULL
		  THEN 'None'
	   ELSE [Employees].[FirstName] + ' ' + [Employees].[LastName]
    END AS                                        [Employee],
    CASE
	   WHEN [Departments].[Name] IS NULL
		  THEN 'None'
	   ELSE [Departments].[Name]
    END AS                                        [DepName],
    [Categories].[Name] AS                        [CatName],
    [Reports].[Description],
    format([Reports].[OpenDate], 'dd.MM.yyyy') AS [OpenDate],
    [Status].[label],
    [Users].[Name] AS                             [UsrName]
FROM [Reports]
LEFT JOIN [Employees]
	ON [Reports].[EmployeeId] = [Employees].[Id]
LEFT JOIN [Departments]
	ON [Employees].[DepartmentId] = [Departments].[Id]
LEFT JOIN [Categories]
	ON [Reports].[CategoryId] = [Categories].[Id]
LEFT JOIN [Status]
	ON [Reports].[StatusId] = [Status].[Id]
LEFT JOIN [Users]
	ON [Reports].[UserId] = [Users].[Id]
ORDER BY
    [Employees].[FirstName] DESC,
    [Employees].[LastName] DESC,
    [Departments].[Name] ASC,
    [Categories].[Name] ASC,
    [Reports].[Description] ASC,
    [OpenDate] ASC,
    [Status].[label] ASC,
    [UsrName] ASC

--11.Hours to Complete-------------------------------------------------------

DROP FUNCTION
    [dbo].[udf_HoursToComplete]
GO

CREATE FUNCTION [udf_HoursToComplete]
(
    @StartDate DATETIME,
    @EndDate   DATETIME
)
RETURNS INT
AS
    BEGIN
	   IF @StartDate IS NULL
		  RETURN 0;
	   IF @EndDate IS NULL
		  RETURN 0;

	   RETURN DATEDIFF(hour, @EndDate, @StartDate) * (-1)
    END
GO

SELECT
    [dbo].[udf_HoursToComplete]([OpenDate], [CloseDate]) AS [TotalHours]
FROM [Reports]

--12.Assign Employee----------------------------------------------------------

CREATE OR ALTER PROC [usp_AssignEmployeeToReport]
    @EmployeeId INT,
    @ReportId   INT
AS
BEGIN
    DECLARE @employeeDep INT=
    (
	   SELECT
		  [DepartmentId]
	   FROM  [Employees]
	   WHERE [Id] = @EmployeeId
    )

    DECLARE @reportDep INT=
    (
	   SELECT
		  [Categories].[DepartmentId]
	   FROM  [Reports]
	   JOIN [Categories]
		    ON [Reports].[CategoryId] = [Categories].[Id]
	   WHERE [Reports].[Id] = @ReportId
    )

    IF @employeeDep <> @reportDep
    BEGIN
	   RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1)
	   RETURN
    END

    UPDATE [Reports]
	 SET
		[Reports].[EmployeeId] = @EmployeeId
    WHERE
	   [Reports].[Id] = @ReportId
END