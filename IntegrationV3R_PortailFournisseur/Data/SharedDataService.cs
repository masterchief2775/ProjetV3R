namespace IntegrationV3R_PortailFournisseur.Data
{
    public class SharedDataService
    {
        public bool Fetched { get; set; } = true;
        public string NEQ { get; set; } = "";
        public string NumLicence { get; set; } = "";
        public string StatueLicence { get; set; } = "";
        public string NumCivique { get; set; } = "";
        public string NomRue { get; set; } = "";
        public string CodePostal { get; set; } = "";
        public string Province { get; set; } = "";
        public string NomVille { get; set; } = "";
    }
}
