using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class UnspscClass
{
    public int ClasseId { get; set; }

    public string FamilleNombre { get; set; } = null!;

    public string ClasseNombre { get; set; } = null!;

    public string ClasseTitreEn { get; set; } = null!;

    public string ClasseTitreFr { get; set; } = null!;

    public virtual UnspscFamille FamilleNombreNavigation { get; set; } = null!;

    public virtual ICollection<UnspscComodite> UnspscComodites { get; } = new List<UnspscComodite>();
}
