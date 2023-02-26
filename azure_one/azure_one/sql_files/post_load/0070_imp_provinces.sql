UPDATE [local_staging].[dbo].[imp_provinces] SET provinces_id=NULL;

UPDATE imp
SET imp.provinces_id = mt.id
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
-- ON mt.province_name = imp.val
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.countries_id = mt.id
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
-- todo 
ON mt.id = imp.countries_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_provinces] SET nok = 1 WHERE countries_id IS NULL
;

UPDATE mt
SET
mt.province_name = CONVERT(VARCHAR(255), imp.val),
mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
ON mt.id = imp.provinces_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[provinces]
(
countries_id,
province_name, 
province_code,
created_at
)
SELECT
    imp.countries_id,
    CONVERT(VARCHAR(255),imp.val) province_name,
    imp.uuid province_code,
    GETDATE()
FROM [local_staging].[dbo].[imp_provinces] imp
LEFT JOIN [local_laciahub].[dbo].[provinces] mt
ON mt.id = imp.provinces_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.provinces_id = mt.id
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
ON mt.province_name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.provinces_id IS NULL;
;