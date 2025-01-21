# TP_Unity_Elkasbaoui

# Application Unity - Statistiques et Gestion des Commentaires

## Fonctionnalités

- **Connexion/Inscription utilisateur** avec authentification via un WebService.
- **Ajout de commentaires** : les utilisateurs peuvent poster des commentaires pour chaque partie.
- **Affichage des statistiques des jeux** : les utilisateurs peuvent voir leurs dernières statistiques de parties.
- **Création d'une nouvelle partie** : les utilisateurs peuvent voir leurs dernières statistiques de parties.
- **Affichage dynamique des données** : mise à jour des statistiques et des commentaires en temps réel.

## Prérequis

Avant de commencer, assurez-vous d'avoir installé les éléments suivants :

- **Unity 2022.3** ou une version supérieure.
- **Base de données MySQL**.
- **Php 7.4** ou une version supérieure.

## Installation

Suivez les étapes ci-dessous pour installer et configurer l'application sur votre machine locale.

### Partie Unity

1. **Clonez le projet Unity** :
   ```bash
   git clone https://github.com/ismaelel/TP_Unity_Elkasbaoui
   ```

2. **Ouvrez le projet dans Unity Hub** :
   - Cliquez sur **Add** et sélectionnez le dossier du projet cloné.

3. **Vérifiez les URLs du WebService** dans les scripts Unity :
   - Modifiez les URLs dans les scripts du dossier Script pour pointer vers votre serveur local ou distant.

### Partie Backend (WebService)

1. **Clonez le backend Flask** :
   ```bash
   git clone https://github.com/ismaelel/unity_project
   ```

2. **Configurez votre serveur web (Apache/Nginx) :**


Placez les fichiers du backend dans le répertoire racine du serveur web (par exemple, /var/www/unity_project).
   

3. **Créez une base de données MySQL :**
  Connectez-vous à votre serveur MySQL et exécutez le script SQL https://github.com/ismaelel/unity_project/blob/main/unity1.sql .
    

4. **Configurez les paramètres de connexion à la base de données :**
   Modifiez le fichier config.php avec vos informations :
    ```php
        <?php
        define('DB_HOST', 'localhost');
        define('DB_NAME', 'unity1');
        define('DB_USER', 'root');
        define('DB_PASSWORD', 'root');
        ?>
    ```


## Déploiement

### Créer un Build Unity

1. **Configurez le projet pour Android ou une autre plateforme cible.**
2. **Générez le build** depuis **File > Build Settings > Build**.

## Astuces

### Début

Lancer l'application depuis la scène "Login"
### Connexion 

Vous pouvez créer un compte et vous connecter avec, ou voici des identifiants : 

    - identifiant : ismael
    - mot de passe : ism123

### Statistiques

Les dernières parties sont affichées, vous pouvez lancer une partie qui se finira automatiquement avec des données aléatoires, pour tester l'affichage. 
Les statistiques s'affichent pour l'utilisateur en cours.

### Musique

N'hésitez pas à activer votre son et découvrir la bande son officielle du jeu !
