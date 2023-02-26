

UPDATE imp
SET imp.companies_id = mt.id
FROM [local_laciahub].[dbo].[companies] mt
INNER JOIN [local_staging].[dbo].[imp_companies] imp
-- ON mt.name = imp.val
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.city_id = mt.id
FROM [local_laciahub].[dbo].[cities] mt
INNER JOIN [local_staging].[dbo].[imp_companies] imp
ON mt.id = imp.city_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.country_id = mt.countries_id
FROM [local_laciahub].[dbo].[cities] mt1
INNER JOIN [local_laciahub].[dbo].[provinces] mt
ON mt1.provinces_id = mt.id
INNER JOIN [local_staging].[dbo].[imp_companies] imp
ON mt1.id = imp.city_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_companies] SET nok = 1 WHERE city_id IS NULL;
UPDATE [local_staging].[dbo].[imp_companies] SET nok = 1 WHERE country_id IS NULL;

UPDATE mt
SET
mt.name = CONVERT(VARCHAR(50), imp.val),
mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[companies] mt
INNER JOIN [local_staging].[dbo].[imp_companies] imp
ON mt.id = imp.companies_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[companies]
(
city_id,
name, 
created_at
)
SELECT
    imp.city_id,
    CONVERT(VARCHAR(50),imp.val) name,
    GETDATE()
FROM [local_staging].[dbo].[imp_companies] imp
LEFT JOIN [local_laciahub].[dbo].[companies] mt
ON mt.id = imp.companies_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.companies_id = mt.id
FROM [local_laciahub].[dbo].[companies] mt
INNER JOIN [local_staging].[dbo].[imp_companies] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.companies_id IS NULL;
;