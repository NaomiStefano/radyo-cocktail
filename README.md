# Test Technique ASP.NET Core - Application de Cocktails

**Bienvenue dans ce test technique** dédié à évaluer vos compétences en développement avec ASP.NET Core, en suivant les principes de la Clean Architecture et en utilisant le pattern CQRS ainsi que le pattern Repository. 
Ce test comprend plusieurs exercices centrés autour d'une application de gestion de cocktails.

## Prérequis
Avoir la dernière version de [.Net](https://dotnet.microsoft.com/en-us/download), l'outil git ainsi que l'[outil d'Entity framework](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) sur votre machine.

Les variables d'environnement suivantes doivent être rajoutées pour le projet :

ASPNETCORE_ENVIRONMENT=Development;

ASPNETCORE_URLS=https://localhost:5001

Comment faire pour [pour Visual studio](https://www.microfocus.com/documentation/enterprise-developer/ed80/ED-VS2019/GUID-49AC1B04-C301-48DA-9DFE-9891B692CF1F.html)
et [pour Rider](https://www.jetbrains.com/help/rider/Unreal_Engine__EzArgs.html#arguments_ezargs)

## Objectifs

Vous serez amené à :

- Ajouter de nouvelles fonctionnalités,
- Implémenter des validations spécifiques,
- Ajouter des propriétés calculées,
- Et augmenter la couverture de tests de l'application.

## Processus 

1. **Clonage du Répertoire:** Une fois prêt(e), envoyez un email pour indiquer votre commencement. Vous recevrez automatiquement en retour le lien du repository GitHub contenant le projet de départ. Clonez-le pour débuter les exercices

2. **Organisation du Travail:** Chaque exercice est indépendant et peut être réalisé dans l'ordre de votre choix. Pour chaque exercice, partez de la branche principale (main), créez une nouvelle branche nommée feature/ex-X où X est le numéro de l'exercice.

3. **Soumission:** Une fois un exercice terminé, poussez votre branche sur le dépôt distant. Répétez le processus pour chaque exercice que vous souhaitez soumettre.

4. **Durée:** Vous disposez d'une heure et demie pour compléter autant d'exercices que possible mais n'hésitez à finaliser votre excercice en cours.

5. **Fin du Test:** Une fois le temps écoulé, merci de nous envoyer votre appréhension du test, votre ressenti et vos remarques éventuelles (astrid@radyo.be).

## Exercices

1. **Propriété "Description" dans l'entité "Cocktail"**
- Ajouter la propriété et effectuer une migration avec l'outil donet-ef
- Modifier certaines features et tests en conséquence.
- Ajouter une validation sur la description pour ne pas qu'elle dépasse les 100 caractères.

2. **Filtre au endpoint de récupération des cocktails**
- Ajouter un filtre permettant de rechercher les cocktails par le nom d'un ingrédient qui le compose.

3. **Unicité du nom du cocktail**
- Ajouter une validation sur le nom du cocktail au niveau applicatif pour qu'il soit unique.
- Ajouter la contrainte en base de données via la configuration d'Entity Framework.

4. **Propriété calculée**
- Ajouter une propriété calculée dans le retour de l'endpoint cocktail afin de calculer le taux d'alcool de celui-ci (arrondi à 2 décimales). Selon cette formule, où:

    - N le nombre d’ingrédients du cocktail en question.

    - vi le volume de l’ingrédient i.

    - ti le degré alcoolique de l’ingrédient i.

    - T le degré alcoolique final du cocktail.

    - D Le taux de dilution lié à la glace fondue. Pour l'exercice sela correspondra à 0.1 par glaçon présent dans le cocktail.

<center>

![alt text](https://latex.codecogs.com/gif.latex?%5CLARGE%20T%20%3D%20%5Cfrac%7B%5Csum%5Climits_%7Bi%3D1%7D%5EN%20v_i%20%5Ccdotp%20t_i%7D%7B%281+D%29%20%5Ccdotp%20%5Csum%5Climits_%7Bi%3D1%7D%5EN%20v_i%7D)

</center>

5. **Test intégration**
- Ajouter un test d'intégration pour couvrir la commande "CocktailAddStep"

6. **Query Filter**
- Ajouter un query filter global permettant de ne plus récupérer les entités qui sont supprimées de manière "logique".

6. **Sécurité**
- Implémenter l'authentification et l'autorisation à l'aide de JWT pour sécuriser les endpoints de l'api. L'issuer doit être valider, il est fourni dans l'appsettings.
Grâce au token (possibilité de le généré via swagger via le bouton "Authorize" sans avoir besoin d'un client_secret), je peux donc accèder à mes endpoints.

        Compte effectif : testing@radyo.be
        Mdp : mrw5aja.dax@qhf4WEC

7. **Permission**
- Impleter uns solution permettant d'autoriser un accès spécifique au endpoint en fonction des permissions présentes dans le token.

        Le compte testing@radyo.be/mrw5aja.dax@qhf4WEC possède les permissions
        - cocktail.read
        - cocktail.write
        - ingredient.read
        - ingredient.write

        Le compte testing.read@radyo.be/mrw5aja.dax@qhf4WEC possède les permissions
        - cocktail.read
        - ingredient.read

8. **Deploiement**
- Pour faciliter le déploiement et l'exécution dans différents environnements, créer un fichier Dockerfile approprié et des instructions pour exécuter l'application dans un conteneur Docker.