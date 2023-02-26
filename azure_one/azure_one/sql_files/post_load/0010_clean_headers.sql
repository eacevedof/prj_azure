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

DELETE FROM [local_staging].[dbo].[imp_languages_company_custom] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_languages_company_custom] WHERE TRIM(COALESCE(uuid,''))='';
