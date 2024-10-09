using IntegrationV3R_PortailFournisseur.Data.Models;
using System.Threading.Tasks;

public interface IMyService
{
    Task SaveBrochureAsync(Brochure brochure);
}
