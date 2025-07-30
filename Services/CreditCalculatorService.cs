using System;
using System.Collections.Generic;
using SimpleFrameworkApp.Models;

namespace SimpleFrameworkApp.Services
{
    public class CreditCalculatorService
    {
        public CreditResultModel Calculate(CreditInputModel input)
        {
            // 1. Frais d'achat
            decimal fraisAchat;
            if (input.FraisAchatAuto)
            {
                fraisAchat = input.MontantAchat > 50000 ? Math.Round(input.MontantAchat * 0.10m, 2) : 0m;
            }
            else
            {
                fraisAchat = input.FraisAchat ?? 0;
            }

            // 2. Montant à emprunter BRUT ou calcul des fonds propres
            decimal montantEmprunteBrut;
            decimal fondsPropres = input.FondsPropres;
            if (input.MontantEmprunteAuto)
            {
                montantEmprunteBrut = input.MontantAchat + fraisAchat - fondsPropres;
            }
            else
            {
                montantEmprunteBrut = input.MontantEmprunte ?? 0;
                fondsPropres = input.MontantAchat + fraisAchat - montantEmprunteBrut;
            }

            // 3. Frais d'hypothèque (2% du montant à emprunter brut)
            decimal fraisHypotheque = Math.Round(montantEmprunteBrut * 0.02m, 2);

            // 4. Montant à emprunter NET
            decimal montantEmprunteNet = montantEmprunteBrut + fraisHypotheque;

            // 5. Taux d'intérêt mensuel arrondi à 3 décimales
            decimal tauxMensuel = (decimal)(Math.Pow(1 + (double)(input.TauxAnnuel / 100), 1.0 / 12) - 1);
            tauxMensuel = Math.Round(tauxMensuel * 100, 3); // en pourcentage
            decimal tauxMensuelDec = Math.Round(tauxMensuel / 100, 5); // pour le calcul

            // 6. Mensualité (formule annuité)
            decimal mensualite = montantEmprunteNet * tauxMensuelDec *
                (decimal)Math.Pow(1 + (double)tauxMensuelDec, input.DureeMois) /
                ((decimal)Math.Pow(1 + (double)tauxMensuelDec, input.DureeMois) - 1);
            mensualite = Math.Round(mensualite, 2);

            // 7. Tableau d’amortissement
            var tableau = new List<AmortissementLigne>();
            decimal soldeDebut = montantEmprunteNet;

            for (int periode = 1; periode <= input.DureeMois; periode++)
            {
                decimal interet = Math.Round(soldeDebut * tauxMensuelDec, 2);
                decimal capitalRembourse = mensualite - interet;
                decimal soldeFin = soldeDebut - capitalRembourse;

                // Dernière ligne : solde de fin doit être 0
                if (periode == input.DureeMois)
                {
                    capitalRembourse = soldeDebut;
                    interet = mensualite - capitalRembourse;
                    soldeFin = 0;
                }

                tableau.Add(new AmortissementLigne
                {
                    Periode = periode,
                    SoldeDebut = Math.Round(soldeDebut, 2),
                    Mensualite = mensualite,
                    Interet = interet,
                    CapitalRembourse = Math.Round(capitalRembourse, 2),
                    SoldeFin = Math.Round(soldeFin, 2)
                });

                soldeDebut = soldeFin;
            }

            // 8. Retourne le résultat
            return new CreditResultModel
            {
                MontantEmprunteBrut = Math.Round(montantEmprunteBrut, 2),
                FraisAchat = Math.Round(fraisAchat, 2),
                FraisHypotheque = Math.Round(fraisHypotheque, 2),
                MontantEmprunteNet = Math.Round(montantEmprunteNet, 2),
                Mensualite = mensualite,
                TauxMensuel = tauxMensuel,
                FondsPropres = Math.Round(fondsPropres, 2),
                TableauAmortissement = tableau
            };
        }
    }
}