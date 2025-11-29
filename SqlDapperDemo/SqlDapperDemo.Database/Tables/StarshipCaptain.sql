CREATE TABLE [dbo].[StarshipCaptain]
(
    [Id] INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_StarshipCaptain PRIMARY KEY,
    [StarshipId] INT NOT NULL,
    [CaptainId] INT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL CONSTRAINT DF_StarshipCaptain_CreatedAt DEFAULT (GETUTCDATE())
);

GO
ALTER TABLE [dbo].[StarshipCaptain]
  ADD CONSTRAINT FK_StarshipCaptain_Starship
  FOREIGN KEY ([StarshipId]) REFERENCES [dbo].[Starship]([Id]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[StarshipCaptain]
  ADD CONSTRAINT FK_StarshipCaptain_Captain
  FOREIGN KEY ([CaptainId]) REFERENCES [dbo].[Captain]([Id]) ON DELETE CASCADE;

GO