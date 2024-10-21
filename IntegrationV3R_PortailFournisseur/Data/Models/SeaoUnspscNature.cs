using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class SeaoUnspscNature
{
    public int NatureId { get; set; }

    public string NatureCode { get; set; } = null!;

    public string? NatureNom { get; set; }

    public virtual ICollection<SeaoUnspscCategory> SeaoUnspscCategories { get; } = new List<SeaoUnspscCategory>();
}
