CREATE TABLE [dbo].[StarshipClass]
(
    [Id]             INT           NOT NULL PRIMARY KEY IDENTITY (1, 1),
    [Name]           NVARCHAR(256) NOT NULL,
    [Description]    NVARCHAR(MAX) NULL,
    [FactionId]      INT           NULL,
    [Length]         INT           NULL,
    [Width]          INT           NULL,
    [Depth]          INT           NULL,
    [Decks]          INT           NULL,
    [Height]         INT           NULL,
    [MaxWarpSpeed]   INT           NULL,
    [Crew]           INT           NULL,
    [CargoCapacity]  INT           NULL,
    [StarshipType]   INT           NULL,
    [EnteredService] DATETIME2     NOT NULL,
    [ExitedService]  DATETIME2     NULL,
    [Active]         BIT           NOT NULL DEFAULT 1,
    [CreatedAt]      DATETIME2     NOT NULL CONSTRAINT DF_StarshipClass_CreatedAt DEFAULT (GETUTCDATE()),
    [LastUpdatedAt]  DATETIME2     NOT NULL CONSTRAINT DF_StarshipClass_LastUpdatedAt DEFAULT (GETUTCDATE())
);

GO
ALTER TABLE dbo.StarshipClass
  ADD CONSTRAINT FK_StarshipClass_Faction
  FOREIGN KEY (FactionId) REFERENCES dbo.Faction (Id);

GO
CREATE UNIQUE INDEX IX_StarshipClass_Name ON dbo.StarshipClass (Name);
