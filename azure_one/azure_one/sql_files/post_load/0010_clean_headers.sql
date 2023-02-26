DELETE FROM [local_staging].[dbo].[imp_languages] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_languages] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_countries] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_provinces] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_cities] WHERE uuid='uuid';
DELETE FROM [local_staging].[dbo].[imp_cities] WHERE TRIM(COALESCE(uuid,''))='';

DELETE FROM [local_staging].[dbo].[imp_languages_company] WHERE companies_uuid='companies_uuid';
DELETE FROM [local_staging].[dbo].[imp_languages_company] WHERE TRIM(COALESCE(companies_uuid,''))='';