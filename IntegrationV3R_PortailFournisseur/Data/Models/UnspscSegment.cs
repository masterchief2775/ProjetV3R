using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class UnspscSegment
{
    public int SegmentId { get; set; }

    public string SegmentNombre { get; set; } = null!;

    public string SegmentTitreEn { get; set; } = null!;

    public string SegmentTitreFr { get; set; } = null!;

    public virtual ICollection<UnspscFamille> UnspscFamilles { get; } = new List<UnspscFamille>();
}
