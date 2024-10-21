using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Fournisseur
{
    public int FournisseurId { get; set; }

    public string NomEntreprise { get; set; } = null!;

    public string Neq { get; set; } = null!;

    public string CourrielEntreprise { get; set; } = null!;

    public string? DetailsEntreprise { get; set; }

    public string EtatDemande { get; set; } = null!;

    public bool? EtatCompte { get; set; }

    public DateTime DateInscription { get; set; }

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual ICollection<Brochure> Brochures { get; } = new List<Brochure>();

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual ICollection<Finance> Finances { get; } = new List<Finance>();

    public virtual ICollection<Produitsservice> Produitsservices { get; } = new List<Produitsservice>();

    public virtual ICollection<Rbq> Rbqs { get; } = new List<Rbq>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
