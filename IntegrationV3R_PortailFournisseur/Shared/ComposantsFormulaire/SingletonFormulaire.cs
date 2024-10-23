using System;

namespace IntegrationV3R_PortailFournisseur.Shared.ComposantsFormulaire
{
    public class SingletonFormulaire
    {
        private static SingletonFormulaire _instance;
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

        public void LogDataToConsole()
        {
            // Log identification data
            Console.WriteLine($"Nom de l'entreprise: {NomEntrepriseInput}");
            Console.WriteLine($"NEQ: {NeqInput}");
            Console.WriteLine($"Email: {EmailInput}");
            // Be cautious with logging sensitive data like passwords
            Console.WriteLine($"Mot de passe: {PasswordInput}");
            Console.WriteLine($"Répéter le mot de passe: {RepeatPasswordInput}");

            // Log address data
            Console.WriteLine($"Numéro Civique: {NumCiviqueInput}");
            Console.WriteLine($"Bureau: {BureauInput}");
            Console.WriteLine($"Rue: {RueInput}");
            Console.WriteLine($"Ville: {VilleInput}");
            Console.WriteLine($"Province: {ProvinceInput}");
            Console.WriteLine($"Code Postal: {CodePostalInput}");
            Console.WriteLine($"Région: {RegionInput}");
            Console.WriteLine($"Numéro de téléphone: {NumeroTelephoneInput}");
            Console.WriteLine($"Site Web: {SiteWebInput}");
        }
    }
}
