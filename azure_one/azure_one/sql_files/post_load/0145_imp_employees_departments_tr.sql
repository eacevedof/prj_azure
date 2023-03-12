UPDATE mt
SET
    mt.department_name = CONVERT(VARCHAR(45), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees_departments_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as employees_departments_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(45),vc_tr.tr_i) as tr
    FROM [local_staging].[dbo].view_employees_departments_tr [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.employees_departments_id = imp.employees_departments_id
AND mt.locale = imp.locale
WHERE 1=1
;


INSERT INTO [local_laciahub].[dbo].[employees_departments_tr]
(employees_departments_id, locale, department_name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as employees_departments_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(45),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM [local_staging].[dbo].view_employees_departments_tr [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[employees_departments_tr] mt
    WHERE 1=1
    AND mt.employees_departments_id = imp.employees_departments_id
    AND mt.locale = imp.locale
)
