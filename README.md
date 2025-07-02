# 🛒 Application de gestion de produits et panier (Blazor Standalone)

Vous allez développer une **petite application Blazor Standalone** pour gérer une liste de produits et un panier d’achat. Cette application ne fait **aucun appel API** : toutes les données doivent être **stockées en mémoire** (dans des listes locales).

## 🎯 Objectifs

Votre application doit permettre à l’utilisateur de :

1. **Afficher la liste des produits disponibles**

   - Chaque produit contient au minimum : `Nom`, `Prix`, `Réduction (%)`, `Description`.

2. **Ajouter un nouveau produit**

   - Un formulaire vous permettra de saisir les informations d’un nouveau produit.

3. **Modifier un produit existant**

   - L’utilisateur doit pouvoir modifier les informations d’un produit (par exemple, son prix ou sa description).

4. **Supprimer un produit**

   - L’utilisateur doit pouvoir retirer un produit de la liste.

5. **Ajouter un produit au panier**

   - L’utilisateur peut ajouter un ou plusieurs produits dans un panier d’achat.

6. **Gérer la quantité de chaque produit dans le panier**

   - Si un produit est déjà dans le panier, l’ajout d’un même produit doit **augmenter le nombre d’exemplaires**, pas créer une nouvelle ligne.

7. **Afficher visuellement les réductions importantes**
   - Tout produit ayant une **réduction de 50% ou plus** doit apparaître avec **le nom en rouge** dans la liste des produits.

## ✅ Contraintes techniques

- Utilisez uniquement **Blazor Standalone**.
- Aucune persistance des données (tout est en mémoire).
