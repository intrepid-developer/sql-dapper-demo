CREATE TABLE [dbo].[StarshipCaptain]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [StarshipId] INT NOT NULL,
    [CaptainId] INT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT StarshipCaptain_CreatedAt(GETUTCDATE())
)

GO
CREATE FOREIGN KEY FK_StarshipCaptain_Starship FOREIGN KEY (StarshipId) REFERENCES dbo.Starship(Id) ON DELETE CASCADE;

GO
CREATE FOREIGN KEY FK_StarshipCaptain_Captain FOREIGN KEY (CaptainId) REFERENCES dbo.Captain(Id) ON DELETE CASCADE;
