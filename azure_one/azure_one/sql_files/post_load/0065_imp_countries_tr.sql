UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[countries_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as countries_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(255),vc_tr.tr_i) as tr
    FROM [local_staging].[dbo].[view_countries_tr] [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.countries_id = imp.countries_id
AND mt.locale = imp.locale
WHERE 1=1
-- AND imp.nok IS NULL
;


-- antes de ejecutar las trads debo cargar todas las maestras
INSERT INTO [local_laciahub].[dbo].[countries_tr]
(countries_id, locale, name, created_at)

SELECT * 
FROM (
    -- error: paises repetidos por nombre. Por eso necesito uuid
    SELECT DISTINCT
        vc_tr.mt_id as countries_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(255),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM [local_staging].[dbo].view_countries_tr [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[countries_tr] mt
    WHERE 1=1
    AND mt.countries_id = imp.countries_id
    AND mt.locale = imp.locale
)
