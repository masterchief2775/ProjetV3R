using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Viewallfinance
{
    public int FinanceId { get; set; }

    public int FournisseurId { get; set; }

    public string Tvq { get; set; } = null!;

    public string Tps { get; set; } = null!;

    public string ConditionPaiement { get; set; } = null!;

    public string Devise { get; set; } = null!;

    public string ModeCom { get; set; } = null!;

    public DateTime Timestamps { get; set; }
}
