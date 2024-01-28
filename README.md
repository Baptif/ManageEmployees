# ManageEmployees
🏗️ Voici le projet ManageEmployee, une API .NET 🏗️

## Setup
* Initialiser la base de données en SQL SERVER grace au fichier à la racine : sqlInitManageEmployees.sql
* Cloner le projet
* Positionnez vous à la racine du projet et ouvrez un PowerShell.
* Lancer la commande suivante (Dans la partie Server, mettre ce qui correspond sur votre PC) : 
```
dotnet ef dbcontext scaffold "Server=;Database=ManageEmployees;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir Infrastructures/Database/ -c ManageEmployeeDbContext -d -f
```
* Ouvrer le fichier appsetings.json, modifier la ligne 3 :
```
"ManageEmployees": ";Database=ManageEmployees;Trusted_Connection=True;TrustServerCertificate=true"
```

Le projet devrait maintenant fonctionner !
