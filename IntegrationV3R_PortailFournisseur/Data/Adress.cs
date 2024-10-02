using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Adress
{
    public int AdresseId { get; set; }

    public int FournisseurId { get; set; }

    public int NumeroCivique { get; set; }

    public string Rue { get; set; } = null!;

    public string? Bureau { get; set; }

    public string Ville { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string CodePostal { get; set; } = null!;

    public string CodeRegionAdministrative { get; set; } = null!;

    public string NumTel { get; set; } = null!;

    public string? Poste { get; set; }

    public DateTime Timestamps { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
