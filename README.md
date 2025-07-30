# Backend - Simulateur de Crédit (ASP.NET 4.8)

## Présentation

Ce projet est le backend du test technique SOFTT365.  
Il fournit une API REST en C# (.NET Framework 4.8) pour effectuer tous les calculs de crédit immobilier côté serveur, conformément au cahier des charges fourni.

- Les calculs sont **100% côté serveur** (AUCUN calcul côté client).
- L'API reçoit les données du formulaire, réalise tous les calculs, et renvoie les résultats complets (y compris le tableau d'amortissement).

## Structure du projet

- **Controllers/**
  - `CreditController.cs` : Contrôleur principal de l'API (endpoint `/api/credit/calculate`).
- **Services/**
  - `CreditCalculatorService.cs` : Service de calcul du crédit, toute la logique métier y est centralisée.
- **Models/**
  - `CreditInputModel.cs` : Données d'entrée (montant, fonds propres, durée, taux, etc.)
  - `CreditResultModel.cs` : Résultat du calcul (mensualité, montant à emprunter, tableau d'amortissement, etc.)
  - `AmortissementLigne.cs` : Ligne du tableau d'amortissement.

## Installation / Lancement

1. **Prérequis**
   - Visual Studio 2017/2019/2022
   - .NET Framework 4.8 installé

2. **Ouverture**
   - Ouvrir la solution dans Visual Studio.

3. **Restauration**
   - Restaurer les packages NuGet manquants (clic droit sur la solution > "Restaurer les packages NuGet").

4. **Compilation et exécution**
   - Compiler la solution.
   - Lancer le projet (F5 ou bouton "Démarrer").
   - L'API sera accessible sur :
     ```
     https://localhost:44389/api/credit/calculate
     ```
   - Peut être testée avec Postman ou le front Angular 2 fourni.

## Endpoint principal

### POST `/api/credit/calculate`

- **Body JSON** (exemple) :
```json
{
  "montantAchat": 120000,
  "fondsPropres": 20000,
  "dureeMois": 240,
  "tauxAnnuel": 2.4,
  "fraisAchat": null,
  "montantEmprunte": null,
  "fraisAchatAuto": true,
  "montantEmprunteAuto": true
}
```
- **Réponse** :
  - Montant à emprunter brut/net
  - Frais d'achat, frais d'hypothèque
  - Mensualité, taux mensuel
  - Fonds propres
  - Tableau d'amortissement complet (une ligne par mois)

## Détail sur les calculs et commentaires

- Toute la logique métier se trouve dans **`CreditCalculatorService`**.
- **Chaque étape de calcul est clairement commentée dans le code** :
  - Frais d'achat (auto ou manuel)
  - Calcul du montant à emprunter brut/net
  - Frais d'hypothèque (2%)
  - Calcul du taux mensuel (formule exacte, arrondi à 3 décimales)
  - Calcul de la mensualité (formule annuité, arrondi à 2 décimales)
  - Génération du tableau d'amortissement (avec ajustement de la dernière ligne pour finir à 0)
- Les commentaires dans le code expliquent la logique de chaque section, pour faciliter la compréhension et la maintenance.

## Exemple de test

Utilisez Postman :
- Méthode : POST
- URL : `https://localhost:44389/api/credit/calculate`
- Body : (cf. exemple ci-dessus)
- Résultat : JSON avec tous les champs attendus.

## Support

Pour toute question technique, contactez Amira Ben Moussa (amira.benmoussa@softt365.com) en mettant Mr Zied en copie.

---

© 2025 - Aymen Khiari
