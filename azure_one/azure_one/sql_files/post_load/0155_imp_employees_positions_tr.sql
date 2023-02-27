UPDATE mt
SET
    mt.position_name = CONVERT(VARCHAR(255), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees_positions_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as employees_positions_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(255),vc_tr.tr_i) as tr
    FROM view_employees_positions_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.employees_positions_id = imp.employees_positions_id
AND mt.locale = imp.locale
WHERE 1=1
;


INSERT INTO [local_laciahub].[dbo].[employees_positions_tr]
(employees_positions_id, locale, position_name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as employees_positions_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(255),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM view_employees_positions_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[employees_positions_tr] mt
    WHERE 1=1
    AND mt.employees_positions_id = imp.employees_positions_id
    AND mt.locale = imp.locale
)
