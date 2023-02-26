UPDATE [local_staging].[dbo].[imp_cities] SET cities_id=NULL;

UPDATE imp
SET imp.cities_id = mt.id
FROM [local_laciahub].[dbo].[cities]  mt
INNER JOIN [local_staging].[dbo].[imp_cities] imp
-- ON mt.province_name = imp.val
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.provinces_id = mt.id
FROM [local_laciahub].[dbo].[provinces]  mt
INNER JOIN [local_staging].[dbo].[imp_cities] imp
-- todo 
ON mt.id = imp.provinces_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_cities] SET nok = 1 WHERE provinces_id IS NULL
;

UPDATE mt
SET
mt.province_name = CONVERT(VARCHAR(50), imp.val),
mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[cities]  mt
INNER JOIN [local_staging].[dbo].[imp_cities] imp
ON mt.id = imp.cities_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[cities]
(
provinces_id,
province_name, 
-- province_code, no es obligatorio
created_at
)
SELECT
    imp.provinces_id,
    CONVERT(VARCHAR(50),imp.val) province_name,
    -- imp.uuid province_code,
    -- 'n. p.' province_code,
    GETDATE()
FROM [local_staging].[dbo].[imp_cities] imp
LEFT JOIN [local_laciahub].[dbo].[cities] mt
ON mt.id = imp.cities_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.cities_id = mt.id
FROM [local_laciahub].[dbo].[cities] mt
INNER JOIN [local_staging].[dbo].[imp_cities] imp
ON mt.province_name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.cities_id IS NULL;
;