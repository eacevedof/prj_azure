-- cleaning first line
DELETE FROM [local_staging].[dbo].[imp_languages] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_languages] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_countries] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_provinces] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_provinces] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_cities] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_cities] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_companies] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_companies] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_languages_company_custom] WHERE companies_uuid='companies_uuid';
DELETE FROM [local_staging].[dbo].[imp_languages_company_custom] WHERE TRIM(COALESCE(companies_uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_user_types] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_user_types] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_status_employees] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_status_employees] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_employees_departments] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_employees_departments] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_employees_positions] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_employees_positions] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_roles] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_roles] WHERE TRIM(COALESCE(uuid,''))='';

UPDATE [local_staging].[dbo].[imp_employees] SET nok=1 WHERE uuid='uuid';
UPDATE [local_staging].[dbo].[imp_employees] SET nok=1 WHERE TRIM(COALESCE(uuid,''))='';

