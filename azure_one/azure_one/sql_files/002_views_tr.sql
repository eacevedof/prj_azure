EXEC sp_executesql N'
CREATE VIEW [dbo].[view_countries_tr] AS
    SELECT 1 tr_num, imp.tr_1
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 2 tr_num, imp.tr_2
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 3 tr_num, imp.tr_3
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 4 tr_num, imp.tr_4
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 5 tr_num, imp.tr_5
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 6 tr_num, imp.tr_6
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 7 tr_num, imp.tr_7
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 8 tr_num, imp.tr_8
    FROM [local_staging].[dbo].[imp_countries] imp
        UNION
    SELECT 9 tr_num, imp.tr_8
    FROM [local_staging].[dbo].[imp_countries] imp
';