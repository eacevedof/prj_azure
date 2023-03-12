CREATE VIEW [local_staging].[dbo].[view_employees_departments_tr]
AS
SELECT imp.uuid, imp.employees_departments_id as mt_id, 1 tr_num, imp.tr_1 as tr_i
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_1,'')!=''
    
UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 2 tr_num, imp.tr_2
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_2,'')!=''

UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 3 tr_num, imp.tr_3
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_3,'')!=''

UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 4 tr_num, imp.tr_4
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_4,'')!=''

UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 5 tr_num, imp.tr_5
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_5,'')!=''

UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 6 tr_num, imp.tr_6
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_6,'')!=''
    
UNION
SELECT imp.uuid, imp.employees_departments_id as mt_id, 7 tr_num, imp.tr_7
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_7,'')!=''

UNION
SELECT imp.uuid, imp.employees_departments_id as mt_id, 8 tr_num, imp.tr_8
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_8,'')!=''

UNION

SELECT imp.uuid, imp.employees_departments_id as mt_id, 9 tr_num, imp.tr_9
FROM [local_staging].[dbo].[imp_employees_departments] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_9,'')!=''
;