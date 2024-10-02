using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Viewfournisseurbyid
{
    public int FournisseurId { get; set; }

    public string NomEntreprise { get; set; } = null!;

    public string Rbq { get; set; } = null!;

    public string Neq { get; set; } = null!;

    public string CourrielEntreprise { get; set; } = null!;

    public string? DetailsEntreprise { get; set; }

    public string EtatDemande { get; set; } = null!;

    public bool? EtatCompte { get; set; }

    public DateTime DateInscription { get; set; }
}
