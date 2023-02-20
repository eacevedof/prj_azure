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

db-backup: ## t= path-where-you/want/to-copy/to
	clear;
	docker cp cont-mssql:/var/opt/mssql/data/local_staging.bak $(HOME)/projects/temper/sql/cargas