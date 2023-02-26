UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[user_types_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as user_types_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(255),vc_tr.tr_i) as tr
    FROM view_user_types_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.user_types_id = imp.user_types_id
AND mt.locale = imp.locale
WHERE 1=1
;


-- antes de ejecutar las trads debo cargar todas las maestras
INSERT INTO [local_laciahub].[dbo].[user_types_tr]
(user_types_id, locale, name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as user_types_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(255),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM view_user_types_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[user_types_tr] mt
    WHERE 1=1
    AND mt.user_types_id = imp.user_types_id
    AND mt.locale = imp.locale
)
