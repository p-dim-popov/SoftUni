USE [SoftUni]
GO

-- Problem 1. One-To-One Relationships
DROP TABLE IF EXISTS [Persons]
DROP TABLE IF EXISTS [Passports]

CREATE TABLE [Persons]
(
    [PersonID]   INT            NOT NULL,
    [FirstName]  NVARCHAR(32)   NOT NULL,
    [Salary]     DECIMAL(18, 2) NOT NULL,
    [PassportID] INT            NOT NULL
)

CREATE TABLE [Passports]
(
    [PassportID]     INT          NOT NULL,
    [PassportNumber] NVARCHAR(10) NOT NULL
)
-- go

INSERT INTO
    [Persons]
    ([PersonID], [FirstName], [Salary], [PassportID])
VALUES (1, 'Roberto', 43300.00, 102),
       (2, 'Tom', 56100.00, 103),
       (3, 'Yana', 60200.00, 101)
-- go

INSERT INTO
    [Passports]
    ([PassportID], [PassportNumber])
VALUES (101, 'N34FG21B'),
       (102, 'K65LO4R7'),
       (103, 'ZE657QP2')
-- go

ALTER TABLE [Persons]
    ADD CONSTRAINT PK_Persons_PersonID
        PRIMARY KEY ([PersonID])

ALTER TABLE [Passports]
    ADD CONSTRAINT [PK_Passports_PassportID]
        PRIMARY KEY ([PassportID])

ALTER TABLE [Persons]
    ADD CONSTRAINT [FK_Persons_PassportID]
        FOREIGN KEY ([PassportID])
            REFERENCES [Passports] ([PassportID])

-- Problem 2. One-To-Many Relationship
DROP TABLE IF EXISTS [Models]
DROP TABLE IF EXISTS [Manufacturers]

CREATE TABLE [Models]
(
    [ModelID]        INT          NOT NULL,
    [Name]           NVARCHAR(32) NOT NULL,
    [ManufacturerID] INT          NOT NULL
)

CREATE TABLE [Manufacturers]
(
    [ManufacturerID] INT          NOT NULL,
    [Name]           NVARCHAR(32) NOT NULL,
    [EstablishedOn]  DATE         NOT NULL
)

INSERT INTO
    [Models]
    ([ModelID], [Name], [ManufacturerID])
VALUES (101, 'X1', 1),
       (102, 'i6', 1),
       (103, 'Model S', 2),
       (104, 'Model X', 2),
       (105, 'Model 3', 2),
       (106, 'Nova', 3)

INSERT INTO
    [Manufacturers]
    ([ManufacturerID], [Name], [EstablishedOn])
VALUES (1, 'BMW', '19160307'),
       (2, 'Tesla', '20030101'),
       (3, 'Lada', '19660501')

ALTER TABLE [Models]
    ADD CONSTRAINT [PK_Models_ModelID]
        PRIMARY KEY ([ModelID])

ALTER TABLE [Manufacturers]
    ADD CONSTRAINT [PK_Manufacturers_ManufacturerID]
        PRIMARY KEY ([ManufacturerID])

ALTER TABLE [Models]
    ADD CONSTRAINT [FK_Models_ManufacturerID]
        FOREIGN KEY ([ManufacturerID])
            REFERENCES [Manufacturers] ([ManufacturerID])

-- Problem 3. Many-To-Many Relationship
DROP TABLE IF EXISTS [StudentsExams]
DROP TABLE IF EXISTS [Exams]
DROP TABLE IF EXISTS [Students]

CREATE TABLE [Students]
(
    [StudentID] INT          NOT NULL,
    [Name]      NVARCHAR(32) NOT NULL
)

CREATE TABLE [Exams]
(
    [ExamID] INT          NOT NULL,
    [Name]   NVARCHAR(32) NOT NULL
)

CREATE TABLE [StudentsExams]
(
    [StudentID] INT NOT NULL,
    [ExamID]    INT NOT NULL
)

INSERT INTO
    [Students]
    (StudentID, Name)
VALUES (1, 'Mila'),
       (2, 'Toni'),
       (3, 'Ron')

INSERT INTO
    [Exams]
    (ExamID, Name)
VALUES (101, 'SpringMVC'),
       (102, 'Neo4j'),
       (103, 'Oracle 11g')

INSERT INTO
    [StudentsExams]
    (StudentID, ExamID)
VALUES (1, 101),
       (1, 102),
       (2, 101),
       (3, 103),
       (2, 102),
       (2, 103)

ALTER TABLE [Students]
    ADD CONSTRAINT [PK_Students_StudentID]
        PRIMARY KEY (StudentID)

ALTER TABLE [Exams]
    ADD CONSTRAINT [PK_Exams_ExamID]
        PRIMARY KEY (ExamID)

ALTER TABLE [StudentsExams]
    ADD CONSTRAINT [FK_StudentsExams_StudentID]
        FOREIGN KEY (StudentID)
            REFERENCES [Students] (StudentID)

ALTER TABLE [StudentsExams]
    ADD CONSTRAINT [FK_StudentsExams_ExamID]
        FOREIGN KEY (ExamID)
            REFERENCES [Exams] (ExamID)

ALTER TABLE [StudentsExams]
    ADD CONSTRAINT [PK_StudentsExams_StudentID_ExamID]
        PRIMARY KEY (StudentID, ExamID)

-- Problem 4. Self-Referencing
CREATE TABLE [Teachers]
(
    [TeacherID] INT          NOT NULL,
    [Name]      NVARCHAR(32) NOT NULL,
    [ManagerID] INT
)

INSERT INTO
    [Teachers]
    (TeacherID, Name, ManagerID)
VALUES (101, 'John', NULL),
       (102, 'Maya', 106),
       (103, 'Silvia', 106),
       (104, 'Ted', 105),
       (105, 'Mark', 101),
       (106, 'Greta', 101)

ALTER TABLE [Teachers]
    ADD CONSTRAINT [PK_Teachers_TeacherID]
        PRIMARY KEY (TeacherID)

ALTER TABLE [Teachers]
    ADD CONSTRAINT [FK_Teachers_ManagerID]
        FOREIGN KEY (ManagerID)
            REFERENCES [Teachers] (TeacherID)

-- Problem 5. Online Store Database
USE [master]
GO

DROP DATABASE IF EXISTS [OnlineStore]
GO

CREATE DATABASE [OnlineStore]
GO

USE [OnlineStore]
GO

CREATE TABLE [Cities]
(
    [CityID] INT         NOT NULL,
    [Name]   VARCHAR(50) NOT NULL
)

CREATE TABLE [Customers]
(
    [CustomerID] INT         NOT NULL,
    [Name]       VARCHAR(50) NOT NULL,
    [Birthday]   DATE        NOT NULL,
    [CityID]     INT         NOT NULL
)

CREATE TABLE [Orders]
(
    [OrderID]    INT NOT NULL,
    [CustomerID] INT NOT NULL
)

CREATE TABLE [ItemTypes]
(
    [ItemTypeID] INT         NOT NULL,
    [Name]       VARCHAR(50) NOT NULL
)

CREATE TABLE [Items]
(
    [ItemID]     INT         NOT NULL,
    [Name]       VARCHAR(50) NOT NULL,
    [ItemTypeID] INT         NOT NULL
)

CREATE TABLE [OrderItems]
(
    [OrderID] INT NOT NULL,
    [ItemID]  INT NOT NULL
)

ALTER TABLE [Cities]
    ADD CONSTRAINT [PK_Cities_CityID]
        PRIMARY KEY (CityID)

ALTER TABLE [Customers]
    ADD
        CONSTRAINT [PK_Customers_CustomerID]
            PRIMARY KEY (CustomerID),
        CONSTRAINT [FK_Customers_CityID]
            FOREIGN KEY (CityID)
                REFERENCES [Cities] (CityID)

ALTER TABLE [Orders]
    ADD
        CONSTRAINT [PK_Orders_OrderID]
            PRIMARY KEY (OrderID),
        CONSTRAINT [FK_Orders_CustomerID]
            FOREIGN KEY (CustomerID)
                REFERENCES [Customers] (CustomerID)

ALTER TABLE [ItemTypes]
    ADD CONSTRAINT [PK_ItemTypes_ItemTypeID]
        PRIMARY KEY (ItemTypeID)

ALTER TABLE [Items]
    ADD
        CONSTRAINT [PK_Items_ItemID]
            PRIMARY KEY (ItemID),
        CONSTRAINT [FK_Items_ItemTypeID]
            FOREIGN KEY (ItemTypeID)
                REFERENCES [ItemTypes] (ItemTypeID)

ALTER TABLE [OrderItems]
    ADD
        CONSTRAINT [FK_OrderItems_ItemID]
            FOREIGN KEY (ItemID)
                REFERENCES [Items] (ItemID),
        CONSTRAINT [FK_OrderItems_OrderID]
            FOREIGN KEY (OrderID)
                REFERENCES [Orders] (OrderID),
        CONSTRAINT [PK_OrderItems_ItemID_OrderID]
            PRIMARY KEY (ItemID, OrderID)

-- Problem 6. University Database
USE [master]
GO

DROP DATABASE IF EXISTS [University]
GO

CREATE DATABASE [University]
GO

USE [University]
GO

CREATE TABLE [Majors]
(
    [MajorID] INT         NOT NULL PRIMARY KEY,
    [Name]    VARCHAR(50) NOT NULL
)


CREATE TABLE [Students]
(
    [StudentID]     INT         NOT NULL PRIMARY KEY,
    [StudentNumber] VARCHAR(30) NOT NULL,
    [StudentName]   VARCHAR(50) NOT NULL,
    [MajorID]       INT         NOT NULL FOREIGN KEY REFERENCES [Majors] (MajorID)
)

CREATE TABLE [Subjects]
(
    [SubjectID]   INT         NOT NULL PRIMARY KEY,
    [SubjectName] VARCHAR(50) NOT NULL
)

CREATE TABLE [Agenda]
(
    [StudentID] INT NOT NULL FOREIGN KEY REFERENCES [Students] (StudentID),
    [SubjectID] INT NOT NULL FOREIGN KEY REFERENCES [Subjects] (SubjectID),
    CONSTRAINT [PK_Agenda_StudentID_SubjectID] PRIMARY KEY (StudentID, SubjectID)
)

CREATE TABLE [Payments]
(
    [PaymentID]     INT     NOT NULL PRIMARY KEY,
    [PaymentDate]   DATE    NOT NULL,
    [PaymentAmount] DECIMAL NOT NULL,
    [StudentID]     INT     NOT NULL FOREIGN KEY REFERENCES [Students] (StudentID)
)

-- Problem 9. *Peaks in Rila
USE [Geography]
GO

SELECT
       [MountainRange],
       [PeakName],
       [Elevation]
FROM [Peaks]
JOIN [Mountains]
ON [Peaks].[MountainId] = [Mountains].[Id]
WHERE [MountainRange] = 'Rila'
ORDER BY [Elevation] DESC