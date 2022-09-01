# Creation-of-a-database-management-tool-C-sharp-to-SQL-
BDD – Rapport du Problème ![](Ressources/Aspose.Words.e5964a3f-f463-4a2f-9e08-5ec97b5c9d67.001.png)

*Par Pariente Samuel et Ortega Marius ![](Aspose.Words.e5964a3f-f463-4a2f-9e08-5ec97b5c9d67.002.png)*

I-  Introduction 

Dans le cadre de ce projet, nous avons conçu une base de données pour une boutique de vélo nommée Velomax.  Pour  accomplir  cette  tâche  nous  avons  conçu  12  entités  et  14  associations  donnant finalement 15 tables dans notre code SQL. Dans ce rapport, nous vous décrirons dans un premier temps la structure de notre base de données SQL, puis dans un second temps la structure du code C# permettant une interaction efficace avec la base de données.  

II-  Structure de la base de données (SQL) 

1-  Modèle E/A 

En amont de la création de notre base de données, nous avons développé le modèle E/A de notre base : 

![](Aspose.Words.e5964a3f-f463-4a2f-9e08-5ec97b5c9d67.003.jpeg)

2-  Code SQL 

Une fois le modèle E/A finalisé, nous avons modélisé l’ensemble de nos tables SQL. Parmi ces tables certaines  comportaient  des  subtilités  dans  le  comportement  qui  nécessitaient  des  traitements particuliers.  

- **L’adresse :**  

Nous avons envisagé plusieurs implémentations pour l’adresse. Après réflexion, nous avons choisi de ne pas avoir de table « rue » liées à l’adresse car il en existe un trop grand nombre. Toutefois, chaque table ayant une adresse possède une clé étrangère sur la table ‘Code Postal’. A partir du code postal, on peut déduire la ville, et la province ce qui évite des redondances dans les attributs des différentes tables. 

- **Commande :** 

Du  fait  qu’une  commande  peut  être  passée  par  un  client  particulier  ou  une  boutique partenaire, nous avons choisi d’avoir une clé étrangère de chacune de ces tables dans notre table ‘Commande’. Lorsque la commande est passée par un client particulier la clé étrangère liée à la boutique sera nulle et inversement. 

3-  Peuplement 

Afin de tester les différentes fonctionnalité de notre base, nous avons effectué un peuplement de nos différentes tables.  

Nos tables sont peuplées comme suit : 



|Nom de la table |Nombre d’éléments |Nom de la table |Nombre d’éléments |
| - | - | - | - |
|Ville |5 |Client Particulier |4 |
|Province |5 |Client Boutique |5 |
|Code Postal |16 |Vélo |9 |
|Fidélité |3 |Composition Vélo |13 |
|Remise |5 |Commande |5 |
|Contenu Commande |7 |Supplément |6 |
|Fournisseur |6 |Approvisionnement |6 |
|Piece |5 |X |X |
III-  Structure du code appelant (C#) 

1-  Les requêtes 

Les fonctions ‘Requete’ et ‘Execute’ permettent respectivement d’exécuter « Queries » et « Non- Queries ». Elles nous permettent de ne pas avoir à la refaire à chaque requête SQL. On passe juste une string contenant le code SQL en paramètre. Ci-après un exemple d’utilisation de ‘Requete’ : 

![](Aspose.Words.e5964a3f-f463-4a2f-9e08-5ec97b5c9d67.004.png)

2-  Les Classes 

Les Classes qui ont été créés sont les suivantes : 



|Nom classe |Nom classe |Nom classe |
| - | - | - |
|Client Boutique |Client Particulier |Commande |
|Fournisseur |MyTuple |Pièce |
|Programme fidélité |Remise Boutique |Statistiques |
|Vélo |X |X |
Chaque classe ayant le nom d’une table réfère à cette même table. Elles sont composées des 3 mêmes fonctions de base adapté à chaque classe :  

- **La fonction Add() :** 

Une fonction qui fait une requête SQL pour créer une nouvelle instance dans la table. 

- **La fonction Edit() :** 

Une fonction qui utilise le mot clef SQL “Update” pour éditer une instance de la table.  

- **La fonction Del() :** 

Une fonction qui supprime une instance de la table. 

3-  Gestion des tables d’association 

Dans le code C#, les tables d’association du modèle E/A ne sont pas directement reliées à une classe portant leur nom.  Elles sont attachées à une classe puis stockées dans des listes. Par exemple, la classe Vélo comporte une liste de pièces et un nombre de pièces ce qui correspond à la table ‘Composition Vélo’. 

Cela  nous  donne  un  avantage  conséquent  dans  la  création  d’instances  car  toutes  les  tables d’association seront donc créées automatiquement.  

Reprenons l’exemple de vélo : Si l’on crée un vélo, la table composition vélo sera remplie de manière automatique.  

4-  Les exports vers Json et XML 

- **Json :**  

Nous avons une option dans le menu qui permet d’exporter toutes les tables en .json. Cet export est réalisé grâce à la librairie Web.Script.Serialization. 

- **XML :**  

Nous avons fait un export XML pour les pièces à faible stock associé au fournisseur de ces même pièces.  
