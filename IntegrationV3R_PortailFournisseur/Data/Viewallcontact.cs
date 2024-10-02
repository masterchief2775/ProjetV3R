using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Viewallcontact
{
    public int ContactId { get; set; }

    public int FournisseurId { get; set; }

    public string NomContact { get; set; } = null!;

    public string PrenomContact { get; set; } = null!;

    public string FonctionContact { get; set; } = null!;

    public string Courriel { get; set; } = null!;

    public string TypeTel { get; set; } = null!;

    public string NumTel { get; set; } = null!;

    public string? PosteTel { get; set; }

    public DateTime Timestamps { get; set; }
}
