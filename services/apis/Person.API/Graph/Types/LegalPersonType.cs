using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class LegalPersonType : ObjectType<LegalPerson>
{
    protected override void Configure(IObjectTypeDescriptor<LegalPerson> descriptor)
    {
        descriptor.Field(t => t.PltEmployeesRangeNumberID).IsProjected(true);
        descriptor.Field(t => t.PltPersonOrganizationTypeID).IsProjected(true);
    }
}
