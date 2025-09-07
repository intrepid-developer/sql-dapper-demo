-- Seed starter data for demo purposes
-- Inserts are idempotent (guarded by IF NOT EXISTS)
SET NOCOUNT ON;

-- Factions
IF NOT EXISTS (SELECT 1 FROM dbo.Faction WHERE Name = 'United Federation of Planets')
BEGIN
  INSERT dbo.Faction (Name, Colour, CreatedAt, LastUpdatedAt)
  VALUES ('United Federation of Planets', 'Blue', GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Faction WHERE Name = 'Klingon Empire')
BEGIN
  INSERT dbo.Faction (Name, Colour, CreatedAt, LastUpdatedAt)
  VALUES ('Klingon Empire', 'Red', GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Faction WHERE Name = 'Cardassian Union')
BEGIN
  INSERT dbo.Faction (Name, Colour, CreatedAt, LastUpdatedAt)
  VALUES ('Cardassian Union', 'Gold', GETUTCDATE(), GETUTCDATE());
END;

DECLARE @FederationId INT = (SELECT Id FROM dbo.Faction WHERE Name = 'United Federation of Planets');
DECLARE @KlingonId    INT = (SELECT Id FROM dbo.Faction WHERE Name = 'Klingon Empire');
DECLARE @CardassianId INT = (SELECT Id FROM dbo.Faction WHERE Name = 'Cardassian Union');

-- Captains (a few from each faction)
IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'James T. Kirk')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('James T. Kirk', 'Captain', 'Earth', '2233-03-22', NULL, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'Jean-Luc Picard')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('Jean-Luc Picard', 'Captain', 'Earth', '2305-07-13', NULL, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'General Martok')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('General Martok', 'General', 'Qo''noS', '2336-01-01', NULL, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'Kang')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('Kang', 'Captain', 'Qo''noS', '2220-01-01', NULL, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'Gul Dukat')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('Gul Dukat', 'Gul', 'Cardassia Prime', '2333-01-01', NULL, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Captain WHERE Name = 'Gul Evek')
BEGIN
  INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt)
  VALUES ('Gul Evek', 'Gul', 'Cardassia Prime', '2325-01-01', NULL, GETUTCDATE(), GETUTCDATE());
END;

DECLARE @KirkId   INT = (SELECT Id FROM dbo.Captain WHERE Name = 'James T. Kirk');
DECLARE @PicardId INT = (SELECT Id FROM dbo.Captain WHERE Name = 'Jean-Luc Picard');
DECLARE @MartokId INT = (SELECT Id FROM dbo.Captain WHERE Name = 'General Martok');
DECLARE @KangId   INT = (SELECT Id FROM dbo.Captain WHERE Name = 'Kang');
DECLARE @DukatId  INT = (SELECT Id FROM dbo.Captain WHERE Name = 'Gul Dukat');
DECLARE @EvekId   INT = (SELECT Id FROM dbo.Captain WHERE Name = 'Gul Evek');

-- Starship classes
IF NOT EXISTS (SELECT 1 FROM dbo.StarshipClass WHERE Name = 'Constitution-class')
BEGIN
  INSERT dbo.StarshipClass (Name, Description, FactionId, EnteredService, Active, CreatedAt, LastUpdatedAt)
  VALUES ('Constitution-class', 'Federation heavy cruiser', @FederationId, '2245-01-01', 0, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipClass WHERE Name = 'Galaxy-class')
BEGIN
  INSERT dbo.StarshipClass (Name, Description, FactionId, EnteredService, Active, CreatedAt, LastUpdatedAt)
  VALUES ('Galaxy-class', 'Federation explorer', @FederationId, '2357-01-01', 0, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipClass WHERE Name = 'K''t''inga-class')
BEGIN
  INSERT dbo.StarshipClass (Name, Description, FactionId, EnteredService, Active, CreatedAt, LastUpdatedAt)
  VALUES ('K''t''inga-class', 'Klingon battlecruiser', @KlingonId, '2270-01-01', 0, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipClass WHERE Name = 'Bird-of-Prey')
BEGIN
  INSERT dbo.StarshipClass (Name, Description, FactionId, EnteredService, Active, CreatedAt, LastUpdatedAt)
  VALUES ('Bird-of-Prey', 'Klingon raider', @KlingonId, '2280-01-01', 1, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipClass WHERE Name = 'Galor-class')
BEGIN
  INSERT dbo.StarshipClass (Name, Description, FactionId, EnteredService, Active, CreatedAt, LastUpdatedAt)
  VALUES ('Galor-class', 'Cardassian warship', @CardassianId, '2320-01-01', 1, GETUTCDATE(), GETUTCDATE());
END;

DECLARE @ConstitutionId INT = (SELECT Id FROM dbo.StarshipClass WHERE Name = 'Constitution-class');
DECLARE @GalaxyId       INT = (SELECT Id FROM dbo.StarshipClass WHERE Name = 'Galaxy-class');
DECLARE @KtingaId       INT = (SELECT Id FROM dbo.StarshipClass WHERE Name = 'K''t''inga-class');
DECLARE @BopId          INT = (SELECT Id FROM dbo.StarshipClass WHERE Name = 'Bird-of-Prey');
DECLARE @GalorId        INT = (SELECT Id FROM dbo.StarshipClass WHERE Name = 'Galor-class');

-- Starships
IF NOT EXISTS (SELECT 1 FROM dbo.Starship WHERE Registration = 'NCC-1701')
BEGIN
  INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt)
  VALUES ('USS Enterprise', 'NCC-1701', '2245-01-01', NULL, @ConstitutionId, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Starship WHERE Registration = 'NCC-1701-D')
BEGIN
  INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt)
  VALUES ('USS Enterprise-D', 'NCC-1701-D', '2363-10-04', NULL, @GalaxyId, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Starship WHERE Registration = 'IKS-ROTARRAN')
BEGIN
  INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt)
  VALUES ('IKS Rotarran', 'IKS-ROTARRAN', '2373-01-01', NULL, @BopId, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Starship WHERE Registration = 'IKS-KRONOS-ONE')
BEGIN
  INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt)
  VALUES ('Kronos One', 'IKS-KRONOS-ONE', '2289-01-01', NULL, @KtingaId, GETUTCDATE(), GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Starship WHERE Registration = 'CUC-PRAKESH-01')
BEGIN
  INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt)
  VALUES ('CSS Prakesh', 'CUC-PRAKESH-01', '2360-01-01', NULL, @GalorId, GETUTCDATE(), GETUTCDATE());
END;

DECLARE @EnterpriseId   INT = (SELECT Id FROM dbo.Starship WHERE Registration = 'NCC-1701');
DECLARE @EnterpriseDId  INT = (SELECT Id FROM dbo.Starship WHERE Registration = 'NCC-1701-D');
DECLARE @RotarranId     INT = (SELECT Id FROM dbo.Starship WHERE Registration = 'IKS-ROTARRAN');
DECLARE @KronosOneId    INT = (SELECT Id FROM dbo.Starship WHERE Registration = 'IKS-KRONOS-ONE');
DECLARE @PrakeshId      INT = (SELECT Id FROM dbo.Starship WHERE Registration = 'CUC-PRAKESH-01');

-- Assign captains to ships
IF NOT EXISTS (SELECT 1 FROM dbo.StarshipCaptain WHERE StarshipId = @EnterpriseId AND CaptainId = @KirkId)
BEGIN
  INSERT dbo.StarshipCaptain (StarshipId, CaptainId, CreatedAt)
  VALUES (@EnterpriseId, @KirkId, GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipCaptain WHERE StarshipId = @EnterpriseDId AND CaptainId = @PicardId)
BEGIN
  INSERT dbo.StarshipCaptain (StarshipId, CaptainId, CreatedAt)
  VALUES (@EnterpriseDId, @PicardId, GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipCaptain WHERE StarshipId = @RotarranId AND CaptainId = @MartokId)
BEGIN
  INSERT dbo.StarshipCaptain (StarshipId, CaptainId, CreatedAt)
  VALUES (@RotarranId, @MartokId, GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipCaptain WHERE StarshipId = @KronosOneId AND CaptainId = @KangId)
BEGIN
  INSERT dbo.StarshipCaptain (StarshipId, CaptainId, CreatedAt)
  VALUES (@KronosOneId, @KangId, GETUTCDATE());
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StarshipCaptain WHERE StarshipId = @PrakeshId AND CaptainId = @DukatId)
BEGIN
  INSERT dbo.StarshipCaptain (StarshipId, CaptainId, CreatedAt)
  VALUES (@PrakeshId, @DukatId, GETUTCDATE());
END;
