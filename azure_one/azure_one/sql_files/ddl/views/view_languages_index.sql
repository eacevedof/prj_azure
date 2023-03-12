USE [local_staging]; 

CREATE VIEW[dbo].[view_languages_index]
AS
SELECT
DISTINCT imp.lang_from, imp.tr_num
FROM [local_staging].[dbo].[imp_languages_company_custom] imp
WHERE 1=1
AND imp.nok IS NULL
AND COALESCE(imp.tr_num,'')!=''
;