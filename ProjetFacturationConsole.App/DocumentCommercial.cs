using System.Collections.Generic;

namespace ProjetFacturationConsole.App
{
    public abstract class DocumentCommercial
    {
        public List<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();

        public void AjouterLigne(LigneFacture ligne)
        {
            Lignes.Add(ligne);
        }

        public decimal CalculerTotalHT()
        {
            decimal totalHT = 0;
            foreach (var ligne in Lignes)
            {
                totalHT += ligne.CalculerTotalHT();
            }
            return totalHT;
        }

        public decimal CalculerTotalTVA()
        {
            decimal totalTVA = 0;
            foreach (var ligne in Lignes)
            {
                totalTVA += ligne.CalculerMontantTVA();
            }
            return totalTVA;
        }

        public decimal CalculerTotalTTC()
        {
            decimal totalTTC = 0;
            foreach (var ligne in Lignes)
            {
                totalTTC += ligne.CalculerTotalTTC();
            }
            return totalTTC;
        }
    }
}