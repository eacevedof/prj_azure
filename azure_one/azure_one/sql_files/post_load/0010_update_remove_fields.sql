UPDATE [local_staging].[dbo].[imp_keys_and_values] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
-- UPDATE [local_staging].[dbo].[imp_errors] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_keys_and_values] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_languages] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_countries] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_provinces] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_cities] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_companies] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
-- UPDATE [local_staging].[dbo].[imp_languages_company_custom] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_user_types] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_status_employees] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_employees_departments] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_employees_positions] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_roles] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_employees] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_permissions] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_assets_types] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';
UPDATE [local_staging].[dbo].[imp_assets_types_attr] SET remove=NULL WHERE TRIM(COALESCE(remove,''))='';

