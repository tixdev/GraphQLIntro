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
    [GraphQLName("pltAssetAlternativeCodes")]
    public IQueryable<AssetAlternativeCode> GetAssetAlternativeCodes(PlausibilityDbContext context)
    {
        return context.AssetAlternativeCodes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetNoteTypes")]
    public IQueryable<AssetNoteType> GetAssetNoteTypes(PlausibilityDbContext context)
    {
        return context.AssetNoteTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetStatuss")]
    public IQueryable<AssetStatus> GetAssetStatuss(PlausibilityDbContext context)
    {
        return context.AssetStatuss.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetStatusReasons")]
    public IQueryable<AssetStatusReason> GetAssetStatusReasons(PlausibilityDbContext context)
    {
        return context.AssetStatusReasons.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetToAssetLinks")]
    public IQueryable<AssetToAssetLink> GetAssetToAssetLinks(PlausibilityDbContext context)
    {
        return context.AssetToAssetLinks.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetToPersonLinks")]
    public IQueryable<AssetToPersonLink> GetAssetToPersonLinks(PlausibilityDbContext context)
    {
        return context.AssetToPersonLinks.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetToRelationshipLinks")]
    public IQueryable<AssetToRelationshipLink> GetAssetToRelationshipLinks(PlausibilityDbContext context)
    {
        return context.AssetToRelationshipLinks.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltAssetTypes")]
    public IQueryable<AssetType> GetAssetTypes(PlausibilityDbContext context)
    {
        return context.AssetTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltBusinessDateTypes")]
    public IQueryable<BusinessDateType> GetBusinessDateTypes(PlausibilityDbContext context)
    {
        return context.BusinessDateTypes.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltDebitCardLimits")]
    public IQueryable<DebitCardLimit> GetDebitCardLimits(PlausibilityDbContext context)
    {
        return context.DebitCardLimits.Include(x => x.Translations);
    }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("pltPowerOfSignatures")]
    public IQueryable<PowerOfSignature> GetPowerOfSignatures(PlausibilityDbContext context)
    {
        return context.PowerOfSignatures.Include(x => x.Translations);
    }


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
