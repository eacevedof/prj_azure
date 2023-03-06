/*
--id, role_id, permission_id
select * from role_has_permissions
*/
INSERT INTO [local_laciahub].[dbo].[role_has_permissions] (role_id, permission_id)
SELECT imp.roles_id, imp.permission_id
FROM [local_staging].[dbo].[imp_permissions] imp
WHERE 1=1
AND imp.nok IS NULL
AND imp.permission_type = 'by-role'
AND NOT EXISTS (
  SELECT mt.role_id, mt.permission_id
  FROM [local_laciahub].[dbo].[role_has_permissions] mt
  WHERE 1=1
  AND mt.role_id = imp.roles_id
  AND mt.permission_id = imp.permissions_id
)
