SELECT COUNT(*)
FROM ShortLinks
WHERE DirectLink NOT LIKE '%dramaday%' 
AND DirectLink NOT LIKE '%filecrypt%'
AND DirectLink IS NOT NULL;

select * from ShortLinks;

SELECT * FROM ShortLinks
SELECT TOP 1 * FROM EpVersion
SELECT TOP 1 * FROM Media
SELECT TOP 1 * FROM Seasons
SELECT TOP 1 * FROM MediaVersions
SELECT TOP 1 * FROM Episodes
SELECT TOP 1 * FROM BatchEpisodes
select top 1 * from Short

select * from UknownEpisodes;


WITH temp AS (
    SELECT 
        be.Id AS be_Id,  -- BatchEpisodes Id
        be.RangeStart, 
        be.RangeEnd, 
        ev.Id AS ev_Id,  -- EpVersion Id
        ev.EpisodeVerisonName, 
        ev.EpisodeId, 
        sl.Id AS sl_Id,  -- ShortLinks Id
        sl.Host, 
        sl.LinkUrl, 
        sl.EpVersionId, 
        sl.DirectLink
    FROM BatchEpisodes be
    JOIN EpVersion ev ON ev.EpisodeId = be.Id
    JOIN ShortLinks sl ON sl.EpVersionId = ev.Id
)
SELECT * FROM temp
where ((select count(*) ShortLinks where EpVersionId = temp.EpVersionId) - (select count(*) ShortLinks where EpVersionId = temp.EpVersionId AND DirectLink IS NULL)) = 0 AND LinkUrl NOT LIKE '%pixeldrain%' AND LinkUrl NOT LIKE '%dddrive%';



SELECT
    m.DramaDayId, 
    m.KrTitle, 
    m.EnTitle, 
    s.SeasonNumber,
    mv.MediaVersionName,
    ev.EpisodeVerisonName,
    sl.Host, 
    sl.LinkUrl, 
    sl.EpVersionId, 
    sl.DirectLink
FROM Media m 
JOIN Seasons s ON m.DramaDayId = s.MediaDramaDayId
JOIN MediaVersions mv ON s.SeasonNumber = mv.SeasonId
JOIN Episodes e ON mv.Id = e.MediaVersionId
LEFT JOIN EpVersion ev ON e.Id = ev.EpisodeId
LEFT JOIN ShortLinks sl ON ev.Id = sl.EpVersionId
WHERE DramaDayId = 18213;











