using Asset.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Asset.API.Data;

public partial class AssetContext : DbContext
{
    public AssetContext()
    {
    }

    public AssetContext(DbContextOptions<AssetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Asset> Asset { get; set; }

    public virtual DbSet<AssetAlternativeCode> AssetAlternativeCode { get; set; }

    public virtual DbSet<AssetBusinessVisaDebitCardRequest> AssetBusinessVisaDebitCardRequest { get; set; }

    public virtual DbSet<AssetDate> AssetDate { get; set; }

    public virtual DbSet<AssetDebitCard> AssetDebitCard { get; set; }

    public virtual DbSet<AssetDebitCardLimit> AssetDebitCardLimit { get; set; }

    public virtual DbSet<AssetDetail> AssetDetail { get; set; }

    public virtual DbSet<AssetIntermediary> AssetIntermediary { get; set; }

    public virtual DbSet<AssetLifeCycleStatus> AssetLifeCycleStatus { get; set; }

    public virtual DbSet<AssetNote> AssetNote { get; set; }

    public virtual DbSet<AssetOther> AssetOther { get; set; }

    public virtual DbSet<AssetSafetyBox> AssetSafetyBox { get; set; }

    public virtual DbSet<AssetToAsset> AssetToAsset { get; set; }

    public virtual DbSet<AssetToCondition> AssetToCondition { get; set; }

    public virtual DbSet<AssetToDirectDebit> AssetToDirectDebit { get; set; }

    public virtual DbSet<AssetToMandateForPaymentInvoice> AssetToMandateForPaymentInvoice { get; set; }

    public virtual DbSet<AssetToPerson> AssetToPerson { get; set; }

    public virtual DbSet<AssetToRelationship> AssetToRelationship { get; set; }

    public virtual DbSet<AssetToStandingOrder> AssetToStandingOrder { get; set; }

    public virtual DbSet<AssetVisaDebitCard> AssetVisaDebitCard { get; set; }

    public virtual DbSet<DebitCardNumbersLegacy> DebitCardNumbersLegacy { get; set; }

    public virtual DbSet<ECashAccountDetail> ECashAccountDetail { get; set; }

    public virtual DbSet<FiscalReportAccounting> FiscalReportAccounting { get; set; }

    public virtual DbSet<FiscalReportIssuing> FiscalReportIssuing { get; set; }

    public virtual DbSet<OrdinarySecuritiesDepositDetail> OrdinarySecuritiesDepositDetail { get; set; }

    public virtual DbSet<RCDIDProgressiveCode> RCDIDProgressiveCode { get; set; }

    public virtual DbSet<StandingOrderAssetEventLog> StandingOrderAssetEventLog { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<Models.Asset>(entity =>
        {
            entity.HasKey(e => new { e.AssetID, e.GroupBankID });

            entity.ToTable("Asset", "asset");

            entity.HasIndex(e => e.Number, "IX_Asset_AssetNumber").HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.ProductID, e.GroupBankID }, "IX_Asset_AssetProduct");

            entity.HasIndex(e => new { e.AssetID, e.RelationshipID, e.GroupBankID }, "IX_Asset_RelationshipID");

            entity.HasIndex(e => new { e.RelationshipID, e.PltAssetTypeID, e.Number, e.GroupBankID }, "IX_Asset_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.ProductID, e.RelationshipID, e.AssetID }, "_dta_index_Asset_25_812022124__K2_K3_K1").HasFillFactor(90);

            entity.Property(e => e.AssetID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AssetAlternativeCode>(entity =>
        {
            entity.HasKey(e => new { e.AssetAlternativeCodeID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetAlternativeCode", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetAlternativeCode_AssetID").IsClustered();

            entity.HasIndex(e => new { e.AssetID, e.PltAssetAlternativeCodeID, e.ValidEndDate }, "IX_AssetAlternativeCode_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.AssetAlternativeCodeID).ValueGeneratedOnAdd();
            entity.Property(e => e.AlternativeCode).HasMaxLength(50);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetAlternativeCode_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAlternativeCodes)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetAlternativeCode_Asset");
        });

        modelBuilder.Entity<AssetBusinessVisaDebitCardRequest>(entity =>
        {
            entity.HasKey(e => e.BusinessVisaDebitID).HasName("PK_Asset.BusinessVisaDebitCardRequest");

            entity.ToTable("AssetBusinessVisaDebitCardRequest", "asset");

            entity.Property(e => e.Channel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Corner");
            entity.Property(e => e.Limit).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PersonNumber)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<AssetDate>(entity =>
        {
            entity.HasKey(e => new { e.AssetDateID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetDate", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetDate_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.Property(e => e.AssetDateID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetDate_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetDates)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDate_Asset");
        });

        modelBuilder.Entity<AssetDebitCard>(entity =>
        {
            entity.HasKey(e => new { e.AssetDebitCardID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetDebitCard", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetDebitCard_AssetID").IsClustered();

            entity.HasIndex(e => new { e.AssetID, e.ValidEndDate }, "IX_AssetDebitCard_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetDebitCardID).ValueGeneratedOnAdd();
            entity.Property(e => e.Header1).HasMaxLength(24);
            entity.Property(e => e.Header2).HasMaxLength(24);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetDebitCard_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetDebitCards)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDebitCard_Asset");
        });

        modelBuilder.Entity<AssetDebitCardLimit>(entity =>
        {
            entity.HasKey(e => new { e.AssetDebitCardLimitID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetDebitCardLimit", "asset");

            entity.HasIndex(e => new { e.AssetDebitCardLimitID, e.GroupBankID }, "IX_AssetDebitCardLimit_AssetDebitcard").IsClustered();

            entity.HasIndex(e => new { e.AssetDebitCardID, e.PltDebitCardLimitID, e.ValidEndDate }, "IX_AssetDebitCardLimit_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetDebitCardLimitID).ValueGeneratedOnAdd();
            entity.Property(e => e.LimitAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetDebitCardLimit_ValidEndDate");

            entity.HasOne(d => d.AssetDebitCard).WithMany(p => p.AssetDebitCardLimits)
                .HasForeignKey(d => new { d.AssetDebitCardID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDebitCardLimit_AssetDebitCard");
        });

        modelBuilder.Entity<AssetDetail>(entity =>
        {
            entity.HasKey(e => new { e.AssetDetailID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetDetail", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetDetail_AssetID").IsClustered();

            entity.HasIndex(e => new { e.AssetID, e.ValidEndDate }, "IX_AssetDetail_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.AssetDetailID).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetDetail_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetDetails)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDetail_Asset");
        });

        modelBuilder.Entity<AssetIntermediary>(entity =>
        {
            entity.HasKey(e => new { e.AssetIntermediaryID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetIntermediary", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetIntermediary_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.ValidEndDate }, "IX_AssetIntermediary_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.AssetIntermediaryID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetIntermediary_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetIntermediaries)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetIntermediary_Asset");
        });

        modelBuilder.Entity<AssetLifeCycleStatus>(entity =>
        {
            entity.HasKey(e => new { e.AssetLifeCycleStatusID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetLifeCycleStatus", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetLifeCycleStatus_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.PltAssetStatusID, e.GroupBankID }, "IX_AssetLifeCycleStatus_PltAssetStatusID");

            entity.HasIndex(e => new { e.AssetID, e.PltAssetStatusID, e.ValidEndDate }, "IX_AssetLifeCycleStatus_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetLifeCycleStatusID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetLifeCycleStatus_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetLifeCycleStatuses)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLifeCycleStatus_Asset");
        });

        modelBuilder.Entity<AssetNote>(entity =>
        {
            entity.HasKey(e => new { e.AssetNoteID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetNote", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetNote_AssetID").IsClustered();

            entity.Property(e => e.AssetNoteID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetNote_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetNotes)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetNote_Asset");
        });

        modelBuilder.Entity<AssetOther>(entity =>
        {
            entity.HasKey(e => new { e.AssetOtherID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetOther", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetOther_AssetID").IsClustered();

            entity.HasIndex(e => new { e.AssetID, e.ValidEndDate }, "IX_AssetOther_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetOtherID).ValueGeneratedOnAdd();
            entity.Property(e => e.Amount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetOther_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetOthers)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetOther_Asset");
        });

        modelBuilder.Entity<AssetSafetyBox>(entity =>
        {
            entity.HasKey(e => new { e.AssetSafetyBoxID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetSafetyBox", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetSafetyBox_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.ValidEndDate }, "IX_AssetSafetyBox_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetSafetyBoxID).ValueGeneratedOnAdd();
            entity.Property(e => e.BailementAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetSafetyBox_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetSafetyBoxes)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetSafetyBox_Asset");
        });

        modelBuilder.Entity<AssetToAsset>(entity =>
        {
            entity.HasKey(e => new { e.AssetToAssetID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetToAsset", "asset");

            entity.HasIndex(e => new { e.AssetChildID, e.GroupBankID }, "IX_AssetToAsset_AssetChildID").HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetToAsset_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.AssetChildID, e.PltAssetToAssetLinkID, e.ValidEndDate }, "IX_AssetToAsset_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetToAssetID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetToAsset_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetToAssetAssets)
                .HasForeignKey(d => new { d.AssetChildID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetToAsset_AssetChild");

            entity.HasOne(d => d.AssetNavigation).WithMany(p => p.AssetToAssetAssetNavigations)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetToAsset_Asset");
        });

        modelBuilder.Entity<AssetToCondition>(entity =>
        {
            entity.HasKey(e => new { e.AssetToConditionID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetToCondition", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetToCondition_AssetID").IsClustered();

            entity.HasIndex(e => new { e.AssetID, e.ConditionID, e.CodeID, e.CodeValueID, e.ValidEndDate }, "IX_AssetToCondition_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.AssetToConditionID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetToCondition_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetToConditions)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetToCondition_Asset");
        });

        modelBuilder.Entity<AssetToDirectDebit>(entity =>
        {
            entity.HasKey(e => new { e.DDAConfigurationID, e.AssetDirectDebitID, e.GroupBankID }).HasName("PK__AssetToD__7BB077100398C4CE");

            entity.ToTable("AssetToDirectDebit", "asset");
        });

        modelBuilder.Entity<AssetToMandateForPaymentInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AssetToMandateForPaymentInvoice", "asset");

            entity.HasIndex(e => new { e.MandateID, e.GroupBankID }, "IX_AssetToMandateForPaymentInvoice").IsUnique();
        });

        modelBuilder.Entity<AssetToPerson>(entity =>
        {
            entity.HasKey(e => new { e.AssetToPersonID, e.GroupBankID })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("AssetToPerson", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetToPerson_AssetID")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.PersonID, e.GroupBankID }, "IX_AssetToPerson_PersonID").HasFillFactor(90);

            entity.HasIndex(e => new { e.AssetID, e.PersonID, e.PltAssetToPersonLinkID, e.ValidEndDate }, "IX_AssetToPerson_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.AssetToPersonID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetToPerson_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetToPeople)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetToPerson_Asset");
        });

        modelBuilder.Entity<AssetToRelationship>(entity =>
        {
            entity.HasKey(e => new { e.AssetToRelationshipID, e.GroupBankID }).IsClustered(false);

            entity.ToTable("AssetToRelationship", "asset");

            entity.HasIndex(e => new { e.AssetID, e.GroupBankID }, "IX_AssetToRelationship_AssetID").IsClustered();

            entity.HasIndex(e => new { e.RelationshipID, e.GroupBankID }, "IX_AssetToRelationship_RelationshipID");

            entity.HasIndex(e => new { e.AssetID, e.RelationshipID, e.PltAssetToRelationshipLinkID, e.ValidEndDate }, "IX_AssetToRelationship_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.AssetToRelationshipID).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_AssetToRelationship_ValidEndDate");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetToRelationships)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetToRelationship_Asset");
        });

        modelBuilder.Entity<AssetToStandingOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AssetToStandingOrder", "asset");

            entity.HasIndex(e => new { e.StandingOrderID, e.GroupBankID }, "IX_AssetToStandingOrder").IsUnique();
        });

        modelBuilder.Entity<AssetVisaDebitCard>(entity =>
        {
            entity.HasKey(e => new { e.AssetVisaDebitCardID, e.GroupBankID });

            entity.ToTable("AssetVisaDebitCard", "asset");

            entity.Property(e => e.AssetVisaDebitCardID).ValueGeneratedOnAdd();
            entity.Property(e => e.CornerCardID).HasMaxLength(30);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetVisaDebitCards)
                .HasForeignKey(d => new { d.AssetID, d.GroupBankID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetVisaDebitCard_Asset");
        });

        modelBuilder.Entity<DebitCardNumbersLegacy>(entity =>
        {
            entity.HasKey(e => e.DebitCardNumberLegacyID);

            entity.ToTable("DebitCardNumbersLegacy", "asset");

            entity.HasIndex(e => e.Number, "IX_DebitCardNumbersLegacy").IsUnique();
        });

        modelBuilder.Entity<ECashAccountDetail>(entity =>
        {
            entity.HasKey(e => new { e.RelationshipNumber, e.AccountNumber, e.DinitCode, e.ValidStartDate }).IsClustered(false);

            entity.ToTable("ECashAccountDetail", "asset");

            entity.HasIndex(e => new { e.DinitCode, e.ValidStartDate }, "IX_ECashAccountDetail_DinitCode")
                .IsDescending(false, true)
                .IsClustered();

            entity.Property(e => e.DinitCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AmortizationAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.CumulativeUnpaidAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.CumulativeUnpaidFeeAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.CumulativeUnpaidInterestAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.CumulativeUnpaidPPIAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.CumulativeUnpaidPrincipalAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.ECashAccountDetailID).ValueGeneratedOnAdd();
            entity.Property(e => e.InitialAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.RemainingPrincipalAmount).HasColumnType("decimal(28, 8)");
        });

        modelBuilder.Entity<FiscalReportAccounting>(entity =>
        {
            entity.ToTable("FiscalReportAccounting", "asset");

            entity.HasIndex(e => e.FiscalReportIssuingID, "UK_FiscalReportIssuingID").IsUnique();

            entity.Property(e => e.ChargeAmount).HasColumnType("decimal(28, 8)");
            entity.Property(e => e.ExternalKey).HasMaxLength(64);
            entity.Property(e => e.IsoCode).HasMaxLength(3);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())");

            entity.HasOne(d => d.FiscalReportIssuing).WithOne(p => p.FiscalReportAccounting)
                .HasForeignKey<FiscalReportAccounting>(d => d.FiscalReportIssuingID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FiscalReportAccounting_FiscalReportIssuing");
        });

        modelBuilder.Entity<FiscalReportIssuing>(entity =>
        {
            entity.ToTable("FiscalReportIssuing", "asset");

            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())");
        });

        modelBuilder.Entity<OrdinarySecuritiesDepositDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrdinarySecuritiesDepositDetail", "asset");

            entity.HasIndex(e => new { e.AssetID, e.ValueDate }, "OrdinarySecuritiesDepositBalance_Unique_AssetID_Date").IsUnique();

            entity.Property(e => e.AccountCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Timestamp).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.TotalEquity)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(15, 2)");
        });

        modelBuilder.Entity<RCDIDProgressiveCode>(entity =>
        {
            entity.HasKey(e => new { e.Channel, e.RCDID }).HasName("PK__tmp_ms_x__3A59C0A8D3948A3E");

            entity.ToTable("RCDIDProgressiveCode", "asset");

            entity.Property(e => e.Channel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Corner");
            entity.Property(e => e.RCDID)
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.MaestroCardNumber)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<StandingOrderAssetEventLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StandingOrderAssetEventLog", "asset");

            entity.HasIndex(e => new { e.StandingOrderID, e.GroupBankID }, "IX_StandingOrderAssetEventLog");

            entity.Property(e => e.Event).HasColumnType("xml");
        });
    }
}
