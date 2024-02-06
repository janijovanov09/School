// The app runs SQL Lite DB (SchoolDB). Connections string is in the appsettings. To create the DB, because the init migrations already exists just run:

update-database



You can remove the migrations and recreate them using the commands:

1. add-migration InitializeDatabase
2. update-database

There is endpoints.http and from there you can test the endpoints (there is an example for each endpoint)

The app is using minimal apis.

Swagger is configured: https://localhost:5001/swagger/index.html

The app will run using the 5001 port on locahost: https://localhost:5001

