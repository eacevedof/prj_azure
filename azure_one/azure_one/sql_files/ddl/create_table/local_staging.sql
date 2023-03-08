USE [master]
GO

DROP DATABASE IF EXISTS [local_staging]
GO

CREATE DATABASE [local_staging]
GO

USE [local_staging]
GO

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
    [nok]                        INT            NULL,
    [created_at]                 DATETIME       CONSTRAINT [DEFAULT_imp_assets_types_created_at] DEFAULT (getdate()) NULL,
    [updated_at]                 DATETIME       NULL,
    [imp_uuid]                   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_imp_assets_types] PRIMARY KEY CLUSTERED ([id] ASC)
)
;

