

USE master
GO

CREATE DATABASE [Airport]
GO

USE Airport
GO

--1.Database Design-------------------------------------------------

CREATE TABLE [Planes]
(
    [Id]    INT IDENTITY PRIMARY KEY,
    [Name]  VARCHAR(30) NOT NULL,
    [Seats] INT NOT NULL,
    [Range] INT NOT NULL
)

CREATE TABLE [Flights]
(
    [Id]            INT IDENTITY PRIMARY KEY,
    [DepartureTime] DATETIME,
    [ArrivalTime]   DATETIME,
    [Origin]        VARCHAR(50) NOT NULL,
    [Destination]   VARCHAR(50) NOT NULL,
    [PlaneId]       INT FOREIGN KEY REFERENCES [Planes](
    [Id]) NOT NULL
)

CREATE TABLE [Passengers]
(
    [Id]         INT IDENTITY PRIMARY KEY,
    [FirstName]  VARCHAR(30) NOT NULL,
    [LastName]   VARCHAR(30) NOT NULL,
    [Age]        INT NOT NULL,
    [Address]    VARCHAR(30) NOT NULL,
    [PassportId] CHAR(11) NOT NULL
)

CREATE TABLE [LuggageTypes]
(
    [Id]   INT IDENTITY PRIMARY KEY,
    [Type] VARCHAR(30) NOT NULL
)

CREATE TABLE [Luggages]
(
    [Id]            INT IDENTITY PRIMARY KEY,
    [LuggageTypeId] INT FOREIGN KEY REFERENCES [LuggageTypes](
    [Id]) NOT NULL,
    [PassengerId]   INT FOREIGN KEY REFERENCES [Passengers](
    [Id]) NOT NULL
)

CREATE TABLE [Tickets]
(
    [Id]          INT IDENTITY PRIMARY KEY,
    [PassengerId] INT FOREIGN KEY REFERENCES [Passengers](
    [Id]) NOT NULL,
    [FlightId]    INT FOREIGN KEY REFERENCES [Flights](
    [Id]) NOT NULL,
    [LuggageId]   INT FOREIGN KEY REFERENCES [Luggages](
    [Id]) NOT NULL,
    [Price]       DECIMAL(18, 2) NOT NULL
)

/*
Section 2. DML (10 pts)
Before you start, you must import “DataSet-Airport.sql”. 
If you have created the structure correctly, 
the data should be successfully inserted without any errors.
*/

--2.Insert--------------------------------------------------------------

INSERT INTO [Planes]
(
    [Planes].[Name],
    [Planes].[Seats],
    [Planes].[Range]
)
VALUES
    (
    'Airbus 336', 112, 5132
    ),
    (
    'Airbus 330', 432, 5325
    ),
    (
    'Boeing 369', 231, 2355
    ),
    (
    'Stelt 297', 254, 2143
    ),
    (
    'Boeing 338', 165, 5111
    ),
    (
    'Airbus 558', 387, 1342
    ),
    (
    'Boeing 128', 345, 5541
    )

INSERT INTO [LuggageTypes]
VALUES
    (
    'Crossbody Bag'
    ),
    (
    'School Backpack'
    ),
    (
    'Shoulder Bag'
    )

--3.Update-------------------------------------------------------------------

UPDATE [Tickets]
  SET
	 [Tickets].[Price]*=1.13
FROM   [Tickets]
JOIN [Flights]
	  ON [Tickets].[FlightId] = [Flights].[Id]
WHERE
    [Flights].[Destination] = 'Carlsbad'

--4.Delete---------------------------------Ayn Halagim-------------------

DELETE FROM [Tickets]
WHERE
    [Tickets].[FlightId] IN
(
    SELECT
	   [Flights].[Id]
    FROM  [Flights]
    WHERE [Flights].[Destination] = 'Ayn Halagim'
)

DELETE FROM [Flights]
WHERE
    [Flights].[Destination] = 'Ayn Halagim'

--5.Trips------------------------------------------------------------------------

SELECT
    [Flights].[Origin],
    [Flights].[Destination]
FROM [Flights]
ORDER BY
    [Flights].[Origin] ASC,
    [Flights].[Destination] ASC

--6.The "Tr" Planes--------------------------------------------------------------

SELECT
    [Planes].[Id],
    [Planes].[Name],
    [Planes].[Seats],
    [Planes].[Range]
FROM  [Planes]
WHERE [Planes].[Name] LIKE '%tr%'
ORDER BY
    [Planes].[Id] ASC,
    [Planes].[Name] ASC,
    [Planes].[Seats] ASC

--7.Flight Profits--------------------------------------------------------------

SELECT
    [Tickets].[FlightId],
    SUM([Tickets].[Price]) AS [Price]
FROM [Tickets]
GROUP BY
    [Tickets].[FlightId]
ORDER BY
    [Price] DESC,
    [Tickets].[FlightId] ASC

--8.Passengers and Prices-------------------------------------------------------

SELECT TOP 10
    [Passengers].[FirstName],
    [Passengers].[LastName],
    [Tickets].[Price]
FROM [Passengers]
JOIN [Tickets]
	ON [Passengers].[Id] = [Tickets].[PassengerId]
ORDER BY
    [Tickets].[Price] DESC,
    [Passengers].[FirstName] ASC,
    [Passengers].[LastName] ASC

--9.Most Used Luggage's---------------------------------------------------------

SELECT
    [LuggageTypes].[Type],
    COUNT([Luggages].[Id]) AS [MostUsedLuggage]
FROM [LuggageTypes]
JOIN [Luggages]
	ON [LuggageTypes].[Id] = [Luggages].[LuggageTypeId]
GROUP BY
    [LuggageTypes].[Type]
ORDER BY
    [MostUsedLuggage] DESC,
    [LuggageTypes].[Type]

--10.Passenger Trips------------------------------------------------------

SELECT
    [Passengers].[FirstName] + ' ' + [Passengers].[LastName] AS [Full Name],
    [Flights].[Origin],
    [Flights].[Destination]
FROM [Passengers]
JOIN [Tickets]
	ON [Passengers].[Id] = [Tickets].[PassengerId]
JOIN [Flights]
	ON [Tickets].[FlightId] = [Flights].[Id]
ORDER BY
    [Full Name] ASC,
    [Flights].[Origin] ASC,
    [Flights].[Destination] ASC

--11.Non Adventures People----------------------------------------------------

SELECT
    [Passengers].[FirstName],
    [Passengers].[LastName],
    [Passengers].[Age]
FROM  [Passengers]
LEFT JOIN [Tickets]
	 ON [Passengers].[Id] = [Tickets].[PassengerId]
WHERE [Tickets].[Id] IS NULL
ORDER BY
    [age] DESC,
    [Passengers].[FirstName] ASC,
    [Passengers].[LastName] DESC

--12.Lost Luggage's--------------------------------------------------------------

SELECT
    [Passengers].[PassportId],
    [Passengers].[Address]
FROM  [Passengers]
LEFT JOIN [Luggages]
	 ON [Passengers].[Id] = [Luggages].[PassengerId]
WHERE [Luggages].[Id] IS NULL
ORDER BY
    [Passengers].[PassportId] ASC,
    [Passengers].[Address] ASC

--13.Count of Trips--------------------------------------------------------------

SELECT
    [Passengers].[FirstName],
    [Passengers].[LastName],
    COUNT([Tickets].[Id]) AS [TotalTrips]
FROM [Passengers]
LEFT JOIN [Tickets]
	ON [Passengers].[Id] = [Tickets].[PassengerId]
GROUP BY
    [Passengers].[FirstName],
    [Passengers].[LastName]
ORDER BY
    [TotalTrips] DESC,
    [Passengers].[FirstName] ASC,
    [Passengers].[LastName] DESC

--14.Full Info-------------------------------------------------------------------

SELECT
    [Passengers].[FirstName] + ' ' + [Passengers].[LastName] AS [FullName],
    [Planes].[Name] AS                                          [PlaneName],
    [Flights].[Origin] + ' - ' + [Flights].[Destination] AS     [Trip],
    [LuggageTypes].[Type] AS                                    [LuggageType]
FROM  [Passengers]
FULL JOIN [Tickets]
	 ON [Passengers].[Id] = [Tickets].[PassengerId]
FULL JOIN [Flights]
	 ON [Tickets].[FlightId] = [Flights].[Id]
FULL JOIN [Planes]
	 ON [Flights].[PlaneId] = [Planes].[Id]
JOIN [Luggages]
	 ON [Tickets].[LuggageId] = [Luggages].[Id]
FULL JOIN [LuggageTypes]
	 ON [Luggages].[LuggageTypeId] = [LuggageTypes].[Id]
WHERE [Tickets].[Id] IS NOT NULL
ORDER BY
    [FullName] ASC,
    [Planes].[Name] ASC,
    [Flights].[Origin] ASC,
    [Flights].[Destination] ASC,
    [LuggageType] ASC

-- "Debugging"-------------------------------------------
--SELECT
--    *
--FROM  [Passengers]
--JOIN [Tickets]
--	 ON [Passengers].[Id] = [Tickets].[PassengerId]
--JOIN [Flights]
--	 ON [Tickets].[FlightId] = [Flights].[Id]
--JOIN [Planes]
--	 ON [Flights].[PlaneId] = [Planes].[Id]
--JOIN [Luggages]
--	 ON [Tickets].[LuggageId] = [Luggages].[Id]
--WHERE [Passengers].[FirstName] = 'Adolphe'
--SELECT
--    *
--FROM  [Luggages]
--JOIN [LuggageTypes]
--	 ON [Luggages].[LuggageTypeId] = [LuggageTypes].[Id]
--WHERE [Luggages].[Id] = 36
--
--
--
--15.Most Expensive Trips---------------------------------------------

WITH cte_15
	AS (SELECT
		   [Passengers].[FirstName],
		   [Passengers].[LastName],
		   [Flights].[Destination],
		   [Tickets].[Price],
		   ROW_NUMBER() OVER(PARTITION BY [Tickets].[PassengerId]
		   ORDER BY
		   [Tickets].[Price] DESC) AS [PriceRankForPassenger]
	    FROM [Passengers]
	    JOIN [Tickets]
		    ON [Passengers].[Id] = [Tickets].[PassengerId]
	    JOIN [Flights]
		    ON [Tickets].[FlightId] = [Flights].[Id])
	SELECT
	    [cte_15].[FirstName],
	    [cte_15].[LastName],
	    [cte_15].[Destination],
	    [cte_15].[Price]
	FROM  [cte_15]
	WHERE [cte_15].[PriceRankForPassenger] = 1
	ORDER BY
	    [cte_15].[Price] DESC,
	    [cte_15].[FirstName] ASC,
	    [cte_15].[LastName] ASC,
	    [cte_15].[Destination] ASC
GO
--16.Destinations Info---------------------------------------------------------

SELECT
    [Flights].[Destination],
    COUNT([Tickets].[Id]) AS [TripsCount]
FROM [Flights]
FULL OUTER JOIN [Tickets]
	ON [Flights].[Id] = [Tickets].[FlightId]
GROUP BY
    [Flights].[Destination]
ORDER BY
    [TripsCount] DESC,
    [Flights].[Destination] ASC

--17. PSP------------------------------------------------------------------------

SELECT
    [Planes].[Name],
    [Planes].[Seats],
    COUNT([Tickets].[Id]) AS [PassCount]
FROM [Planes]
LEFT JOIN [Flights]
	ON [Planes].[Id] = [Flights].[PlaneId]
LEFT JOIN [Tickets]
	ON [Flights].[Id] = [Tickets].[FlightId]
GROUP BY
    [Planes].[Name],
    [Planes].[Seats]
ORDER BY
    [PassCount] DESC,
    [Planes].[Name] ASC,
    [Planes].[Seats] ASC

/*
Section 4. Programmability (20 pts)
*/

--18.Vacation---------------------------------------------------------------

DROP FUNCTION IF EXISTS
    [dbo].[udf_CalculateTickets]
GO

CREATE FUNCTION [udf_CalculateTickets]
(
    @origin      VARCHAR(MAX),
    @destination VARCHAR(MAX),
    @peopleCount INT
)
RETURNS VARCHAR(MAX)
AS
    BEGIN
	   IF @peopleCount <= 0
		  RETURN 'Invalid people count!'

	   DECLARE @flightId INT=
	   (
		  SELECT
			 [Flights].[Id]
		  FROM  [Flights]
		  WHERE [Flights].[Origin] = @origin
			   AND [Flights].[Destination] = @destination
	   )

	   IF @flightId IS NULL
		  RETURN 'Invalid flight!'

	   DECLARE @flightPrice DECIMAL(18, 2)=
	   (
		  SELECT
			 [Tickets].[Price]
		  FROM  [Tickets]
		  WHERE [Tickets].[FlightId] = @flightId
	   ) * @peopleCount * 1.0

	   RETURN CONCAT('Total price ', @flightPrice)
    END
GO

SELECT
    [dbo].[udf_CalculateTickets]('Kolyshley', 'Rancabolang', 33)

SELECT
    [dbo].[udf_CalculateTickets]('Kolyshley', 'Rancabolang', -1)

SELECT
    [dbo].[udf_CalculateTickets]('Invalid', 'Rancabolang', 33)

--19.Wrong Data--------------------------------------------------------------

CREATE OR ALTER PROC [usp_CancelFlights]
AS
BEGIN
    UPDATE [Flights]
	 SET
		[DepartureTime] = NULL,
		[ArrivalTime] = NULL
    WHERE
	   [ArrivalTime] - [DepartureTime] > 0
END
GO

EXEC [usp_CancelFlights]

--20. Deleted Planes------------------------------------------------------------

CREATE TABLE [DeletedPlanes]
(
    [Id]    INT,
    [Name]  VARCHAR(60),
    [Seats] INT,
    [Range] INT
)

CREATE TRIGGER [trig_DeletedPlanes] ON [Planes]
FOR DELETE
AS
    BEGIN
	   INSERT INTO [DeletedPlanes]
	   (
		  [DeletedPlanes].[Id],
		  [DeletedPlanes].[Name],
		  [DeletedPlanes].[Seats],
		  [DeletedPlanes].[Range]
	   )
			SELECT
			    [DELETED].[Id],
			    [DELETED].[Name],
			    [DELETED].[Seats],
			    [DELETED].[Range]
			FROM [DELETED]
    END
GO

DELETE [Tickets]
WHERE
    [FlightId] IN
(
    SELECT
	   [Id]
    FROM  [Flights]
    WHERE [PlaneId] = 8
)

DELETE FROM [Flights]
WHERE
    [PlaneId] = 8

DELETE FROM [Planes]
WHERE
    [Id] = 8