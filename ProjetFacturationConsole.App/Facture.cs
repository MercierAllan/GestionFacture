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
    }
}