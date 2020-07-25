

CREATE DATABASE [TripService]
GO

USE TripService
GO

--create----------------------------------------------

CREATE TABLE [Cities]
(
    [Id]          INT IDENTITY PRIMARY KEY,
    [Name]        NVARCHAR(20) NOT NULL,
    [CountryCode] CHAR(2) NOT NULL
)

CREATE TABLE [Hotels]
(
    [Id]            INT IDENTITY PRIMARY KEY,
    [Name]          NVARCHAR(30) NOT NULL,
    [CityId]        INT FOREIGN KEY REFERENCES [Cities](
    [Id]) NOT NULL,
    [EmployeeCount] INT NOT NULL,
    [BaseRate]      DECIMAL(18, 2)
)

CREATE TABLE [Rooms]
(
    [Id]      INT IDENTITY PRIMARY KEY,
    [Price]   DECIMAL(18, 2) NOT NULL,
    [Type]    NVARCHAR(20) NOT NULL,
    [Beds]    INT NOT NULL,
    [HotelId] INT FOREIGN KEY REFERENCES [Hotels](
    [Id]) NOT NULL
)

CREATE TABLE [Trips]
(
    [Id]          INT IDENTITY PRIMARY KEY,
    [RoomId]      INT FOREIGN KEY REFERENCES [Rooms](
    [Id]) NOT NULL,
    [BookDate]    DATE NOT NULL,
    [ArrivalDate] DATE NOT NULL,
    [ReturnDate]  DATE NOT NULL,
    [CancelDate]  DATE,
    CONSTRAINT [CHK_Trips_BookDate] CHECK([BookDate] < [ArrivalDate]),
    CONSTRAINT [CHK_Trips_ArrivalDate] CHECK([ArrivalDate] < [ReturnDate])
)

CREATE TABLE [Accounts]
(
    [Id]         INT IDENTITY PRIMARY KEY,
    [FirstName]  NVARCHAR(50) NOT NULL,
    [MiddleName] NVARCHAR(20),
    [LastName]   NVARCHAR(50) NOT NULL,
    [CityId]     INT FOREIGN KEY REFERENCES [Cities](
    [Id]),
    [BirthDate]  DATE NOT NULL,
    [Email]      VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE [AccountsTrips]
(
    [AccountId] INT FOREIGN KEY REFERENCES [Accounts](
    [Id]) NOT NULL,
    [TripId]    INT FOREIGN KEY REFERENCES [Trips](
    [Id]) NOT NULL,
    [Luggage]   INT NOT NULL
				CHECK([Luggage] >= 0),
    CONSTRAINT [PK_AccountsTrips] PRIMARY KEY([AccountId], [TripId])
)

--insert----------------------------------------------------------------

INSERT INTO [Accounts]
(
    [Accounts].[FirstName],
    [Accounts].[MiddleName],
    [Accounts].[LastName],
    [Accounts].[CityId],
    [Accounts].[BirthDate],
    [Accounts].[Email]
)
VALUES
    (
    'John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'
    ),
    (
    'Gosho', NULL, 'Petrov', 11, '1978-05-16', 'g_petrov@gmail.com'
    ),
    (
    'Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'
    ),
    (
    'Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg'
    )

INSERT INTO [Trips]
(
    [Trips].[RoomId],
    [Trips].[BookDate],
    [Trips].[ArrivalDate],
    [Trips].[ReturnDate],
    [Trips].[CancelDate]
)
VALUES
    (
    101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'
    ),
    (
    102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'
    ),
    (
    103, '2013-07-17', '2013-07-23', '2013-07-24', NULL
    ),
    (
    104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'
    ),
    (
    109, '2017-08-07', '2017-08-28', '2017-08-29', NULL
    )

--update-------------------------------------------------------------------

UPDATE [Rooms]
  SET
	 [Rooms].[Price]*=1.14
WHERE
    [Rooms].[HotelId] IN(5, 7, 9)

--delete------------------------------------------------------------------

DELETE FROM [AccountsTrips]
WHERE
    [AccountsTrips].[AccountId] = 47

--5eeemails-----------------------------------------------------------------

SELECT
    [Accounts].[FirstName],
    [Accounts].[LastName],
    format([Accounts].[BirthDate], 'MM-dd-yyyy'),
    [Cities].[Name],
    [Accounts].[Email]
FROM  [Accounts]
JOIN [Cities]
	 ON [Accounts].[CityId] = [Cities].[Id]
WHERE [Accounts].[Email] LIKE 'e%'
ORDER BY
    [Cities].[Name] ASC

--6citystats--------------------------------------------------------------------

SELECT
    [Cities].[Name],
    COUNT([Hotels].[Id]) AS [hotels]
FROM [Cities]
JOIN [Hotels]
	ON [Cities].[Id] = [Hotels].[CityId]
GROUP BY
    [Cities].[Name]
ORDER BY
    [hotels] DESC,
    [Cities].[Name] ASC

--7long...-----------------------------------------------------------------------

WITH cte_7_1
	AS (SELECT
		   [Accounts].[Id],
		   [Accounts].[FirstName] + ' ' + [Accounts].[LastName] AS       [fullName],
		   DATEDIFF(day, [Trips].[ArrivalDate], [Trips].[ReturnDate]) AS [Trip]
	    FROM  [Trips]
	    JOIN [AccountsTrips]
			ON [Trips].[Id] = [AccountsTrips].[TripId]
	    JOIN [Accounts]
			ON [AccountsTrips].[AccountId] = [Accounts].[Id]
	    WHERE [Accounts].[MiddleName] IS NULL
			AND [Trips].[CancelDate] IS NULL)
	SELECT
	    [cte_7_1].[Id] AS        [accId],
	    [fullName],
	    MAX([cte_7_1].[Trip]) AS [longestTrip],
	    MIN([cte_7_1].[Trip]) AS [shortestTrip]
	FROM [cte_7_1]
	GROUP BY
	    [cte_7_1].[Id],
	    [cte_7_1].[fullName]
	ORDER BY
	    [longestTrip] DESC,
	    [shortestTrip] ASC

--8metro-----------------------------------------------------------------------

SELECT TOP 10
    [Cities].[Id],
    [Cities].[Name],
    [Cities].[CountryCode],
    COUNT([Accounts].[Id]) AS [acc]
FROM [Cities]
JOIN [Accounts]
	ON [Cities].[Id] = [Accounts].[CityId]
GROUP BY
    [Cities].[Id],
    [Cities].[Name],
    [Cities].[CountryCode]
ORDER BY
    [acc] DESC

--9-------------------------------------------------------------------------------

SELECT
    [Accounts].[Id],
    [Accounts].[Email],
    [Cities].[Name],
    COUNT([Trips].[Id]) AS [trips]
FROM  [Accounts]
JOIN [AccountsTrips]
	 ON [Accounts].[Id] = [AccountsTrips].[AccountId]
JOIN [Trips]
	 ON [AccountsTrips].[TripId] = [Trips].[Id]
JOIN [Rooms]
	 ON [Trips].[RoomId] = [Rooms].[Id]
JOIN [Hotels]
	 ON [Rooms].[HotelId] = [Hotels].[Id]
JOIN [Cities]
	 ON [Accounts].[CityId] = [Cities].[Id]
WHERE [Hotels].[CityId] = [Accounts].[CityId]
GROUP BY
    [Accounts].[Id],
    [Accounts].[Email],
    [Cities].[Name]
ORDER BY
    [trips] DESC,
    [Accounts].[Id]

--10--------------------------------------------------------------------------

SELECT
    [Trips].[Id],
    CASE
	   WHEN [Accounts].[MiddleName] IS NULL
		  THEN [Accounts].[FirstName] + ' ' + [Accounts].[LastName]
	   ELSE [Accounts].[FirstName] + ' ' + [Accounts].[MiddleName] + ' ' + [Accounts].[LastName]
    END AS              [fullName],
    [Acc_C].[Name] AS   [from],
    [Hotel_C].[Name] AS [to],
    CASE
	   WHEN [Trips].[CancelDate] IS NOT NULL
		  THEN 'Canceled'
	   ELSE concat(DATEDIFF(day, [Trips].[ArrivalDate], [Trips].[ReturnDate]), ' days')
    END AS              [duration]
FROM [Trips]
JOIN [AccountsTrips]
	ON [Trips].[Id] = [AccountsTrips].[TripId]
JOIN [Accounts]
	ON [AccountsTrips].[AccountId] = [Accounts].[Id]
JOIN [Rooms]
	ON [Trips].[RoomId] = [Rooms].[Id]
JOIN [Hotels]
	ON [Rooms].[HotelId] = [Hotels].[Id]
JOIN [Cities] AS [Hotel_C]
	ON [Hotels].[CityId] = [Hotel_C].[Id]
JOIN [Cities] AS [Acc_C]
	ON [Accounts].[CityId] = [Acc_C].[Id]
ORDER BY
    [fullName],
    [Trips].[Id]

--11---------------------------------------------------------------------------

DROP FUNCTION IF EXISTS
    [dbo].[udf_GetAvailableRoom]
GO

CREATE FUNCTION [udf_GetAvailableRoom]
(
    @HotelId INT,
    @Date    DATE,
    @People  INT
)
RETURNS NVARCHAR(2048)
AS
    BEGIN
	   DECLARE @roomId INT=
	   (
		  SELECT TOP 1
			 [Rooms].[Id]
		  FROM  [Rooms]
		  WHERE [Rooms].[HotelId] = @HotelId
			   AND [Rooms].[Beds] >= @People
			   AND [Rooms].[Id] NOT IN
		  (
			 SELECT
				[Rooms].[Id]
			 FROM  [Rooms]
			 JOIN [Trips]
				  ON [Trips].[RoomId] = [Rooms].[Id]
			 WHERE [Rooms].[HotelId] = @HotelId
				  AND (@Date BETWEEN [ArrivalDate] AND [ReturnDate])
		  )
		  ORDER BY
			 [Rooms].[Price] DESC
	   )

	   IF @roomId IS NOT NULL
	   BEGIN
		  DECLARE @roomType NVARCHAR(128)=
		  (
			 SELECT
				[Rooms].[Type]
			 FROM  [Rooms]
			 WHERE [Rooms].[Id] = @roomId
		  )

		  DECLARE @roomBeds INT=
		  (
			 SELECT
				[Rooms].[Beds]
			 FROM  [Rooms]
			 WHERE [Rooms].[Id] = @roomId
		  )

		  DECLARE @roomPrice DECIMAL(18, 2)=
		  (
			 SELECT
				[Rooms].[Price]
			 FROM  [Rooms]
			 WHERE [Rooms].[Id] = @roomId
		  )

		  DECLARE @hotelBaseRate DECIMAL(18, 2)=
		  (
			 SELECT
				[Hotels].[BaseRate]
			 FROM  [Hotels]
			 WHERE [Hotels].[Id] = @HotelId
		  )
		  --CONCAT('Room ', @roomId, ': ', @roomType, ' (', @roomBeds, ' beds) - $', (@hotelBaseRate + @roomPrice) * @People)
		  DECLARE @fMes NVARCHAR(1028)= FORMATMESSAGE('Room %i: ', @roomId) + @roomType
		  DECLARE @sMes NVARCHAR(1028)= concat(' (', @roomBeds, ' beds) - $', format((@hotelBaseRate + @roomPrice) * @People, 'f2'))
		  RETURN @fMes + @sMes

	   END

	   RETURN 'No rooms available'
    END
GO

SELECT
    [dbo].[udf_GetAvailableRoom](112, '2011-12-17', 2)

SELECT
    [dbo].[udf_GetAvailableRoom](94, '2015-07-26', 3)

SELECT
    *
FROM  [Rooms]
JOIN [Trips]
	 ON [Trips].[RoomId] = [Rooms].[Id]
WHERE [Rooms].[HotelId] = 112
	 AND (('2011-12-17' BETWEEN [ArrivalDate] AND [ReturnDate])
		 AND [Trips].[CancelDate] IS NOT NULL)
ORDER BY
    [Rooms].[Price] DESC

SELECT
    *
FROM  [Rooms]
WHERE [Rooms].[HotelId] = 112
	 AND [Rooms].[Beds] >= 2
	 AND [Rooms].[Id] NOT IN
(
    SELECT
	   [Rooms].[Id]
    FROM  [Rooms]
    JOIN [Trips]
		ON [Trips].[RoomId] = [Rooms].[Id]
    WHERE [Rooms].[HotelId] = 112
		AND ('2011-12-17' BETWEEN [ArrivalDate] AND [ReturnDate])
)
--
--12.Switch Room----------------------------------------------------------------

CREATE OR ALTER PROC [usp_SwitchRoom]
    @TripId       INT,
    @TargetRoomId INT
AS
BEGIN
    DECLARE @currentHotelId INT=
    (
	   SELECT
		  [Rooms].[HotelId]
	   FROM  [Rooms]
	   JOIN [Trips]
		    ON [Rooms].[Id] = [Trips].[RoomId]
	   WHERE [Trips].[Id] = @TripId
    )

    DECLARE @newHotelId INT=
    (
	   SELECT
		  [Hotels].[Id]
	   FROM  [Rooms]
	   JOIN [Hotels]
		    ON [Rooms].[HotelId] = [Hotels].[Id]
	   WHERE [Rooms].[Id] = @TargetRoomId
    )

    IF @currentHotelId <> @newHotelId
    BEGIN
	   RAISERROR('Target room is in another hotel!', 16, 1)
	   RETURN
    END

    DECLARE @newRoomBeds INT=
    (
	   SELECT
		  [Rooms].[Beds]
	   FROM  [Rooms]
	   WHERE [Rooms].[Id] = @TargetRoomId
    )

    DECLARE @bedsNeeded INT=
    (
	   SELECT
		  COUNT(*)
	   FROM  [AccountsTrips]
	   WHERE [AccountsTrips].[TripId] = @TripId
    )

    IF @newRoomBeds < @bedsNeeded
    BEGIN
	   RAISERROR('Not enough beds in target room!', 16, 1)
	   RETURN
    END

    UPDATE [Trips]
	 SET
		[Trips].[RoomId] = @TargetRoomId
    WHERE
	   [Trips].[Id] = @TripId
END

    EXEC [usp_SwitchRoom]
	    10,
	    11
    SELECT
	   [RoomId]
    FROM  [Trips]
    WHERE [Id] = 10

    EXEC [usp_SwitchRoom]
	    10,
	    7

    EXEC [usp_SwitchRoom]
	    10,
	    8