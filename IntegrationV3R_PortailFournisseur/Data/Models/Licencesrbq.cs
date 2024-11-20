using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Licencesrbq
{
    public int RbqId { get; set; }

    public int FournisseurId { get; set; }

    public string NumLicence { get; set; } = null!;

    public string StatutLicence { get; set; } = null!;

    public string Categorie { get; set; } = null!;

    public virtual Fournisseur Fournisseur { get; set; } = null!;

    public virtual ICollection<SouscategorieLicencerbq> SouscategorieLicencerbqs { get; } = new List<SouscategorieLicencerbq>();
}
