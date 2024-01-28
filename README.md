# ManageEmployees
🏗️ Voici le projet ManageEmployee, une API .NET et son front 🏗️

## Setup
Version .Net 8.0.0
Version Node 20.11.0

### Backend
* Tout d'abord cloner le projet 🚀
* Initialiser la base de données en SQL SERVER grace au fichier à la racine : sqlInitManageEmployees.sql
* Se positionner à la racine du projet et ouvrir un PowerShell.
* Lancer la commande suivante : 
```
dotnet ef dbcontext scaffold "Server=LAPTOP-I7LAPVCI\\SQLEXPRESS01;Database=ManageEmployees;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir Infrastructures/Database/ -c ManageEmployeeDbContext -d -f
```
Dans la partie Server="", MODIFIER pour que ça corresponde au nom du server que vous avez utilisé dans SQL SERVER !

* Ouvrer le fichier appsetings.json, modifier la ligne 3 :
```
"ManageEmployees": "Server=LAPTOP-I7LAPVCI\\SQLEXPRESS01;Database=ManageEmployees;Trusted_Connection=True;TrustServerCertificate=true"
```
Pareil dans cette commande, MODIFIER la partie Server="" !
Lancer le Back en utilisant Visual Studio.

### FrontEnd
* Installer Node sur votre PC (si ce n'est pas déjà fait), version 20 minimum
* Se positionner dans le dossier front-manageemployees
* Lancer la commande suivante : 
```
npm install
```
Lancer le Front avec la commande (assurez vous d'avoir le Back qui tourne) :
```
npm run dev
```

Le projet devrait maintenant fonctionner !

## Documentation
La documentation de l'API est disponible dans le fichier : apiDocumentation.pdf
