# 📘 Guideline — Créer une application Blazor

Ce document décrit les étapes pour créer une application Blazor WebAssembly **Standalone** avec Visual Studio, et donne une guideline pour organiser et développer le projet.

---

## Planifier le projet

- 🎯 Définir les objectifs fonctionnels : que doit faire l’application ?
- 📑 Répertorier les pages/components attendus (par exemple : Login, Dashboard, CRUD sur des entités…)
- 🗂️ Définir les modèles de données (DTO, entités…)
- 🔒 Penser aux aspects sécurité dès le début (authentification / autorisation)
- 🎨 Prévoir un design ou thème (Bootstrap, Tailwind, MudBlazor…)

---

## Créer le projet (avec Visual Studio en mode _Standalone_)

### 🖱️ Étapes :

#### 1. Ouvrir Visual Studio

- Lancer Visual Studio.
- Cliquer sur **Créer un nouveau projet**.

#### 2. Choisir le modèle

- Dans la boîte de dialogue :
  - Rechercher _Blazor_.
  - Sélectionner **Blazor WebAssembly Standalone App**.
  - Cliquer sur **Suivant**.

#### 3. Configurer le projet

- Donner un **nom** au projet (exemple : `MonApplicationBlazor`).
- Choisir un emplacement et un nom de solution si nécessaire.
- Cliquer sur **Suivant**.

#### 4. Configurer les options supplémentaires

- Dans la page _Frameworks_ :
  - Vérifier que le **Framework cible** est `.NET 8` (ou supérieur).
  - Déocher toutes les options
  - Cocher uniquement  "_Configurer pour HTTPS_"

#### 5. Créer le projet

- Cliquer sur **Créer**.

Visual Studio va générer l’arborescence d’un projet Blazor WebAssembly Standalone avec les fichiers suivants :

- `Program.cs`
- `App.razor`
- `Layout/MainLayout.razor`
- `Pages/`
- `wwwroot/`

#### 6. Tester

- Appuyer sur **F5** pour lancer le projet en mode débogage.
- Le navigateur s’ouvre à `https://localhost:xxxx` et affiche la page d’accueil Blazor.

---

## Configurer la structure

- 🧹 Nettoyer le projet des pages/components inutiles (comme `Counter.razor` si vous avez cocher "inclure le contenu de base")
- 🗂️ Créer des dossiers pour organiser :
  ```
  /Pages
  /Services
  /Models
  /Middlewares
  /States
  /Shared
  /wwwroot (assets statiques)
  ```
- `/Pages` : pour les pages principales de l’application
- `/Services` : pour les services d’accès aux données et API
- `/Models` : pour les modèles de données
- `/Middlewares` : pour les middlewares personnalisés (comme `TokenInterceptor.cs`)
- `/States` : pour la gestion d’état (si nécessaire)
- `/Shared` : pour les composants partagés (comme `NavMenu.razor`, `Footer.razor`)
- `/wwwroot` : pour les fichiers statiques (CSS, JS, images)

---

## Authentification et Autorisation

1.  Implémenter un service d’authentification (par exemple, `AuthService.cs` dans le dossier `Services`) qui communique avec l'API pour récupérer un token JWT.
2.  Créer un state pour gérer l’authentification (par exemple, `AuthState.cs` dans le dossier `State`).
    3 . Utiliser le middleware pour intercepter les requêtes et ajouter le token JWT si nécessaire (ce qui permet d'utiliser les routes sécurisées de l'API).

        - Middleware : `TokenInterceptor.cs` dans le dossier `Middlewares`.
        - sans oublié d'ajouter le middleware dans `program.cs` :

          ```csharp
              builder.Services.AddScoped<TokenInterceptor>();
              builder.Services.AddHttpClient("API", client =>
              {
                  client.BaseAddress = new Uri("https://localhost:7104/");
              }).AddHttpMessageHandler<TokenInterceptor>();
          ```

3.  Page de connexion (par exemple, `Login.razor` dans le dossier `Pages`) et redirection vers le dashboard après connexion réussie.
4.  Protéger les routes/pages nécessitant une authentification en utilisant l’attribut `[Authorize]` sur les composants Razor.
5.  Gérer la déconnexion en supprimant le token et en redirigeant vers la page de connexion.

## Models

Créer des modèles de données dans le dossier `Models` pour représenter les entités de votre application. Attention à bien respecter les conventions de nommage et à analyser les DTOs de l'API si vous consommez une API. (Par exemple, `ProductL.cs` représente un produit reçu de l'API lorsque l'on souhaite récupérer la liste des produits, `ProductCreateForm` représente un formulaire de création de produit, `Product` représente un produit complet avec toutes ses propriétés, etc.).

## Services

Créer des services dans le dossier `Services` pour encapsuler la logique métier et l’accès aux données. (Par exemple, `ProductService.cs` pour gérer les produits, `AuthService.cs` pour l’authentification).

## Pages et Composants

Créer des pages et composants dans le dossier `Pages` et `Shared` :

- Pages principales (par exemple, `Home.razor`, `Login.razor`, `ProductList.razor`), les pages sont accessible via des URL.
- Composants partagés (par exemple, `NavMenu.razor`, `Footer.razor`, `ProductCard.razor`), les composants peuvent être placer dans des pages (ou d'autre composant).
- Utiliser des routes pour naviguer entre les pages (par exemple, `@page "/login"` dans `Login.razor`).
- Utiliser des paramètres de route pour passer des données entre les pages (par exemple, `@page "/product/{id:int}"` dans `ProductDetails.razor`).

## Styles et Thèmes

Utiliser des bibliothèques de styles comme Bootstrap, Tailwind CSS ou MudBlazor pour styliser l’application. Ajouter les fichiers CSS nécessaires dans le dossier `wwwroot/css` et les référencer dans `index.html`.

Note: Si vous souhaitez utiliser des fichiers CSS encapsulés dans des composants, vous pouvez utiliser les fichiers `.razor.css` pour chaque composant. Par exemple, si vous avez un composant `ProductCard.razor`, vous pouvez créer un fichier `ProductCard.razor.css` pour y mettre les styles spécifiques à ce composant et décommencer la ligne `<link href="MonProjet.styles.css" rel="stylesheet" />` dans `index.html`.

---

🚀 Bonne création de ton application Blazor !
