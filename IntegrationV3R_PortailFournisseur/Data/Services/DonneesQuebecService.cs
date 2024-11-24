using IntegrationV3R_PortailFournisseur.Data.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace IntegrationV3R_PortailFournisseur.Data.Services
{
    public class DonneesQuebecService
    {
        private readonly HttpClient _httpClient;

        public DonneesQuebecService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Fiche?> GetRecordByLicenceAsync(string numeroDeLicence)
        {
            string query = $"SELECT * from \"32f6ec46-85fd-45e9-945b-965d9235840a\" WHERE \"Numero de licence\" = '{numeroDeLicence}'";
            string url = $"/recherche/api/3/action/datastore_search_sql?sql={HttpUtility.UrlEncode(query)}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
            return response?.Result?.Records.FirstOrDefault();
        }
    }

    public class ApiResponse
    {
        public bool Succes { get; set; }
        public ResultData? Result { get; set; }
    }

    public class ResultData
    {
        public List<Fiche> Records { get; set; }
    }

    public class Fiche
    {
        //Identification
        [JsonPropertyName("NEQ")]
        public string NEQ { get; set; }

        [JsonPropertyName("Nom de l'intervenant")]
        public string NomEntreprise { get; set; }

        [JsonPropertyName("Courriel")]
        public string Courriel { get; set; }

        //Adresse
        [JsonPropertyName("Adresse")]
        public string Adresse { get; set; }

        [JsonPropertyName("Numero de telephone")]
        public string NumTelephone { get; set; }        

        [JsonPropertyName("Region administrative")]
        public string RegionAdministrative { get; set; }

        [JsonPropertyName("Code de region administrative")]
        public string CodeRegionAdministrative { get; set; }

        //RBQ
        [JsonPropertyName("Numero de licence")]
        public string NumeroDeLicence { get; set; }

        [JsonPropertyName("Statut de la licence")]
        public string StatutLicence { get; set; }

        [JsonPropertyName("Type de licence")]
        public string TypeLicence { get; set; }

        [JsonPropertyName("Categorie")]
        public string CategorieLicence { get; set; }

        [JsonPropertyName("Restriction")]
        public string Restriction { get; set; }




    }
}
