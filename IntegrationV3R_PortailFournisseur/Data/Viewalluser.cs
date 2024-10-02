using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class Viewalluser
{
    public int UserId { get; set; }

    public int FournisseurId { get; set; }

    public string Pseudo { get; set; } = null!;

    public DateTime DateCreation { get; set; }
}
