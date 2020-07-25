

USE [master]
GO

DROP DATABASE IF EXISTS
    [School]
GO

CREATE DATABASE [School]
GO

USE [School]
GO

-- 1.Database Design -----------------------------------------------

CREATE TABLE [Students]
(
    [Id]         INT IDENTITY(1, 1) PRIMARY KEY,
    [FirstName]  NVARCHAR(30) NOT NULL,
    [MiddleName] NVARCHAR(25),
    [LastName]   NVARCHAR(30) NOT NULL,
    [Age]        TINYINT CHECK([Age] BETWEEN 5 AND 100),
    [Address]    NVARCHAR(50),
    [Phone]      NCHAR(10)
)

CREATE TABLE [Subjects]
(
    [Id]      INT IDENTITY(1, 1) PRIMARY KEY,
    [Name]    NVARCHAR(20) NOT NULL,
    [Lessons] INT CHECK([Lessons] > 0) NOT NULL
)

CREATE TABLE [StudentsSubjects]
(
    [Id]        INT IDENTITY(1, 1) PRIMARY KEY,
    [StudentId] INT FOREIGN KEY REFERENCES [Students](
    [Id]) NOT NULL,
    [SubjectId] INT FOREIGN KEY REFERENCES [Subjects](
    [Id]) NOT NULL,
    [Grade]     DECIMAL(18, 2) CHECK([Grade] BETWEEN 2 AND 6) NOT NULL
)

CREATE TABLE [Exams]
(
    [Id]        INT IDENTITY(1, 1) PRIMARY KEY,
    Date        DATETIME,
    [SubjectId] INT FOREIGN KEY REFERENCES [Subjects](
    [Id]) NOT NULL
)

CREATE TABLE [StudentsExams]
(
    [StudentId] INT FOREIGN KEY REFERENCES [Students](
    [id]) NOT NULL,
    [ExamId]    INT FOREIGN KEY REFERENCES [Students](
    [Id]) NOT NULL,
    [Grade]     DECIMAL(18, 2) CHECK([Grade] BETWEEN 2 AND 6) NOT NULL,
    CONSTRAINT [PK_StudentsExams] PRIMARY KEY([StudentId], [ExamId])
)

CREATE TABLE [Teachers]
(
    [Id]        INT IDENTITY(1, 1) PRIMARY KEY,
    [FirstName] NVARCHAR(20) NOT NULL,
    [LastName]  NVARCHAR(20) NOT NULL,
    [Address]   NVARCHAR(20) NOT NULL,
    [Phone]     NCHAR(10),
    [SubjectId] INT FOREIGN KEY REFERENCES [Subjects](
    [Id]) NOT NULL
)

CREATE TABLE [StudentsTeachers]
(
    [StudentId] INT FOREIGN KEY REFERENCES [Students](
    [Id]) NOT NULL,
    [TeacherId] INT FOREIGN KEY REFERENCES [Teachers](
    [Id]) NOT NULL,
    CONSTRAINT [PK_StudentsTeachers] PRIMARY KEY([StudentId], [TeacherId])
)

/*
Section 2. DML (10 pts)
Before you start, you must import “DataSet-School.sql”. 
If you have created the structure correctly, the data should be successfully inserted without any errors.
*/

-- 2.Insert ------------------------------------------------------------

INSERT INTO [Teachers]
(
    [Teachers].[FirstName],
    [Teachers].[LastName],
    [Teachers].[Address],
    [Teachers].[Phone],
    [Teachers].[SubjectId]
)
VALUES
    (
    'Ruthanne', 'Bamb', '84948 Mesta Junction', '3105500146', 6
    ),
    (
    'Gerrard', 'Lowin', '370 Talisman Plaza', '3324874824', 2
    ),
    (
    'Merrile', 'Lambdin', '81 Dahle Plaza', '4373065154', 5
    ),
    (
    'Bert', 'Ivie', '2 Gateway Circle', '4409584510', 6
    )

INSERT INTO [Subjects]
(
    [Subjects].[Name],
    [Subjects].[Lessons]
)
VALUES
    (
    'Geometry', 12
    ),
    (
    'Health', 10
    ),
    (
    'Drama', 7
    ),
    (
    'Sports', 9
    )

-- 3.Update --------------------------------------------------

UPDATE [StudentsSubjects]
  SET
	 [StudentsSubjects].[Grade] = 6
WHERE
    [StudentsSubjects].[SubjectId] IN(1, 2)
AND [StudentsSubjects].[Grade] >= 5.50

-- 4. Delete ---------------------------------------------------

DELETE FROM [StudentsTeachers]
WHERE
    [StudentsTeachers].[TeacherId] IN
(
    SELECT
	   [Teachers].[Id]
    FROM  [Teachers]
    WHERE [Teachers].[Phone] LIKE '%72%'
)

DELETE FROM [Teachers]
WHERE
    [Teachers].[Phone] LIKE '%72%'

/*
Section 3. Querying (40 pts)
You need to start with a fresh dataset, 
so recreate your DB and import the sample data again (DataSet-School.sql).
*/

-- 5.Teen Students ---------------------------------------------

SELECT
    [Students].[FirstName],
    [Students].[LastName],
    [Students].[Age]
FROM  [Students]
WHERE [Students].[Age] >= 12
ORDER BY
    [Students].[FirstName] ASC,
    [Students].[LastName] ASC

-- 6.Cool Addresses ----------------------------------------------

SELECT
    CASE
	   WHEN [Students].[MiddleName] IS NULL
		  THEN [Students].[FirstName] + '  ' + [Students].[LastName]
	   ELSE [Students].[FirstName] + ' ' + [Students].[MiddleName] + ' ' + [Students].[LastName]
    END AS [Full Name],
    [Students].[Address]
FROM  [Students]
WHERE LOWER([Students].[Address]) LIKE '%road%'
ORDER BY
    [Students].[FirstName] ASC,
    [Students].[LastName] ASC,
    [Students].[Address] ASC

-- 7.42 Phones ---------------------------------------------------

SELECT
    [Students].[FirstName],
    [Students].[Address],
    [Students].[Phone]
FROM  [Students]
WHERE [Students].[MiddleName] IS NOT NULL
	 AND [Students].[Phone] LIKE '42%'
ORDER BY
    [Students].[FirstName] ASC

-- 8.Students Teachers ----------------------------------------------

SELECT
    [Students].[FirstName],
    [Students].[LastName],
    COUNT([StudentsTeachers].[TeacherId]) AS [TeachersCount]
FROM [Students]
INNER JOIN [StudentsTeachers]
	ON [Students].[Id] = [StudentsTeachers].[StudentId]
GROUP BY
    [Students].[FirstName],
    [Students].[LastName]

-- 9.Subjects with Students ----------------------------------------------

SELECT
    [Teachers].[FirstName] + ' ' + [Teachers].[LastName] AS [Full Name],
    CONCAT([Subjects].[Name], '-', [Subjects].[Lessons]) AS [Subjects],
    COUNT([StudentsTeachers].[StudentId]) AS                [Students]
FROM [Teachers]
INNER JOIN [Subjects]
	ON [Teachers].[SubjectId] = [Subjects].[Id]
INNER JOIN [StudentsTeachers]
	ON [Teachers].[Id] = [StudentsTeachers].[TeacherId]
GROUP BY
    [Teachers].[FirstName],
    [Teachers].[LastName],
    [Subjects].[Name],
    [Subjects].[Lessons]
ORDER BY
    [Students] DESC,
    [Full Name] ASC,
    [Subjects] ASC

-- 10.Students to Go ------------------------------------------------

SELECT
    [Students].[FirstName] + ' ' + [Students].[LastName] AS [Full Name]
FROM  [Students]
LEFT /*or FULL */JOIN [StudentsExams]
	 ON [Students].[Id] = [StudentsExams].[StudentId]
WHERE [StudentsExams].[Grade] IS NULL
ORDER BY
    [Full Name] ASC

-- 11.Busiest Teachers ------------------------------------------------

SELECT TOP 10
    [Teachers].[FirstName],
    [Teachers].[LastName],
    COUNT([StudentsTeachers].[StudentId]) AS [Students]
FROM [Teachers]
INNER JOIN [Subjects]
	ON [Teachers].[SubjectId] = [Subjects].[Id]
INNER JOIN [StudentsTeachers]
	ON [Teachers].[Id] = [StudentsTeachers].[TeacherId]
GROUP BY
    [Teachers].[FirstName],
    [Teachers].[LastName],
    [Subjects].[Name],
    [Subjects].[Lessons]
ORDER BY
    [Students] DESC,
    [Teachers].[FirstName],
    [Teachers].[LastName]

-- 12.Top Students ------------------------------------------------

SELECT TOP 10
    [Students].[FirstName] AS                                                               [First Name],
    [Students].[LastName] AS                                                                [Last Name],
    CAST(ROUND(1.0 * SUM([StudentsExams].[Grade]) / COUNT([Grade]), 2) AS DECIMAL(3, 2)) AS [Grade]
FROM [Students]
INNER JOIN [StudentsExams]
	ON [Students].[Id] = [StudentsExams].[StudentId]
GROUP BY
    [Students].[FirstName],
    [Students].[LastName]
ORDER BY
    [Grade] DESC,
    [First Name] ASC,
    [Last Name] ASC

-- 13.Second Highest Grade --------------------------------------------

WITH cte_13
	AS (SELECT
		   [Students].[FirstName],
		   [Students].[LastName],
		   ROW_NUMBER() OVER(PARTITION BY [Students].[FirstName],
								    [Students].[LastName]
		   ORDER BY
		   [StudentsSubjects].[Grade] DESC) AS [GradeRank],
		   [StudentsSubjects].[Grade]
	    FROM [Students]
	    INNER JOIN [StudentsSubjects]
		    ON [Students].[Id] = [StudentsSubjects].[StudentId])
	SELECT
	    [cte_13].[FirstName],
	    [cte_13].[LastName],
	    [cte_13].[Grade]
	FROM  [cte_13]
	WHERE [cte_13].[GradeRank] = 2

-- 14.Not So In The Studying ----------------------------------------

SELECT
    CASE
	   WHEN [Students].[MiddleName] IS NULL
		  THEN [Students].[FirstName] + ' ' + [Students].[LastName]
	   ELSE [Students].[FirstName] + ' ' + [Students].[MiddleName] + ' ' + [Students].[LastName]
    END AS [Full Name]
FROM  [Students]
LEFT JOIN [StudentsSubjects]
	 ON [Students].[Id] = [StudentsSubjects].[StudentId]
WHERE [StudentsSubjects].[SubjectId] IS NULL
ORDER BY
    [Full Name] ASC

-- 15.Top Student per Teacher ----------------------------------------

WITH [cte_15_1]
	AS (SELECT
		   [Teachers].[FirstName] + ' ' + [Teachers].[LastName] AS [Teacher Full Name],
		   [Students].[FirstName] + ' ' + [Students].[LastName] AS [Student Full Name],
		   [StudentsSubjects].[Grade] AS                           [Average Grade],
		   AVG([Subjects].[Name]) AS                               [Subject Name]
	    FROM [Teachers]
	    JOIN [StudentsTeachers]
		    ON [StudentsTeachers].[TeacherId] = [Teachers].[Id]
	    JOIN [Students]
		    ON [Students].[Id] = [StudentsTeachers].[StudentId]
	    JOIN [StudentsSubjects]
		    ON [StudentsSubjects].[StudentId] = [Students].[Id]
	    JOIN [Subjects]
		    ON [Subjects].[Id] = [StudentsSubjects].[SubjectId]
			  AND [Subjects].[Id] = [Teachers].[SubjectId]
	    GROUP BY
		   [Teachers].[FirstName],
		   [Teachers].[LastName],
		   [Students].[FirstName],
		   [Students].[LastName],
		   [Subjects].[Name]),
	[cte_15_2]
	AS (SELECT
		   [cte_15_1].[Teacher Full Name],
		   [cte_15_1].[Subject Name],
		   [cte_15_1].[Student Full Name],
		   [cte_15_1].[Average Grade],
		   ROW_NUMBER() OVER(PARTITION BY [cte_15_1].[Teacher Full Name]
		   ORDER BY
		   [cte_15_1].[Average Grade] DESC) AS [GradeRow]
	    FROM [cte_15_1]
	    GROUP BY
		   [cte_15_1].[Teacher Full Name],
		   [cte_15_1].[Subject Name],
		   [cte_15_1].[Student Full Name],
		   [cte_15_1].[Average Grade])
	SELECT
	    [cte_15_2].[Teacher Full Name],
	    [cte_15_2].[Subject Name],
	    [cte_15_2].[Student Full Name],
	    CAST([cte_15_2].[Average Grade] AS DECIMAL(3, 2)) AS [Grade]
	FROM  [cte_15_2]
	WHERE [cte_15_2].[GradeRow] = 1
	ORDER BY
	    [Subject Name] ASC,
	    [Teacher Full Name] ASC,
	    [Grade] DESC

-- 16.Average Grade per Subject ----------------------------------------------

SELECT
    [Subjects].[Name],
    AVG([StudentsSubjects].[Grade]) AS [AverageGrade]
FROM [Subjects]
JOIN [StudentsSubjects]
	ON [Subjects].[Id] = [StudentsSubjects].[SubjectId]
GROUP BY
    [Subjects].[Name],
    [Subjects].[Id]
ORDER BY
    [Subjects].[Id] ASC

-- 17.Exams Information -------------------------------------------------------

WITH cte_17_1
	AS (SELECT
		   CASE
			  WHEN DATEPART(month, [E].[Date]) BETWEEN 1 AND 3
				 THEN 'Q1'
			  WHEN DATEPART(month, [E].[Date]) BETWEEN 4 AND 6
				 THEN 'Q2'
			  WHEN DATEPART(month, [E].[Date]) BETWEEN 7 AND 9
				 THEN 'Q3'
			  WHEN DATEPART(month, [E].[Date]) BETWEEN 10 AND 12
				 THEN 'Q4'
			  ELSE 'TBA'
		   END AS                     [Quarter],
		   [E].[Date],
		   [S].[Name],
		   COUNT([SE].[StudentId]) AS [StudentsCount]
	    FROM  [Exams] AS [E]
	    JOIN [StudentsExams] AS [SE]
			ON [E].[Id] = [SE].[ExamId]
	    JOIN [Subjects] AS [S]
			ON [E].[SubjectId] = [S].[Id]
	    WHERE [SE].[Grade] >= 4.00
	    GROUP BY
		   [S].[Name],
		   [E].[Date])
	SELECT
	    [cte_17_1].[Quarter],
	    [cte_17_1].[Name],
	    SUM([cte_17_1].[StudentsCount]) AS [StudentsCount]
	FROM [cte_17_1]
	GROUP BY
	    [cte_17_1].[Quarter],
	    [cte_17_1].[Name]
	ORDER BY
	    [cte_17_1].[Quarter] ASC

/*
Section 4. Programmability (20 pts)
*/

CREATE FUNCTION [udf_ExamGradesToUpdate]
(
    @studentId INT,
    @grade     DECIMAL(3, 2)
)
RETURNS VARCHAR(60)
AS
    BEGIN
	   IF @grade > 6.00
		  RETURN 'Grade cannot be above 6.00!'

	   DECLARE @studentCheck INT=
	   (
		  SELECT
			 COUNT(*)
		  FROM  [Students]
		  WHERE [Students].[Id] = @studentId
	   )

	   IF @studentCheck != 1
		  RETURN 'The student with provided id does not exist in the school!'

	   DECLARE @gradesToUpdate INT=
	   (
		  SELECT
			 COUNT(*)
		  FROM  [Exams] AS [E]
		  JOIN [StudentsExams] AS [SE]
			   ON [E].[Id] = [SE].[ExamId]
		  WHERE [SE].[StudentId] = @studentId
			   AND [SE].[Grade] BETWEEN @grade AND @grade + 0.5
	   )

	   DECLARE @studentName VARCHAR(60)=
	   (
		  SELECT
			 [Students].[FirstName]
		  FROM  [Students]
		  WHERE [Students].[Id] = @studentId
	   )

	   RETURN CONCAT('You have to update ', @gradesToUpdate, ' grades for the student ', @studentName)
    END

SELECT
    [dbo].[udf_ExamGradesToUpdate](12, 6.20)

SELECT
    [dbo].[udf_ExamGradesToUpdate](12, 5.50)

SELECT
    [dbo].[udf_ExamGradesToUpdate](121, 5.50)

-- 19.Exclude from school --------------------------------------------------------

CREATE OR ALTER PROC [usp_ExcludeFromSchool]
(
    @StudentId INT
)
AS
BEGIN
    DECLARE @studentCheck INT=
    (
	   SELECT
		  COUNT(*)
	   FROM  [Students]
	   WHERE [Students].[Id] = @StudentId
    )

    IF(@studentCheck = 0)
    BEGIN
	   RAISERROR('This school has no student with the provided id!', 16, 1)
	   RETURN
    END

    DELETE FROM [StudentsTeachers]
    WHERE
	   [StudentId] = @StudentId

    DELETE FROM [StudentsSubjects]
    WHERE
	   [StudentId] = @StudentId

    DELETE FROM [StudentsExams]
    WHERE
	   [StudentId] = @StudentId

    DELETE FROM [Students]
    WHERE
	   [Id] = @StudentId
END
GO
-- 20.Deleted Student ---------------------------------------------------------

CREATE TABLE [ExcludedStudents]
(
    [StudentId]   INT,
    [StudentName] VARCHAR(60)
)

GO

CREATE TRIGGER [tr_ExcludeStudent] ON [Students]
FOR DELETE
AS
    BEGIN
	   INSERT INTO [ExcludedStudents]
	   (
		  [ExcludedStudents].[StudentId],
		  [ExcludedStudents].[StudentName]
	   )
			SELECT
			    [DELETED].[Id],
			    [DELETED].[FirstName] + ' ' + [DELETED].[LastName]
			FROM [DELETED]
    END