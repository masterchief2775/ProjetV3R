using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Conditionspaiement
{
    public int ConditionsPaiementsId { get; set; }

    public string CodeConditionsPaiements { get; set; } = null!;

    public string NomConditionsPaiements { get; set; } = null!;

    public virtual ICollection<Finance> Finances { get; } = new List<Finance>();
}
