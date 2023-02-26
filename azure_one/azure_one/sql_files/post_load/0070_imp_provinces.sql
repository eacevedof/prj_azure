UPDATE [local_staging].[dbo].[imp_provinces] SET provinces_id=NULL;

UPDATE imp
SET provinces_id = mt.id
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_provinces] imp
ON mt.id = imp.provinces_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[provinces]
(
 name, 
 iso2, iso3, phone_code,
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) name,
    imp.uuid iso2, imp.uuid iso3, imp.uuid phone_code,
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
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.provinces_id IS NULL;
;