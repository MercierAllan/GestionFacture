// GestionFacturation.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            if (Clients.Count == 0)
            {
                ChargerClientsDepuisJson();
            }
            if (Entreprises.Count == 0)
            {
                ChargerEntreprisesDepuisJson();
            }

            Console.WriteLine("Liste des entreprises : ");
            foreach (var entrepriseAffichee in Entreprises)
            {
                Console.WriteLine($"{entrepriseAffichee.Id} - {entrepriseAffichee.Nom}");
            }

            Console.Write("Entrez l'ID de l'entreprise : ");
            int idEntreprise = int.Parse(Console.ReadLine());
            Entreprise entreprise = DictionnaireEntreprises[idEntreprise];

            Console.WriteLine("Liste des clients : ");
            foreach (var clientAffiche in Clients)
            {
                Console.WriteLine($"{clientAffiche.Id} - {clientAffiche.Nom}");
            }

            Console.Write("Entrez l'ID du client : ");
            int idClient = int.Parse(Console.ReadLine());
            Client client = DictionnaireClients[idClient];

            Console.Write("Date d'émission (dd/MM/yyyy) : ");
            DateTime dateEmission = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Facture facture = new Facture($"F{DateTime.Now.Year})-{Guid.NewGuid().ToString().Substring(0, 3).ToUpper()}", dateEmission, entreprise, client);

            bool ajouterLigne = true;
            while (ajouterLigne)            {
                Console.Write("Description de la ligne : ");
                string description = Console.ReadLine();

                Console.Write("Quantité : ");
                int quantite = int.Parse(Console.ReadLine());

                Console.Write("Prix unitaire HT : ");
                decimal prixUnitaireHT = decimal.Parse(Console.ReadLine());

                Console.Write("Taux de TVA (%) : ");
                decimal tauxTVA = decimal.Parse(Console.ReadLine());

                LigneFacture ligne = new LigneFacture(description, quantite, prixUnitaireHT, tauxTVA);
                facture.AjouterLigne(ligne);

                Console.Write("Ajouter une autre ligne ? (oui/non) : ");
                ajouterLigne = Console.ReadLine().ToLower() == "oui";
            }

            facture.AfficherFacture();

            Console.Write("Confirmer la génération de la facture ? (oui/non) : ");
            if (Console.ReadLine().ToLower() == "oui")
            {
                GenererFichierTexteFacture(facture);
                Console.WriteLine("Facture générée avec succès !");
            }

        }

        public void GenererFichierTexteFacture(Facture facture)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# FACTURE");
            sb.AppendLine($"Numéro: {facture.Numero}");
            sb.AppendLine($"Date d'émission : {facture.DateEmission:dd/MM/yyyy}");
            sb.AppendLine($"Date d'échéance : {facture.DateEcheance:dd/MM/yyyy}");
            sb.AppendLine($"Statut : {facture.Statut}");
            sb.AppendLine("\nEntreprise :");
            sb.AppendLine($"{facture.Entreprise.Id} - {facture.Entreprise.Nom} - {facture.Entreprise.Email} - {facture.Entreprise.Telephone} - {facture.Entreprise.Adresse} - {facture.Entreprise.Ville} - {facture.Entreprise.CodePostal} - {facture.Entreprise.Siret}");
            sb.AppendLine("\nClient :");
            sb.AppendLine($"{facture.Client.Id} - {facture.Client.Nom} - {facture.Client.Email} - {facture.Client.Telephone} - {facture.Client.Adresse} - {facture.Client.Ville} - {facture.Client.CodePostal} - {facture.Client.DateInscription:dd/MM/yyyy}");
            sb.AppendLine("\nLignes :");

            int i = 1;
            foreach (var ligne in facture.Lignes)
            {
                sb.AppendLine($"{i}. {ligne.Description} - Qte : {ligne.Quantite} - PU HT : {ligne.PrixUnitaireHT} - TVA : {ligne.TauxTVA} - Total HT : {ligne.CalculerTotalHT()} - Total TTC : {ligne.CalculerTotalTTC()}");
                i++;
            }

            sb.AppendLine($"\nTotal HT : {facture.CalculerTotalHT()}");
            sb.AppendLine($"Total TVA: {facture.CalculerTotalTVA()}");
            sb.AppendLine($"Total TTC: {facture.CalculerTotalTTC()}");

            string nomFichier = $"Facture_{facture.Numero}.txt";
            File.WriteAllText(nomFichier, sb.ToString());
            Console.WriteLine($"Fichier {nomFichier} généré avec succès");
        }

        public void AfficherCarnetContacts()
        {
        }
    }
}