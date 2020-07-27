

USE [master]
GO

CREATE DATABASE [Bitbucket]
GO

USE [Bitbucket]
GO

/*
Section 1. DDL (30 pts)
*/

-- 1.Database Design ----------------------------------------------------------

CREATE TABLE [Users]
(
    [Id]       INT IDENTITY PRIMARY KEY,
    [Username] VARCHAR(30) NOT NULL,
    [Password] VARCHAR(30) NOT NULL,
    [Email]    VARCHAR(50) NOT NULL
)

CREATE TABLE [Repositories]
(
    [Id]   INT IDENTITY PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE [RepositoriesContributors]
(
    [RepositoryId]  INT FOREIGN KEY REFERENCES [Repositories](
    [Id]) NOT NULL,
    [ContributorId] INT FOREIGN KEY REFERENCES [Users](
    [Id]) NOT NULL
		CONSTRAINT [PK_RepositoriesContributors] PRIMARY KEY([RepositoryId], [ContributorId])
)

CREATE TABLE [Issues]
(
    [Id]           INT IDENTITY PRIMARY KEY,
    [Title]        VARCHAR(255) NOT NULL,
    [IssueStatus]  CHAR(6) NOT NULL,
    [RepositoryId] INT FOREIGN KEY REFERENCES [Repositories](
    [Id]) NOT NULL,
    [AssigneeId]   INT FOREIGN KEY REFERENCES [Users](
    [Id]) NOT NULL
)

CREATE TABLE [Commits]
(
    [Id]            INT IDENTITY PRIMARY KEY,
    [Message]       VARCHAR(255) NOT NULL,
    [IssueId]       INT FOREIGN KEY REFERENCES [Issues](
    [Id]),
    [RepositoryId]  INT FOREIGN KEY REFERENCES [Repositories](
    [Id]) NOT NULL,
    [ContributorId] INT FOREIGN KEY REFERENCES [Users](
    [Id]) NOT NULL
)

CREATE TABLE [Files]
(
    [Id]       INT IDENTITY PRIMARY KEY,
    [Name]     VARCHAR(100) NOT NULL,
    [Size]     DECIMAL(18, 2) NOT NULL,
    [ParentId] INT FOREIGN KEY REFERENCES [Files](
    [Id]),
    [CommitId] INT FOREIGN KEY REFERENCES [Commits](
    [Id]) NOT NULL
)

/*
Section 2. DML (10 pts)
Before you start, you must import “DataSet-Bitbucket.sql”.
If you have created the structure correctly, 
the data should be successfully inserted without any errors.
*/

-- 2.Insert ------------------------------------------------------------------

INSERT INTO [Files]
(
    [Files].[Name],
    [Files].[Size],
    [Files].[ParentId],
    [Files].[CommitId]
)
VALUES
    (
    'Trade.idk', 2598.0, 1, 1
    ),
    (
    'menu.net', 9238.31, 2, 2
    ),
    (
    'Administrate.soshy', 1246.93, 3, 3
    ),
    (
    'Controller.php', 7353.15, 4, 4
    ),
    (
    'Find.java', 9957.86, 5, 5
    ),
    (
    'Controller.json', 14034.87, 3, 6
    ),
    (
    'Operate.xix', 7662.92, 7, 7
    )

INSERT INTO [Issues]
(
    [Issues].[Title],
    [Issues].[IssueStatus],
    [Issues].[RepositoryId],
    [Issues].[AssigneeId]
)
VALUES
    (
    'Critical Problem with HomeController.cs file', 'open', 1, 4
    ),
    (
    'Typo fix in Judge.html', 'open', 4, 3
    ),
    (
    'Implement documentation for UsersService.cs', 'open', 8, 2
    ),
    (
    'Unreachable code in Index.cs', 'open', 9, 8
    )

-- 3.Update ------------------------------------------------------------------

UPDATE [Issues]
  SET
	 [Issues].[IssueStatus] = 'closed'
WHERE
    [Issues].[AssigneeId] = 6

-- 4.Delete -------------------------------------------------------------------

DECLARE @repoId INT=
(
    SELECT
	   [Repositories].[Id]
    FROM  [Repositories]
    WHERE [Repositories].[Name] = 'Softuni-Teamwork'
)

DELETE FROM [RepositoriesContributors]
WHERE
    [RepositoriesContributors].[RepositoryId] = @repoId

DELETE FROM [Issues]
WHERE
    [Issues].[RepositoryId] = @repoId

DELETE FROM [Files]
WHERE
    [Files].[CommitId] IN
(
    SELECT
	   [Commits].[Id]
    FROM  [Commits]
    WHERE [Commits].[RepositoryId] = @repoId
)

DELETE FROM [Commits]
WHERE
    [Commits].[RepositoryId] = @repoId

DELETE FROM [Repositories]
WHERE
    [Repositories].[Id] = @repoId
GO
/*
Section 3. Querying (40 pts)
You need to start with a fresh dataset,
so recreate your DB and import the sample data again (DataSet-Bitbucket.sql).
*/

-- 5.Commits -----------------------------------------------------------------

SELECT
    [Commits].[Id],
    [Commits].[Message],
    [Commits].[RepositoryId],
    [Commits].[ContributorId]
FROM [Commits]
ORDER BY
    [Commits].[Id] ASC,
    [Commits].[Message] ASC,
    [Commits].[RepositoryId] ASC,
    [Commits].[ContributorId] ASC

-- 6.Heavy HTML ---------------------------------------------------------------

SELECT
    [Files].[Id],
    [Files].[Name],
    [Files].[Size]
FROM  [Files]
WHERE [Files].[Size] > 1000.0
	 AND [Files].[Name] LIKE '%html%'
ORDER BY
    [Files].[Size] DESC,
    [Files].[Id] ASC,
    [Files].[Name] ASC

-- 7.Issues and Users ----------------------------------------------------------

SELECT
    [Issues].[Id],
    [Users].[Username] + ' : ' + [Issues].[Title] AS [IssueAssignee]
FROM [Issues]
JOIN [Users]
	ON [Issues].[AssigneeId] = [Users].[Id]
ORDER BY
    [Issues].[Id] DESC,
    [IssueAssignee] ASC

-- 8.Non-Directory Files -------------------------------------------------------

SELECT
    [F].[Id],
    [F].[Name],
    concat([F].[Size], 'KB') AS [Size]
FROM [Files] AS [F]
WHERE
(
    SELECT
	   COUNT(*)
    FROM  [Files] AS [Sub_F]
    WHERE [F].[Id] = [Sub_F].[ParentId]
) = 0
ORDER BY
    [F].[Id] ASC,
    [F].[Name] ASC,
    [Size] DESC
-- OR

SELECT
    [F].[Id],
    [F].[Name],
    CONCAT([F].[Size], 'KB') AS [Size]
FROM  [Files] AS [F]
LEFT JOIN [Files] AS [Self_F]
	 ON [F].[Id] = [Self_F].[ParentId]
WHERE [Self_F].[ParentId] IS NULL
ORDER BY
    [F].[Id] ASC,
    [F].[Name] ASC,
    [F].[Size] DESC

-- 9.Most Contributed Repositories ------------------------------------------------
--logical solution

SELECT TOP 5
    [R].[Id],
    [R].[Name],
    COUNT([C].[Id]) AS [Commits]
FROM [Repositories] AS [R]
JOIN [Commits] AS [C]
	ON [R].[Id] = [C].[RepositoryId]
GROUP BY
    [R].[Id],
    [R].[Name]
ORDER BY
    [Commits] DESC,
    [R].[Id] ASC,
    [R].[Name] ASC
--required solution???

SELECT TOP 5
    [R].[Id],
    [R].[Name],
    COUNT([RC].[ContributorId]) AS [Commits]
FROM [Repositories] AS [R]
JOIN [Commits] AS [C]
	ON [C].[RepositoryId] = [R].[Id]
JOIN [RepositoriesContributors] AS [RC]
	ON [RC].[RepositoryId] = [R].[Id]
GROUP BY
    [R].[Id],
    [R].[Name]
ORDER BY
    [Commits] DESC,
    [R].[Id],
    [R].[Name]

-- 10.User and Files ---------------------------------------------------------

SELECT
    [U].[Username],
    AVG([F].[Size]) AS [Size]
FROM [Users] AS [U]
JOIN [Commits] AS [C]
	ON [U].[Id] = [C].[ContributorId]
JOIN [Files] AS [F]
	ON [C].[Id] = [F].[CommitId]
GROUP BY
    [U].[Username]
ORDER BY
    [Size] DESC,
    [U].[Username] ASC

/*
Section 4. Programmability (20 pts)
*/

-- 11. User Total Commits -----------------------------------------------

CREATE FUNCTION [udf_UserTotalCommits]
(
    @Username VARCHAR(MAX)
)
RETURNS INT
AS
    BEGIN
	   RETURN
	   (
		  SELECT
			 COUNT(*)
		  FROM  [Commits]
		  JOIN [Users]
			   ON [Commits].[ContributorId] = [Users].[Id]
		  WHERE [Users].[Username] = @Username
	   )
    END

-- 12. Find by Extensions --------------------------------------------------

CREATE OR ALTER PROC [usp_FindByExtension]
    @Extension VARCHAR(MAX)
AS
BEGIN
    SELECT
	   [Files].[Id],
	   [Files].[Name],
	   CONCAT([Files].[Size], 'KB') AS [Size]
    FROM  [Files]
    WHERE [Files].[Name] LIKE('%.' + @Extension)
END