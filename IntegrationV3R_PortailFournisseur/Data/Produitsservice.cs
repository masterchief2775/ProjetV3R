using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Produitsservice
{
    public int ProduitId { get; set; }

    public int FournisseurId { get; set; }

    public string Unspsc { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
