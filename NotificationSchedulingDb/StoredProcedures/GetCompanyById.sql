CREATE PROCEDURE [dbo].[GetCompanyById]
	@companyId uniqueidentifier
AS
	SELECT Id, Name, Number, Type, Market, IsScheduleCreated
	FROM [dbo].[Company]
	WHERE Id = @companyId
RETURN 0
