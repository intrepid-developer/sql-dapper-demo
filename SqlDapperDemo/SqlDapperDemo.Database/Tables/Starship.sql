CREATE TABLE [dbo].[Starship]
(
    [Id]             INT           NOT NULL PRIMARY KEY IDENTITY (1, 1),
    [Name]           NVARCHAR(255) NOT NULL,
    [Registration]   NVARCHAR(255) NOT NULL,
    [Commissioned]   DATETIME2     NOT NULL,
    [Decommissioned] DATETIME2     NULL,
    [ClassId]        INT           NOT NULL,
    [CreatedAt]      DATETIME2     NOT NULL DEFAULT DF_Starship_CreatedAt(GETUTCDATE()),
    [LastUpdatedAt]  DATETIME2     NOT NULL DEFAULT DF_Starship_LastUpdatedAt(GETUTCDATE()),
)