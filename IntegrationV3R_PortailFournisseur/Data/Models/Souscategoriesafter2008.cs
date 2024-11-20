using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Souscategoriesafter2008
{
    public int SousCategorieAfter2008Id { get; set; }

    public string NumeroSousCategorieAfter2008 { get; set; } = null!;

    public string NomSousCategorieAfter2008 { get; set; } = null!;

    public virtual ICollection<SouscategorieLicencerbq> SouscategorieLicencerbqs { get; } = new List<SouscategorieLicencerbq>();
}
