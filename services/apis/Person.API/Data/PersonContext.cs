using Microsoft.EntityFrameworkCore;
using Person.API.Models;
using Shared.Temporal;

namespace Person.API.Data;

public class PersonContext(DbContextOptions<PersonContext> options, ITemporalContext temporalContext) : DbContext(options)
{
    public DbSet<Models.Person> Person { get; set; }
    public DbSet<PersonDetail> PersonDetail { get; set; }
    public DbSet<PersonDetailSensibleData> PersonDetailSensibleData { get; set; }
    public DbSet<NaturalPerson> NaturalPerson { get; set; }
    public DbSet<NaturalPersonSensibleData> NaturalPersonSensibleData { get; set; }
    public DbSet<LegalPerson> LegalPerson { get; set; }
    public DbSet<LegalPersonSensibleData> LegalPersonSensibleData { get; set; }
    public DbSet<InternalPerson> InternalPerson { get; set; }
    public DbSet<GroupPerson> GroupPerson { get; set; }
    public DbSet<GroupPersonSensibleData> GroupPersonSensibleData { get; set; }
    public DbSet<PersonOnlineService> PersonOnlineService { get; set; }
    public DbSet<PersonName> PersonName { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Person — root
        modelBuilder.Entity<Models.Person>(e =>
        {
            e.ToTable("Person", "Person");
            e.HasKey(p => p.PersonID);
        });

        // PersonDetail — 1:1 with Person (PersonDetailID = PersonID via FK on PersonID)
        modelBuilder.Entity<PersonDetail>(e =>
        {
            e.ToTable("PersonDetail", "Person");
            e.HasKey(p => p.PersonDetailID);
            e.HasOne(p => p.Person).WithOne(p => p.PersonDetail)
                .HasForeignKey<PersonDetail>(p => p.PersonID);
        });

        // PersonDetailSensibleData — 1:1 with PersonDetail
        modelBuilder.Entity<PersonDetailSensibleData>(e =>
        {
            e.ToTable("PersonDetailSensibleData", "Person");
            e.HasKey(p => p.PersonDetailID);
            e.HasOne(p => p.PersonDetail).WithOne(p => p.SensibleData)
                .HasForeignKey<PersonDetailSensibleData>(p => p.PersonDetailID);
        });

        // NaturalPerson — 1:1 with Person (NaturalPersonID = PersonID)
        modelBuilder.Entity<NaturalPerson>(e =>
        {
            e.ToTable("NaturalPerson", "Person");
            e.HasKey(p => p.NaturalPersonID);
            e.HasOne(p => p.Person).WithOne(p => p.NaturalPerson)
                .HasForeignKey<NaturalPerson>(p => p.NaturalPersonID);
        });

        // NaturalPersonSensibleData — 1:1 with NaturalPerson
        modelBuilder.Entity<NaturalPersonSensibleData>(e =>
        {
            e.ToTable("NaturalPersonSensibleData", "Person");
            e.HasKey(p => p.NaturalPersonID);
            e.HasOne(p => p.NaturalPerson).WithOne(p => p.SensibleData)
                .HasForeignKey<NaturalPersonSensibleData>(p => p.NaturalPersonID)
                .IsRequired(false);
        });

        // LegalPerson — 1:1 with Person (LegalPersonID = PersonID)
        modelBuilder.Entity<LegalPerson>(e =>
        {
            e.ToTable("LegalPerson", "Person");
            e.HasKey(p => p.LegalPersonID);
            e.HasOne(p => p.Person).WithOne(p => p.LegalPerson)
                .HasForeignKey<LegalPerson>(p => p.LegalPersonID);
        });

        // LegalPersonSensibleData — 1:1 with LegalPerson
        modelBuilder.Entity<LegalPersonSensibleData>(e =>
        {
            e.ToTable("LegalPersonSensibleData", "Person");
            e.HasKey(p => p.LegalPersonID);
            e.HasOne(p => p.LegalPerson).WithOne(p => p.SensibleData)
                .HasForeignKey<LegalPersonSensibleData>(p => p.LegalPersonID)
                .IsRequired(false);
        });

        // InternalPerson — 1:1 with Person (InternalPersonID = PersonID)
        modelBuilder.Entity<InternalPerson>(e =>
        {
            e.ToTable("InternalPerson", "Person");
            e.HasKey(p => p.InternalPersonID);
            e.HasOne(p => p.Person).WithOne(p => p.InternalPerson)
                .HasForeignKey<InternalPerson>(p => p.InternalPersonID);
        });

        // GroupPerson — 1:1 with Person (GroupPersonID = PersonID)
        modelBuilder.Entity<GroupPerson>(e =>
        {
            e.ToTable("GroupPerson", "Person");
            e.HasKey(p => p.GroupPersonID);
            e.HasOne(p => p.Person).WithOne(p => p.GroupPerson)
                .HasForeignKey<GroupPerson>(p => p.GroupPersonID);
        });

        // GroupPersonSensibleData — 1:1 with GroupPerson
        modelBuilder.Entity<GroupPersonSensibleData>(e =>
        {
            e.ToTable("GroupPersonSensibleData", "Person");
            e.HasKey(p => p.GroupPersonID);
            e.HasOne(p => p.GroupPerson).WithOne(p => p.SensibleData)
                .HasForeignKey<GroupPersonSensibleData>(p => p.GroupPersonID)
                .IsRequired(false);
        });

        // PersonOnlineService — 1:1 with Person
        modelBuilder.Entity<PersonOnlineService>(e =>
        {
            e.ToTable("PersonOnlineService", "Person");
            e.HasKey(p => p.PersonOnlineServiceID);
            e.HasOne(p => p.Person).WithOne(p => p.PersonOnlineService)
                .HasForeignKey<PersonOnlineService>(p => p.PersonID);
        });

        // PersonName — 1:N with Person
        modelBuilder.Entity<PersonName>(e =>
        {
            e.ToTable("PersonName", "Person");
            e.HasKey(p => p.PersonNameID);
            e.HasOne(p => p.Person).WithMany(p => p.PersonName)
                .HasForeignKey(p => p.PersonID);
        });

        // Apply Global Query Filter for Temporal Entities dynamically
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITemporalEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(PersonContext).GetMethod(nameof(SetTemporalFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null)
                {
                    method.MakeGenericMethod(entityType.ClrType).Invoke(this, new object[] { modelBuilder });
                }
            }
        }
    }

    private void SetTemporalFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ITemporalEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            temporalContext.Mode == TemporalFilterMode.All ||
            (temporalContext.Mode == TemporalFilterMode.AsOf 
                && e.ValidStartDate <= temporalContext.CurrentAsOfDate 
                && e.ValidEndDate >= temporalContext.CurrentAsOfDate) ||
            (temporalContext.Mode == TemporalFilterMode.ActiveBetween 
                && temporalContext.IsRangeStartProvided 
                && e.ValidStartDate <= temporalContext.SafeRangeEnd 
                && e.ValidEndDate >= temporalContext.SafeRangeStart)
        );
    }
}
