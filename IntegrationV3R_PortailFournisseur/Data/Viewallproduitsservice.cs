using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Viewallproduitsservice
{
    public int ProduitId { get; set; }

    public int FournisseurId { get; set; }

    public string Unspsc { get; set; } = null!;

    public DateTime Timestamps { get; set; }
}
