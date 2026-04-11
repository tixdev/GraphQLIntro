using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.TypeExtensions;

[ExtendObjectType(typeof(Person.API.Models.Person))]
public class PersonPlausibilityExtensions
{
    public PersonNatureFederationStub? GetPersonNature([Parent] Person.API.Models.Person model)
        => model.PltPersonNatureID != 0 ? new PersonNatureFederationStub { PersonNatureID =  model.PltPersonNatureID } : null;

    public PersonCodingTypeFederationStub? GetPersonCodingType([Parent] Person.API.Models.Person model)
        => model.PltPersonCodingTypeID != 0 ? new PersonCodingTypeFederationStub { PersonCodingTypeID =  model.PltPersonCodingTypeID } : null;
}

[ExtendObjectType(typeof(InternalPerson))]
public class InternalPersonPlausibilityExtensions
{
    public PersonInternalTypeFederationStub? GetPersonInternalType([Parent] InternalPerson model)
        => model.PltPersonInternalTypeID != 0 ? new PersonInternalTypeFederationStub { PersonInternalTypeID =  model.PltPersonInternalTypeID } : null;
}

[ExtendObjectType(typeof(LegalPerson))]
public class LegalPersonPlausibilityExtensions
{
    public EmployeesRangeNumberFederationStub? GetEmployeesRangeNumber([Parent] LegalPerson model)
        => model.PltEmployeesRangeNumberID != null ? new EmployeesRangeNumberFederationStub { EmployeesRangeNumberID =  model.PltEmployeesRangeNumberID.Value } : null;

    public PersonOrganizationTypeFederationStub? GetPersonOrganizationType([Parent] LegalPerson model)
        => model.PltPersonOrganizationTypeID != null ? new PersonOrganizationTypeFederationStub { PersonOrganizationTypeID =  model.PltPersonOrganizationTypeID.Value } : null;
}

[ExtendObjectType(typeof(LegalPersonSensibleData))]
public class LegalPersonSensibleDataPlausibilityExtensions
{
    public NationAlpha2FederationStub? GetRegisteredOffice([Parent] LegalPersonSensibleData model)
        => model.PltRegisteredOfficeID != null ? new NationAlpha2FederationStub { NationAlpha2ID =  model.PltRegisteredOfficeID.Value } : null;
}

[ExtendObjectType(typeof(NaturalPerson))]
public class NaturalPersonPlausibilityExtensions
{
    public GenderCodeFederationStub? GetGenderCode([Parent] NaturalPerson model)
        => model.PltGenderCodeID != null ? new GenderCodeFederationStub { GenderCodeID =  model.PltGenderCodeID.Value } : null;

    public PersonMaritalStatusFederationStub? GetPersonMaritalStatus([Parent] NaturalPerson model)
        => model.PltPersonMaritalStatusID != null ? new PersonMaritalStatusFederationStub { PersonMaritalStatusID =  model.PltPersonMaritalStatusID.Value } : null;

    public MaritalRegimeFederationStub? GetMaritalRegime([Parent] NaturalPerson model)
        => model.PltMaritalRegimeID != null ? new MaritalRegimeFederationStub { MaritalRegimeID =  model.PltMaritalRegimeID.Value } : null;

    public PersonDependentsNumberFederationStub? GetPersonDependentsNumber([Parent] NaturalPerson model)
        => model.PltPersonDependentsNumberID != null ? new PersonDependentsNumberFederationStub { PersonDependentsNumberID =  model.PltPersonDependentsNumberID.Value } : null;

    public PersonProfessionFederationStub? GetPersonProfession([Parent] NaturalPerson model)
        => model.PltPersonProfessionID != null ? new PersonProfessionFederationStub { PersonProfessionID =  model.PltPersonProfessionID.Value } : null;

    public PepFederationStub? GetPep([Parent] NaturalPerson model)
        => model.PltPepID != null ? new PepFederationStub { PepID =  model.PltPepID.Value } : null;
}

[ExtendObjectType(typeof(NaturalPersonSensibleData))]
public class NaturalPersonSensibleDataPlausibilityExtensions
{
    public NationFederationStub? GetBirthNation([Parent] NaturalPersonSensibleData model)
        => model.PltBirthNationID != null ? new NationFederationStub { NationID =  model.PltBirthNationID.Value } : null;

    public NationalityFederationStub? GetSecondNationality([Parent] NaturalPersonSensibleData model)
        => model.PltSecondNationalityID != null ? new NationalityFederationStub { NationalityID =  model.PltSecondNationalityID.Value } : null;

    public ResidencyFederationStub? GetResidency([Parent] NaturalPersonSensibleData model)
        => model.PltResidencyID != null ? new ResidencyFederationStub { ResidencyID =  model.PltResidencyID.Value } : null;
}

[ExtendObjectType(typeof(GroupPerson))]
public class GroupPersonPlausibilityExtensions
{
    public JoinTypeFederationStub? GetJoinType([Parent] GroupPerson model)
        => model.PltJoinTypeID != null ? new JoinTypeFederationStub { JoinTypeID =  model.PltJoinTypeID.Value } : null;
}

[ExtendObjectType(typeof(PersonDetail))]
public class PersonDetailPlausibilityExtensions
{
    public PersonAcquisitionSourceFederationStub? GetPersonAcquisitionSource([Parent] PersonDetail model)
        => model.PltPersonAcquisitionSourceID != null ? new PersonAcquisitionSourceFederationStub { PersonAcquisitionSourceID =  model.PltPersonAcquisitionSourceID.Value } : null;

    public PersonalityFederationStub? GetPersonality([Parent] PersonDetail model)
        => model.PltPersonalityID != null ? new PersonalityFederationStub { PersonalityID =  model.PltPersonalityID.Value } : null;
}

[ExtendObjectType(typeof(PersonDetailSensibleData))]
public class PersonDetailSensibleDataPlausibilityExtensions
{
    public NationalityFederationStub? GetNationality([Parent] PersonDetailSensibleData model)
        => model.PltNationalityID != null ? new NationalityFederationStub { NationalityID =  model.PltNationalityID.Value } : null;

    public NogaFederationStub? GetNoga([Parent] PersonDetailSensibleData model)
        => model.PltNogaID != null ? new NogaFederationStub { NogaID =  model.PltNogaID.Value } : null;
}

[ExtendObjectType(typeof(PersonName))]
public class PersonNamePlausibilityExtensions
{
    public PersonNameTypeFederationStub? GetPersonNameType([Parent] PersonName model)
        => model.PltPersonNameTypeID != 0 ? new PersonNameTypeFederationStub { PersonNameTypeID = model.PltPersonNameTypeID } : null;
}
