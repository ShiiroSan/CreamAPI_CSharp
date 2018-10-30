[LAST CHANGES]

[10/10/18]
Tout marche à l'heure actuelle. 

[30/10/18]
Probleme de connexion interrompu résolu. User agent défectueux, tout ça tout ça...
Correction de la partie réalisant l'écriture du appid au sein de l'ini. Utilisation d'une seule bibli ini-parser.
Cette fois, la valeur appid = X est vraiment remplacé, pas d'ajout d'un valeur bidon à la fin du fichier.
Même chose pour l'écriture dans [dlc], on passe direct pas ini-parser. Plus simple.
Création d'un appel constructeur unique pour toute la gestion ini.

Ajoutez la récupération automatique de la nouvelle mise à jour sur CSRINRU. [Bloqué tant que le site est pas accessible]
