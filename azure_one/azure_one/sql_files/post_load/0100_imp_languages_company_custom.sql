
-- actualizo la compañia
UPDATE imp
SET 
  imp.companies_id = mt.id
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.id = imp.companies_uuid
WHERE 1=1
AND imp.nok IS NULL
;

-- el lang-id por locale
UPDATE imp
SET 
  imp.languages_company_id = mt.id
FROM [local_laciahub].[dbo].[languages] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.locale = imp.lang_from
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET 
  imp.languages_target_id = mt.id
FROM [local_laciahub].[dbo].[languages] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.locale = imp.lang_tr
WHERE 1=1
AND imp.nok IS NULL
;


UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE languages_id IS NULL;
UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE companies_id IS NULL;
UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE languages_target_id IS NULL;

UPDATE mt
SET
-- mt.languages_id = imp.languages_id,
-- mt.companies_id = imp.companies_id,
mt.language_name = CONVERT(VARCHAR(250),imp.language_name),
-- mt.languages_target_id = imp.languages_target_id
mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.languages_id = imp.languages_id
AND mt.companies_id = imp.companies_id
AND mt.languages_target_id = imp.languages_target_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[languages_company_custom]
(
languages_id, companies_id, language_name,
languages_target_id, -- by_default, 
created_at
)
SELECT
    imp.languages_id,
    imp.companies_id,
    CONVERT(VARCHAR(250),imp.language_name) language_name,
    CONVERT(VARCHAR(250),imp.languages_target_id) languages_target_id,
    -- 0 by_default,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_languages_company_custom] imp
WHERE 1=1
AND imp.nok IS NULL
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[languages_company_custom] mt
    WHERE 1=1
    AND mt.languages_id = imp.languages_id
    AND mt.companies_id = imp.companies_id
    AND mt.languages_target_id = imp.languages_target_id
)
;

-- por defecto todo a 0
UPDATE [local_laciahub].[dbo].[languages_company_custom] SET by_default=0;

-- dejo en por defecto siempre
UPDATE mt
SET mt.by_default = 1
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_laciahub].[dbo].[languages] mt2
ON mt.languages_id = mt2
WHERE 1=1
AND mt2.locale = 'en'
