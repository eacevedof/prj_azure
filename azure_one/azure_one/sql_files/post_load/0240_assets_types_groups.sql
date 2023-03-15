/*
select id, assets_types_id, asset_attribute_group_name
from assets_attributes_groups
where 1=1

select 
entity_type, 
fk1_uuid, fk1_entity_id,
uuid, entity_id 
val_1, 
tr_v1_1,tr_v1_2,tr_v1_3,tr_v1_4,tr_v1_5,
tr_v1_6, tr_v1_7, tr_v1_8, tr_v1_9

from [dbo].[imp_keys_and_values]
*/

UPDATE imp
SET
    imp.fk1_entity_id = mt.assets_types_id
FROM [local_staging].[dbo].[imp_keys_and_values] imp    
INNER JOIN [local_laciahub].[dbo].[assets_attributes_groups]  mt
ON imp.fk1_uuid = mt.assets_types_id
WHERE 1=1
AND imp.nok IS NULL
AND imp.entity_type = 'assets_attributes_groups'
;

UPDATE imp
SET
    imp.entity_id = mt.id
FROM [local_staging].[dbo].[imp_keys_and_values] imp    
INNER JOIN [local_laciahub].[dbo].[assets_attributes_groups]  mt
ON imp.uuid = mt.id
WHERE 1=1
AND imp.nok IS NULL
AND imp.entity_type = 'assets_attributes_groups'
;

UPDATE [local_staging].[dbo].[imp_assets_types_groups] SET nok=1 WHERE fk1_entity_id IS NULL
;

UPDATE mt
SET
    mt.assets_types_id = imp.fk1_entity_id,
    mt.asset_attribute_group_name = imp.val_1,
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_groups]  mt
INNER JOIN [local_staging].[dbo].[imp_imp_keys_and_values] imp
ON mt.id = imp.assets_types_groups_id
WHERE 1=1
AND imp.nok IS NULL
AND imp.entity_type = 'assets_attributes_groups'
;

SET IDENTITY_INSERT [local_laciahub].[dbo].[assets_types_groups] ON;

INSERT INTO [local_laciahub].[dbo].[assets_types_groups]
(
    id, assets_types_id, asset_attribute_group_name, created_at
)
SELECT
    imp.entity_id as id, 
    imp.fk1_entity_id as assets_types_id, 
    CONVERT(VARCHAR(255), imp.val_1) as asset_attribute_group_name,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_imp_keys_and_values] imp
LEFT JOIN [local_laciahub].[dbo].[assets_types_groups] mt
ON mt.id = imp.entity_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

SET IDENTITY_INSERT [local_laciahub].[dbo].[assets_types_groups] OFF;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET 
imp.entity_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_groups] mt
INNER JOIN [local_staging].[dbo].[imp_imp_keys_and_values] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
AND imp.entity_id IS NULL;
;