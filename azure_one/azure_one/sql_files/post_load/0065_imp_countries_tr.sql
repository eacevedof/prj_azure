-- antes de ejecutar las trads debo cargar todas las maestras
INSERT INTO [local_laciahub].[dbo].[countries_tr]
(countries_id, locale, name, created_at)

SELECT * 
FROM (
    SELECT 
        vc_tr.mt_id as countries_id, 
        vli.lang_from as locale, 
        vc_tr.tr_i as tr, 
        GETDATE() created_at 
    FROM view_countries_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
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
