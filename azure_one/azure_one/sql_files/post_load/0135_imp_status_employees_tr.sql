UPDATE mt
SET
    mt.status_name = CONVERT(VARCHAR(255), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[status_employees_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as status_employees_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(255),vc_tr.tr_i) as tr
    FROM view_status_employees_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) imp
ON mt.status_employees_employee_status = imp.status_employees_id
AND mt.locale = imp.locale
WHERE 1=1
;

INSERT INTO [local_laciahub].[dbo].[status_employees_tr]
(status_employees_employee_status, locale, status_name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as status_employees_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(255),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM view_status_employees_tr [vc_tr]
    INNER JOIN view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[status_employees_tr] mt
    WHERE 1=1
    AND mt.status_employees_employee_status = imp.status_employees_id
    AND mt.locale = imp.locale
)
;