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

curl-test: ## curl get http://localhost:7071/api/azure_one
	curl http://localhost:7071/api/azure_one

ssh-db: ## ssh db
	docker exec -it --user root cont-mssql bash

db-backup: ## t= path-where-you/want/to-copy/to
	clear;
	docker exec -it cont-mssql /opt/mssql-tools/bin/sqlcmd -b -V16 -S localhost -U SA -Q "BACKUP DATABASE [local_staging] TO DISK = N'/var/opt/mssql/data/local_staging.bak' with NOFORMAT, NOINIT, NAME = 'local_staging-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
	docker cp cont-mssql:/var/opt/mssql/data/local_staging.bak $(HOME)/projects/temper/sql/cargas
	cd $(HOME)/projects/temper/sql/cargas; git add .; git commit -m "local-staging.bak"; git push;
	
db-q: ## q="use xxx; select * from users"
	clear;
	docker exec -it cont-mssql sh -c "cd /opt/mssql-tools/bin/; ./sqlcmd -U SA -Q \"$(q)\""

run:
	clear;
	dotnet build ./azure_one/azure_one.csproj --property:GenerateFullPaths=true --consoleloggerparameters:NoSummary;
	dotnet publish ./azure_one/azure_one.csproj --property:GenerateFullPaths=true --consoleloggerparameters:NoSummary;
	dotnet watch run --project azure_one/bin/Debug/net6.0/azure_one.dll;
	# para el siguiente comando se necesita: brew tap azure/functions; brew install azure-functions-core-tools@4
	# cd ./azure_one; func start;
