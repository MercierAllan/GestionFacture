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

        public void ImporterClientsDepuisCsv()
        {
            try
            {
                string cheminFichier = "clients.csv";
                if (!File.Exists(cheminFichier))
                {
                    throw new FileNotFoundException("Le fichier clients.csv n'existe pas.");
                }

                Clients.Clear();
                DictionnaireClients.Clear();

                using (var reader = new StreamReader(cheminFichier))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        if (values.Length < 8)
                        {
                            throw new FormatException("Format de ligne invalide dans le fichier clients.csv.");
                        }

                        int id = int.Parse(values[0]);
                        string nom = values[1];
                        string email = values[2];
                        string telephone = values[3];
                        string adresse = values[4];
                        string ville = values[5];
                        string codePostal = values[6];
                        DateTime dateInscription = DateTime.Parse(values[7]);

                        if (dateInscription > DateTime.Now)
                        {
                            throw new ArgumentException($"La date d'inscription du client {nom} ne peut pas être supérieure à la date du jour.");
                        }

                        Client client = new Client(id, nom, email, telephone, adresse, ville, codePostal, dateInscription);
                        Clients.Add(client);
                        DictionnaireClients.Add(id, client);
                    }
                }

                string json = JsonSerializer.Serialize(Clients, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("clients.json", json);
                Console.WriteLine("Import des clients terminé et clients.json généré.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'import des clients : {ex.Message}");
            }
        }

        public void ImporterEntreprisesDepuisCsv()
        {
            try
            {
                string cheminFichier = "entreprises.csv";
                if (!File.Exists(cheminFichier))
                {
                    throw new FileNotFoundException("Le fichier entreprises.csv n'existe pas.");
                }

                Entreprises.Clear();
                DictionnaireEntreprises.Clear();

                using (var reader = new StreamReader(cheminFichier))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        if (values.Length < 8)
                        {
                            throw new FormatException("Format de ligne invalide dans le fichier entreprises.csv.");
                        }

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
                Console.WriteLine("Import des entreprises terminé et entreprises.json généré.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'import des entreprises : {ex.Message}");
            }
        }

        public void ChargerClientsDepuisJson()
        {
            string cheminFichier = "clients.json";
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier clients.json n'existe pas.");
                return;
            }

            string json = File.ReadAllText(cheminFichier);
            Clients = JsonSerializer.Deserialize<List<Client>>(json);
            DictionnaireClients.Clear();
            foreach (var client in Clients)
            {
                DictionnaireClients.Add(client.Id, client);
            }
            Console.WriteLine("Chargement des clients depuis clients.json terminé.");
        }

        public void ChargerEntreprisesDepuisJson()
        {
            string cheminFichier = "entreprises.json";
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier entreprises.json n'existe pas.");
                return;
            }

            string json = File.ReadAllText(cheminFichier);
            Entreprises = JsonSerializer.Deserialize<List<Entreprise>>(json);
            DictionnaireEntreprises.Clear();
            foreach (var entreprise in Entreprises)
            {
                DictionnaireEntreprises.Add(entreprise.Id, entreprise);
            }
            Console.WriteLine("Chargement des entreprises depuis entreprises.json terminé.");
        }

        public void AfficherClients()
        {
            if (Clients.Count == 0)
            {
                ChargerClientsDepuisJson();
            }

            Console.WriteLine("\nListe des clients :");
            foreach (var client in Clients)
            {
                Console.WriteLine($"{client.Id} - {client.Nom}");
            }
        }

        public void AfficherEntreprises()
        {
            if (Entreprises.Count == 0)
            {
                ChargerEntreprisesDepuisJson();
            }

            Console.WriteLine("\nListe des entreprises :");
            foreach (var entreprise in Entreprises)
            {
                Console.WriteLine($"{entreprise.Id} - {entreprise.Nom}");
            }
        }

        public void CreerFacture()
        {
            try
            {
                if (Clients.Count == 0)
                {
                    ChargerClientsDepuisJson();
                }
                if (Entreprises.Count == 0)
                {
                    ChargerEntreprisesDepuisJson();
                }

                Console.WriteLine("\nListe des entreprises :");
                foreach (var ent in Entreprises)
                {
                    Console.WriteLine($"{ent.Id} - {ent.Nom}");
                }

                Console.Write("\nChoisissez l'identifiant d'une entreprise : ");
                int idEntreprise;
                if (!int.TryParse(Console.ReadLine(), out idEntreprise))
                {
                    throw new ArgumentException("L'identifiant de l'entreprise doit être un nombre valide.");
                }
                if (!DictionnaireEntreprises.ContainsKey(idEntreprise))
                {
                    throw new ArgumentException("Identifiant d'entreprise invalide.");
                }
                Entreprise entreprise = DictionnaireEntreprises[idEntreprise];

                Console.WriteLine("\nListe des clients :");
                foreach (var clientItem in Clients)
                {
                    Console.WriteLine($"{clientItem.Id} - {clientItem.Nom}");
                }

                Console.Write("\nChoisissez l'identifiant d'un client : ");
                int idClient;
                if (!int.TryParse(Console.ReadLine(), out idClient))
                {
                    throw new ArgumentException("L'identifiant du client doit être un nombre valide.");
                }
                if (!DictionnaireClients.ContainsKey(idClient))
                {
                    throw new ArgumentException("Identifiant de client invalide.");
                }
                Client client = DictionnaireClients[idClient];

                Console.Write("\nDate d'émission (format jj/MM/aaaa) : ");
                DateTime dateEmission;
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEmission))
                {
                    throw new ArgumentException("Format de date invalide. Utilisez le format jj/MM/aaaa.");
                }

                Facture facture = new Facture($"F{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 3).ToUpper()}", dateEmission, entreprise, client);

                bool ajouterLigne = true;
                while (ajouterLigne)
                {
                    Console.Write("\nDescription de la ligne : ");
                    string description = Console.ReadLine();

                    Console.Write("Quantité : ");
                    int quantite;
                    if (!int.TryParse(Console.ReadLine(), out quantite) || quantite <= 0)
                    {
                        throw new ArgumentException("La quantité doit être un nombre supérieur à zéro.");
                    }

                    Console.Write("Prix unitaire HT : ");
                    decimal prixUnitaireHT;
                    if (!decimal.TryParse(Console.ReadLine(), out prixUnitaireHT) || prixUnitaireHT <= 0)
                    {
                        throw new ArgumentException("Le prix unitaire HT doit être un nombre supérieur à zéro.");
                    }

                    Console.Write("Taux de TVA : ");
                    decimal tauxTVA;
                    if (!decimal.TryParse(Console.ReadLine(), out tauxTVA) || tauxTVA < 0)
                    {
                        throw new ArgumentException("Le taux de TVA doit être un nombre positif.");
                    }

                    LigneFacture ligne = new LigneFacture(description, quantite, prixUnitaireHT, tauxTVA);
                    facture.AjouterLigne(ligne);

                    Console.Write("\nVoulez-vous ajouter une autre ligne ? (oui/non) : ");
                    ajouterLigne = Console.ReadLine().ToLower() == "oui";
                }

                if (facture.Lignes.Count < 2)
                {
                    throw new ArgumentException("Une facture doit contenir au minimum deux lignes.");
                }

                facture.AfficherFacture();

                Console.Write("\nConfirmer la génération du fichier texte ? (oui/non) : ");
                if (Console.ReadLine().ToLower() == "oui")
                {
                    GenererFichierTexteFacture(facture);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la facture : {ex.Message}");
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

            string nomFichier = $"facture_{facture.Numero}.txt";
            File.WriteAllText(nomFichier, sb.ToString());
            Console.WriteLine($"Fichier {nomFichier} généré avec succès.");
        }

        public void AfficherCarnetContacts()
        {
            if (Clients.Count == 0)
            {
                ChargerClientsDepuisJson();
            }
            if (Entreprises.Count == 0)
            {
                ChargerEntreprisesDepuisJson();
            }

            List<Personne> carnet = new List<Personne>();
            carnet.AddRange(Clients);
            carnet.AddRange(Entreprises);

            Console.WriteLine("\nCarnet de contacts :");
            foreach (var contact in carnet)
            {
                contact.AfficherInfos();
            }
        }
    }
}