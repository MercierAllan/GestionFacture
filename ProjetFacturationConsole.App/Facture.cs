// Facture.cs
using System;
using System.Text;

namespace ProjetFacturationConsole.App
{
    public class Facture : DocumentCommercial
    {
        public string Numero { get; set; }
        public DateTime DateEmission { get; set; }
        public DateTime DateEcheance { get; set; }
        public string Statut { get; set; }
        public Entreprise Entreprise { get; set; }
        public Client Client { get; set; }

        public Facture(string numero, DateTime dateEmission, Entreprise entreprise, Client client)
        {
            Numero = numero;
            DateEmission = dateEmission;
            DateEcheance = dateEmission.AddDays(30);
            Statut = "Brouillon";
            Entreprise = entreprise;
            Client = client;
        }

        public void AfficherFacture()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# FACTURE");
            sb.AppendLine($"Numéro: {Numero}");
            sb.AppendLine($"Date d'émission : {DateEmission:dd/MM/yyyy}");
            sb.AppendLine($"Date d'échéance : {DateEcheance:dd/MM/yyyy}");
            sb.AppendLine($"Statut : {Statut}");
            sb.AppendLine("\nEntreprise :");
            Entreprise.AfficherInfos();
            sb.AppendLine("\nClient :");
            Client.AfficherInfos();
            sb.AppendLine("\nLignes :");

            int i = 1;
            foreach (var ligne in Lignes)
            {
                sb.AppendLine($"{i}. {ligne.Description} - Qte : {ligne.Quantite} - PU HT : {ligne.PrixUnitaireHT} - TVA : {ligne.TauxTVA} - Total HT : {ligne.CalculerTotalHT()} - Total TTC : {ligne.CalculerTotalTTC()}");
                i++;
            }

            sb.AppendLine($"\nTotal HT : {CalculerTotalHT()}");
            sb.AppendLine($"Total TVA: {CalculerTotalTVA()}");
            sb.AppendLine($"Total TTC: {CalculerTotalTTC()}");

            Console.WriteLine(sb.ToString());
        }
    }
}