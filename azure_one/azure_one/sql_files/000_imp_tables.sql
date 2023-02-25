UPDATE [local_staging].[dbo].[imp_languages] 
SET codesap='xxx'
WHERE 1=1
;

UPDATE [local_staging].[dbo].[imp_languages]
SET tenant_slug='fini'
WHERE 1=1
;

SELECT * 
FROM [local_staging].[dbo].[imp_languages]