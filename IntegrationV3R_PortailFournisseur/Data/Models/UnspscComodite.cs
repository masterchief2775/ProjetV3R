using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class UnspscComodite
{
    public int ComoditeId { get; set; }

    public string ClasseNombre { get; set; } = null!;

    public string ComoditeNombre { get; set; } = null!;

    public string ComoditeTitreEn { get; set; } = null!;

    public string ComoditeTitreFr { get; set; } = null!;

    public virtual UnspscClass ClasseNombreNavigation { get; set; } = null!;

    public virtual ICollection<Produitsservice> Produitsservices { get; } = new List<Produitsservice>();
}
