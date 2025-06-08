How to update database in Visual Studio 2022
- Open Package Manager Console
- Select project DZ.SupplierProcessor
- Write command Add-Migration <NameOfMigration> -OutputDir Database\Migrations

Migration will be executed:
1) Open Package Manager Console and type Update-database
2) When console application is launched migration will be applied automaticallly before jobs are wired up