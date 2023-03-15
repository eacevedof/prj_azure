-- USE [local_staging]; 

CREATE VIEW [dbo].[view_keys_and_values_tr]
AS
SELECT imp.entity_type, imp.entity_id as mt_id, 1 tr_num, imp.tr_v1_1 as tr_i
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_1,'')!=''
    
UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 2 tr_num, imp.tr_v1_2
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_2,'')!=''

UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 3 tr_num, imp.tr_v1_3
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_3,'')!=''

UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 4 tr_num, imp.tr_v1_4
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_4,'')!=''

UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 5 tr_num, imp.tr_v1_5
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_5,'')!=''

UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 6 tr_num, imp.tr_v1_6
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_6,'')!=''
    
UNION
SELECT imp.entity_type, imp.entity_id as mt_id, 7 tr_num, imp.tr_v1_7
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_7,'')!=''

UNION
SELECT imp.entity_type, imp.entity_id as mt_id, 8 tr_num, imp.tr_v1_8
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_8,'')!=''

UNION

SELECT imp.entity_type, imp.entity_id as mt_id, 9 tr_num, imp.tr_v1_9
FROM [local_staging].[dbo].[imp_keys_and_values] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND COALESCE(imp.tr_v1_9,'')!=''
;