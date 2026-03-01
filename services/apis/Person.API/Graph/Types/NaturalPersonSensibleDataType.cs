using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class NaturalPersonSensibleDataType : ObjectType<NaturalPersonSensibleData>
{
    protected override void Configure(IObjectTypeDescriptor<NaturalPersonSensibleData> descriptor)
    {
        descriptor.Field(t => t.PltBirthNationID).IsProjected(true);
        descriptor.Field(t => t.PltSecondNationalityID).IsProjected(true);
        descriptor.Field(t => t.PltResidencyID).IsProjected(true);
    }
}
