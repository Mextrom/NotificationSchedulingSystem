CREATE PROCEDURE [dbo].[AddCompany]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(50),
	@number VARCHAR(10),
    @type INT,
    @market INT,
    @isScheduleCreated BIT
AS
	INSERT INTO [dbo].[Company]
           ([Id]
           ,[Name]
           ,[Number]
           ,[Type]
           ,[Market]
           ,[IsScheduleCreated])
     VALUES
           (@id
           ,@name
           ,@number
           ,@type
           ,@market
           ,@isScheduleCreated)
RETURN 0
