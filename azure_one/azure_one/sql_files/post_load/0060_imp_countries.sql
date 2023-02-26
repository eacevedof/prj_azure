UPDATE [local_staging].[dbo].[imp_countries] SET countries_id=NULL;

UPDATE imp
SET countries_id = mt.id
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_countries] imp
-- esto hay que cambiarlo a por uuid
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    -- mt.iso2 = CONVERT(VARCHAR(255), imp.iso2),
    -- mt.iso3 = CONVERT(VARCHAR(255), imp.iso3),
    -- mt.phone_code = CONVERT(VARCHAR(255), imp.phone_code),
    -- mt.codesap = imp.codesap,
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_countries] imp
ON mt.id = imp.countries_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[countries]
(
 name, 
 iso2, iso3, phone_code,
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) name,
    imp.uuid iso2, imp.uuid iso3, imp.uuid phone_code,
    -- CONVERT(VARCHAR(255),imp.iso2),
    -- CONVERT(VARCHAR(255),imp.iso3),
    -- CONVERT(VARCHAR(255),imp.phone_code),
    GETDATE()
FROM [local_staging].[dbo].[imp_countries] imp
LEFT JOIN [local_laciahub].[dbo].[countries] mt
ON mt.id = imp.countries_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.countries_id = mt.id
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_countries] imp
-- esto hay que cambiarlo a por uuid
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.countries_id IS NULL;
;