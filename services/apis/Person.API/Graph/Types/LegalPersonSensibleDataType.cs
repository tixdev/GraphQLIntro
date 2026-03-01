using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class LegalPersonSensibleDataType : ObjectType<LegalPersonSensibleData>
{
    protected override void Configure(IObjectTypeDescriptor<LegalPersonSensibleData> descriptor)
    {
        descriptor.Field(t => t.PltRegisteredOfficeID).IsProjected(true);
    }
}
