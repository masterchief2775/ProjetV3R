﻿using IntegrationV3R_PortailFournisseur.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationV3R_PortailFournisseur.Shared.ComposantsFormulaire
{
    public class SingletonFormulaire
    {
        private static SingletonFormulaire _instance;
        private static readonly object _lock = new object();

        // Propriétés pour les données du formulaire
        public string NomEntrepriseInput { get; set; } = string.Empty;
        public string NeqInput { get; set; } = string.Empty;
        public string EmailInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; } = string.Empty;
        public string RepeatPasswordInput { get; set; } = string.Empty;

        public string NumCiviqueInput { get; set; } = string.Empty;
        public string BureauInput { get; set; } = string.Empty;
        public string RueInput { get; set; } = string.Empty;
        public string VilleInput { get; set; } = string.Empty;
        public string ProvinceInput { get; set; } = string.Empty;
        public string CodePostalInput { get; set; } = string.Empty;
        public string RegionInput { get; set; } = string.Empty;
        public string NumeroTelephoneInput { get; set; } = string.Empty;
        public string SiteWebInput { get; set; } = string.Empty;

        public List<ContactInput> ContactsInput = new List<ContactInput>();
        public string DescriptionProduitsServicesInput { get; set; } = string.Empty;
        public List<UnspscComodite> ProduitsServicesSelectionnesInput = new List<UnspscComodite>();

        public string RBQNumberInput { get; set; } = string.Empty;
        public string SelectedStatus { get; set; } = string.Empty;
        public string SelectedLicenseType { get; set; } = string.Empty;
        public string SelectedCategory { get; set; } = string.Empty;
        public DateTime StartDateInput { get; set; } = DateTime.Now;
        public DateTime EndDateInput { get; set; } = DateTime.Now;
        public List<Souscategorieafter2008> SelectedSubCategories = new List<Souscategorieafter2008>();

        private SingletonFormulaire() { }

        public static SingletonFormulaire Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonFormulaire();
                        }
                    }
                }
                return _instance;
            }
        }

        // Méthode pour enregistrer toutes les données en base de données
        public void SaveToDatabase(ApplicationDbContext context)
        {
            // Créer une nouvelle instance de Fournisseur
            var fournisseur = new Fournisseur
            {
                NomEntreprise = NomEntrepriseInput,
                Neq = NeqInput,
                CourrielEntreprise = EmailInput,
                EtatDemande = "En attente",
                DateInscription = DateTime.Now
            };

            // Ajouter le fournisseur dans le contexte
            context.Fournisseurs.Add(fournisseur);
            context.SaveChanges();

            // Sauvegarder l'adresse
            var adresse = new Adress
            {
                FournisseurId = fournisseur.FournisseurId,
                NumeroCivique = NumCiviqueInput,
                Rue = RueInput,
                Ville = VilleInput,
                Province = ProvinceInput,
                CodePostal = CodePostalInput,
                CodeRegionAdministrative = RegionInput,
                NumTel = NumeroTelephoneInput,
                Pays = "Canada"
            };
            context.Adresses.Add(adresse);

            // Sauvegarder les contacts
            foreach (var contactInput in ContactsInput)
            {
                var contact = new Contact
                {
                    FournisseurId = fournisseur.FournisseurId,
                    PrenomContact = contactInput.Prenom,
                    NomContact = contactInput.Nom,
                    FonctionContact = contactInput.Role,
                    CourrielContact = contactInput.Email,
                    NumTelContact = contactInput.NumeroTelephone,
                    TypeTel = contactInput.TypeTelephone,
                    PosteTelContact = contactInput.Poste
                };
                context.Contacts.Add(contact);
            }

            // Sauvegarder les produits/services
            foreach (var produitServiceInput in ProduitsServicesSelectionnesInput)
            {
                var produitService = new Produitsservice
                {
                    FournisseurId = fournisseur.FournisseurId,
                    ComoditeId = produitServiceInput.ComoditeId,
                    Details = DescriptionProduitsServicesInput
                };
                context.Produitsservices.Add(produitService);
            }

            // Sauvegarder la licence RBQ
            var rbq = new Rbq
            {
                FournisseurId = fournisseur.FournisseurId,
                NumLicence = RBQNumberInput,
                StatutLicence = SelectedStatus,
                Categorie = SelectedCategory,
                SousCategorie = string.Join(",", SelectedSubCategories.Select(s => s.NomSousCategorieAfter2008)),
                DateEmission = DateOnly.FromDateTime(StartDateInput),
                DateRenouvellememt = DateOnly.FromDateTime(EndDateInput)
            };
            context.Rbqs.Add(rbq);

            // Appliquer toutes les modifications
            context.SaveChanges();
        }

        // Méthode de log pour la console (facultative)
        public void LogDataToConsole()
        {
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("\n***IDENTIFICATION***");
            Console.WriteLine($"Nom de l'entreprise: {NomEntrepriseInput}");
            Console.WriteLine($"NEQ: {NeqInput}");
            Console.WriteLine($"Email: {EmailInput}");
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
            Console.WriteLine("\n***LISTE DES CONTACTS***");
            foreach (ContactInput contact in ContactsInput)
            {
                Console.WriteLine($"\n\tNom complet: {contact.Prenom} {contact.Nom} \n\tFonction: {contact.Role} \n\tEmail: {contact.Email} \n\t" +
                    $"Telephone: {contact.NumeroTelephone} Poste {contact.Poste} - {contact.TypeTelephone}");
            }
            Console.WriteLine("\n***PRODUITS***");
            Console.WriteLine($"Description des produits et services offerts:\n\t {DescriptionProduitsServicesInput}");
            foreach (UnspscComodite produit in ProduitsServicesSelectionnesInput)
            {
                Console.WriteLine($"\t{produit.ComoditeNombre} - {produit.ComoditeTitreFr}");
            }
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
    }

    // Classe ContactInput utilisée pour le formulaire
    public class ContactInput
    {
        public string Prenom { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NumeroTelephone { get; set; } = string.Empty;
        public string TypeTelephone { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;
    }
}
