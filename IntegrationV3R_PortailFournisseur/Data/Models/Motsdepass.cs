using System;
using System.Collections.Generic;

namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class Motsdepass
{
    public int MdpId { get; set; }

    public int UserId { get; set; }

    public string Mdp { get; set; } = null!;

    public string IpChangementMdp { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual User User { get; set; } = null!;
}
