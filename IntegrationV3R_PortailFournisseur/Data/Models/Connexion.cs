using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Connexion
{
    public int ConnexionId { get; set; }

    public int UserId { get; set; }

    public string IpConnexion { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual User User { get; set; } = null!;
}
