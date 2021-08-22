CREATE TABLE [dbo].[Schedule]
(
	[CompanyId] UNIQUEIDENTIFIER NOT NULL, 
    [SendDate] DATE NOT NULL, 
    CONSTRAINT [FK_Schedule_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]) 
)
