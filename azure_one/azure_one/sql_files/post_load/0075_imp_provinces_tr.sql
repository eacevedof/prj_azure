
UPDATE mt
SET
    mt.province_name = CONVERT(VARCHAR(50), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[provinces_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as provinces_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(50),vc_tr.tr_i) as tr
    FROM [local_staging].[dbo].view_provinces_tr [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.provinces_id = imp.provinces_id
AND mt.locale = imp.locale
WHERE 1=1
;

-- antes de ejecutar las trads debo cargar todas las maestras
INSERT INTO [local_laciahub].[dbo].[provinces_tr]
(provinces_id, locale, province_name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as provinces_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(50),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM [local_staging].[dbo].view_provinces_tr [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[provinces_tr] mt
    WHERE 1=1
    AND mt.provinces_id = imp.provinces_id
    AND mt.locale = imp.locale
)
