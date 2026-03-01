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
    }
}
