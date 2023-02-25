DELETE FROM [local_staging].[dbo].[imp_languages] WHERE uuid='uuid'
;

UPDATE mt
SET
    mt.language_name = CONVERT(VARCHAR(250), imp.val),
    -- mt.codesap = imp.codesap,
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[languages]  mt
INNER JOIN [local_staging].[dbo].[imp_languages] imp
ON mt.locale = imp.uuid
;

-- token no permite nulls
-- todo codesap
INSERT INTO [local_laciahub].[dbo].[languages]
(token,locale, language_name, created_at)

SELECT '',imp.uuid, imp.val, GETDATE() 
FROM [local_staging].[dbo].[imp_languages] imp
LEFT JOIN [local_laciahub].[dbo].[languages] mt
ON mt.locale = imp.uuid
WHERE 1=1
AND mt.id IS NULL
;

UPDATE [local_laciahub].[dbo].[languages]
    SET token = 'LNG'+RIGHT('00000' + CONVERT(VARCHAR(10), id), 6)
WHERE 1=1
AND COALESCE(TRIM(token),'') = ''
;