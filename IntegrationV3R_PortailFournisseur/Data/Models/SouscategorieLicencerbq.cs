﻿using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class SouscategorieLicencerbq
{
    public int SousCategrorieRbqId { get; set; }
    
    public string NumLicence { get; set; }

    public string NumeroSousCategorie { get; set; }

    public virtual Licencesrbq NumLicenceNavigation { get; set; } = null!;

    public virtual Souscategoriesafter2008 NumeroSousCategorieNavigation { get; set; } = null!;
}
