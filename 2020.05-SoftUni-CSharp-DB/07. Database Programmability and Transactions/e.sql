/*
Part 1.Queries for SoftUni Database
*/

USE [SoftUni]
GO

-- Problem 1.Employees with Salary Above 35000

CREATE OR ALTER PROCEDURE [usp_GetEmployeesSalaryAbove35000]
AS
    SELECT
	   [Employees].[FirstName] AS [First Name],
	   [Employees].[LastName] AS  [Last Name]
    FROM  [Employees]
    WHERE [Employees].[Salary] > 35000
GO
-- Problem 2.Employees with Salary Above Number

CREATE OR ALTER PROC [usp_GetEmployeesSalaryAboveNumber]
    @Num DECIMAL(18, 4)
AS
    SELECT
	   [Employees].[FirstName] AS [First Name],
	   [Employees].[LastName] AS  [Last Name]
    FROM  [Employees]
    WHERE [Employees].[Salary] >= @Num
GO

EXEC [usp_GetEmployeesSalaryAboveNumber]
	48100
-- Problem 3.Town Names Starting With

CREATE OR ALTER PROC [usp_GetTownsStartingWith]
    @ThisWord VARCHAR(10)
AS
    SELECT
	   [Towns].[Name] AS [Town]
    FROM  [Towns]
    WHERE UPPER([Towns].[Name]) LIKE(@ThisWord + '%')
GO

EXEC [usp_GetTownsStartingWith]
	'b'
-- Problem 4.Employees from Town

CREATE OR ALTER PROC [usp_GetEmployeesFromTown]
    @TownName VARCHAR(30)
AS
    SELECT
	   [Employees].[FirstName] AS [First Name],
	   [Employees].[LastName] AS  [Last Name]
    FROM  [Employees]
    INNER JOIN [Addresses]
		ON [Employees].[AddressID] = [Addresses].[AddressID]
    INNER JOIN [Towns]
		ON [Addresses].[TownID] = [Towns].[TownID]
    WHERE [Towns].[Name] = @TownName
GO

EXEC [usp_GetEmployeesFromTown]
	'Sofia'

-- Problem 5.Salary Level Function

CREATE FUNCTION [ufn_GetSalaryLevel]
(
    @Salary DECIMAL(18, 4)
)
RETURNS VARCHAR(7)
AS
    BEGIN
	   RETURN CASE
			    WHEN @salary < 30000
				   THEN 'Low'
			    WHEN @salary BETWEEN 30000 AND 50000
				   THEN 'Average'
			    WHEN @salary > 50000
				   THEN 'High'
			    ELSE NULL
			END
    END

-- Problem 6.Employees by Salary Level

CREATE OR ALTER PROC [usp_EmployeesBySalaryLevel]
    @SalaryLevel VARCHAR(10)
AS
    SELECT
	   [Employees].[FirstName] AS [First Name],
	   [Employees].[LastName] AS  [Last Name]
    FROM  [Employees]
    WHERE [dbo].[ufn_GetSalaryLevel]([Employees].[Salary]) = @SalaryLevel
GO

EXEC [dbo].[usp_EmployeesBySalaryLevel]
	'High'

-- Problem 7.Define Function

DROP FUNCTION IF EXISTS
    [ufn_IsWordComprised]
GO

CREATE FUNCTION [ufn_IsWordComprised]
(
    @setOfLetters VARCHAR(60),
    @word         VARCHAR(60)
)
RETURNS BIT
AS
    BEGIN
	   DECLARE @i INT= LEN(@word)

	   WHILE(@i > 0)
	   BEGIN
		  IF(CHARINDEX(SUBSTRING(@word, @i, 1), @setOfLetters) = 0)
			 RETURN 0
		  SET @i-=1
	   END

	   RETURN 1
    END
GO

SELECT
    [dbo].[ufn_IsWordComprised]('pwosenskwermsd', 'word')

-- second way  60/100 but something is buggy about same letter twice in [] LIKE

DROP FUNCTION IF EXISTS
    [ufn_IsWordComprised]
GO

CREATE FUNCTION [ufn_IsWordComprised]
(
    @setOfLetters VARCHAR(60),
    @word         VARCHAR(60)
)
RETURNS BIT
AS
    BEGIN
	   DECLARE @regex VARCHAR(62)= REPLICATE('[' + @setOfLetters + ']', LEN(@word))
	   IF UPPER(@word) LIKE UPPER(@regex)
		  RETURN 1
	   RETURN 0
    END
GO

SELECT
    [dbo].[ufn_IsWordComprised]('pwosenskwermsd', 'word')

-- Problem 8.* Delete Employees and Departments

CREATE PROC [usp_DeleteEmployeesFromDepartment]
(
    @departmentId INT
)
AS
    DECLARE @empIDsToBeDeleted TABLE
    (
	   [Id] INT
    )

    INSERT INTO @empIDsToBeDeleted
		 SELECT
			[e].[EmployeeID]
		 FROM  [Employees] AS [e]
		 WHERE [e].[DepartmentID] = @departmentId

    ALTER TABLE [Departments] ALTER COLUMN [ManagerID] INT NULL

    DELETE FROM [EmployeesProjects]
    WHERE
	   [EmployeeID] IN
    (
	   SELECT
		  [Id]
	   FROM @empIDsToBeDeleted
    )

    UPDATE   [Employees]
	 SET
		[ManagerID] = NULL
    WHERE
	   [ManagerID] IN
    (
	   SELECT
		  [Id]
	   FROM @empIDsToBeDeleted
    )

    UPDATE   [Departments]
	 SET
		[ManagerID] = NULL
    WHERE
	   [ManagerID] IN
    (
	   SELECT
		  [Id]
	   FROM @empIDsToBeDeleted
    )

    DELETE FROM [Employees]
    WHERE
	   [EmployeeID] IN
    (
	   SELECT
		  [Id]
	   FROM @empIDsToBeDeleted
    )

    DELETE FROM [Departments]
    WHERE
	   [DepartmentID] = @departmentId

    SELECT
	   COUNT(*) AS [Employees Count]
    FROM  [Employees] AS [e]
    JOIN [Departments] AS [d]
		ON [d].[DepartmentID] = [e].[DepartmentID]
    WHERE [e].[DepartmentID] = @departmentId
GO

/*
Part 2.Queries for Bank Database
*/

USE [Bank]
GO

-- Problem 9.Find Full Name

CREATE OR ALTER PROC [usp_GetHoldersFullName]
AS
    SELECT
	   [FirstName] + ' ' + [LastName] AS [Full Name]
    FROM [AccountHolders]
GO

-- Problem 10.People with Balance Higher Than

CREATE OR ALTER PROC [usp_GetHoldersWithBalanceHigherThan]
    @num MONEY
AS
    WITH cte_10ex
	    AS (SELECT
			  [AccountHolders].[Id]
		   FROM [AccountHolders]
		   INNER JOIN [Accounts]
			   ON [AccountHolders].[Id] = [Accounts].[AccountHolderId]
		   GROUP BY
			  [AccountHolders].[Id]
		   HAVING SUM([Accounts].[Balance]) > @num)
	    SELECT
		   [AccountHolders].[FirstName] AS [First Name],
		   [AccountHolders].[LastName] AS  [Last Name]
	    FROM [cte_10ex]
	    INNER JOIN [AccountHolders]
		    ON [cte_10ex].[Id] = [AccountHolders].[Id]
	    ORDER BY
		   [AccountHolders].[FirstName],
		   [AccountHolders].[LastName]
GO

EXEC [usp_GetHoldersWithBalanceHigherThan]
	16000

-- Problem 11.Future Value Function

DROP FUNCTION IF EXISTS
    [ufn_CalculateFutureValue]
GO

CREATE FUNCTION [ufn_CalculateFutureValue]
(
    @sum                MONEY,
    @yearlyInterestRate FLOAT,
    @numberOfYears      INT
)
RETURNS MONEY
AS
    BEGIN
	   RETURN 1.0 * @sum * POWER((1 + @yearlyInterestRate), @numberOfYears)
    END
GO

SELECT
    [dbo].[ufn_CalculateFutureValue](1000, 0.1, 5)

-- Problem 12.Calculating Interest

USE [Bank]
GO

CREATE OR ALTER PROCEDURE [usp_CalculateFutureValueForAccount]
    @AccountId    INT,
    @InterestRate FLOAT
AS
BEGIN
    SELECT
	   [Accounts].[Id] AS                                               [Account Id],
	   [AccountHolders].[FirstName] AS                                  [First Name],
	   [AccountHolders].[LastName] AS                                   [Last Name],
	   [Accounts].[Balance] AS                                          [Current Balance],
	   [dbo].[ufn_CalculateFutureValue]([Balance], @InterestRate, 5) AS [Balance in 5 years]
    FROM  [Accounts]
    INNER JOIN [AccountHolders]
		ON [Accounts].[AccountHolderId] = [AccountHolders].[Id]
    WHERE [Accounts].[Id] = @AccountId
END
GO

EXEC [usp_CalculateFutureValueForAccount]
	1,
	0.1

/*
Part 3.Queries for Diablo Database
*/

USE [Diablo]
GO

-- Problem 13.*Scalar Function: Cash in User Games Odd Rows

DROP FUNCTION IF EXISTS
    [dbo].[ufn_CashInUsersGames]
GO

CREATE FUNCTION [ufn_CashInUsersGames]
(
    @GameName VARCHAR(60)
)
RETURNS TABLE
AS
    RETURN
(
    SELECT
	   SUM([cte_in_game_cash_with_rows].[Cash]) AS [SumCash]
    FROM
    (
	   SELECT
		  [UsersGames].[Cash],
		  ROW_NUMBER() OVER(
		  ORDER BY
		  [Cash] DESC) AS [Row]
	   FROM  [UsersGames]
	   INNER JOIN [Games]
		    ON [UsersGames].[GameId] = [Games].[Id]
	   WHERE [Games].[Name] = @GameName
    ) AS [cte_in_game_cash_with_rows]
    WHERE [cte_in_game_cash_with_rows].[Row] % 2 <> 0
)
-- another way

DROP FUNCTION IF EXISTS
    [dbo].[ufn_CashInUsersGames]
GO

CREATE FUNCTION [ufn_CashInUsersGames]
(
    @GameName VARCHAR(MAX)
)
RETURNS @ReturnedTable TABLE
(
    [SumCash] MONEY
)
AS
    BEGIN
	   DECLARE @result MONEY=
	   (
		  SELECT
			 SUM([cte_13].[Cash]) AS [Cash]
		  FROM
		  (
			 SELECT
				[Cash],
				ROW_NUMBER() OVER(
				ORDER BY
				[Cash] DESC) AS [RowNumber]
			 FROM  [UsersGames]
			 INNER JOIN [Games]
				  ON [UsersGames].[GameId] = [Games].[Id]
			 WHERE [Games].[Name] = @GameName
		  ) AS [cte_13]
		  WHERE [cte_13].[RowNumber] % 2 != 0
	   )

	   INSERT INTO @ReturnedTable
	   VALUES
		  (
		  @result
		  )
	   RETURN
    END

/*
Section II.Triggers and Transactions
*/

USE [Bank]
GO

-- Problem 14.Create Table Logs

CREATE TABLE [Logs]
(
    [LogId]     INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [AccountId] INT NOT NULL
				FOREIGN KEY REFERENCES [Accounts](
    [Id]),
    [OldSum]    MONEY,
    [NewSum]    MONEY
)

CREATE TRIGGER [InsertNewEntryIntoLogs] ON [Accounts]
AFTER UPDATE
AS
    INSERT INTO [Logs]
    VALUES
	   (
    (
	   SELECT
		  [Id]
	   FROM [inserted]
    ),
    (
	   SELECT
		  [Balance]
	   FROM [deleted]
    ),
    (
	   SELECT
		  [Balance]
	   FROM [inserted]
    )
	   )
GO
-- Problem 15.Create Table Emails

CREATE TABLE [NotificationEmails]
(
    [Id]       INT
    PRIMARY KEY IDENTITY(1, 1),
    [Recipent] INT NOT NULL
			    FOREIGN KEY REFERENCES [Accounts](
    [Id]),
    [Subject]  VARCHAR(60) NOT NULL,
    [Body]     VARCHAR(MAX)
)

CREATE TRIGGER [CreateNewEmailWheneverNewRecordIsInsertedInLogsTable] ON [Logs]
FOR INSERT
AS
    INSERT INTO [NotificationEmails]
    VALUES
	   (
    (
	   SELECT
		  [AccountId]
	   FROM [INSERTED]
    ), CONCAT('Balance change for account: ',
    (
	   SELECT
		  [AccountId]
	   FROM [INSERTED]
    )), CONCAT('On', GETDATE(), 'your balance was changed from ',
    (
	   SELECT
		  [OldSum]
	   FROM [INSERTED]
    ), ' to ',
    (
	   SELECT
		  [NewSum]
	   FROM [INSERTED]
    ))
	   )
GO

-- Problem 16.Deposit Money

CREATE OR ALTER PROCEDURE [usp_DepositMoney]
    @AccountId   INT,
    @MoneyAmount MONEY
AS
BEGIN
    IF @MoneyAmount > 0
    BEGIN
	   UPDATE [Accounts]
		SET
		    [Balance]+=@MoneyAmount
	   WHERE
		  [Id] = @AccountId
    END
END
GO
-- Problem 17.Withdraw Money

CREATE OR ALTER PROC [usp_WithdrawMoney]
    @AccountId   INT,
    @MoneyAmount MONEY
AS
BEGIN
    IF @MoneyAmmount > 0
    BEGIN
	   UPDATE [Accounts]
		SET
		    [Balance]-=@MoneyAmount
	   WHERE
		  [Id] = @AccountId
    END

END
GO

-- Problem 18.Money Transfer

CREATE OR ALTER PROC [usp_TransferMoney]
    @SenderId   INT,
    @ReceiverId INT,
    @Amount     MONEY
AS
BEGIN
    EXEC [dbo].[usp_WithdrawMoney]
	    @SenderId,
	    @Amount
    EXEC [dbo].[usp_DepositMoney]
	    @ReceiverId,
	    @Amount
END
GO

/*
Part 2.Queries for Diablo Database
*/

USE [Diablo]
GO

-- Problem 20.*Massive Shopping
GO

CREATE OR ALTER PROC [usp_MassiveShopping]
    @Username   VARCHAR(50),
    @GameName   VARCHAR(50),
    @LowerRange INT,
    @UpperRange INT
AS
BEGIN
    BEGIN TRANSACTION

    DECLARE @UserID INT=
    (
	   SELECT
		  [Id]
	   FROM  [Users]
	   WHERE [Username] = @Username
    )
    DECLARE @GameID INT=
    (
	   SELECT
		  [Id]
	   FROM  [Games]
	   WHERE [Name] = @GameName
    )
    DECLARE @UserMoney MONEY=
    (
	   SELECT
		  [Cash]
	   FROM  [UsersGames]
	   WHERE [UserId] = @UserID
		    AND [GameId] = @GameID
    )
    DECLARE @ItemsTotalPrice MONEY
    DECLARE @UserGameID INT=
    (
	   SELECT
		  [Id]
	   FROM  [UsersGames]
	   WHERE [UserId] = @UserID
		    AND [GameId] = @GameID
    )

    SET @ItemsTotalPrice =
    (
	   SELECT
		  SUM([Price])
	   FROM  [Items]
	   WHERE [MinLevel] BETWEEN 11 AND 12
    )

    IF(@UserMoney - @ItemsTotalPrice >= 0)
    BEGIN
	   INSERT INTO [UserGameItems]
			SELECT
			    [i].[Id],
			    @UserGameID
			FROM  [Items] AS [i]
			WHERE [i].[Id] IN
			(
			    SELECT
				   [Id]
			    FROM  [Items]
			    WHERE [MinLevel] BETWEEN @LowerRange AND @UpperRange
			)

	   UPDATE [UsersGames]
		SET
		    [Cash]-=@ItemsTotalPrice
	   WHERE
		  [GameId] = @GameID
		  AND [UserId] = @UserID
	   COMMIT
    END
	   ELSE
    BEGIN
	   ROLLBACK
    END

    SET @UserMoney =
    (
	   SELECT
		  [Cash]
	   FROM  [UsersGames]
	   WHERE [UserId] = @UserID
		    AND [GameId] = @GameID
    )

END

    EXEC [dbo].[usp_MassiveShopping]
	    'Stamat',
	    'Safflower',
	    11,
	    12
    EXEC [dbo].[usp_MassiveShopping]
	    'Stamat',
	    'Safflower',
	    19,
	    21

    SELECT
	   [Items].[Name] AS [Item Name]
    FROM  [UserGameItems]
    INNER JOIN [Items]
		ON [UserGameItems].[ItemId] = [Items].[Id]
    WHERE [UserGameItems].[UserGameId] =
    (
	   SELECT
		  [Users].[Id]
	   FROM  [Users]
	   WHERE [Users].[Username] = 'Stamat'
    )
GO

/*
Part 3.Queries for SoftUni Database
*/

USE [SoftUni]
GO

-- Problem 21.Employees with Three Projects

CREATE OR ALTER PROC [usp_AssignProject]
(
    @employeeId INT,
    @projectId  INT
)
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @employeeProjectsCount INT=
    (
	   SELECT
		  COUNT(*)
	   FROM  [EmployeesProjects]
	   WHERE [EmployeesProjects].[EmployeeID] = @employeeId
    )
    IF(@employeeProjectsCount >= 3)
    BEGIN
	   ROLLBACK
	   RAISERROR('The employee has too many projects!', 16, 1)
    END
	   ELSE
    BEGIN
	   INSERT INTO [EmployeesProjects]
	   VALUES
		  (
		  @employeeId, @projectId
		  )
	   COMMIT
    END
END
    --
CREATE PROC [usp_AssignProject]
(
    @EmployeeId INT,
    @ProjectID  INT
)
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @ProjectsCount INT=
    (
	   SELECT
		  COUNT([ProjectID])
	   FROM  [EmployeesProjects]
	   WHERE [EmployeeID] = @EmployeeId
    )
    IF(@ProjectsCount >= 3)
    BEGIN
	   ROLLBACK
	   RAISERROR('The employee has too many projects!', 16, 1)
    END
	   ELSE
    BEGIN
	   INSERT INTO [EmployeesProjects]
	   VALUES
		  (
		  @EmployeeId, @ProjectID
		  )
	   COMMIT
    END
END