CREATE VIEW [dbo].[vwCaptain]
AS
WITH CommandHistory AS
(
    SELECT
        sc.CaptainId,
        sc.StarshipId,
        ROW_NUMBER() OVER (
            PARTITION BY sc.CaptainId
            ORDER BY
                CASE WHEN s.Decommissioned IS NULL THEN 0 ELSE 1 END,
                sc.CreatedAt DESC,
                sc.Id DESC
        ) AS AssignmentRank
    FROM dbo.StarshipCaptain AS sc
    INNER JOIN dbo.Starship AS s
        ON s.Id = sc.StarshipId
)
SELECT
    c.Name,
    c.Rank,
    c.HomePlanet,
    c.Born,
    c.Died,
    f.Name AS FactionName,
    f.Colour AS FactionColour,
    s.Name AS StarshipName,
    s.Registration AS StarshipRegistration,
    s.Commissioned AS StarshipCommissioned,
    s.Decommissioned AS StarshipDecommissioned
FROM dbo.Captain AS c
LEFT JOIN CommandHistory AS ch
    ON ch.CaptainId = c.Id
    AND ch.AssignmentRank = 1
LEFT JOIN dbo.Starship AS s
    ON s.Id = ch.StarshipId
LEFT JOIN dbo.StarshipClass AS cls
    ON cls.Id = s.ClassId
LEFT JOIN dbo.Faction AS f
    ON f.Id = cls.FactionId;
GO
