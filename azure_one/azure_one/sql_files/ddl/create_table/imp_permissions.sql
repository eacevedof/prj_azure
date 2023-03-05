DROP TABLE [local_staging].[dbo].[imp_permissions];

CREATE TABLE [local_staging].[dbo].[imp_permissions] (
    [id]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]              NVARCHAR (50)  NULL,
    [uuid]                     NVARCHAR (50)  NULL,
    [roles_uuid]               NVARCHAR (50)  NULL,
    [roles_id]                 NVARCHAR (50)  NULL,
    [roles_name]               NVARCHAR (250) NULL,
    [permission_id]            NVARCHAR (50)  NULL,
    [permission_slug]          NVARCHAR (250)  NULL,
    [permission_type]          NVARCHAR (50)  DEFAULT 'by-role' NULL,

    [nok]                      INT            NULL,
    [created_at]               DATETIME       CONSTRAINT [DEFAULT_imp_permissions_created_at] DEFAULT (getdate()) NULL,
    [updated_at]               DATETIME       NULL,
    [imp_uuid]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_permissions] PRIMARY KEY CLUSTERED ([id] ASC)
);

/*
select '['+slug+']' f, ' NVARCHAR(10) NULL,' c from permissions order by slug

-- role_has_permissions
SELECT *
FROM 
(
    SELECT r.id entity_uuid, r.name entity_name, p.slug permission_slug, 'by-role' permission_type
    FROM roles r
    LEFT JOIN role_has_permissions rhp
    ON r.id = rhp.role_id
    LEFT JOIN permissions p
    ON rhp.permission_id = p.id
    WHERE 1=1
    -- AND p.id NOT IN (select permission_id from model_has_permissions)
    -- ORDER BY 2, 3
    UNION


    SELECT atp.id asset_type_uuid, atp.asset_type_name asset_type_name, p.slug permission_slug, 'by-asset-type' permission_type
    FROM assets_types atp
    LEFT JOIN model_has_permissions mhp
    ON atp.id = mhp.model_id
    LEFT JOIN permissions p
    ON mhp.permission_id = p.id
    WHERE 1=1
    -- AND p.id IN ( select permission_id from model_has_permissions)
) t
ORDER BY 4 DESC, 2, 3
*/
