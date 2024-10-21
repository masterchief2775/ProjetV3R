namespace IntegrationV3R_PortailFournisseur.Data.Services
{
    public class SharedDataService
    {
        public bool Fetched { get; set; } = true;
        //Identification
        public string NEQ { get; set; } = "";
        public string NomEntreprise { get; set; } = "";
        public string Courriel { get; set; } = "";
        //Adresse
        public string NumCivique { get; set; } = "";
        public string NomRue { get; set; } = "";
        public string CodePostal { get; set; } = "";
        public string Province { get; set; } = "";
        public string NomVille { get; set; } = "";
        public string NumTelephone { get; set; } = "";
        public string RegionAdministrative { get; set; } = "";
        public string CodeRegionAdministrative { get; set; } = "";

        //RBQ
        public string NumLicence { get; set; } = "";
        public string StatutLicence { get; set; } = "";
        public string TypeLicence { get; set; } = "";
        public string CategorieLicence { get; set; } = "";

    }
}
