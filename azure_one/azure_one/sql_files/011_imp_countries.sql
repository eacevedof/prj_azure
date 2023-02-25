DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid='uuid'
;
DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid IS NULL
;

UPDATE [local_staging].[dbo].[imp_countries] SET tr_1 = NULL WHERE tr_1 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_2 = NULL WHERE tr_2 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_3 = NULL WHERE tr_3 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_4 = NULL WHERE tr_4 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_5 = NULL WHERE tr_5 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_6 = NULL WHERE tr_6 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_7 = NULL WHERE tr_7 ='';
UPDATE [local_staging].[dbo].[imp_countries] SET tr_8 = NULL WHERE tr_8 ='';

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    -- mt.iso2 = CONVERT(VARCHAR(255), imp.iso2),
    -- mt.iso3 = CONVERT(VARCHAR(255), imp.iso3),
    -- mt.phone_code = CONVERT(VARCHAR(255), imp.phone_code),
    -- mt.codesap = imp.codesap,
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_countries] imp
-- todo
ON mt.id = CONVERT(INT,imp.uuid)
;

INSERT INTO [local_laciahub].[dbo].[countries]
(
 name, 
 iso2, iso3, phone_code,
 created_at
)
SELECT 
    CONVERT(VARCHAR(255),imp.val) name,
    imp.uuid iso2, imp.uuid iso3, imp.uuid phone_code,
    -- CONVERT(VARCHAR(255),imp.iso2),
    -- CONVERT(VARCHAR(255),imp.iso3),
    -- CONVERT(VARCHAR(255),imp.phone_code),
    GETDATE() 
FROM [local_staging].[dbo].[imp_countries] imp
LEFT JOIN [local_laciahub].[dbo].[countries] mt
ON mt.id = CONVERT(INT, imp.uuid)
WHERE 1=1
AND mt.id IS NULL
;

-- case when 
SELECT 
    DISTINCT lang_from, tr_num 
FROM [local_staging].[dbo].[imp_languages_company]
ORDER BY tr_num

CREATE VIEW IF NOT EXISTS [local_staging].[dbo].[view_countries_tr]
AS
    SELECT 1 tr_num, imp.tr_1
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 2 tr_num, imp.tr_2
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 3 tr_num, imp.tr_3
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 4 tr_num, imp.tr_4
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 5 tr_num, imp.tr_5
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 6 tr_num, imp.tr_6
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 7 tr_num, imp.tr_7
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 8 tr_num, imp.tr_8
    FROM [local_staging].[dbo].[imp_countries]
        UNION
    SELECT 9 tr_num, imp.tr_8
    FROM [local_staging].[dbo].[imp_countries]
