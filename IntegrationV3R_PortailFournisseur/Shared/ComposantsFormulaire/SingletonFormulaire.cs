using IntegrationV3R_PortailFournisseur.Data.Models;
using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Shared.ComposantsFormulaire
{
    public class SingletonFormulaire
    {
        private static readonly object _lock = new object();

        // Properties to hold form data for identification
        public string NomEntrepriseInput { get; set; } = string.Empty;
        public string NeqInput { get; set; } = string.Empty;
        public string EmailInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; } = string.Empty;
        public string RepeatPasswordInput { get; set; } = string.Empty;

        // Properties to hold form data for address
        public string NumCiviqueInput { get; set; } = string.Empty;
        public string BureauInput { get; set; } = string.Empty;
        public string RueInput { get; set; } = string.Empty;
        public string VilleInput { get; set; } = string.Empty;
        public string ProvinceInput { get; set; } = string.Empty;
        public string CodePostalInput { get; set; } = string.Empty;
        public string RegionInput { get; set; } = string.Empty;
        public string NumeroTelephoneInput { get; set; } = string.Empty;
        public string SiteWebInput { get; set; } = string.Empty;

        // Properties to hold data from contacts
        public List<ContactInput> ContactsInput = new List<ContactInput>();

        // Properties to hold data from produits/services 
        public string DescriptionProduitsServicesInput { get; set; } = string.Empty;
        public List<UnspscComodite> ProduitsServicesSelectionnesInput = new List<UnspscComodite>();

        // Properties to hold data from RBQ
        public string RBQNumberInput { get; set; } = string.Empty;
        public string SelectedStatus { get; set; } = string.Empty;
        public string SelectedLicenseType { get; set; } = string.Empty;
        public string SelectedCategory { get; set; } = string.Empty;
        public DateTime StartDateInput { get; set; } = DateTime.Now;
        public DateTime EndDateInput { get; set; } = DateTime.Now;

        public List<Souscategoriesafter2008> SelectedSubCategories = new List<Souscategoriesafter2008>();

        // New properties for Finances
        public string TpsInput { get; set; } = string.Empty;  // Numéro de TPS
        public string TvqInput { get; set; } = string.Empty;  // Numéro de TVQ
        public string ConditionPaiement { get; set; } = "Dans les 30 jours sans déduction"; // Default option for conditions
        public string Devise { get; set; } = "CAD"; // Default to CAD
        public string ModeCom { get; set; } = "email"; // Default to email for communication mode

        public SingletonFormulaire() { }

        public void LogDataToConsole()
        {
            Console.WriteLine("*****************************************************************************************");
            // Log identification data
            Console.WriteLine("\n***IDENTIFICATION***");
            Console.WriteLine($"Nom de l'entreprise: {NomEntrepriseInput}");
            Console.WriteLine($"NEQ: {NeqInput}");
            Console.WriteLine($"Email: {EmailInput}");
            Console.WriteLine($"Mot de passe: {PasswordInput}");
            Console.WriteLine($"Répéter le mot de passe: {RepeatPasswordInput}");

            // Log address data
            Console.WriteLine("\n***ADRESSE***");
            Console.WriteLine($"Numéro Civique: {NumCiviqueInput}");
            Console.WriteLine($"Bureau: {BureauInput}");
            Console.WriteLine($"Rue: {RueInput}");
            Console.WriteLine($"Ville: {VilleInput}");
            Console.WriteLine($"Province: {ProvinceInput}");
            Console.WriteLine($"Code Postal: {CodePostalInput}");
            Console.WriteLine($"Région: {RegionInput}");
            Console.WriteLine($"Numéro de téléphone: {NumeroTelephoneInput}");
            Console.WriteLine($"Site Web: {SiteWebInput}");

            // Log Contacts
            Console.WriteLine("\n***LISTE DES CONTACTS***");
            foreach (ContactInput contact in ContactsInput)
            {
                Console.WriteLine($"\n\tNom complet: {contact.Prenom} {contact.Nom} \n\tFonction: {contact.Role} \n\tEmail: {contact.Email} \n\t" +
                                  $"Telephone: {contact.NumeroTelephone} Poste {contact.Poste} - {contact.TypeTelephone}");
            }

            // Log Produits
            Console.WriteLine("\n***PRODUITS***");
            Console.WriteLine($"Description des produits et services offerts:\n\t {DescriptionProduitsServicesInput}");
            Console.WriteLine("Liste des produits et services selectionnees\n");
            foreach (UnspscComodite produit in ProduitsServicesSelectionnesInput)
            {
                Console.WriteLine($"\t{produit.ComoditeNombre} - {produit.ComoditeTitreFr}");
            }

            // Log RBQ
            Console.WriteLine("\n***RBQ***");
            Console.WriteLine($"Code RBQ : {RBQNumberInput}");
            Console.WriteLine($"Statut Licence : {SelectedStatus}");
            Console.WriteLine($"Type Licence : {SelectedLicenseType}");
            Console.WriteLine($"Categorie : {SelectedCategory}");
            foreach (var sousCategorie in SelectedSubCategories)
            {
                Console.WriteLine($"\t{sousCategorie.NumeroSousCategorieAfter2008} - {sousCategorie.NomSousCategorieAfter2008}");
            }

            // Log Finances data
            Console.WriteLine("\n***FINANCES***");
            Console.WriteLine($"Numéro de TPS: {TpsInput}");
            Console.WriteLine($"Numéro de TVQ: {TvqInput}");
            Console.WriteLine($"Conditions de paiement: {ConditionPaiement}");
            Console.WriteLine($"Devise: {Devise}");
            Console.WriteLine($"Mode de communication: {ModeCom}");

            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }

        public async Task SaveDataAsync(ApplicationDbContext dbContext)
        {
            /*
            // Example code to save financial data along with other fields
            var fournisseur = new Fournisseur
            {
                NomEntreprise = this.NomEntrepriseInput,
                Neq = this.NeqInput,
                CourrielEntreprise = this.EmailInput,
                EtatDemande = "En attente",
                DateInscription = DateTime.Now,
                Tps = this.TpsInput,
                Tvq = this.TvqInput,
                ConditionPaiement = this.ConditionPaiement,
                Devise = this.Devise,
                ModeCom = this.ModeCom
            };
            dbContext.Fournisseurs.Add(fournisseur);
            await dbContext.SaveChangesAsync();
            */
        }
    }

    public class ContactInput
    {
        public string Prenom { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NumeroTelephone { get; set; } = string.Empty;
        public string TypeTelephone { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;

        // Error messages
        public string PrenomError { get; set; } = string.Empty;
        public string NomError { get; set; } = string.Empty;
        public string RoleError { get; set; } = string.Empty;
        public string EmailError { get; set; } = string.Empty;
        public string NumeroTelephoneError { get; set; } = string.Empty;
        public string TypeTelephoneError { get; set; } = string.Empty;
        public string PosteError { get; set; } = string.Empty;
    }
}
