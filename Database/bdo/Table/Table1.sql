CREATE TABLE [dbo].[Table1]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userid] UNIQUEIDENTIFIER NULL, 
    [username] NVARCHAR(50) NULL, 
    [hashPassword] NVARCHAR(50) NULL, 
    [Groupid] NVARCHAR(50) NULL
)
