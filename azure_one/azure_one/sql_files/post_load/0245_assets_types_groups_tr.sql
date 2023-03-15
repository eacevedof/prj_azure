/*
select id, assets_attributes_groups_id, asset_attribute_group_name
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

UPDATE mt
SET
    mt.asset_attribute_group_name = CONVERT(VARCHAR(255), imp.tr),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_attributes_groups_tr]  mt
INNER JOIN (
    SELECT DISTINCT
    vc_tr.mt_id as assets_attributes_groups_id, 
    vli.lang_from as locale,
    CONVERT(VARCHAR(255),vc_tr.tr_i) as tr
    FROM [local_staging].[dbo].[view_keys_and_values_tr] [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
    AND vc_tr.entity_type = 'assets_attributes_groups'
) imp
ON mt.assets_attributes_groups_id = imp.assets_attributes_groups_id
AND mt.locale = imp.locale
WHERE 1=1
;

INSERT INTO [local_laciahub].[dbo].[assets_attributes_groups_tr]
(assets_attributes_groups_id, locale, asset_attribute_group_name, created_at)

SELECT * 
FROM (
    SELECT DISTINCT
        vc_tr.mt_id as assets_attributes_groups_id, 
        vli.lang_from as locale, 
        CONVERT(VARCHAR(255),vc_tr.tr_i) as tr, 
        GETDATE() created_at 
    FROM [local_staging].[dbo].[view_keys_and_values_tr] [vc_tr]
    INNER JOIN [local_staging].[dbo].view_languages_index [vli]
    ON [vc_tr].[tr_num] = [vli].[tr_num]
    AND vc_tr.entity_type = 'assets_attributes_groups'
) AS imp
WHERE 1=1
AND NOT EXISTS 
(
    SELECT id
    FROM [local_laciahub].[dbo].[assets_attributes_groups_tr] mt
    WHERE 1=1
    AND mt.assets_attributes_groups_id = imp.assets_attributes_groups_id
    AND mt.locale = imp.locale
)
