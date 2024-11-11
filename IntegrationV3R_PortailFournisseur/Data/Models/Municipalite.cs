using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Municipalite
{
    public int MunicipaliteId { get; set; }

    public string CodeTerritoire { get; set; } = null!;

    public string CodeMunicipalite { get; set; } = null!;

    public string NomMunicipalite { get; set; } = null!;

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual Territoire CodeTerritoireNavigation { get; set; } = null!;
}
