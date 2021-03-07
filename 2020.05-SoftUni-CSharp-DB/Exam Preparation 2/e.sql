

USE master
GO

CREATE DATABASE [School]
GO

USE School
GO

--1.Create-------------------------------------

CREATE TABLE [Students]
(
    [Id]         INT IDENTITY PRIMARY KEY,
    [FirstName]  NVARCHAR(30) NOT NULL,
    [MiddleName] NVARCHAR(25),
    [LastName]   NVARCHAR(30) NOT NULL,
    [Age]        INT CHECK([Age] BETWEEN 5 AND 100),
    [Address]    NVARCHAR(50),
    [Phone]      NCHAR(10)
)

CREATE TABLE [Subjects]
(
    [Id]      INT IDENTITY PRIMARY KEY,
    [Name]    NVARCHAR(20) NOT NULL,
    [Lessons] INT CHECK([Lessons] > 0) NOT NULL
)

CREATE TABLE [StudentsSubjects]
(
    [Id]        INT IDENTITY PRIMARY KEY,
    [StudentId] INT FOREIGN KEY REFERENCES [Students](
    [Id]) NOT NULL,
    [SubjectId] INT FOREIGN KEY REFERENCES [Subjects](
    [Id]) NOT NULL,
    [Grade]     DECIMAL(3, 2) CHECK([Grade] BETWEEN 2 AND 6) NOT NULL
)

CREATE TABLE [Exams]
(
    [Id]        INT IDENTITY PRIMARY KEY,
    Date        DATETIME,
    [SubjectId] INT FOREIGN KEY REFERENCES [Subjects](
    [Id]) NOT NULL
)

CREATE TABLE [StudentsExams]
(
    [StudentId] [INT] FOREIGN KEY REFERENCES [Students](
    [Id]) NOT NULL,
    [ExamId]    INT FOREIGN KEY REFERENCES [Exams](
    [Id]) NOT NULL,
    [Grade]     DECIMAL(3, 2) CHECK([Grade] BETWEEN 2 AND 6) NOT NULL,
    CONSTRAINT [PK_StudentExams] PRIMARY KEY([StudentId], [ExamId])
)

CREATE TABLE [Teachers]
(
    [Id]        INT IDENTITY PRIMARY KEY,
    [FirstName] NVARCHAR(20) NOT NULL,
    [LastName]  NVARCHAR(20) NOT NULL,
    [Address]   NVARCHAR(20) NOT NULL,
    [Phone]     CHAR(20),
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

--2.Inserts-------------------------------------------------------------------
--3.Update--------------------------------------------------------------------
--4.Delete--------------------------------------------------------------------
--5.Teen Students-------------------------------------------------------------

SELECT
    [Students].[FirstName],
    [Students].[LastName],
    [Students].[Age]
FROM  [Students]
WHERE [Students].[Age] >= 12
ORDER BY
    [Students].[FirstName] ASC,
    [Students].[LastName] ASC

--6.Students Teachers----------------------------------------------------------

SELECT
    [Students].[FirstName],
    [Students].[LastName],
    COUNT([StudentsTeachers].[TeacherId])
FROM [Students]
JOIN [StudentsTeachers]
	ON [Students].[Id] = [StudentsTeachers].[StudentId]
GROUP BY
    [Students].[FirstName],
    [Students].[LastName]

--7.Students to Go------------------------------------------------------------

SELECT
    [Students].[FirstName] + ' ' + [Students].[LastName] AS [FullName]
FROM  [Students]
LEFT JOIN [StudentsExams]
	 ON [Students].[Id] = [StudentsExams].[StudentId]
WHERE [StudentsExams].[ExamId] IS NULL
ORDER BY
    [FullName] ASC

--8.Top Students---------------------------------------------------------------

SELECT TOP 10
    [Students].[FirstName],
    [Students].[LastName],
    FORMAT(AVG([StudentsExams].[Grade]), 'f2') AS [Grade]
FROM [Students]
JOIN [StudentsExams]
	ON [Students].[Id] = [StudentsExams].[StudentId]
GROUP BY
    [Students].[FirstName],
    [Students].[LastName]
ORDER BY
    [Grade] DESC,
    [Students].[FirstName] ASC,
    [Students].[LastName] ASC

--9.Not So In The Studying------------------------------------------------------

SELECT
    CASE
	   WHEN [Students].[MiddleName] IS NULL
		  THEN [Students].[FirstName] + ' ' + [Students].[LastName]
	   ELSE [Students].[FirstName] + ' ' + [Students].[MiddleName] + ' ' + [Students].[LastName]
    END AS [FullName]
FROM  [Students]
LEFT JOIN [StudentsSubjects]
	 ON [Students].[Id] = [StudentsSubjects].[StudentId]
WHERE [StudentsSubjects].[SubjectId] IS NULL
ORDER BY
    [FullName]

--10.Average Grade per Subject---------------------------------------------------

SELECT
    [Subjects].[Name],
    AVG([StudentsSubjects].[Grade]) AS [AvgGrade]
FROM [Subjects]
JOIN [StudentsSubjects]
	ON [Subjects].[Id] = [StudentsSubjects].[SubjectId]
GROUP BY
    [Subjects].[Name],
    [Subjects].[Id]
ORDER BY
    [Subjects].[Id] ASC

--11.Exam Grades----------------------------------------------------------------

CREATE FUNCTION [udf_ExamGradesToUpdate]
(
    @studId INT,
    @grade  DECIMAL(3, 2)
)
RETURNS VARCHAR(MAX)
AS
    BEGIN
	   DECLARE @studFirstName VARCHAR(MAX)=
	   (
		  SELECT
			 [Students].[FirstName]
		  FROM  [Students]
		  WHERE [Students].[Id] = @studId
	   )

	   IF @studFirstName IS NULL
		  RETURN 'The student with provided id does not exist in the school!'

	   IF @grade > 6.0
		  RETURN 'Grade cannot be above 6.00!'

	   DECLARE @gradesToUpdate INT=
	   (
		  SELECT
			 COUNT([StudentsExams].[Grade])
		  FROM  [Exams]
		  JOIN [StudentsExams]
			   ON [Exams].[Id] = [StudentsExams].[ExamId]
		  WHERE [StudentsExams].[StudentId] = @studId
			   AND [StudentsExams].[Grade] BETWEEN @grade AND @grade + 0.5
		  GROUP BY
			 [StudentsExams].[StudentId]
	   )

	   RETURN CONCAT('You have to update ', @gradesToUpdate, ' grades for the student ', @studFirstName)
    END
GO

--12.Exclude from school-------------------------------------------------------------

CREATE OR ALTER PROC [usp_ExcludeFromSchool]
    @StudentId INT
AS
BEGIN
    DECLARE @studCheck INT=
    (
	   SELECT
		  [Students].[Id]
	   FROM  [Students]
	   WHERE [Id] = @StudentId
    )

    IF @studCheck IS NULL
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