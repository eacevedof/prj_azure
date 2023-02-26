```sql
DROP VIEW [dbo].[view_languages_index];
CREATE VIEW [dbo].[view_languages_index]
AS
SELECT
DISTINCT imp.lang_from, imp.tr_num
FROM [local_staging].[dbo].[imp_languages_company] imp
WHERE 1=1
AND COALESCE(imp.tr_num,'')!=''
;

DROP VIEW [dbo].[view_countries_tr];
CREATE VIEW [dbo].[view_countries_tr]
AS
SELECT imp.uuid, 1 tr_num, imp.tr_1 as tr_i
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_1,'')!=''
    
UNION

SELECT imp.uuid, 2 tr_num, imp.tr_2
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_2,'')!=''

UNION

SELECT imp.uuid, 3 tr_num, imp.tr_3
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_3,'')!=''

UNION

SELECT imp.uuid, 4 tr_num, imp.tr_4
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_4,'')!=''

UNION

SELECT imp.uuid, 5 tr_num, imp.tr_5
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_5,'')!=''

UNION

SELECT imp.uuid, 6 tr_num, imp.tr_6
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_6,'')!=''
    
UNION
SELECT imp.uuid, 7 tr_num, imp.tr_7
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_7,'')!=''

UNION
SELECT imp.uuid, 8 tr_num, imp.tr_8
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_8,'')!=''

UNION

SELECT imp.uuid, 9 tr_num, imp.tr_9
FROM [local_staging].[dbo].[imp_countries] imp
WHERE 1=1
AND COALESCE(imp.tr_9,'')!=''
```