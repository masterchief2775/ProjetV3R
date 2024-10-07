using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Produitsservice
{
    public int ProduitId { get; set; }

    public int FournisseurId { get; set; }

    public int ComoditeId { get; set; }

    public string Details { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual UnspscComodite Comodite { get; set; } = null!;

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
