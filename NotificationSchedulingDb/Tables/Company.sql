CREATE TABLE [dbo].[Company]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Number] VARCHAR(10) NOT NULL, 
    [Type] INT NOT NULL, 
    [Market] INT NOT NULL, 
    [IsScheduleCreated] BIT NOT NULL
)
