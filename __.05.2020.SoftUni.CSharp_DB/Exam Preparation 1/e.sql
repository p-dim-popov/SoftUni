

CREATE DATABASE [Airport]
GO

USE Airport
GO

--5.The "Tr" Planes-----------------------------------------------------

SELECT
    [Planes].[Id],
    [Planes].[Name],
    [Planes].[Seats],
    [Planes].[Range]
FROM  [Planes]
WHERE [Planes].[Name] LIKE '%[Tt][Rr]%'
ORDER BY
    [Planes].[Id],
    [Planes].[Name],
    [Planes].[Seats],
    [Planes].[Range]

--6.Flight Profits--------------------------------------------------------

SELECT
    [Tickets].[FlightId],
    SUM([Tickets].[Price]) AS [TotalPrice]
FROM [Tickets]
GROUP BY
    [Tickets].[FlightId]
ORDER BY
    [TotalPrice] DESC,
    [Tickets].[FlightId] ASC

--7.Passenger Trips--------------------------------------------------------

SELECT
    [Passengers].[FirstName] + ' ' + [Passengers].[LastName] AS [FullName],
    [Flights].[Origin],
    [Flights].[Destination]
FROM [Passengers]
JOIN [Tickets]
	ON [Passengers].[Id] = [Tickets].[PassengerId]
JOIN [Flights]
	ON [Tickets].[FlightId] = [Flights].[Id]
ORDER BY
    [FullName] ASC,
    [Flights].[Origin] ASC,
    [Flights].[Destination] ASC

--8.Non Adventures People----------------------------------------------------

SELECT
    [Passengers].[FirstName],
    [Passengers].[LastName],
    [Passengers].[Age]
FROM  [Passengers]
LEFT JOIN [Tickets]
	 ON [Passengers].[Id] = [Tickets].[PassengerId]
WHERE [Tickets].[Id] IS NULL
ORDER BY
    [Passengers].[Age] DESC,
    [Passengers].[FirstName] ASC,
    [Passengers].[LastName] ASC

--9.Full Info------------------------------------------------------------------

SELECT
    [Passengers].[FirstName] + ' ' + [Passengers].[LastName] AS [fullname],
    [Planes].[Name] AS                                          [planename],
    [Flights].[Origin] + ' - ' + [Flights].[Destination] AS     [trip],
    [LuggageTypes].[Type]
FROM [Passengers]
JOIN [Tickets]
	ON [Passengers].[Id] = [Tickets].[PassengerId]
JOIN [Flights]
	ON [Tickets].[FlightId] = [Flights].[Id]
JOIN [Planes]
	ON [Flights].[PlaneId] = [Planes].[Id]
JOIN [Luggages]
	ON [Tickets].[LuggageId] = [Luggages].[Id]
JOIN [LuggageTypes]
	ON [Luggages].[LuggageTypeId] = [LuggageTypes].[Id]
ORDER BY
    [fullname] ASC,
    [Planes].[Name] ASC,
    [Flights].[Origin] ASC,
    [Flights].[Destination] ASC,
    [LuggageTypes].[Type] ASC

--10.PSP----------------------------------------------------------------------

SELECT
    [Planes].[Name],
    [Planes].[Seats],
    COUNT([Tickets].[PassengerId]) AS [passCount]
FROM [Planes]
LEFT JOIN [Flights]
	ON [Planes].[Id] = [Flights].[PlaneId]
LEFT JOIN [Tickets]
	ON [Flights].[Id] = [Tickets].[FlightId]
GROUP BY
    [Planes].[Name],
    [Planes].[Seats]
ORDER BY
    [passCount] DESC,
    [Planes].[Name] ASC,
    [Planes].[Seats] ASC