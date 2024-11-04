using IntegrationV3R_PortailFournisseur.Data.Models;
using System;

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

        //Properties to hold data from contacts
        public List<ContactFormulaire> ContactsInput = new List<ContactFormulaire>();



        // Properties to holld data from produits/services 
        public string DescriptionProduitsServicesInput { get; set; } = string.Empty;

        public List<UnspscComodite> ProduitsServicesSelectionnesInput = new List<UnspscComodite>();

        //Properties to hold data from RBQ
        public string RBQNumberInput { get; set; } = string.Empty;
        public string SelectedStatus { get; set; } = string.Empty;
        public string SelectedLicenseType { get; set; } = string.Empty;
        public string SelectedCategory { get; set; } = string.Empty;
        public DateTime StartDateInput { get; set; } = DateTime.Now;
        public DateTime EndDateInput { get; set; } = DateTime.Now;

        public List<Souscategorieafter2008> SelectedSubCategories = new List<Souscategorieafter2008>();


        public void LogDataToConsole()
        {
            Console.WriteLine("*****************************************************************************************");
            // Log identification data
            Console.WriteLine("\n***IDENTIFICATION***");
            Console.WriteLine($"Nom de l'entreprise: {NomEntrepriseInput}");
            Console.WriteLine($"NEQ: {NeqInput}");
            Console.WriteLine($"Email: {EmailInput}");
            // Be cautious with logging sensitive data like passwords
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
            /*Console.WriteLine("\n***LISTE DES CONTACTS***");
            foreach (Contact contact in ContactsInput)
            {
                Console.WriteLine($"\n\tNom complet: {ContactFormulaire.Prenom} {contact.Nom} \n\tFonction: {contact.Role} \n\tEmail: {contact.Email} \n\t" +
                    $"Telephone: {contact.NumeroTelephone} Poste {contact.Poste} - {contact.TypeTelephone}");
            }
            // Log Produits
            Console.WriteLine("\n***PRODUITS***");
            Console.WriteLine($"Description des produits et services offerts:\n\t {DescriptionProduitsServicesInput}");
            Console.WriteLine("Liste des produits et services selectionnees\n");
            foreach (UnspscComodite produit in ProduitsServicesSelectionnesInput)
            {
                Console.WriteLine($"\t{produit.ComoditeNombre} - {produit.ComoditeTitreFr}");
            }*/

            // Log RBQ
            Console.WriteLine("\n***RBQ***");
            Console.WriteLine($"Code RBQ : {RBQNumberInput}");
            Console.WriteLine($"Statut Licence : {SelectedStatus}");
            Console.WriteLine($"Type Licence : {SelectedLicenseType}");
            Console.WriteLine($"Type Licence : {SelectedCategory}");
            foreach (var sousCategorie in SelectedSubCategories)
            {
                Console.WriteLine($"\t{sousCategorie.NumeroSousCategorieAfter2008} - {sousCategorie.NomSousCategorieAfter2008}");
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }


        public async Task SaveDataAsync(ApplicationDbContext dbContext)
        {
            // Création de l'entité Fournisseur
            var fournisseur = new Fournisseur
            {
                NomEntreprise = this.NomEntrepriseInput,
                Neq = this.NeqInput,
                CourrielEntreprise = this.EmailInput,
                EtatDemande = "En attente",
                DateInscription = DateTime.Now
            };
            dbContext.Fournisseurs.Add(fournisseur);
            await dbContext.SaveChangesAsync();

            // Création de l'adresse
            var adresse = new Adress
            {
                NumeroCivique = this.NumCiviqueInput,
                Bureau = this.BureauInput,
                Rue = this.RueInput,
                Ville = this.VilleInput,
                Province = this.ProvinceInput,
                CodePostal = this.CodePostalInput,
                FournisseurId = fournisseur.FournisseurId
            };
            dbContext.Adresses.Add(adresse);


            // Ajouter les contacts
            foreach (var contactInput in this.ContactsInput)
            {
                var contact = new Contact
                {
                    PrenomContact = contactInput.Prenom,
                    NomContact = contactInput.Nom,
                    FonctionContact = contactInput.Role,
                    CourrielContact = contactInput.Email,
                    NumTelContact = contactInput.NumeroTelephone,
                    TypeTel = contactInput.TypeTelephone,
                    PosteTelContact = contactInput.Poste,
                    FournisseurId = fournisseur.FournisseurId
                };
                dbContext.Contacts.Add(contact);
            }

                // Ajouter les produits/services
                foreach (var produit in this.ProduitsServicesSelectionnesInput)
            {
                var produitService = new Produitsservice
                {
                    Details = this.DescriptionProduitsServicesInput,
                    FournisseurId = fournisseur.FournisseurId
                };
                dbContext.Produitsservices.Add(produitService);
            }

            // Ajouter les informations RBQ
            var rbq = new Rbq
            {
                NumLicence = this.RBQNumberInput,
                StatutLicence = this.SelectedStatus,
                FournisseurId = fournisseur.FournisseurId
            };
            dbContext.Rbqs.Add(rbq);

            // Sauvegarder les modifications
            await dbContext.SaveChangesAsync();
        }
    }

    public class ContactFormulaire
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
