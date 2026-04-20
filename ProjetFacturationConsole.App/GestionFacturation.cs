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
                Console.WriteLine("\n1 - Importer les clients depuis le CSV");
                Console.WriteLine("2 - Importer les entreprises depuis le CSV");
                Console.WriteLine("3 - Afficher les clients");
                Console.WriteLine("4 - Afficher les entreprises");
                Console.WriteLine("5 - Créer une facture");
                Console.WriteLine("6 - Afficher le carnet de contacts");
                Console.WriteLine("0 - Quitter");

                Console.Write("\nChoisissez une option : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        ImporterClientsDepuisCsv();
                        break;
                    case "2":
                        ImporterEntreprisesDepuisCsv();
                        break;
                    case "3":
                        AfficherClients();
                        break;
                    case "4":
                        AfficherEntreprises();
                        break;
                    case "5":
                        CreerFacture();
                        break;
                    case "6":
                        AfficherCarnetContacts();
                        break;
                    case "0":
                        quitter = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide.");
                        break;
                }
            }
        }
        // Fait différamment car sinon problème avec le code Microsoft.
        public void ImporterClientsDepuisCsv()
        {
            string cheminFichier = "clients.csv";
            if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Le fichier clients.csv n'existe pas");
            return;
        }

        Clients.Clear();
        DictionnaireClients.Clear();

        using (var reader = new StreamReader(cheminFichier))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                int id = int.Parse(values[0]);
                string nom = values[1];
                string email = values[2];
                string telephone = values[3];
                string adresse = values[4];
                string ville = values[5];
                string codePostal = values[6];
                DateTime dateInscription = DateTime.Parse(values[7]);

                Client client = new Client(id, nom, email, telephone, adresse, ville, codePostal, dateInscription);
                Clients.Add(client);
                DictionnaireClients.Add(id, client);
            }
        }

        string json = JsonSerializer.Serialize(Clients, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("clients.json", json);
        Console.WriteLine("Import des clients terminé et clients.json généré");
        }

        // Fait différamment car sinon problème avec le code Microsoft.
        public void ImporterEntreprisesDepuisCsv()
        {
            string cheminFichier = "entreprises.csv";
        if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Le fichier entreprises.csv n'existe pas");
            return;
        }

        Entreprises.Clear();
        DictionnaireEntreprises.Clear();

        using (var reader = new StreamReader(cheminFichier))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                int id = int.Parse(values[0]);
                string nom = values[1];
                string email = values[2];
                string telephone = values[3];
                string adresse = values[4];
                string ville = values[5];
                string codePostal = values[6];
                string siret = values[7];

                Entreprise entreprise = new Entreprise(id, nom, email, telephone, adresse, ville, codePostal, siret);
                Entreprises.Add(entreprise);
                DictionnaireEntreprises.Add(id, entreprise);
            }
        }

        string json = JsonSerializer.Serialize(Entreprises, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("entreprises.json", json);
        Console.WriteLine("Import des entreprises terminé et entreprises.json généré");
        }

        public void ChargerClientsDepuisJson()
        {
            string cheminFichier = "clients.json";
        if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Le fichier clients.json n'existe pas");
            return;
        }
        
        string json = File.ReadAllText(cheminFichier);
        Clients = JsonSerializer.Deserialize<List<Client>>(json);
        DictionnaireClients.Clear();
        foreach (var client in Clients)
        {
            DictionnaireClients.Add(client.Id, client);
        }
        }

        public void ChargerEntreprisesDepuisJson()
        {
            string cheminFichier = "entreprises.json";
        if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Le fichier entreprises.json n'existe pas");
            return;
        }
        
        string json = File.ReadAllText(cheminFichier);
        Entreprises = JsonSerializer.Deserialize<List<Entreprise>>(json);
        DictionnaireEntreprises.Clear();
        foreach (var entreprise in Entreprises)
        {
            DictionnaireEntreprises.Add(entreprise.Id, entreprise);
        }
        }

        public void AfficherClients()
        {
            if (Clients.Count == 0)
            {
                ChargerClientsDepuisJson();
            }
            Console.WriteLine("Liste des Clients : ");
            foreach (var client in Clients)
            {
                Console.WriteLine($"{client.Id} - {client.Nom} - {client.Email} - {client.Telephone} - {client.Adresse} - {client.Ville} - {client.CodePostal} - {client.DateInscription:dd/MM/yyyy}");
            }
        }

        public void AfficherEntreprises()
        {
            if (Entreprises.Count == 0)
            {
                ChargerEntreprisesDepuisJson();
            }
            Console.WriteLine("Liste des Entreprises : ");
            foreach (var entreprise in Entreprises)
            {
                Console.WriteLine($"{entreprise.Id} - {entreprise.Nom} - {entreprise.Email} - {entreprise.Telephone} - {entreprise.Adresse} - {entreprise.Ville} - {entreprise.CodePostal} - {entreprise.Siret}");
            }
        }

        public void CreerFacture()
        {
        }

        public void GenererFichierTexteFacture(Facture facture)
        {
        }

        public void AfficherCarnetContacts()
        {
        }
    }
}