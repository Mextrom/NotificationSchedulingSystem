CREATE PROCEDURE [dbo].[GetScheduleByCompanyId]
	@companyId uniqueidentifier
AS
	SELECT SendDate
	FROM [dbo].[Schedule]
	WHERE CompanyId = @companyId
	ORDER BY SendDate ASC
RETURN 0
