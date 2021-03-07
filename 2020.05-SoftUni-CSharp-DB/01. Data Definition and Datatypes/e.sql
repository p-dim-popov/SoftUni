-- Exercises: Data Definition and Data Types --

-- Problem 1. Create Database
DROP DATABASE IF EXISTS Minions
GO

CREATE DATABASE Minions
GO

USE Minions
GO

-- Problem 2. Create Tables
DROP TABLE IF EXISTS Towns
DROP TABLE IF EXISTS Minions

CREATE TABLE Minions
(
	Id INT NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Age INT, 
	CONSTRAINT PK_Minions_Id PRIMARY KEY (Id)
)

CREATE TABLE Towns
(
	Id INT NOT NULL,
	Name NVARCHAR(50) NOT NULL, 
	CONSTRAINT PK_Towns_Id PRIMARY KEY (Id)
)

GO

-- Problem 3. Alter Minions Table
ALTER TABLE Minions
ADD TownId INT

ALTER TABLE Minions
ADD CONSTRAINT FK_Minions_TownId
FOREIGN KEY (TownId) REFERENCES	Towns(Id)

GO 

-- Problem 4. Insert Records in Both Tables
INSERT INTO Towns (Id, Name) VALUES
	(1, 'Sofia'), 
	(2, 'Plovdiv'),
	(3, 'Varna')

INSERT INTO Minions (Id, Name, Age, TownId) VALUES
	(1, 'Kevin', 22, 1),
	(2, 'Bob', 15, 3),
	(3, 'Steward', NULL, 2)
GO

-- Problem 5. Truncate Table Minions
TRUNCATE TABLE Minions
GO

-- Problem 6. Drop All Tables
DROP TABLE Minions
DROP TABLE Towns
GO

-- Problem 7. Create Table People
CREATE TABLE People
(
	Id INT IDENTITY(1, 1),
	Name NVARCHAR(200) NOT NULL,
	Picture VARBINARY(2048),
	Height DECIMAL(10, 2) NOT NULL,
	Weight DECIMAL(10, 2) NOT NULL, 
	Gender CHAR(1) NOT NULL CHECK(Gender IN ('m', 'f')),
	Birthdate DATE,
	Biography NVARCHAR(MAX),
	CONSTRAINT PK_People_Id PRIMARY KEY (Id)
)

INSERT INTO People (Name, Picture, Height, Weight, Gender, Birthdate, Biography) VALUES
	('Joro', 11011, 1.71, 71, 'm', '19900220', 'Gamer'),
	('Jorko', NULL, 1.72, 72, 'm', '19900221', NULL),
	('Jori', 11011, 1.73, 73, 'm', '19900222', NULL),
	('Jore', NULL, 1.74, 74, 'm', '19900223', 'Not gamer'),
	('Jorj', 11011, 1.75, 75, 'f', '19900224', 'Nothing')
GO

-- Problem 8. Create Table Users
CREATE TABLE Users
(
	Id BIGINT IDENTITY(1, 1),
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(900),
	LastLoginTime TIME, 
	IsDeleted BIT,
	CONSTRAINT PK_Users_Id PRIMARY KEY (Id)
)

INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, IsDeleted) VALUES
	('joro', '123123', 11110, '13:30', 1), -- TODO TIME SET
	('jorko', '123223', NULL, NULL, NULL),
	('jori', '123163', NULL, NULL, 1),
	('jore', '123173', NULL, '13:33', 0),
	('jorj', '123193', 1, NULL, NULL)
GO

-- Problem 9. Change Primary Key
ALTER TABLE Users
DROP CONSTRAINT PK_Users_Id

ALTER TABLE Users
ADD CONSTRAINT PK_Users_Id_Username PRIMARY KEY (Id, Username)

GO

-- Problem 10. Add Check Constraint
ALTER TABLE Users
ADD CONSTRAINT CHK_Users_Password CHECK(LEN(Password) >= 5)
GO

-- Problem 11. Set Default Value of a Field
ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime DEFAULT CURRENT_TIMESTAMP FOR LastLoginTime
GO

-- Problem 12. Set Unique Field
ALTER TABLE Users
DROP CONSTRAINT PK_Users_Id_Username

ALTER TABLE Users
ADD 
	CONSTRAINT PK_Users_Id PRIMARY KEY (Id),
	CONSTRAINT UX_Users_Username UNIQUE (Username),
	CONSTRAINT CHK_Users_Username CHECK (LEN(Username) >= 3)
GO

-- Problem 13. Movies Database
DROP DATABASE IF EXISTS Movies
GO

CREATE DATABASE Movies
GO

USE Movies
GO

DROP TABLE IF EXISTS Movies
DROP TABLE IF EXISTS Directors
DROP TABLE IF EXISTS Genres
DROP TABLE IF EXISTS Categories

CREATE TABLE Directors 
(
	Id INT IDENTITY(1,1),
	DirectorName NVARCHAR(64) NOT NULL,
	Notes NVARCHAR(MAX),

	CONSTRAINT PK_Directors_Id PRIMARY KEY (Id)
)

CREATE TABLE Genres 
(
	Id INT IDENTITY(1,1),
	GenreName NVARCHAR(64) NOT NULL,
	Notes NVARCHAR(MAX),

	CONSTRAINT PK_Genres_Id PRIMARY KEY (Id)
)

CREATE TABLE Categories 
(
	Id INT IDENTITY(1,1),
	CategoryName NVARCHAR(64) NOT NULL,
	Notes NVARCHAR(MAX),

	CONSTRAINT PK_Categories_Id PRIMARY KEY (Id)
)

CREATE TABLE Movies
(
	Id INT IDENTITY(1,1),
	Title NVARCHAR(256) NOT NULL,
	DirectorId INT NOT NULL, 
	CopyrightYear SMALLINT NOT NULL,
	Length TIME NOT NULL,
	GenreId INT NOT NULL, 
	CategoryId INT NOT NULL, 
	Rating TINYINT,
	Notes NVARCHAR(MAX),

	CONSTRAINT PK_Movies_Id PRIMARY KEY	(Id),
	CONSTRAINT FK_Movies_DirectorId FOREIGN KEY (DirectorId) 
		REFERENCES Directors(Id),
	CONSTRAINT FK_Movies_GenreId FOREIGN KEY (GenreId)
		REFERENCES Genres(Id),
	CONSTRAINT FK_Movies_CategoryId FOREIGN KEY (CategoryId)
		REFERENCES Categories(Id)
)

INSERT INTO Directors
	(DirectorName, Notes) VALUES
	('Steven Sp.', 'Hmm...'),
	('Trevor N.', NULL),
	('Lol Deo', 'Hmmph...'),
	('Johny Jolly', 'Just briliant'),
	('Gecko Gecken', 'G.E.C.K.')

INSERT INTO Genres
	(GenreName, Notes) VALUES
	('Horror', 'AAA...'),
	('Thriller', 'Woo... Ahhh.. AAA.'),
	('Comedy', 'Haha!'),
	('Family Comedy', 'Ahhh.. You get it?'),
	('Boring', 'Look how cement thickens')

INSERT INTO Categories
	(CategoryName, Notes) VALUES
	('Action Films', 'Vin Diesel and his bros again.. Probably Chuck Norris'),
	('Crime & Gangster Films', NULL),
	('Drama Films', 'Oh no, Richard... But I love him... But...'),
	('Epics/Historical Films', 'And then there is no Sparta...'),
	('Disaster Films', 'No comment.. not good')

INSERT INTO Movies
	(Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating, Notes) VALUES
	('Johny', 1, 2010, '4:21', 1, 1, 10, 'Boring AF'),
	('America', 2, 2011, '2:10', 2, 3, 3, 'As usual'),
	('The Grand', 1, 2009, '4:21', 5, 4, NULL, NULL),
	('Not so special...', 1, 2011, '4:21', 3, 5, 4, 'Hmm..'),
	('America! Again...', 1, 2021, '4:21', 4, 2, 6, 'Not funny.')

-- Problem 14. Car Rental Database
USE master
GO

DROP DATABASE IF EXISTS CarRental
GO

CREATE DATABASE CarRental
GO

USE CarRental
GO

DROP TABLE IF EXISTS [RentalOrders]
DROP TABLE IF EXISTS [Customers]
DROP TABLE IF EXISTS [Employees]
DROP TABLE IF EXISTS [Cars]
DROP TABLE IF EXISTS [Categories]

CREATE TABLE Categories
(
	Id INT IDENTITY(1, 1),
	CategoryName NVARCHAR(32) NOT NULL,
	DailyRate DECIMAL(8, 2) NOT NULL,
	WeeklyRate DECIMAL(16, 2) NOT NULL,
	MontlyRate DECIMAL(32, 2) NOT NULL,
	WeekendRate DECIMAL(12, 2) NOT NULL,
	
	CONSTRAINT PK_Categories_Id PRIMARY KEY (Id)
)

CREATE TABLE Cars
(
	Id INT IDENTITY(1, 1),
	PlateNumber NVARCHAR(16) NOT NULL,
	Manufacturer NVARCHAR(64) NOT NULL,
	Model NVARCHAR(64) NOT NULL, 
	CarYear SMALLINT NOT NULL,
	CategoryId INT NOT NULL, 
	Doors TINYINT NOT NULL,
	Picture VARBINARY(2048),
	Condition NVARCHAR(32),
	Available BIT,

	CONSTRAINT PK_Cars_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Cars_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)

CREATE TABLE Employees
(
	Id INT IDENTITY(1, 1),
	FirstName NVARCHAR(32) NOT NULL,
	LastName NVARCHAR(32), 
	Title NVARCHAR(16) NOT NULL,
	Notes NVARCHAR(256),

	CONSTRAINT PK_Employees_Id PRIMARY KEY (Id)
)

CREATE TABLE Customers
(
	Id INT IDENTITY(1, 1),
	DriverLicenseNumber VARCHAR(32) NOT NULL,
	FullName NVARCHAR(128) NOT NULL, 
	Address NVARCHAR(128) NOT NULL,
	City NVARCHAR(32) NOT NULL,
	ZIPCode NVARCHAR(32) NOT NULL,
	Notes NVARCHAR(256),

	CONSTRAINT PK_Customers_Id PRIMARY KEY (Id)
)

CREATE TABLE RentalOrders
(
	Id INT IDENTITY(1, 1),
	EmployeeId INT NOT NULL,
	CustomerId INT NOT NULL, 
	CarId INT NOT NULL, 
	TankLevel DECIMAL(4,2) NOT NULL,
	KilometrageStart DECIMAL(7, 2) NOT NULL, 
	KilometrageEnd DECIMAL(7, 2) NOT NULL,
	TotalKilometrage AS KilometrageEnd - KilometrageStart,
	StartDate DATE NOT NULL, 
	EndDate DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, StartDate, EndDate),  
	RateApplied DECIMAL(16, 2) NOT NULL,
	TaxRate AS RateApplied * 0.2,
	OrderStatus BIT NOT NULL,
	Notes NVARCHAR(256),

	CONSTRAINT PK_RentalOrders_Id PRIMARY KEY (Id),
	CONSTRAINT FK_RentalOrders_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
	CONSTRAINT FK_RentalOrders_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
	CONSTRAINT FK_RentalOrders_CarId FOREIGN KEY (CarId) REFERENCES Cars(Id)
)

INSERT INTO Categories
	(CategoryName, DailyRate, WeeklyRate, MontlyRate, WeekendRate) VALUES
	('Expensive', 200.00, 1000.00, 20000.00, 2000.00),
	('Middle class', 100.00, 500.00, 1000.00, 150.00),
	('Cheap', 9.99, 49.99, 499.99, 19.99)

INSERT INTO Cars
	(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
	('CHING123', 'Fera', '12', 1998, 1, 2, NULL, 'Good', 1),
	('12344231', 'Rad', 'F-5', 2010, 2, 4, NULL, 'Crummy', 1),
	('A123-GAS', 'Mongo', 'DB', 2020, 3, 5, NULL, 'Perfect', 0)

INSERT INTO Employees
	(FirstName, LastName, Title, Notes) VALUES
	('John', 'Smith', 'Manager', 'Hmmph, what am I doing...'),
	('Johny', 'Smithy', 'Seller', 'Hmmph, what am I doing...'),
	('Johnto', 'Small', 'Seller', 'Hmmph, what am I doing...')

INSERT INTO Customers
	(DriverLicenseNumber, FullName, Address, City, ZIPCode, Notes) VALUES
	('N-12313-GW', 'Jenner Torn', '21 Anthony str.', 'City1', '1231231-SS', NULL),
	('123 426 ED', 'Jihno Torno', '22 Anth str.', 'City2','1231', NULL),
	('EQ 1256123', 'Jenjer Tornt', '23 Anthy str.', 'City3','1231-AQ', NULL)

INSERT INTO RentalOrders
	(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate, RateApplied, OrderStatus, Notes) VALUES
	(1, 1, 1, 10.0, 15000.0, 15100.0, '20200220', '20200222', 200.00, 1, NULL),
	(2, 2, 2, 0.0, 10000.0, 15000.0, '20200221', '20200222', 100.00, 1, NULL),
	(3, 3, 3, 2.0, 100.0, 200.0, '20200222', '20200227', 20.00, 1, NULL)

-- Problem 15. Hotel Database
USE master
GO

DROP DATABASE IF EXISTS Hotel
GO

CREATE DATABASE Hotel
GO

USE Hotel
GO

-- CREATE TABLES

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(32) NOT NULL,
	LastName NVARCHAR(32),
	Title NVARCHAR(32) NOT NULL,
	Notes NVARCHAR(256)
)

CREATE TABLE Customers
(
	AccountNumber INT PRIMARY KEY IDENTITY(1, 1),
	FirstName NVARCHAR(32) NOT NULL,
	LastName NVARCHAR(32),
	PhoneNumber VARCHAR(32) NOT NULL,
	EmergencyName NVARCHAR(32) NOT NULL,
	EmergencyNumber VARCHAR(32) NOT NULL,
	Notes NVARCHAR(256)
)

CREATE TABLE RoomStatus
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	RoomStatus NVARCHAR(16),
	Notes NVARCHAR(256)
)

CREATE TABLE RoomTypes
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	RoomType NVARCHAR(32),
	Notes NVARCHAR(256)
)

CREATE TABLE BedTypes
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	BedType NVARCHAR(32),
	Notes NVARCHAR(256)
)

CREATE TABLE Rooms
(
	RoomNumber INT PRIMARY KEY, 
	RoomType INT FOREIGN KEY REFERENCES RoomTypes(Id) NOT NULL,
	BedType INT FOREIGN KEY REFERENCES BedTypes(Id) NOT NULL,
	Rate DECIMAL(18,2) NOT NULL, 
	RoomStatus INT FOREIGN KEY REFERENCES RoomStatus(Id) NOT NULL,
	Notes NVARCHAR(256)
)

CREATE TABLE Payments
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	PaymentDate DATE NOT NULL, 
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
	AmountCharged DECIMAL(18,2) NOT NULL,
	TaxRate DECIMAL(18,2) NOT NULL,
	TaxAmount DECIMAL(19,2) NOT NULL,
	PaymentTotal AS TaxAmount + AmountCharged,
	Notes NVARCHAR(256)
)

CREATE TABLE Occupancies
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	DateOccupied DATE NOT NULL, 
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber) NOT NULL, 
	RateApplied DECIMAL(8,2),
	PhoneCharge DECIMAL(8,2),
	Notes NVARCHAR(256)
)

-- INSERTS

INSERT INTO Employees
	(FirstName, LastName, Title, Notes) 
	VALUES
	('Velizar', 'Velikov', 'Receptionist', 'Nice customer'),
	('Ivan', 'Ivanov', 'Concierge', 'Nice one'),
	('Elisaveta', 'Bagriana', 'Cleaner', 'Poetesa')

INSERT INTO Customers
	(FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes) 
	VALUES
	('Ginka', 'Shikerova', '+359888147581', 'Gosho', '7708315342', NULL),
	('Chaika', 'Stavreva', '+359888279683', 'Gosho', '7708315342', NULL),
	('Mladen', 'Mladenov', '+359888378785', 'Gosho', '7708315342', NULL)

INSERT INTO RoomStatus
	(RoomStatus, Notes) 
	VALUES
	('Dirty', 'Check the towels'),
	('Clean', 'Refill the minibar'),
	('Pls do not enter', 'Move the bed for couple')

INSERT INTO RoomTypes 
	(RoomType, Notes)
	VALUES
	(N'Suite', 'Two beds'),
	(N'Wedding suite', 'One king size bed'),
	(N'Apartment', 'Up to 3 adults and 2 children')

INSERT INTO BedTypes
	(BedType, Notes)
	VALUES
	('Double', 'One adult and one child'),
	('King size', 'Two adults'),
	('Couch', 'One child')

INSERT INTO Rooms 
	(RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes)
	VALUES
	(12, 1, 1, 49.99, 1, NULL),
	(15, 2, 2, 79.99, 2, NULL),
	(23, 2, 3, 120.0, 3, NULL)

INSERT INTO Payments
	(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate, TaxAmount, Notes)
	VALUES
	(1, (SELECT CURRENT_TIMESTAMP), 1, '20200220', '20200222', 10.4, 10.1, 1.1, NULL),
	(2, (SELECT CURRENT_TIMESTAMP), 2, '20200220', '20200222', 10.4, 10.1, 1.1, NULL),
	(3, (SELECT CURRENT_TIMESTAMP), 3, '20200220', '20200222', 10.4, 10.1, 1.1, NULL)

INSERT INTO Occupancies
	(EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes)
	VALUES
	(1, (SELECT CURRENT_TIMESTAMP), 1, 12, 15.2, 2.2, NULL),
	(2, (SELECT CURRENT_TIMESTAMP), 2, 15, 15.4, 2.1, NULL),
	(3, (SELECT CURRENT_TIMESTAMP), 3, 23, 15.6, 2.0, NULL)

-- Problem 16. Create SoftUni Database

--DROP DATABASE IF EXISTS SoftUni
USE master
GO

DROP DATABASE SoftUni
GO

CREATE DATABASE SoftUni
GO

USE SoftUni
GO

-- CREATE TABLES

CREATE TABLE Towns
(
	Id INT IDENTITY(1,1),
	Name NVARCHAR(32) NOT NULL,

	CONSTRAINT PK_Towns_Id PRIMARY KEY (Id)
)

CREATE TABLE Addresses
(
	Id INT IDENTITY(1,1),
	AddressText NVARCHAR NOT NULL,
	TownId INT NOT NULL,

	CONSTRAINT PK_Addresses_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Addresses_TownId FOREIGN KEY (TownId) REFERENCES Towns(Id)
)

CREATE TABLE Departments
(
	Id INT IDENTITY(1,1),
	Name NVARCHAR(32) NOT NULL,
	
	CONSTRAINT PK_Departments_Id PRIMARY KEY (Id)
)

CREATE TABLE Employees
(
	Id INT IDENTITY(1,1),
	FirstName NVARCHAR(32) NOT NULL,
	MiddleName NVARCHAR(32),
	LastName NVARCHAR(32) NOT NULL,
	JobTitle NVARCHAR(32) NOT NULL,
	DepartmentId INT NOT NULL,
	HireDate DATE NOT NULL,
	Salary DECIMAL(18,2) NOT NULL,
	AddressId INT,

	CONSTRAINT PK_Employees_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Employees_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
	CONSTRAINT FK_Employees_AddressId FOREIGN KEY (AddressId) REFERENCES Addresses(Id)
)

-- INSERTS

INSERT INTO Towns (Name) VALUES ('Sofia'), ('Plovdiv'), ('Varna'), ('Burgas')

INSERT INTO Departments (Name) 
VALUES 
	('Engineering'), 
	('Sales'), 
	('Marketing'), 
	('Software Development'), 
	('Quality Assurance')

INSERT INTO Employees 
	(FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
VALUES
	('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '20130201', 3500.00),
	('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '20040302', 4000.00),
	('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '20160828', 525.25),
	('Georgi', 'Terziev', 'Ivanov', 'CEO', 2, '20071209', 3000.00),
	('Peter', 'Pan', 'Pan', 'Intern', 3, '20160828', 599.88)

-- Problem 19. Basic Select All Fields
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

-- Problem 20. Basic Select All Fields and Order Them
SELECT * FROM Towns
ORDER BY Name ASC

SELECT * FROM Departments
ORDER BY Name ASC

SELECT * FROM Employees
ORDER BY Salary DESC

-- Problem 21. Basic Select Some Fields
SELECT Name FROM Towns
ORDER BY Name ASC

SELECT Name FROM Departments
ORDER BY Name ASC

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC

-- Problem 22. Increase Employees Salary
UPDATE Employees
SET Salary = Salary * 1.1

SELECT Salary FROM Employees

-- Problem 23. Decrease Tax Rate
USE Hotel
GO

UPDATE Payments
SET TaxRate = TaxRate * 0.97

SELECT TaxRate FROM Payments

-- Problem 24. Delete All Records
TRUNCATE TABLE Occupancies
