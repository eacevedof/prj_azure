/*
select id, assets_attributes_groups_id, assets_types_id,
asset_type_attr_key,
asset_type_attr_name,
asset_type_attr_value_type,
asset_type_attr_type_selector_multiple,
asset_type_attr_mandatory
from [dbo].[assets_types_attr]

select  
asset_type_group_uuid,
assets_attributes_groups_id,
assets_types_uuid,
assets_types_id,
uuid,
assets_types_attr_id,
asset_type_attr_value_type,
asset_type_attr_selector_multiple,
asset_type_attr_mandatory,
val,
codesap
from [dbo].[imp_assets_types_attr]
*/
UPDATE mt
SET
    imp.assets_attributes_groups_id = mt.id
FROM [local_staging].[dbo].[imp_assets_types_attr] imp 
INNER JOIN [local_laciahub].[dbo].[assets_types_groups]  mt
ON mt.asset_type_group_uuid = imp.id
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
;

UPDATE [local_staging].[dbo].[imp_keys_and_values] 
SET nok=1 
WHERE 1=1
AND fk1_entity_id IS NULL 
AND nok IS NULL
AND entity_type = 'assets_attributes_groups'
;

UPDATE mt
SET
    imp.assets_attributes_groups_id = mt.id
FROM [local_staging].[dbo].[imp_assets_types_attr] imp 
INNER JOIN [local_laciahub].[dbo].[assets_types_groups]  mt
ON mt.asset_type_group_uuid = imp.id
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
;

UPDATE mt
SET
    mt.employee_name = CONVERT(VARCHAR(45), imp.employee_name),
    mt.employee_surname_1 = CONVERT(VARCHAR(45), imp.employee_surname_1),
    mt.employee_surname_2 = CONVERT(VARCHAR(45), imp.employee_surname_2),
    -- mt.employee_phone = CONVERT(VARCHAR(255), imp.employee_phone),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_attr]  mt
INNER JOIN [local_staging].[dbo].[imp_assets_types_attr] imp
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
;

-- crear empleados
INSERT INTO [local_laciahub].[dbo].[assets_types_attr]
(
    users_id, companies_id, employee_token, employee_code, employee_photo, employee_name, 
    employee_surname_1, employee_surname_2, 
    -- employee_phone, 
    employee_email, employee_status, email_notification,
    created_at
)
SELECT
    imp.users_id, imp.company_id companies_id, 
    (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-','')))) employee_token,
    CONVERT(VARCHAR(9),(SELECT CONVERT(VARCHAR,imp.id)+CONVERT(VARCHAR(45),CONVERT(INT,RAND()*1000000000)))) employee_code,
    NULL employee_photo,
    CONVERT(VARCHAR(45), imp.employee_name) employee_name, 
    CONVERT(VARCHAR(45), imp.employee_surname_1) employee_surname_1, 
    CONVERT(VARCHAR(45), imp.employee_surname_2) employee_surname_2,
    -- CONVERT(VARCHAR(45), employee_phone) employee_phone,
    CONVERT(VARCHAR(45), imp.employee_email) employee_email,
    1 employee_status,
    1 email_notification,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_assets_types_attr] imp
LEFT JOIN [local_laciahub].[dbo].[assets_types_attr] mt
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;


UPDATE imp
SET imp.assets_types_attr_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_attr]  mt
INNER JOIN [local_staging].[dbo].[imp_assets_types_attr] imp
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND imp.assets_types_attr_id IS NULL;
;