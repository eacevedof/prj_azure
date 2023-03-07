/*
-- permisos de assets
-- permission_id (), model_type, model_id (por Modules\Asset\Models\Type)
select * from model_has_permissions

-- permisos por assets
access_to_assets
assets_creation
assets_deleting
assets_exporting
assets_file_deleting
assets_file_download
assets_file_inspection
assets_file_renaming
assets_file_updating
assets_relationship_edition
assets_updating
assets_versions_review
*/
INSERT INTO [local_laciahub].[dbo].[model_has_permissions] (model_id, permission_id, model_type)
SELECT imp.entity_id, imp.permissions_id, 'Modules\Asset\Models\Type' model_type
FROM [local_staging].[dbo].[imp_permissions] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.permissions_type = 'by-asset-type'
AND NOT EXISTS (
  SELECT mt.model_id, mt.permission_id
  FROM [local_laciahub].[dbo].[model_has_permissions] mt
  WHERE 1=1
  AND mt.model_type = 'Modules\Asset\Models\Type'
  AND mt.model_id = imp.entity_id
  AND mt.permission_id = imp.permissions_id
)
