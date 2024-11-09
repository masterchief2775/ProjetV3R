using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Territoire
{
    public int TerritoireId { get; set; }

    public string CodeRegionAdministrative { get; set; } = null!;

    public string CodeTerritoire { get; set; } = null!;

    public string NomTerritoire { get; set; } = null!;

    public virtual Regionadministrative CodeRegionAdministrativeNavigation { get; set; } = null!;

    public virtual ICollection<Municipalite> Municipalites { get; } = new List<Municipalite>();
}
