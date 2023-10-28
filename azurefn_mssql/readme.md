### info

## List of uers
- curl -X GET  http://localhost:7071/api/users-search?search=eduardoaf.com

## Create User
- curl -X POST -H "Content-Type: application/json" -d '{"fullName":"Eduardo A. F.", "email": "eaf@eaf.com"}' http://localhost:7071/api/user-create