﻿using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string CodeProvince { get; set; } = null!;

    public string NomProvince { get; set; } = null!;

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();
}
