using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public int FournisseurId { get; set; }

    public string Identifiant { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public virtual ICollection<Connexion> Connexions { get; } = new List<Connexion>();

    public virtual Fournisseur Fournisseur { get; set; } = null!;

    public virtual ICollection<Motsdepass> Motsdepasses { get; } = new List<Motsdepass>();
}
