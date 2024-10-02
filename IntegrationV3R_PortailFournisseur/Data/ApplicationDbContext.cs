using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntegrationV3R_PortailFournisseur.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Brochure> Brochures { get; set; }

    public virtual DbSet<Connexion> Connexions { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Motsdepass> Motsdepasses { get; set; }

    public virtual DbSet<Produitsservice> Produitsservices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Viewalladress> Viewalladresses { get; set; }

    public virtual DbSet<Viewallbrochure> Viewallbrochures { get; set; }

    public virtual DbSet<Viewallconnexion> Viewallconnexions { get; set; }

    public virtual DbSet<Viewallcontact> Viewallcontacts { get; set; }

    public virtual DbSet<Viewallfinance> Viewallfinances { get; set; }

    public virtual DbSet<Viewallfournisseur> Viewallfournisseurs { get; set; }

    public virtual DbSet<Viewallmotsdepass> Viewallmotsdepasses { get; set; }

    public virtual DbSet<Viewallproduitsservice> Viewallproduitsservices { get; set; }

    public virtual DbSet<Viewalluser> Viewallusers { get; set; }

    public virtual DbSet<Viewfournisseurbyid> Viewfournisseurbyids { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cours.cegep3r.info;port=3306;database=a2024_420517ri_gr2-eq1_1888838-louis-fernand-henri-folcher;user=1888838;password=1888838", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.AdresseId).HasName("PRIMARY");

            entity.ToTable("adresses");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.AdresseId)
                .HasColumnType("int(11)")
                .HasColumnName("adresseId");
            entity.Property(e => e.Bureau)
                .HasMaxLength(2)
                .HasColumnName("bureau");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(6)
                .HasColumnName("codePostal");
            entity.Property(e => e.CodeRegionAdministrative)
                .HasMaxLength(32)
                .HasColumnName("codeRegionAdministrative");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NumTel)
                .HasMaxLength(10)
                .HasColumnName("numTel");
            entity.Property(e => e.NumeroCivique)
                .HasColumnType("int(5)")
                .HasColumnName("numeroCivique");
            entity.Property(e => e.Poste)
                .HasMaxLength(5)
                .HasColumnName("poste");
            entity.Property(e => e.Province)
                .HasMaxLength(32)
                .HasColumnName("province");
            entity.Property(e => e.Rue).HasMaxLength(64);
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Ville)
                .HasMaxLength(64)
                .HasColumnName("ville");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_ibfk_1");
        });

        modelBuilder.Entity<Brochure>(entity =>
        {
            entity.HasKey(e => e.BrochureId).HasName("PRIMARY");

            entity.ToTable("brochure");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.BrochureId)
                .HasColumnType("int(11)")
                .HasColumnName("brochureId");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Nom)
                .HasMaxLength(32)
                .HasColumnName("nom");
            entity.Property(e => e.Taille)
                .HasMaxLength(8)
                .HasColumnName("taille");
            entity.Property(e => e.TypeFichier)
                .HasMaxLength(4)
                .HasColumnName("typeFichier");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Brochures)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("brochure_ibfk_1");
        });

        modelBuilder.Entity<Connexion>(entity =>
        {
            entity.HasKey(e => e.ConnexionId).HasName("PRIMARY");

            entity.ToTable("connexions");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.ConnexionId)
                .HasColumnType("int(11)")
                .HasColumnName("connexionId");
            entity.Property(e => e.Ipconnexion)
                .HasMaxLength(45)
                .HasColumnName("IPConnexion");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Connexions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("connexions_ibfk_1");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PRIMARY");

            entity.ToTable("contacts");

            entity.HasIndex(e => e.Courriel, "courriel").IsUnique();

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.ContactId).HasColumnType("int(11)");
            entity.Property(e => e.Courriel)
                .HasMaxLength(64)
                .HasColumnName("courriel");
            entity.Property(e => e.FonctionContact)
                .HasMaxLength(32)
                .HasColumnName("fonctionContact");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NomContact)
                .HasMaxLength(32)
                .HasColumnName("nomContact");
            entity.Property(e => e.NumTel)
                .HasMaxLength(10)
                .HasColumnName("numTel");
            entity.Property(e => e.PosteTel)
                .HasMaxLength(5)
                .HasColumnName("posteTel");
            entity.Property(e => e.PrenomContact)
                .HasMaxLength(32)
                .HasColumnName("prenomContact");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.TypeTel)
                .HasMaxLength(8)
                .HasColumnName("typeTel");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contacts_ibfk_1");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.HasKey(e => e.FinanceId).HasName("PRIMARY");

            entity.ToTable("finances");

            entity.HasIndex(e => e.Tps, "TPS").IsUnique();

            entity.HasIndex(e => e.Tvq, "TVQ").IsUnique();

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.FinanceId)
                .HasColumnType("int(11)")
                .HasColumnName("financeId");
            entity.Property(e => e.ConditionPaiement)
                .HasMaxLength(32)
                .HasColumnName("conditionPaiement");
            entity.Property(e => e.Devise).HasMaxLength(4);
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.ModeCom).HasMaxLength(16);
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Tps)
                .HasMaxLength(15)
                .HasColumnName("TPS");
            entity.Property(e => e.Tvq)
                .HasMaxLength(16)
                .HasColumnName("TVQ");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Finances)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("finances_ibfk_1");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.FournisseurId).HasName("PRIMARY");

            entity.ToTable("fournisseurs");

            entity.HasIndex(e => e.Neq, "NEQ").IsUnique();

            entity.HasIndex(e => e.CourrielEntreprise, "courrielEntreprise").IsUnique();

            entity.HasIndex(e => e.NomEntreprise, "nomEntreprise").IsUnique();

            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.CourrielEntreprise)
                .HasMaxLength(64)
                .HasColumnName("courrielEntreprise");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateInscription");
            entity.Property(e => e.DetailsEntreprise)
                .HasMaxLength(512)
                .HasColumnName("detailsEntreprise");
            entity.Property(e => e.EtatCompte)
                .IsRequired()
                .HasDefaultValueSql("false")
                .HasColumnName("etatCompte");
            entity.Property(e => e.EtatDemande)
                .HasMaxLength(32)
                .HasColumnName("etatDemande");
            entity.Property(e => e.Neq)
                .HasMaxLength(10)
                .HasColumnName("NEQ");
            entity.Property(e => e.NomEntreprise)
                .HasMaxLength(64)
                .HasColumnName("nomEntreprise");
            entity.Property(e => e.Rbq)
                .HasMaxLength(10)
                .HasColumnName("RBQ");
        });

        modelBuilder.Entity<Motsdepass>(entity =>
        {
            entity.HasKey(e => e.MdpId).HasName("PRIMARY");

            entity.ToTable("motsdepasses");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.MdpId)
                .HasColumnType("int(11)")
                .HasColumnName("mdpId");
            entity.Property(e => e.IpchangementMdp)
                .HasMaxLength(45)
                .HasColumnName("IPChangementMdp");
            entity.Property(e => e.Mdp)
                .HasMaxLength(128)
                .HasColumnName("MDP");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Motsdepasses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("motsdepasses_ibfk_1");
        });

        modelBuilder.Entity<Produitsservice>(entity =>
        {
            entity.HasKey(e => e.ProduitId).HasName("PRIMARY");

            entity.ToTable("produitsservices");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.ProduitId)
                .HasColumnType("int(11)")
                .HasColumnName("produitId");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Unspsc)
                .HasMaxLength(8)
                .HasColumnName("UNSPSC");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Produitsservices)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("produitsservices_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Pseudo, "PSEUDO").IsUnique();

            entity.HasIndex(e => e.FournisseurId, "fournisseurID");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurID");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(32)
                .HasColumnName("PSEUDO");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Users)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<Viewalladress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewalladresses");

            entity.Property(e => e.AdresseId)
                .HasColumnType("int(11)")
                .HasColumnName("adresseId");
            entity.Property(e => e.Bureau)
                .HasMaxLength(2)
                .HasColumnName("bureau");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(6)
                .HasColumnName("codePostal");
            entity.Property(e => e.CodeRegionAdministrative)
                .HasMaxLength(32)
                .HasColumnName("codeRegionAdministrative");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NumTel)
                .HasMaxLength(10)
                .HasColumnName("numTel");
            entity.Property(e => e.NumeroCivique)
                .HasColumnType("int(5)")
                .HasColumnName("numeroCivique");
            entity.Property(e => e.Poste)
                .HasMaxLength(5)
                .HasColumnName("poste");
            entity.Property(e => e.Province)
                .HasMaxLength(32)
                .HasColumnName("province");
            entity.Property(e => e.Rue).HasMaxLength(64);
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Ville)
                .HasMaxLength(64)
                .HasColumnName("ville");
        });

        modelBuilder.Entity<Viewallbrochure>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallbrochures");

            entity.Property(e => e.BrochureId)
                .HasColumnType("int(11)")
                .HasColumnName("brochureId");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Nom)
                .HasMaxLength(32)
                .HasColumnName("nom");
            entity.Property(e => e.Taille)
                .HasMaxLength(8)
                .HasColumnName("taille");
            entity.Property(e => e.TypeFichier)
                .HasMaxLength(4)
                .HasColumnName("typeFichier");
        });

        modelBuilder.Entity<Viewallconnexion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallconnexions");

            entity.Property(e => e.ConnexionId)
                .HasColumnType("int(11)")
                .HasColumnName("connexionId");
            entity.Property(e => e.Ipconnexion)
                .HasMaxLength(45)
                .HasColumnName("IPConnexion");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
        });

        modelBuilder.Entity<Viewallcontact>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallcontacts");

            entity.Property(e => e.ContactId).HasColumnType("int(11)");
            entity.Property(e => e.Courriel)
                .HasMaxLength(64)
                .HasColumnName("courriel");
            entity.Property(e => e.FonctionContact)
                .HasMaxLength(32)
                .HasColumnName("fonctionContact");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NomContact)
                .HasMaxLength(32)
                .HasColumnName("nomContact");
            entity.Property(e => e.NumTel)
                .HasMaxLength(10)
                .HasColumnName("numTel");
            entity.Property(e => e.PosteTel)
                .HasMaxLength(5)
                .HasColumnName("posteTel");
            entity.Property(e => e.PrenomContact)
                .HasMaxLength(32)
                .HasColumnName("prenomContact");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.TypeTel)
                .HasMaxLength(8)
                .HasColumnName("typeTel");
        });

        modelBuilder.Entity<Viewallfinance>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallfinances");

            entity.Property(e => e.ConditionPaiement)
                .HasMaxLength(32)
                .HasColumnName("conditionPaiement");
            entity.Property(e => e.Devise).HasMaxLength(4);
            entity.Property(e => e.FinanceId)
                .HasColumnType("int(11)")
                .HasColumnName("financeId");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.ModeCom).HasMaxLength(16);
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Tps)
                .HasMaxLength(15)
                .HasColumnName("TPS");
            entity.Property(e => e.Tvq)
                .HasMaxLength(16)
                .HasColumnName("TVQ");
        });

        modelBuilder.Entity<Viewallfournisseur>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallfournisseurs");

            entity.Property(e => e.CourrielEntreprise)
                .HasMaxLength(64)
                .HasColumnName("courrielEntreprise");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateInscription");
            entity.Property(e => e.DetailsEntreprise)
                .HasMaxLength(512)
                .HasColumnName("detailsEntreprise");
            entity.Property(e => e.EtatCompte)
                .IsRequired()
                .HasDefaultValueSql("false")
                .HasColumnName("etatCompte");
            entity.Property(e => e.EtatDemande)
                .HasMaxLength(32)
                .HasColumnName("etatDemande");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Neq)
                .HasMaxLength(10)
                .HasColumnName("NEQ");
            entity.Property(e => e.NomEntreprise)
                .HasMaxLength(64)
                .HasColumnName("nomEntreprise");
            entity.Property(e => e.Rbq)
                .HasMaxLength(10)
                .HasColumnName("RBQ");
        });

        modelBuilder.Entity<Viewallmotsdepass>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallmotsdepasses");

            entity.Property(e => e.IpchangementMdp)
                .HasMaxLength(45)
                .HasColumnName("IPChangementMdp");
            entity.Property(e => e.Mdp)
                .HasMaxLength(128)
                .HasColumnName("MDP");
            entity.Property(e => e.MdpId)
                .HasColumnType("int(11)")
                .HasColumnName("mdpId");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
        });

        modelBuilder.Entity<Viewallproduitsservice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallproduitsservices");

            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.ProduitId)
                .HasColumnType("int(11)")
                .HasColumnName("produitId");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Unspsc)
                .HasMaxLength(8)
                .HasColumnName("UNSPSC");
        });

        modelBuilder.Entity<Viewalluser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewallusers");

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurID");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(32)
                .HasColumnName("PSEUDO");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
        });

        modelBuilder.Entity<Viewfournisseurbyid>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewfournisseurbyid");

            entity.Property(e => e.CourrielEntreprise)
                .HasMaxLength(64)
                .HasColumnName("courrielEntreprise");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateInscription");
            entity.Property(e => e.DetailsEntreprise)
                .HasMaxLength(512)
                .HasColumnName("detailsEntreprise");
            entity.Property(e => e.EtatCompte)
                .IsRequired()
                .HasDefaultValueSql("false")
                .HasColumnName("etatCompte");
            entity.Property(e => e.EtatDemande)
                .HasMaxLength(32)
                .HasColumnName("etatDemande");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Neq)
                .HasMaxLength(10)
                .HasColumnName("NEQ");
            entity.Property(e => e.NomEntreprise)
                .HasMaxLength(64)
                .HasColumnName("nomEntreprise");
            entity.Property(e => e.Rbq)
                .HasMaxLength(10)
                .HasColumnName("RBQ");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
