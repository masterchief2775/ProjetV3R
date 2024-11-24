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
        public string TpsInput { get; set; } = string.Empty;  // Numéro de TPS
        public string TvqInput { get; set; } = string.Empty;  // Numéro de TVQ
        public string ConditionPaiement { get; set; } = "Dans les 30 jours sans déduction"; // Default option for conditions
        public string Devise { get; set; } = "CAD"; // Default to CAD
        public string ModeCom { get; set; } = "email"; // Default to email for communication mode

        private readonly IHttpContextAccessor _httpContextAccessor;
        private NavigationManager Navigation;

        public SingletonFormulaire(IHttpContextAccessor httpContextAccessor, NavigationManager _navigation) 
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            Navigation = _navigation ?? throw new ArgumentNullException(nameof(_navigation));
        }        
                

        [Parameter]
        public EventCallback UploadSuccessful { get; set; }

        

        public async void SendIt()
        {
            await SendMail();
        }

        public async Task SaveDataAsync(ApplicationDbContext dbContext)
        {
            bool _isUploadSuccessful = true; 
            try
            {
               
            }
            catch (Exception ex)
            {
                _isUploadSuccessful = false;
            }

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
                /********************************************************************************************************************************************************************************************/
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
