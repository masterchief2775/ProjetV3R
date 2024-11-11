using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Regionadministrative
{
    public int RegionAdminId { get; set; }

    public string CodeRegionAdministrative { get; set; } = null!;

    public string NomRegionAmdin { get; set; } = null!;

    public virtual ICollection<Territoire> Territoires { get; } = new List<Territoire>();
}
