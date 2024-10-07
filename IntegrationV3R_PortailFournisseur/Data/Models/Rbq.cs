using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Rbq
{
    public int RbqId { get; set; }

    public int FournisseurId { get; set; }

    public string NumLicence { get; set; } = null!;

    public string StatutLicence { get; set; } = null!;

    public string Categorie { get; set; } = null!;

    public string SousCategorie { get; set; } = null!;

    public DateOnly DateEmission { get; set; }

    public DateOnly DateRenouvellememt { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
