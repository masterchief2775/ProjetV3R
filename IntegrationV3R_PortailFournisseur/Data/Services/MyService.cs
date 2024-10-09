using IntegrationV3R_PortailFournisseur.Data.Models;
using System.Threading.Tasks;

public class MyService : IMyService
{
    private readonly ApplicationDbContext _context;

    public MyService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task SaveBrochureAsync(Brochure brochure)
    {
        _context.Brochures.Add(brochure);
        await _context.SaveChangesAsync();
    }
}
