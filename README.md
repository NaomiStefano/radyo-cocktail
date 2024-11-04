# Test Technique ASP.NET Core - Application de Cocktails

**Bienvenue !** Ce test a pour objectif d’évaluer vos compétences en développement avec ASP.NET Core en appliquant les principes de la Clean Architecture, le pattern CQRS, et le pattern Repository. Le thème de ce test est une application de gestion de cocktails.

## Prérequis
Assurez-vous d’avoir la dernière version de [.NET](https://dotnet.microsoft.com/en-us/download), l’outil Git, ainsi que l’[outil Entity Framework](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) installés sur votre machine.

Ajoutez les variables d’environnement suivantes au projet :

- `ASPNETCORE_ENVIRONMENT=Development`
- `ASPNETCORE_URLS=https://localhost:5001`

Pour les configurer, consultez les instructions pour [Visual Studio](https://www.microfocus.com/documentation/enterprise-developer/ed80/ED-VS2019/GUID-49AC1B04-C301-48DA-9DFE-9891B692CF1F.html) ou pour [Rider](https://www.jetbrains.com/help/rider/Unreal_Engine__EzArgs.html#arguments_ezargs).

**Note :** Le projet doit être lancé en mode **DEBUG**.

Une fois le projet lancé, Swagger sera accessible via l’URL suivante : `https://localhost:5001/swagger/index.html`

## Objectifs

Votre mission sera de :
- Ajouter de nouvelles fonctionnalités
- Implémenter des validations spécifiques
- Ajouter des propriétés calculées
- Améliorer la couverture de tests

## Processus

1. **Clonage du Répertoire :** Envoyez un email pour signaler le début du test. Vous recevrez un lien vers le repository GitHub du projet de départ. Clonez ce repository pour commencer.

2. **Organisation du Travail :** Chaque exercice est indépendant. Pour chaque exercice, partez de la branche principale (`main`) et créez une nouvelle branche nommée `feature/ex-X` où X est le numéro de l’exercice.

3. **Soumission :** Une fois un exercice terminé, poussez la branche correspondante sur le dépôt distant. Répétez pour chaque exercice complété.

4. **Durée :** Vous avez une heure et demie pour réaliser un maximum d'exercices. Terminez l’exercice en cours même si le temps est écoulé. Vous pouvez poursuivre au-delà si vous le souhaitez.

5. **Feedback :** À la fin du test, envoyez votre ressenti et vos remarques éventuelles à [astrid@radyo.be](mailto:astrid@radyo.be).

## Exercices

1. **Ajout de la propriété "Description" dans l’entité "Cocktail"**
   - Ajouter la propriété `Description` (nullable) à l’entité et réaliser une migration avec `dotnet-ef`.
   - Un script (ps1 ou sh selon l’environnement) est disponible dans `src/Infrastructure` :
     ```ps
     .\migrations.ps1 NomDeLaMigration
     ```
     ou
     ```sh
     .\migrations.sh NomDeLaMigration
     ```
   - Modifier les fonctionnalités et tests en conséquence, et ajouter une validation limitant la description à 100 caractères maximum.

2. **Filtrage par ingrédient**
   - Ajouter un filtre au endpoint de récupération des cocktails permettant la recherche par nom d’ingrédient.

3. **Unicité du nom du cocktail**
   - Implémenter une validation d’unicité du nom au niveau applicatif (ex. : validateur asynchrone de Fluent Validation).
   - Ajouter une contrainte d’unicité en base de données via Entity Framework.

4. **Propriété calculée pour le taux d’alcool**
   - Calculer le taux d’alcool (arrondi à deux décimales) pour chaque cocktail dans le retour de l’endpoint.
   - Utiliser la formule suivante :
     
     \[ T = \frac{\sum_{i=1}^N v_i \cdot t_i}{(1 + D) \cdot \sum_{i=1}^N v_i} \]

     où `N` est le nombre d’ingrédients, `v_i` est le volume d’un ingrédient, `t_i` est le degré alcoolique de l’ingrédient, et `D` est le taux de dilution (0,1 par glaçon). Idéalement, cette propriété devrait être calculée dans le DTO via une projection.

5. **Test d’intégration**
   - Ajouter un test d’intégration pour la commande `CocktailAddStep`.

6. **Query Filter global**
   - Mettre en place un query filter global pour exclure les entités marquées comme "logiquement supprimées".

7. **Sécurisation avec JWT**
   - Implémenter l’authentification et l’autorisation via JWT. Le `token` peut être généré via Swagger avec le bouton "Authorize".

     Compte de test : 
     - `Email` : testing@radyo.be
     - `Mot de passe` : mrw5aja.dax@qhf4WEC

8. **Gestion des permissions**
   - Contrôler l’accès aux endpoints en fonction des permissions dans le token JWT.

     Permissions des comptes de test :
     - testing@radyo.be/mrw5aja.dax@qhf4WEC : `cocktail.read`, `cocktail.write`, `ingredient.read`, `ingredient.write`
     - testing.read@radyo.be/mrw5aja.dax@qhf4WEC : `cocktail.read`, `ingredient.read`

9. **Déploiement avec Docker**
   - Créer un `Dockerfile` pour faciliter le déploiement et les instructions pour exécuter l’application dans un conteneur Docker.

