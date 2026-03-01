using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Queries;

public class PlausibilityQueries
{

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<EmployeesRangeNumber> GetEmployeesRangeNumbers(PlausibilityDbContext context)
    {
        return context.EmployeesRangeNumbers.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GenderCode> GetGenderCodes(PlausibilityDbContext context)
    {
        return context.GenderCodes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<JoinType> GetJoinTypes(PlausibilityDbContext context)
    {
        return context.JoinTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MaritalRegime> GetMaritalRegimes(PlausibilityDbContext context)
    {
        return context.MaritalRegimes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<NationAlpha2> GetNationAlpha2s(PlausibilityDbContext context)
    {
        return context.NationAlpha2s.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Noga> GetNogas(PlausibilityDbContext context)
    {
        return context.Nogas.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Pep> GetPeps(PlausibilityDbContext context)
    {
        return context.Peps.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonAcquisitionSource> GetPersonAcquisitionSources(PlausibilityDbContext context)
    {
        return context.PersonAcquisitionSources.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Personality> GetPersonalitys(PlausibilityDbContext context)
    {
        return context.Personalitys.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonCodingType> GetPersonCodingTypes(PlausibilityDbContext context)
    {
        return context.PersonCodingTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonDependentsNumber> GetPersonDependentsNumbers(PlausibilityDbContext context)
    {
        return context.PersonDependentsNumbers.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonInternalType> GetPersonInternalTypes(PlausibilityDbContext context)
    {
        return context.PersonInternalTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonMaritalStatus> GetPersonMaritalStatuss(PlausibilityDbContext context)
    {
        return context.PersonMaritalStatuss.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonNature> GetPersonNatures(PlausibilityDbContext context)
    {
        return context.PersonNatures.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonOrganizationType> GetPersonOrganizationTypes(PlausibilityDbContext context)
    {
        return context.PersonOrganizationTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonProfession> GetPersonProfessions(PlausibilityDbContext context)
    {
        return context.PersonProfessions.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonToRelationshipRole> GetPersonToRelationshipRoles(PlausibilityDbContext context)
    {
        return context.PersonToRelationshipRoles.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RelationshipType> GetRelationshipTypes(PlausibilityDbContext context)
    {
        return context.RelationshipTypes.Include(x => x.Translations);
    }
}
