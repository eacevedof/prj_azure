#!/bin/bash
TODAY := $(shell date +'%Y-%m-%d')

help: ## Show this help message
	@echo "usage: make [target]"
	@echo
	@echo "targets:"
	@egrep "^(.+)\:\ ##\ (.+)" ${MAKEFILE_LIST} | column -t -c 2 -s ":#"

gitpush: ## git push m=any message
	clear;
	git add .; git commit -m "$(m)"; git push;

ssh-db: ## ssh db
	docker exec -it --user root cont-mssql bash

db-backup: ## t= path-where-you/want/to-copy/to
	clear;
	docker exec -it cont-mssql /opt/mssql-tools/bin/sqlcmd -b -V16 -S localhost -U SA -Q "BACKUP DATABASE [local_staging] TO DISK = N'/var/opt/mssql/backups/local_staging.bak' with NOFORMAT, NOINIT, NAME = 'local_staging-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
	docker cp cont-mssql:/var/opt/mssql/data/local_staging.bak $(HOME)/projects/temper/sql/cargas