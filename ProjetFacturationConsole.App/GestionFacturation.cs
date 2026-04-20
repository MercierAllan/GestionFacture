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
    }
}