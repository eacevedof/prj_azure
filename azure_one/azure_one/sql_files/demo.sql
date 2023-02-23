SELECT * FROM countries
;
         
SELECT * FROM languages
;
         
UPDATE [local_staging].[dbo].[languages] 
SET codesap='xxx'
WHERE 1=1
;

UPDATE [local_staging].[dbo].[languages]
SET tenant_slug='fini'
WHERE 1=1
;