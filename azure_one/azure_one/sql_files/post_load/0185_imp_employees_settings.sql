INSERT INTO [local_laciahub].[dbo].[employees_settings]
(
[employees_id], [employees_setting_token], [employees_setting_lang], [employees_setting_cost_hr], 
[employees_setting_currency], [employees_setting_expiration], [created_at]
)
SELECT
    imp.employees_id as employees_id, 
    (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-','')))) employees_setting_token,
    imp.language_uuid employees_setting_lang,
    NULL employees_setting_cost_hr,
    NULL employees_setting_currency,
    NULL employees_setting_expiration,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_employees] imp
LEFT JOIN [local_laciahub].[dbo].[employees_settings] mt
ON mt.employees_id = imp.employees_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;