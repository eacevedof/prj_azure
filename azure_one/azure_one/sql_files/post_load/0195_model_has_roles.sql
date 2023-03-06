INSERT INTO [local_laciahub].[dbo].[model_has_roles] 
(role_id, model_type, model_id)
SELECT DISTINCT
    imp.roles_id,
    'App\Models\User' model_type,
    imp.users_id
FROM [local_staging].[dbo].[imp_employees] imp
WHERE 1=1
AND imp.nok IS NULL
AND NOT EXISTS (
    SELECT mt.role_id, mt.model_id
    FROM [local_laciahub].[dbo].[model_has_roles] mt
    WHERE 1=1
    AND mt.model_type = 'App\Models\User'
    AND imp.roles_id = mt.role_id
    AND imp.users_id = mt.model_id   
)
