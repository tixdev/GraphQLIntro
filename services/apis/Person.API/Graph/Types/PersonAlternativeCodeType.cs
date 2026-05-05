using HotChocolate.Types;
using Person.API.Models;

namespace Person.API.Graph.Types;

public class PersonAlternativeCodeType : ObjectType<PersonAlternativeCode>
{
    protected override void Configure(IObjectTypeDescriptor<PersonAlternativeCode> descriptor)
    {
        descriptor.Field(t => t.PersonAlternativeCodeID).IsProjected();
        descriptor.Field(t => t.PersonID).IsProjected();
        descriptor.Field(t => t.PltPersonAlternativeCodeTypeID).IsProjected();
        descriptor.Field(t => t.AlternativeCodeDescription).IsProjected();
        descriptor.Field(t => t.ValidStartDate).IsProjected();
        descriptor.Field(t => t.ValidEndDate).IsProjected();
        descriptor.Field(t => t.GroupBankID).IsProjected();
        descriptor.Field(t => t.CaseID).IsProjected();
    }
}
