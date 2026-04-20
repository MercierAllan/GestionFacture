using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetFacturationConsole.App;
using System;

namespace ProjetFacturationConsole.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculerTotalHT()
        {
            LigneFacture ligne = new LigneFacture("Test", 2, 150, 20);
            decimal totalHT = ligne.CalculerTotalHT();
            Assert.AreEqual(300, totalHT);
        }

        [TestMethod]
        public void TestCalculerTotalTTC()
        {
            LigneFacture ligne = new LigneFacture("Test", 2, 150, 20);
            decimal totalTTC = ligne.CalculerTotalTTC();
            Assert.AreEqual(360, totalTTC);
        }

        [TestMethod]
        public void TestCalculerTotalTTCFacture()
        {
            Facture facture = new Facture("F2026-001", DateTime.Now, new Entreprise(1, "TechNova", "contact@technova.fr", "0322000001", "25 rue des Lilas", "Amiens", "80000", "12345678900011"),
                new Client(1, "Paul Durand", "paul.durand@mail.fr", "0611223344", "12 rue Victor Hugo", "Amiens", "80000", DateTime.Now));

            facture.AjouterLigne(new LigneFacture("Développement module connexion", 2, 150, 20));
            facture.AjouterLigne(new LigneFacture("Maintenance corrective", 1, 80, 10));

            decimal totalTTC = facture.CalculerTotalTTC();
            Assert.AreEqual(448, totalTTC);
        }
    }
}