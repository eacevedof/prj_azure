#!/bin/bash
gitpush: ## git push m=any message
	clear;
	git add .; git commit -m "$(m)"; git push;
