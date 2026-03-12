using Microsoft.EntityFrameworkCore;
using Plausibility.API.Models;

namespace Plausibility.API.Data;

public class PlausibilityDbContext : DbContext
{
    public PlausibilityDbContext(DbContextOptions<PlausibilityDbContext> options) : base(options) { }

    public DbSet<EmployeesRangeNumber> EmployeesRangeNumbers { get; set; } = null!;
    public DbSet<EmployeesRangeNumberTranslation> EmployeesRangeNumberTranslations { get; set; } = null!;
    public DbSet<GenderCode> GenderCodes { get; set; } = null!;
    public DbSet<GenderCodeTranslation> GenderCodeTranslations { get; set; } = null!;
    public DbSet<JoinType> JoinTypes { get; set; } = null!;
    public DbSet<JoinTypeTranslation> JoinTypeTranslations { get; set; } = null!;
    public DbSet<MaritalRegime> MaritalRegimes { get; set; } = null!;
    public DbSet<MaritalRegimeTranslation> MaritalRegimeTranslations { get; set; } = null!;
    public DbSet<NationAlpha2> NationAlpha2s { get; set; } = null!;
    public DbSet<NationAlpha2Translation> NationAlpha2Translations { get; set; } = null!;
    public DbSet<Noga> Nogas { get; set; } = null!;
    public DbSet<NogaTranslation> NogaTranslations { get; set; } = null!;
    public DbSet<Pep> Peps { get; set; } = null!;
    public DbSet<PepTranslation> PepTranslations { get; set; } = null!;
    public DbSet<PersonAcquisitionSource> PersonAcquisitionSources { get; set; } = null!;
    public DbSet<PersonAcquisitionSourceTranslation> PersonAcquisitionSourceTranslations { get; set; } = null!;
    public DbSet<Personality> Personalitys { get; set; } = null!;
    public DbSet<PersonalityTranslation> PersonalityTranslations { get; set; } = null!;
    public DbSet<PersonCodingType> PersonCodingTypes { get; set; } = null!;
    public DbSet<PersonCodingTypeTranslation> PersonCodingTypeTranslations { get; set; } = null!;
    public DbSet<PersonDependentsNumber> PersonDependentsNumbers { get; set; } = null!;
    public DbSet<PersonDependentsNumberTranslation> PersonDependentsNumberTranslations { get; set; } = null!;
    public DbSet<PersonInternalType> PersonInternalTypes { get; set; } = null!;
    public DbSet<PersonInternalTypeTranslation> PersonInternalTypeTranslations { get; set; } = null!;
    public DbSet<PersonMaritalStatus> PersonMaritalStatuss { get; set; } = null!;
    public DbSet<PersonMaritalStatusTranslation> PersonMaritalStatusTranslations { get; set; } = null!;
    public DbSet<PersonNature> PersonNatures { get; set; } = null!;
    public DbSet<PersonNatureTranslation> PersonNatureTranslations { get; set; } = null!;
    public DbSet<PersonOrganizationType> PersonOrganizationTypes { get; set; } = null!;
    public DbSet<PersonOrganizationTypeTranslation> PersonOrganizationTypeTranslations { get; set; } = null!;
    public DbSet<PersonProfession> PersonProfessions { get; set; } = null!;
    public DbSet<PersonProfessionTranslation> PersonProfessionTranslations { get; set; } = null!;
    public DbSet<PersonToRelationshipRole> PersonToRelationshipRoles { get; set; } = null!;
    public DbSet<PersonToRelationshipRoleTranslation> PersonToRelationshipRoleTranslations { get; set; } = null!;
    public DbSet<RelationshipType> RelationshipTypes { get; set; } = null!;
    public DbSet<RelationshipTypeTranslation> RelationshipTypeTranslations { get; set; } = null!;
    public DbSet<AssetAlternativeCode> AssetAlternativeCodes { get; set; } = null!;
    public DbSet<AssetAlternativeCodeTranslation> AssetAlternativeCodeTranslations { get; set; } = null!;
    public DbSet<AssetNoteType> AssetNoteTypes { get; set; } = null!;
    public DbSet<AssetNoteTypeTranslation> AssetNoteTypeTranslations { get; set; } = null!;
    public DbSet<AssetStatus> AssetStatuss { get; set; } = null!;
    public DbSet<AssetStatusTranslation> AssetStatusTranslations { get; set; } = null!;
    public DbSet<AssetStatusReason> AssetStatusReasons { get; set; } = null!;
    public DbSet<AssetStatusReasonTranslation> AssetStatusReasonTranslations { get; set; } = null!;
    public DbSet<AssetToAssetLink> AssetToAssetLinks { get; set; } = null!;
    public DbSet<AssetToAssetLinkTranslation> AssetToAssetLinkTranslations { get; set; } = null!;
    public DbSet<AssetToPersonLink> AssetToPersonLinks { get; set; } = null!;
    public DbSet<AssetToPersonLinkTranslation> AssetToPersonLinkTranslations { get; set; } = null!;
    public DbSet<AssetToRelationshipLink> AssetToRelationshipLinks { get; set; } = null!;
    public DbSet<AssetToRelationshipLinkTranslation> AssetToRelationshipLinkTranslations { get; set; } = null!;
    public DbSet<AssetType> AssetTypes { get; set; } = null!;
    public DbSet<AssetTypeTranslation> AssetTypeTranslations { get; set; } = null!;
    public DbSet<BusinessDateType> BusinessDateTypes { get; set; } = null!;
    public DbSet<BusinessDateTypeTranslation> BusinessDateTypeTranslations { get; set; } = null!;
    public DbSet<DebitCardLimit> DebitCardLimits { get; set; } = null!;
    public DbSet<DebitCardLimitTranslation> DebitCardLimitTranslations { get; set; } = null!;
    public DbSet<PowerOfSignature> PowerOfSignatures { get; set; } = null!;
    public DbSet<PowerOfSignatureTranslation> PowerOfSignatureTranslations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<EmployeesRangeNumber>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.EmployeesRangeNumberID);
        
        modelBuilder.Entity<GenderCode>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.GenderCodeID);
        
        modelBuilder.Entity<JoinType>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.JoinTypeID);
        
        modelBuilder.Entity<MaritalRegime>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.MaritalRegimeID);
        
        modelBuilder.Entity<NationAlpha2>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.NationAlpha2ID);
        
        modelBuilder.Entity<Noga>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.NogaID);
        
        modelBuilder.Entity<Pep>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PepID);
        
        modelBuilder.Entity<PersonAcquisitionSource>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonAcquisitionSourceID);
        
        modelBuilder.Entity<Personality>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonalityID);
        
        modelBuilder.Entity<PersonCodingType>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonCodingTypeID);
        
        modelBuilder.Entity<PersonDependentsNumber>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonDependentsNumberID);
        
        modelBuilder.Entity<PersonInternalType>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonInternalTypeID);
        
        modelBuilder.Entity<PersonMaritalStatus>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonMaritalStatusID);
        
        modelBuilder.Entity<PersonNature>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonNatureID);
        
        modelBuilder.Entity<PersonOrganizationType>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonOrganizationTypeID);
        
        modelBuilder.Entity<PersonProfession>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonProfessionID);
        
        modelBuilder.Entity<PersonToRelationshipRole>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.PersonToRelationshipRoleID);
        
        modelBuilder.Entity<RelationshipType>()
            .HasMany(e => e.Translations)
            .WithOne()
            .HasForeignKey(e => e.RelationshipTypeID);

        modelBuilder.Entity<AssetAlternativeCode>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetAlternativeCode)
            .HasForeignKey(e => e.AssetAlternativeCodeID);

        modelBuilder.Entity<AssetNoteType>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetNoteType)
            .HasForeignKey(e => e.AssetNoteTypeID);

        modelBuilder.Entity<AssetStatus>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetStatus)
            .HasForeignKey(e => e.AssetStatusID);

        modelBuilder.Entity<AssetStatusReason>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetStatusReason)
            .HasForeignKey(e => e.AssetStatusReasonID);

        modelBuilder.Entity<AssetToAssetLink>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetToAssetLink)
            .HasForeignKey(e => e.AssetToAssetLinkID);

        modelBuilder.Entity<AssetToPersonLink>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetToPersonLink)
            .HasForeignKey(e => e.AssetToPersonLinkID);

        modelBuilder.Entity<AssetToRelationshipLink>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetToRelationshipLink)
            .HasForeignKey(e => e.AssetToRelationshipLinkID);

        modelBuilder.Entity<AssetType>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.AssetType)
            .HasForeignKey(e => e.AssetTypeID);

        modelBuilder.Entity<BusinessDateType>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.BusinessDateType)
            .HasForeignKey(e => e.BusinessDateTypeID);

        modelBuilder.Entity<DebitCardLimit>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.DebitCardLimit)
            .HasForeignKey(e => e.DebitCardLimitID);

        modelBuilder.Entity<PowerOfSignature>()
            .HasMany(e => e.Translations)
            .WithOne(t => t.PowerOfSignature)
            .HasForeignKey(e => e.PowerOfSignatureID);
    }
}
