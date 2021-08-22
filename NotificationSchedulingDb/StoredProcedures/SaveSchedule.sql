CREATE PROCEDURE [dbo].[SaveSchedule]
	@companyId UNIQUEIDENTIFIER,
	@dates VARCHAR(MAX)
AS
	INSERT INTO [dbo].[Schedule]([CompanyId], [SendDate])
	SELECT @companyId, CAST(sendDate.value AS DATE)
	FROM STRING_SPLIT(@dates, ',') sendDate

	UPDATE [dbo].[Company]
	SET IsScheduleCreated = 1
	WHERE Id = @companyId
RETURN 0
