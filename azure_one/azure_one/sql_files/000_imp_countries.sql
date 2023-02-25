DELETE FROM [local_staging].[dbo].[imp_countries] WHERE uuid='uuid'
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.iso2 = CONVERT(VARCHAR(255), imp.iso2),
    mt.iso3 = CONVERT(VARCHAR(255), imp.iso3),
    mt.phone_code = CONVERT(VARCHAR(255), imp.phone_code),
    -- mt.codesap = imp.codesap,
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[countries]  mt
INNER JOIN [local_staging].[dbo].[imp_countries] imp
-- todo
ON mt.id = CONVERT(INT,imp.uuid)
;

-- token no permite nulls
INSERT INTO [local_laciahub].[dbo].[countries]
(name, iso2, iso3, phone_code,created_at)

SELECT 
    CONVERT(VARCHAR(255),imp.name),
    CONVERT(VARCHAR(255),imp.iso2),
    CONVERT(VARCHAR(255),imp.iso3),
    CONVERT(VARCHAR(255),imp.phone_code),
    GETDATE() 
FROM [local_staging].[dbo].[imp_countries] imp
LEFT JOIN [local_laciahub].[dbo].[countries] mt
ON mt.id = CONVERT(INT,imp.uuid)
WHERE 1=1
AND mt.id IS NULL
;

UPDATE [local_laciahub].[dbo].[countries]
    SET token = 'LNG'+RIGHT('00000' + CONVERT(VARCHAR(10), id), 6)
WHERE 1=1
AND COALESCE(TRIM(token),'') = ''
;