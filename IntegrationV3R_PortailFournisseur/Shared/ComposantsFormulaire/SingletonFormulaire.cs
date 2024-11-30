﻿using IntegrationV3R_PortailFournisseur.Data;
using IntegrationV3R_PortailFournisseur.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Linq;




namespace IntegrationV3R_PortailFournisseur.Shared.ComposantsFormulaire
{
    public class SingletonFormulaire
    {
        public bool _isCreationForm = true;

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
        public string MunicipaliteInput { get; set; } = string.Empty;
        public string ProvinceInput { get; set; } = string.Empty;
        public string CodePostalInput { get; set; } = string.Empty;
        public string RegionInput { get; set; } = string.Empty;
        public string NumeroTelephoneInput { get; set; } = string.Empty;
        public string NumeroPosteInput { get; set; } = string.Empty;
        public string SiteWebInput { get; set; } = string.Empty;
        public string NomMunicipaliteInput { get; set; } = string.Empty;

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

        // Section pour Brochures 
        public string UploadDirectory { get; set; } = string.Empty;
        public string OriginalBrochureName { get; set; } = string.Empty;
        public string OriginalCarteAffaireName { get; set; } = string.Empty;
        public Brochure SelectedBrochure = new Brochure();
        public Brochure SelectedCarteAffaire = new Brochure();
        public IBrowserFile BrochureFile { get; set; } = null;
        public IBrowserFile CarteVisiteFile { get; set; } = null;


        // New properties for Finances
        public string TpsInput { get; set; } = string.Empty;
        public string TvqInput { get; set; } = string.Empty;
        public string ConditionPaiement { get; set; } = "Dans les 30 jours sans déduction";
        public string Devise { get; set; } = "CAD";
        public string ModeCom { get; set; } = "Courriel";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private NavigationManager Navigation;
        private ApplicationDbContext dbContext;

        public SingletonFormulaire(IHttpContextAccessor httpContextAccessor, NavigationManager _navigation, ApplicationDbContext _dbContext) 
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            Navigation = _navigation ?? throw new ArgumentNullException(nameof(_navigation));
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        }        
                

        [Parameter]
        public EventCallback UploadSuccessful { get; set; }

        public string EtatDemande { get; set; }
//*******************************************SET FOR INFOS MODIFY***********************************
        public async Task FetchUser(int id, CancellationToken token)
        {
            var user = await dbContext.Users
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Adresses)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Contacts)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Finances)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Produitsservices)
                       .ThenInclude(p => p.Comodite) // Including Comodite
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Licencesrbqs)
                       .ThenInclude(l => l.SouscategorieLicencerbqs) // Ensure SouscategorieLicencerbqs is included here
                .Include(u => u.Fournisseur)
                    .ThenInclude(f => f.Brochures)
               .FirstOrDefaultAsync(u => u.UserId == id);


            //SET FOURNISSEUR
            Fournisseur fournisseur = user.Fournisseur;
            NomEntrepriseInput = fournisseur.NomEntreprise;
            NeqInput = fournisseur.Neq;
            EmailInput = fournisseur.CourrielEntreprise;
            PasswordInput = string.Empty;
            RepeatPasswordInput = string.Empty;

            //SET ETAT DEMANDE POUR VOIR SI SHOW FINANCE
            EtatDemande = fournisseur.EtatDemande;

            //SET ADRESSE
            ICollection<Adress> adresses = user.Fournisseur.Adresses;
            Adress adresse = adresses.SingleOrDefault();
            NumCiviqueInput = adresse.NumeroCivique;
            BureauInput = adresse.Bureau;
            RueInput = adresse.Rue;
            if (!string.IsNullOrEmpty(adresse.CodeMunicipalite))
                MunicipaliteInput = adresse.CodeMunicipalite;
            else
                NomMunicipaliteInput = adresse.NomMunicipalite;
            ProvinceInput = adresse.CodeProvince;
            CodePostalInput = adresse.CodePostal;
            NumeroTelephoneInput = adresse.NumTel;
            NumeroPosteInput = adresse.Poste;
            SiteWebInput = fournisseur.SiteWeb;

            //SET CONTACTS 
            ContactsInput.Clear();
            List<Contact> Contacts = user.Fournisseur.Contacts.ToList();            
            foreach (Contact contact in Contacts)
            {
                ContactInput toAdd = new ContactInput
                {
                    Prenom = contact.PrenomContact,
                    Nom = contact.NomContact,
                    Role = contact.FonctionContact,
                    Email = contact.CourrielContact,
                    NumeroTelephone = contact.NumTelContact,
                    Poste = contact.PosteTelContact,
                    TypeTelephone = contact.TypeTel,

                    PrenomError = string.Empty,
                    NomError = string.Empty,
                    RoleError = string.Empty,
                    EmailError = string.Empty,
                    NumeroTelephoneError = string.Empty,
                    PosteError = string.Empty,
                    TypeTelephoneError = string.Empty
                };

                ContactsInput.Add(toAdd);
            }

            //SET PRODUITS
            DescriptionProduitsServicesInput = fournisseur.DetailsEntreprise;
            List<Produitsservice> produits = user.Fournisseur.Produitsservices.ToList();

            ProduitsServicesSelectionnesInput.Clear();

            foreach (Produitsservice produit in produits) 
            {
                UnspscComodite comodite = produit.Comodite;
                ProduitsServicesSelectionnesInput.Add(comodite);
            } 
            
            //SET LICENCE RBQ
            var licence = fournisseur.Licencesrbqs.SingleOrDefault();
            RBQNumberInput = licence.NumLicence;
            SelectedStatus = licence.StatutLicence;
            SelectedLicenseType = licence.TypeLicence;
            SelectedCategory = licence.Categorie;
            List<SouscategorieLicencerbq> souscategories = licence.SouscategorieLicencerbqs.ToList();

            SelectedSubCategories.Clear();
            foreach(SouscategorieLicencerbq souscategorie in souscategories)
            {
                var entry = dbContext.Souscategoriesafter2008s.Where(s => s.SousCategorieAfter2008Id == souscategorie.IdSousCategorie).FirstOrDefault();
                SelectedSubCategories.Add(entry);
            }

            //SET BROCHURES 
            List<Brochure> brochures = fournisseur.Brochures.ToList();
            
            foreach(Brochure brochure in brochures)
            {
                if (brochure.NoFichier == "Brochure")
                {
                    SelectedBrochure = brochure;
                    OriginalBrochureName = brochure.NomFichier;
                }
                if (brochure.NoFichier == "Carte Affaire")
                {
                    SelectedCarteAffaire = brochure;
                    OriginalCarteAffaireName = brochure.NomFichier;
                }
            }
            

            //SET FINANCES
            var finances = fournisseur.Finances.SingleOrDefault();
            if (finances != null)               
            {
                TpsInput = finances.Tps;
                TvqInput = finances.Tvq;
                ConditionPaiement = finances.CodeConditionPaiement;
                Devise = finances.Devise;
                ModeCom = finances.ModeCom;
            }           
            
		}
//*****************************************MODIFY FORM************************************************
        public async Task ModifyDataAsync(ApplicationDbContext dbContext, int? id)
        {
            var user = await dbContext.Users
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Adresses)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Contacts)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Finances)
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Produitsservices)
                       .ThenInclude(p => p.Comodite) 
               .Include(u => u.Fournisseur)
                   .ThenInclude(f => f.Licencesrbqs)
                       .ThenInclude(l => l.SouscategorieLicencerbqs) 
                .Include(u => u.Fournisseur)
                    .ThenInclude(f => f.Brochures)
               .FirstOrDefaultAsync(u => u.UserId == id);

//MODIFY FOURNISSEUR
            user.Fournisseur.NomEntreprise = NomEntrepriseInput;
            user.Fournisseur.Neq = NeqInput;
            user.Fournisseur.CourrielEntreprise = EmailInput;
            user.Fournisseur.DetailsEntreprise = DescriptionProduitsServicesInput;
            user.Fournisseur.SiteWeb = SiteWebInput;

            await dbContext.SaveChangesAsync();

//MODIFY ADRESSE
            string cleanedCodePostal = Regex.Replace(this.CodePostalInput, @"\s+", "");
            string cleanedNumeroTelephone = Regex.Replace(this.NumeroTelephoneInput, @"-", "");
            Console.WriteLine(MunicipaliteInput + "**********************************");

            Adress adresse = user.Fournisseur.Adresses.SingleOrDefault();
            adresse.NumeroCivique = NumCiviqueInput;
            adresse.Rue = RueInput;
            adresse.Bureau = BureauInput;
            if(!string.IsNullOrEmpty(MunicipaliteInput))
                adresse.CodeMunicipalite = MunicipaliteInput;
            else
                adresse.NomMunicipalite = NomMunicipaliteInput;
            adresse.CodeProvince = ProvinceInput;
            adresse.CodePostal = cleanedCodePostal;
            adresse.NumTel = cleanedNumeroTelephone;
            adresse.Poste = NumeroPosteInput;

            await dbContext.SaveChangesAsync();

//MODIFY CONTACTS
            List<Contact> contacts = user.Fournisseur.Contacts.ToList();

            if (contacts.Count == ContactsInput.Count)
            {
                int index = 0;
                foreach (Contact contact in contacts)
                {
                    string cleanedTelephone = Regex.Replace(ContactsInput[index].NumeroTelephone, @"-", "");
                    contact.PrenomContact = ContactsInput[index].Prenom;
                    contact.NomContact = ContactsInput[index].Nom;
                    contact.FonctionContact = ContactsInput[index].Role;
                    contact.CourrielContact = ContactsInput[index].Email;
                    contact.TypeTel = ContactsInput[index].TypeTelephone;
                    contact.NumTelContact = cleanedTelephone;
                    contact.PosteTelContact = ContactsInput[index].Poste;
                    index++;
                }
            }
            else if (contacts.Count < ContactsInput.Count)
            {
                int currentContacts = contacts.Count;
                int newContacts = ContactsInput.Count;
                int toCreate = newContacts - currentContacts;

                for (int i = 0; i < currentContacts + toCreate; i++)
                {
                    if (i < currentContacts)
                    {
                        string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                        contacts[i].PrenomContact = ContactsInput[i].Prenom;
                        contacts[i].NomContact = ContactsInput[i].Nom;
                        contacts[i].FonctionContact = ContactsInput[i].Role;
                        contacts[i].CourrielContact = ContactsInput[i].Email;
                        contacts[i].TypeTel = ContactsInput[i].TypeTelephone;
                        contacts[i].NumTelContact = cleanedTelephone;
                        contacts[i].PosteTelContact = ContactsInput[i].Poste;
                    }
                    else
                    {
                        string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                        var contact = new Contact
                        {
                            PrenomContact = ContactsInput[i].Prenom,
                            NomContact = ContactsInput[i].Nom,
                            FonctionContact = ContactsInput[i].Role,
                            CourrielContact = ContactsInput[i].Email,
                            TypeTel = ContactsInput[i].TypeTelephone,
                            NumTelContact = cleanedTelephone,
                            PosteTelContact = ContactsInput[i].Poste,
                            FournisseurId = user.FournisseurId
                        };
                        dbContext.Add(contact);
                    }
                }

            }
            else if (contacts.Count > ContactsInput.Count)
            {
                int currentContacts = contacts.Count;
                int newContacts = ContactsInput.Count;
                int toDelete = currentContacts - newContacts;

                for (int i = 0; i < currentContacts; i++)
                {
                    if (i < currentContacts - toDelete)
                    {
                        string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                        contacts[i].PrenomContact = ContactsInput[i].Prenom;
                        contacts[i].NomContact = ContactsInput[i].Nom;
                        contacts[i].FonctionContact = ContactsInput[i].Role;
                        contacts[i].CourrielContact = ContactsInput[i].Email;
                        contacts[i].TypeTel = ContactsInput[i].TypeTelephone;
                        contacts[i].NumTelContact = cleanedTelephone;
                        contacts[i].PosteTelContact = ContactsInput[i].Poste;
                    }
                    else
                    {
                        dbContext.Remove(contacts[i]);
                    }
                }
            }

            await dbContext.SaveChangesAsync();

//MODIFY PRODUITS
            List<Produitsservice> produits = user.Fournisseur.Produitsservices.ToList();

            if(produits.Count == ProduitsServicesSelectionnesInput.Count)
            {
                int index = 0;
                foreach (Produitsservice produit in produits)
                {
                    produit.ComoditeId = ProduitsServicesSelectionnesInput[index].ComoditeId;
                    index++;
                }
            }
            else if(produits.Count < ProduitsServicesSelectionnesInput.Count)
            {
                int currentProduits = produits.Count;
                int newProduits = ProduitsServicesSelectionnesInput.Count;
                int toCreate = newProduits - currentProduits;

                for (int i = 0; i < currentProduits + toCreate; i++)
                {
                    if (i < currentProduits)
                    {
                        produits[i].ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId;
                    }
                    else
                    {
                        var produit = new Produitsservice
                        {
                            ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId,
                            FournisseurId = user.FournisseurId
                        };
                        dbContext.Add(produit);
                    }
                }
            }
            else if (produits.Count > ProduitsServicesSelectionnesInput.Count)
            {
                int currentProduits = produits.Count;
                int newProduits = ProduitsServicesSelectionnesInput.Count;
                int toDelete = currentProduits - newProduits;

                for (int i = 0; i < currentProduits; i++)
                {
                    if (i < currentProduits - toDelete)
                    {
                        produits[i].ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId;
                    }
                    else
                    {
                        dbContext.Remove(produits[i]);
                    }
                }
            }
            await dbContext.SaveChangesAsync();

//MODIFY RBQ
            Licencesrbq licence = user.Fournisseur.Licencesrbqs.SingleOrDefault();
            string cleanedLicence = Regex.Replace(RBQNumberInput, @"-", "");
            licence.NumLicence = cleanedLicence;            
            licence.StatutLicence = SelectedStatus;
            licence.TypeLicence = SelectedLicenseType;
            licence.Categorie = SelectedCategory;

            List<SouscategorieLicencerbq> sousCategories = licence.SouscategorieLicencerbqs.ToList();
                        

            if(sousCategories.Count == SelectedSubCategories.Count)
            {
                int index = 0;
                foreach (SouscategorieLicencerbq liaison in sousCategories)
                {                    
                    liaison.IdSousCategorie = SelectedSubCategories[index].SousCategorieAfter2008Id;
                    index++;                                        
                }
            }
            else if(sousCategories.Count < SelectedSubCategories.Count)
            {
                int currentSubs = sousCategories.Count;
                int newSubs = SelectedSubCategories.Count;
                int toCreate = newSubs - currentSubs;

                for (int i = 0; i < currentSubs + toCreate; i++)
                {
                    if (i < currentSubs)
                    {                        
                        sousCategories[i].IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id;
                    }
                    else
                    {
                        var sub = new SouscategorieLicencerbq
                        {
                            IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id,
                            IdLicence = licence.RbqId
                        };
                        dbContext.Add(sub);
                    }
                }
            }
            else if (sousCategories.Count > SelectedSubCategories.Count)
            {
                int currentSubs = sousCategories.Count;
                int newSubs = SelectedSubCategories.Count;
                int toDelete = currentSubs - newSubs;

                for (int i = 0; i < currentSubs; i++)
                {
                    if (i < currentSubs - toDelete)
                    {
                        sousCategories[i].IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id;
                    }
                    else
                    {
                        dbContext.Remove(sousCategories[i]);
                    }
                }
            }
            await dbContext.SaveChangesAsync();

//MODIFY BROCHURE

            List<Brochure> brochures = user.Fournisseur.Brochures.ToList();            

            foreach(Brochure thisBrochure in brochures)
            {
                if (thisBrochure.NoFichier == "Brochure")
                {
                    if(BrochureFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), SelectedBrochure.LienDocument);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                        }
                        thisBrochure.LienDocument = SelectedBrochure.LienDocument;
                        thisBrochure.NomFichier = SelectedBrochure.NomFichier;
                        thisBrochure.Taille = SelectedBrochure.Taille;
                        thisBrochure.TypeFichier = SelectedBrochure.TypeFichier;
                    } 
                }
                else
                {
                    if (CarteVisiteFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), SelectedCarteAffaire.LienDocument);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                        }
                        thisBrochure.LienDocument = SelectedCarteAffaire.LienDocument;
                        thisBrochure.NomFichier = SelectedCarteAffaire.NomFichier;
                        thisBrochure.Taille = SelectedCarteAffaire.Taille;
                        thisBrochure.TypeFichier = SelectedCarteAffaire.TypeFichier;
                    }
                }
            }

//MODIFT OR ADD FINANCES
            Finance finance = user.Fournisseur.Finances.SingleOrDefault();

            if(user.Fournisseur.EtatDemande == "Approuvée" && finance == null)
            {
                var newFinance = new Finance
                {
                    Tvq = TvqInput,
                    Tps = TpsInput,
                    CodeConditionPaiement = ConditionPaiement,
                    Devise = Devise,
                    ModeCom = ModeCom,
                    FournisseurId = user.FournisseurId
                };
                dbContext.Add(newFinance);
            }
            else if (user.Fournisseur.EtatDemande == "Approuvée" && finance != null)
            {
                finance.Tvq = TvqInput;
                finance.Tps = TpsInput;
                finance.CodeConditionPaiement = ConditionPaiement;
                finance.Devise = Devise;
                finance.ModeCom = ModeCom;
            }            

            await dbContext.SaveChangesAsync();

            Navigation.NavigateTo("/profil?success=true");
        }

//************************************CREATE FORM********************************************
        public async Task SaveDataAsync(ApplicationDbContext dbContext)
        {

            var fournisseur = new Fournisseur
            {
                NomEntreprise = this.NomEntrepriseInput,
                Neq = this.NeqInput,
                CourrielEntreprise = this.EmailInput,
                DetailsEntreprise = this.DescriptionProduitsServicesInput,
                EtatDemande = "En attente",
                EtatCompte = true,
                SiteWeb = this.SiteWebInput,
            };

            dbContext.Fournisseurs.Add(fournisseur);
            await dbContext.SaveChangesAsync();



            // Création de l'adresse
            string cleanedCodePostal = Regex.Replace(this.CodePostalInput, @"\s+", "");
            string cleanedNumeroTelephone = Regex.Replace(this.NumeroTelephoneInput, @"-", "");
            
            if (ProvinceInput == "4")
            {
                var adresse = new Adress
                {
                    NumeroCivique = this.NumCiviqueInput,
                    Bureau = this.BureauInput,
                    Rue = this.RueInput,
                    CodeMunicipalite = this.MunicipaliteInput,
                    NomMunicipalite = null,
                    CodeProvince = this.ProvinceInput,
                    CodePostal = cleanedCodePostal,
                    NumTel = cleanedNumeroTelephone,
                    Poste = this.NumeroPosteInput,
                    FournisseurId = fournisseur.FournisseurId
                };
                dbContext.Adresses.Add(adresse);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var adresse = new Adress
                {
                    NumeroCivique = this.NumCiviqueInput,
                    Bureau = this.BureauInput,
                    Rue = this.RueInput,
                    NomMunicipalite = this.NomMunicipaliteInput,
                    CodeMunicipalite = null,
                    CodeProvince = this.ProvinceInput,
                    CodePostal = cleanedCodePostal,
                    NumTel = cleanedNumeroTelephone,
                    Poste = this.NumeroPosteInput,
                    FournisseurId = fournisseur.FournisseurId
                };
                dbContext.Adresses.Add(adresse);
                await dbContext.SaveChangesAsync();
            }
           

            // Ajouter les contacts
            foreach (var contactInput in this.ContactsInput)
            {
                string cleanedTelephone = Regex.Replace(contactInput.NumeroTelephone, @"-", "");
                var contact = new Contact
                {
                    PrenomContact = contactInput.Prenom,
                    NomContact = contactInput.Nom,
                    FonctionContact = contactInput.Role,
                    CourrielContact = contactInput.Email,
                    NumTelContact = cleanedTelephone,
                    TypeTel = contactInput.TypeTelephone,
                    PosteTelContact = contactInput.Poste,
                    FournisseurId = fournisseur.FournisseurId
                };
                dbContext.Contacts.Add(contact);
            }
            await dbContext.SaveChangesAsync();

            // Ajouter les produits/services
            foreach (var produit in this.ProduitsServicesSelectionnesInput)
            {
                var produitService = new Produitsservice
                {
                    FournisseurId = fournisseur.FournisseurId,
                    ComoditeId = produit.ComoditeId
                };
                dbContext.Produitsservices.Add(produitService);
            }
            await dbContext.SaveChangesAsync();

            // Ajouter les informations RBQ
            string cleanedLicence = Regex.Replace(RBQNumberInput, @"-", "");

            var rbq = new Licencesrbq
            {
                NumLicence = cleanedLicence,
                StatutLicence = this.SelectedStatus,
                TypeLicence= this.SelectedLicenseType,
                Categorie = this.SelectedCategory,
                FournisseurId = fournisseur.FournisseurId
            };
            dbContext.Licencesrbqs.Add(rbq);

            await dbContext.SaveChangesAsync();

            // Ajouter les sous categories RBQ choisies 
            foreach (Souscategoriesafter2008 item in SelectedSubCategories)
            {
                Console.WriteLine(SelectedSubCategories.ToString());
                var liaison = new SouscategorieLicencerbq
                {
                    IdLicence = rbq.RbqId,
                    IdSousCategorie = item.SousCategorieAfter2008Id
                };
                dbContext.SouscategorieLicencerbqs.Add(liaison);
            }

            await dbContext.SaveChangesAsync();



            //Ajoute les brochures 
            if (BrochureFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), SelectedBrochure.LienDocument);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                }
                SelectedBrochure.FournisseurId = fournisseur.FournisseurId;
                dbContext.Brochures.Add(SelectedBrochure);
            }
            await dbContext.SaveChangesAsync();

            if (CarteVisiteFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), SelectedBrochure.LienDocument);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                }
                SelectedCarteAffaire.FournisseurId = fournisseur.FournisseurId;
                dbContext.Brochures.Add(SelectedCarteAffaire);
            }
            await dbContext.SaveChangesAsync();

            //Créer un user
            User user = new User
            {
                FournisseurId = fournisseur.FournisseurId,
                Identifiant = fournisseur.CourrielEntreprise
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();


            //Créer son mdp

            string? ipConnexion = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            Motsdepass mdp = new Motsdepass
            {
                UserId = user.UserId,
                Mdp = ComputeMd5Hash(PasswordInput),
                IpChangementMdp = ipConnexion
            };
            dbContext.Motsdepasses.Add(mdp);
            await dbContext.SaveChangesAsync();

            var check = dbContext.Fournisseurs.FirstOrDefault(f => f.FournisseurId == fournisseur.FournisseurId);  
            
            if (check != null)
            {
                await SendMail();
                Navigation.NavigateTo("/connexion?success=true");
            }
            else
            {
                //FAIRE UN MODAL QUI DIT A L'UTILISATER QU'IL Y A UNE ERREUR
            }
        }

        private string ComputeMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertir le tableau de bytes en une chaîne hexadécimale
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private async Task SendMail()
        {
            string adress = EmailInput;

            // Set up SMTP client
            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {                
                Credentials = new NetworkCredential("d03e91c65ae080", "72c31abf54bb14"),
                EnableSsl = true // Enable SSL for secure communication
            };

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates", "ExempleCourriel.html");
            string body = File.ReadAllText(filePath);

            string lienTemporaire = "https://en.wikipedia.org/wiki/Thumb_signal#/media/File:Jempol_Ngadep_Atas_(cropped).jpg";

            body = body.Replace("{Link}", lienTemporaire);

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("automatique@3rivieres.com"),
                Subject = "Test d'envoie de courriel",
                /* Le body du courriel peut être formatter en HTML selon les normes à la ville dans le projet. Il est donc possible d'instancier 
                   par la suite un courriel avec ce body d'html avec les informations du client*/
                Body = body,
                /*"Ceci est un courriel pour vous laissez savoir que nous avons reçu votre demande d'adhésion à la liste des fournisseurs de la ville de Trois-Rivières" + '\n' +
                '\n' + "Votre dossier est en attente de traitement. Vous recevrez un autre courriel lorsque la décision sera prise." + '\n' + '\n' +
                "Merci et bonne journée !",*/
                IsBodyHtml = true // Set to true if you want to send HTML content
            };

            mailMessage.To.Add(EmailInput); 
            //mailMessage.To.Add("gporlier97@gmail.com");

            // Send the email asynchronously
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }


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
