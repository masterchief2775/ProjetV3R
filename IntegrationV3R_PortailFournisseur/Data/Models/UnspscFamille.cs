﻿using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class UnspscFamille
{
    public int FamilleId { get; set; }

    public string SegmentNombre { get; set; } = null!;

    public string FamilleNombre { get; set; } = null!;

    public string FamilleTitreEn { get; set; } = null!;

    public string FamilleTitreFr { get; set; } = null!;

    public virtual UnspscSegment SegmentNombreNavigation { get; set; } = null!;

    public virtual ICollection<UnspscClass> UnspscClasses { get; } = new List<UnspscClass>();
}
