/*
select * from assets_files_thumbnails

select id uuid, companies_id company_uuid, asset_type_name name, '' description, '' path_thumbnail, '' color  
from assets_types
*/
UPDATE [local_staging].[dbo].[imp_assets_types] SET assets_types_id=NULL;

UPDATE imp
SET imp.assets_types_id = mt.id
FROM [local_staging].[dbo].[imp_assets_types] imp
INNER JOIN [local_laciahub].[dbo].[assets_types] mt
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.company_id = mt.id
FROM [local_staging].[dbo].[imp_assets_types] imp
INNER JOIN [local_laciahub].[dbo].[companies] mt
ON mt.id = imp.company_uuid
WHERE 1=1
AND imp.nok IS NULL
;

