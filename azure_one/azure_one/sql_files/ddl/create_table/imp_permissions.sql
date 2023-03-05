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

SELECT r.id roles_uuid, r.name roles_name, p.slug permission_slug
FROM roles r
CROSS JOIN permissions p
ORDER BY 2, 3
*/
