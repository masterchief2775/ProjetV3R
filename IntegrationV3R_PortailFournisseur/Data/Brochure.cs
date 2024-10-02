using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Brochure
{
    public int BrochureId { get; set; }

    public int FournisseurId { get; set; }

    public string Nom { get; set; } = null!;

    public string TypeFichier { get; set; } = null!;

    public string Taille { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
