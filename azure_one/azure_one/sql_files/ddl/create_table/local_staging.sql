USE [master]
;


-- ALTER DATABASE [local_staging] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
;

DROP DATABASE IF EXISTS [local_staging]
;

CREATE DATABASE [local_staging]
;

USE [local_staging]
;

CREATE TABLE [dbo].[imp_assets_types] (
    [id]                         BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]                NVARCHAR (50)  NULL,
    [company_uuid]               NVARCHAR (50)  NULL,
    [company_id]                 NVARCHAR (50)  NULL,
    [uuid]                       NVARCHAR (50)  NULL,
    [assets_types_id]            NVARCHAR (50)  NULL,
    [val]                        NVARCHAR (255) NULL,
    [description]                NVARCHAR (500) NULL,
    [assets_files_thumbnails_id] NVARCHAR (50)  NULL,
    [tag_thumbnail]              NVARCHAR (255) NULL,
    [path_thumbnail]             NVARCHAR (300) NULL,
    [color]                      NVARCHAR (50)  NULL,
    [codesap]                    NVARCHAR (50)  NULL,
    [tr_1]                       NVARCHAR (500) NULL,
    [tr_2]                       NVARCHAR (500) NULL,
    [tr_3]                       NVARCHAR (500) NULL,
    [tr_4]                       NVARCHAR (500) NULL,
    [tr_5]                       NVARCHAR (500) NULL,
    [tr_6]                       NVARCHAR (500) NULL,
    [tr_7]                       NVARCHAR (500) NULL,
    [tr_8]                       NVARCHAR (500) NULL,
    [tr_9]                       NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]                        INT            NULL,
    [created_at]                 DATETIME       CONSTRAINT [DEFAULT_imp_assets_types_created_at] DEFAULT (getdate()) NULL,
    [updated_at]                 DATETIME       NULL,
    [imp_uuid]                   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_assets_types] PRIMARY KEY CLUSTERED ([id] ASC)
)
;

CREATE TABLE [dbo].[imp_cities] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]    NVARCHAR (50)  NULL,
    [provinces_uuid] NVARCHAR (50)  NULL,
    [provinces_id]   NVARCHAR (50)  NULL,
    [uuid]           NVARCHAR (50)  NULL,
    [cities_id]      NVARCHAR (50)  NULL,
    [val]            NVARCHAR (500) NULL,
    [codesap]        NVARCHAR (50)  NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]            INT            NULL,
    [created_at]     DATETIME       CONSTRAINT [DEFAULT_cities_created_at] DEFAULT (getdate()) NULL,
    [updated_at]     DATETIME       NULL,
    [imp_uuid]       NVARCHAR (50)  NULL,
    CONSTRAINT [PK_cities] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_companies] (
    [id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]            NVARCHAR (50)  NULL,
    [city_uuid]              NVARCHAR (50)  NULL,
    [city_id]                NVARCHAR (50)  NULL,
    [country_uuid]           NVARCHAR (50)  NULL,
    [country_id]             NVARCHAR (50)  NULL,
    [uuid]                   NVARCHAR (50)  NULL,
    [companies_id]           NVARCHAR (50)  NULL,
    [parent_company_uuid]    NVARCHAR (50)  NULL,
    [parent_company_id]      NVARCHAR (50)  NULL,
    [company_token]          NVARCHAR (255) NULL,
    [company_type]           NVARCHAR (255) NULL,
    [company_name]           NVARCHAR (500) NULL,
    [company_address1]       NVARCHAR (500) NULL,
    [company_address2]       NVARCHAR (500) NULL,
    [company_cp]             NVARCHAR (50)  NULL,
    [company_contact_person] NVARCHAR (100) NULL,
    [company_contact_phone]  NVARCHAR (100) NULL,
    [company_contact_email]  NVARCHAR (100) NULL,
    [codesap]                NVARCHAR (50)  NULL,
    [company_cod_int]        NVARCHAR (100) NULL,
    [company_active]         NVARCHAR (50)  NULL,
    [link_expiration_days]   NVARCHAR (50)  NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]                    INT            NULL,
    [created_at]             DATETIME       CONSTRAINT [DEFAULT_companies_created_at] DEFAULT (getdate()) NULL,
    [updated_at]             DATETIME       NULL,
    [imp_uuid]               NVARCHAR (50)  NULL,
    CONSTRAINT [PK_companies] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_countries] (
    [id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]  NVARCHAR (50)  NULL,
    [uuid]         NVARCHAR (50)  NULL,
    [countries_id] NVARCHAR (50)  NULL,
    [val]          NVARCHAR (500) NULL,
    [codesap]      NVARCHAR (50)  NULL,
    [tr_1]         NVARCHAR (500) NULL,
    [tr_2]         NVARCHAR (500) NULL,
    [tr_3]         NVARCHAR (500) NULL,
    [tr_4]         NVARCHAR (500) NULL,
    [tr_5]         NVARCHAR (500) NULL,
    [tr_6]         NVARCHAR (500) NULL,
    [tr_7]         NVARCHAR (500) NULL,
    [tr_8]         NVARCHAR (500) NULL,
    [tr_9]         NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]          INT            NULL,
    [created_at]   DATETIME       CONSTRAINT [DEFAULT_countries_created_at] DEFAULT (getdate()) NULL,
    [updated_at]   DATETIME       NULL,
    [imp_uuid]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_countries] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_employees] (
    [id]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]              NVARCHAR (50)  NULL,
    [uuid]                     NVARCHAR (50)  NULL,
    [employees_id]             VARCHAR (50)   NULL,
    [employee_name]            NVARCHAR (250) NULL,
    [employee_surname_1]       NVARCHAR (250) NULL,
    [employee_surname_2]       NVARCHAR (250) NULL,
    [user_types_uuid]          NVARCHAR (50)  NULL,
    [user_types_id]            NVARCHAR (50)  NULL,
    [company_uuid]             NVARCHAR (50)  NULL,
    [company_id]               NVARCHAR (50)  NULL,
    [employee_email]           NVARCHAR (250) NULL,
    [language_uuid]            NVARCHAR (50)  NULL,
    [department_uuid]          NVARCHAR (50)  NULL,
    [employees_departments_id] NVARCHAR (50)  NULL,
    [position_uuid]            NVARCHAR (50)  NULL,
    [employees_positions_id]   NVARCHAR (50)  NULL,
    [role_uuid]                NVARCHAR (50)  NULL,
    [roles_id]                 NVARCHAR (50)  NULL,
    [users_id]                 NVARCHAR (50)  NULL,
    [codesap]                  NVARCHAR (50)  NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]                      INT            NULL,
    [created_at]               DATETIME       CONSTRAINT [DEFAULT_imp_employees_created_at] DEFAULT (getdate()) NULL,
    [updated_at]               DATETIME       NULL,
    [imp_uuid]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_employees] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_employees_departments] (
    [id]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]              NVARCHAR (50)  NULL,
    [uuid]                     NVARCHAR (50)  NULL,
    [employees_departments_id] NVARCHAR (50)  NULL,
    [position_token]           NVARCHAR (50)  NULL,
    [val]                      NVARCHAR (250) NULL,
    [codesap]                  NVARCHAR (50)  NULL,
    [tr_1]                     NVARCHAR (500) NULL,
    [tr_2]                     NVARCHAR (500) NULL,
    [tr_3]                     NVARCHAR (500) NULL,
    [tr_4]                     NVARCHAR (500) NULL,
    [tr_5]                     NVARCHAR (500) NULL,
    [tr_6]                     NVARCHAR (500) NULL,
    [tr_7]                     NVARCHAR (500) NULL,
    [tr_8]                     NVARCHAR (500) NULL,
    [tr_9]                     NVARCHAR (500) NULL,
    [nok]                      INT            NULL,
    [created_at]               DATETIME       CONSTRAINT [DEFAULT_imp_employees_departments_created_at] DEFAULT (getdate()) NULL,
    [updated_at]               DATETIME       NULL,
    [imp_uuid]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_employees_departments] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_employees_positions] (
    [id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]            NVARCHAR (50)  NULL,
    [uuid]                   NVARCHAR (50)  NULL,
    [employees_positions_id] NVARCHAR (50)  NULL,
    [position_token]         NVARCHAR (50)  NULL,
    [val]                    NVARCHAR (250) NULL,
    [codesap]                NVARCHAR (50)  NULL,
    [tr_1]                   NVARCHAR (500) NULL,
    [tr_2]                   NVARCHAR (500) NULL,
    [tr_3]                   NVARCHAR (500) NULL,
    [tr_4]                   NVARCHAR (500) NULL,
    [tr_5]                   NVARCHAR (500) NULL,
    [tr_6]                   NVARCHAR (500) NULL,
    [tr_7]                   NVARCHAR (500) NULL,
    [tr_8]                   NVARCHAR (500) NULL,
    [tr_9]                   NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]                    INT            NULL,
    [created_at]             DATETIME       CONSTRAINT [DEFAULT_employees_positions_created_at] DEFAULT (getdate()) NULL,
    [updated_at]             DATETIME       NULL,
    [imp_uuid]               NVARCHAR (50)  NULL,
    CONSTRAINT [PK_employees_positions] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_errors] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug] NVARCHAR (50)  NULL,
    [level]       NVARCHAR (50)  NULL,
    [title]       NVARCHAR (500) NULL,
    [reason]      NTEXT          NULL,
    [created_at]  DATETIME       CONSTRAINT [DEFAULT_imp_errors_created_at] DEFAULT (getdate()) NULL,
    [imp_uuid]    NVARCHAR (50)  NULL
);

CREATE TABLE [dbo].[imp_languages] (
    [id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]  NVARCHAR (50)  NULL,
    [uuid]         NVARCHAR (50)  NULL,
    [languages_id] NVARCHAR (50)  NULL,
    [val]          NVARCHAR (500) NULL,
    [codesap]      NVARCHAR (50)  NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]          INT            NULL,
    [created_at]   DATETIME       CONSTRAINT [DEFAULT_imp_languages_created_at] DEFAULT (getdate()) NULL,
    [update_at]    DATETIME       NULL,
    [imp_uuid]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_languages] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_languages_company_custom] (
    [id]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [tenant_slug]         NVARCHAR (50) NULL,
    [languages_id]        NVARCHAR (50) NULL,
    [companies_uuid]      NVARCHAR (50) NULL,
    [companies_id]        NVARCHAR (50) NULL,
    [lang_from]           NVARCHAR (50) NULL,
    [lang_tr]             NVARCHAR (50) NULL,
    [value_tr]            NVARCHAR (50) NULL,
    [languages_target_id] NVARCHAR (50) NULL,
    [tr_num]              VARCHAR (50)  NULL,
    [nok]                 INT           NULL,
    [created_at]          DATETIME      CONSTRAINT [DEFAULT_imp_languages_company_created_at] DEFAULT (getdate()) NULL,
    [updated_at]          DATETIME      NULL,
    [imp_uuid]            NVARCHAR (50) NULL,
    CONSTRAINT [PK_languages_company] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_permissions] (
    [id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]      NVARCHAR (50)  NULL,
    [uuid]             NVARCHAR (50)  NULL,
    [entity_uuid]      NVARCHAR (50)  NULL,
    [entity_id]        NVARCHAR (50)  NULL,
    [entity_name]      NVARCHAR (250) NULL,
    [permissions_id]   NVARCHAR (50)  NULL,
    [permissions_slug] NVARCHAR (250) NULL,
    [permissions_type] NVARCHAR (50)  DEFAULT ('by-role') NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]              INT            NULL,
    [created_at]       DATETIME       CONSTRAINT [DEFAULT_imp_permissions_created_at] DEFAULT (getdate()) NULL,
    [updated_at]       DATETIME       NULL,
    [imp_uuid]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_permissions] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_provinces] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]    NVARCHAR (50)  NULL,
    [countries_uuid] NVARCHAR (50)  NULL,
    [countries_id]   NVARCHAR (50)  NULL,
    [uuid]           NVARCHAR (50)  NULL,
    [provinces_id]   NVARCHAR (50)  NULL,
    [val]            NVARCHAR (500) NULL,
    [codesap]        NVARCHAR (50)  NULL,
    [tr_1]           NVARCHAR (500) NULL,
    [tr_2]           NVARCHAR (500) NULL,
    [tr_3]           NVARCHAR (500) NULL,
    [tr_4]           NVARCHAR (500) NULL,
    [tr_5]           NVARCHAR (500) NULL,
    [tr_6]           NVARCHAR (500) NULL,
    [tr_7]           NVARCHAR (500) NULL,
    [tr_8]           NVARCHAR (500) NULL,
    [tr_9]           NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]            INT            NULL,
    [created_at]     DATETIME       CONSTRAINT [DEFAULT_imp_provincies_created_at] DEFAULT (getdate()) NULL,
    [update_at]      DATETIME       NULL,
    [imp_uuid]       NVARCHAR (50)  NULL,
    CONSTRAINT [PK_provincies] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_roles] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug] NVARCHAR (50)  NULL,
    [uuid]        NVARCHAR (50)  NULL,
    [roles_id]    NVARCHAR (50)  NULL,
    [val]         NVARCHAR (500) NULL,
    [codesap]     NVARCHAR (50)  NULL,
    [tr_1]        NVARCHAR (500) NULL,
    [tr_2]        NVARCHAR (500) NULL,
    [tr_3]        NVARCHAR (500) NULL,
    [tr_4]        NVARCHAR (500) NULL,
    [tr_5]        NVARCHAR (500) NULL,
    [tr_6]        NVARCHAR (500) NULL,
    [tr_7]        NVARCHAR (500) NULL,
    [tr_8]        NVARCHAR (500) NULL,
    [tr_9]        NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]         INT            NULL,
    [created_at]  DATETIME       CONSTRAINT [DEFAULT_imp_roles_created_at] DEFAULT (getdate()) NULL,
    [updated_at]  DATETIME       NULL,
    [imp_uuid]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_roles] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_status_employees] (
    [id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]         NVARCHAR (50)  NULL,
    [uuid]                NVARCHAR (50)  NULL,
    [status_employees_id] NVARCHAR (50)  NULL,
    [val]                 NVARCHAR (255) NULL,
    [codesap]             NVARCHAR (50)  NULL,
    [tr_1]                NVARCHAR (500) NULL,
    [tr_2]                NVARCHAR (500) NULL,
    [tr_3]                NVARCHAR (500) NULL,
    [tr_4]                NVARCHAR (500) NULL,
    [tr_5]                NVARCHAR (500) NULL,
    [tr_6]                NVARCHAR (500) NULL,
    [tr_7]                NVARCHAR (500) NULL,
    [tr_8]                NVARCHAR (500) NULL,
    [tr_9]                NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]                 INT            NULL,
    [created_at]          DATETIME       NULL,
    [updated_at]          DATETIME       NULL,
    [imp_uuid]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_status_employees] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[imp_user_types] (
    [id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]   NVARCHAR (50)  NULL,
    [uuid]          NVARCHAR (50)  NULL,
    [user_types_id] NVARCHAR (50)  NULL,
    [val]           NVARCHAR (255) NULL,
    [codesap]       NVARCHAR (50)  NULL,
    [tr_1]          NVARCHAR (500) NULL,
    [tr_2]          NVARCHAR (500) NULL,
    [tr_3]          NVARCHAR (500) NULL,
    [tr_4]          NVARCHAR (500) NULL,
    [tr_5]          NVARCHAR (500) NULL,
    [tr_6]          NVARCHAR (500) NULL,
    [tr_7]          NVARCHAR (500) NULL,
    [tr_8]          NVARCHAR (500) NULL,
    [tr_9]          NVARCHAR (500) NULL,

    [remove]         NVARCHAR (50)  NULL,
    [nok]           INT            NULL,
    [created_at]    DATETIME       CONSTRAINT [DEFAULT_imp_user_types_created_at] DEFAULT (getdate()) NULL,
    [updated_at]    DATETIME       NULL,
    [imp_uuid]      NVARCHAR (50)  NULL,
    CONSTRAINT [PK_user_types] PRIMARY KEY CLUSTERED ([id] ASC)
);

DROP TABLE [dbo].[imp_keys_and_values]
;
CREATE TABLE [dbo].[imp_keys_and_values] (
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [tenant_slug]       NVARCHAR (50)  NULL,
    [entity_type]      NVARCHAR (100) NULL, 
    [fk1_uuid]          NVARCHAR (50)  NULL,
    [fk1_entity_id]     NVARCHAR (50)  NULL,
    [fk2_uuid]          NVARCHAR (50)  NULL,
    [fk2_entity_id]     NVARCHAR (50)  NULL,    
    [uuid]              NVARCHAR (50)  NULL,
    [entity_id]         NVARCHAR (50)  NULL,
    [codesap]          NVARCHAR (50)  NULL,
    
    [val_1]            NVARCHAR (1500) NULL,
    [tr_v1_1]          NVARCHAR (1500) NULL,
    [tr_v1_2]          NVARCHAR (1500) NULL,
    [tr_v1_3]          NVARCHAR (1500) NULL,
    [tr_v1_4]          NVARCHAR (1500) NULL,
    [tr_v1_5]          NVARCHAR (1500) NULL,
    [tr_v1_6]          NVARCHAR (1500) NULL,
    [tr_v1_7]          NVARCHAR (1500) NULL,
    [tr_v1_8]          NVARCHAR (1500) NULL,
    [tr_v1_9]          NVARCHAR (1500) NULL,

    [val_2]            NVARCHAR (1500) NULL,
    [tr_v2_1]          NVARCHAR (1500) NULL,
    [tr_v2_2]          NVARCHAR (1500) NULL,
    [tr_v2_3]          NVARCHAR (1500) NULL,
    [tr_v2_4]          NVARCHAR (1500) NULL,
    [tr_v2_5]          NVARCHAR (1500) NULL,
    [tr_v2_6]          NVARCHAR (1500) NULL,
    [tr_v2_7]          NVARCHAR (1500) NULL,
    [tr_v2_8]          NVARCHAR (1500) NULL,
    [tr_v2_9]          NVARCHAR (1500) NULL,

    [val_3]            NVARCHAR (1500) NULL,
    [tr_v3_1]          NVARCHAR (1500) NULL,
    [tr_v3_2]          NVARCHAR (1500) NULL,
    [tr_v3_3]          NVARCHAR (1500) NULL,
    [tr_v3_4]          NVARCHAR (1500) NULL,
    [tr_v3_5]          NVARCHAR (1500) NULL,
    [tr_v3_6]          NVARCHAR (1500) NULL,
    [tr_v3_7]          NVARCHAR (1500) NULL,
    [tr_v3_8]          NVARCHAR (1500) NULL,
    [tr_v3_9]          NVARCHAR (1500) NULL,

    [remove]           NVARCHAR (50) NULL,
    [nok]           INT            NULL,
    [created_at]    DATETIME       CONSTRAINT [DEFAULT_imp_keys_and_values_created_at] DEFAULT (getdate()) NULL,
    [updated_at]    DATETIME       NULL,
    [imp_uuid]      NVARCHAR (50)  NULL,
    CONSTRAINT [PK_keys_and_values] PRIMARY KEY CLUSTERED ([id] ASC)
);


