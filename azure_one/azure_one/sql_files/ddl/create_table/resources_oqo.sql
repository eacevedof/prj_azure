-- https://youtu.be/C73iDjnDn0o
CREATE MASTER KEY
;

CREATE DATABASE SCOPED CREDENTIAL credential_dev_oqo
WITH IDENTITY = 'xxx', SECRET='yyy'
;

CREATE EXTERNAL DATA SOURCE source_dev_oqo WITH
(
  TYPE = RDBMS,
  LOCATION = 'azure.server',
  DATABASE_NAME = 'dev-oqo',
  CREDENTIAL = credential_dev_oqo,
)
;

SELECT *
FROM users
WHERE 1=1
WITH (
  DATA_SOURCE = source_dev_oqo
)
;
