

UPDATE imp
SET 
  imp.languages_company_custom_id = mt.id
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
-- ON mt.name = imp.val
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.city_id = mt.id
FROM [local_laciahub].[dbo].[cities] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.id = imp.city_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.country_id = mt.countries_id
FROM [local_laciahub].[dbo].[cities] mt1
INNER JOIN [local_laciahub].[dbo].[provinces] mt
ON mt1.provinces_id = mt.id
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt1.id = imp.city_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE city_id IS NULL;
UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE country_id IS NULL;
UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET nok = 1 WHERE company_type NOT IN ('owner','client','provider');


/*
[city_id],[country_id],[company_token],[company_type],[company_cod_int],[company_name],[company_address1],[company_address2],
[company_cp],[company_contact_person],
[company_contact_phone],[company_contact_email],[company_active],[link_expiration_days]
*/
UPDATE mt
SET
mt.city_id = imp.city_id,
mt.country_id = imp.country_id,
-- mt.company_token = VARCHAR(255) imp.company_token,
mt.company_type = CONVERT(VARCHAR(255),imp.company_type),
-- mt.company_cod_int = VARCHAR(45),
mt.company_name = CONVERT(VARCHAR(255),imp.company_name),
mt.company_address1 = CONVERT(VARCHAR(255),imp.company_address1),
mt.company_address2 = CONVERT(VARCHAR(255),imp.company_address2),
mt.company_cp = CONVERT(VARCHAR(10),imp.company_cp),
mt.company_contact_person = CONVERT(VARCHAR(45),imp.company_contact_person),
mt.company_contact_phone = CONVERT(VARCHAR(45),imp.company_contact_phone),
mt.company_contact_email = CONVERT(VARCHAR(45),imp.company_contact_email),
-- mt.company_active = 1
-- mt.link_expiration_days = 10
mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.id = imp.languages_company_custom_id
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_languages_company_custom]
SET 
company_token = (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-',''))) ),
company_cod_int = CONVERT(VARCHAR(9),(SELECT CONVERT(VARCHAR,id)+CONVERT(VARCHAR(45),CONVERT(INT,RAND()*1000000000)))),
company_active = 1,
link_expiration_days = 10
WHERE 1=1
AND languages_company_custom_id IS NULL
AND nok IS NULL
;



INSERT INTO [local_laciahub].[dbo].[languages_company_custom]
(
city_id, country_id, company_token, company_type, company_cod_int, company_name, company_address1,
company_address2, company_cp, company_contact_person, company_contact_phone, company_contact_email,
company_active, link_expiration_days, created_at
)
SELECT
    imp.city_id,
    imp.country_id,
    imp.company_token,
     CONVERT(VARCHAR(255),imp.company_type) company_type,
    imp.company_cod_int,
    CONVERT(VARCHAR(255),imp.company_name) company_name,
    CONVERT(VARCHAR(255),imp.company_address1) company_address1,
    CONVERT(VARCHAR(255),imp.company_address2) company_address2,
    CONVERT(VARCHAR(10),imp.company_cp) company_cp,
    CONVERT(VARCHAR(45),imp.company_contact_person) company_contact_person,
    CONVERT(VARCHAR(45),imp.company_contact_phone) company_contact_phone,
    CONVERT(VARCHAR(45),imp.company_contact_email) company_contact_email,
    imp.company_active,
    imp.link_expiration_days,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_languages_company_custom] imp
LEFT JOIN [local_laciahub].[dbo].[languages_company_custom] mt
ON mt.id = imp.languages_company_custom_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET 
  imp.languages_company_custom_id = mt.id,
  imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[languages_company_custom] mt
INNER JOIN [local_staging].[dbo].[imp_languages_company_custom] imp
ON mt.company_token = imp.company_token
WHERE 1=1
AND imp.nok IS NULL
AND imp.languages_company_custom_id IS NULL;
;