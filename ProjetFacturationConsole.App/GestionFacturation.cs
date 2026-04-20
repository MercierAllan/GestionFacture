// GestionFacturation.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjetFacturationConsole.App
{
    public class GestionFacturation
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<Entreprise> Entreprises { get; set; } = new List<Entreprise>();
        public Dictionary<int, Client> DictionnaireClients { get; set; } = new Dictionary<int, Client>();
        public Dictionary<int, Entreprise> DictionnaireEntreprises { get; set; } = new Dictionary<int, Entreprise>();

        public void AfficherMenu()
        {
            bool quitter = false;
            while (!quitter)
            {
                Console.WriteLine("1 - Importer les clients depuis le CSV");
                Console.WriteLine("2 - Importer les entreprises depuis le CSV");
                Console.WriteLine("3 - Afficher les clients");
                Console.WriteLine("4 - Afficher les entreprises");
                Console.WriteLine("5 - Créer une facture");
                Console.WriteLine("6 - Afficher le carnet de contacts");
                Console.WriteLine("0 - Quitter");

                Console.Write("Choisissez une option : ");
                string choix = Console.ReadLine();
            }
        }
    }
}