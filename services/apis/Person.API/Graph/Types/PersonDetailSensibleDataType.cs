using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class PersonDetailSensibleDataType : ObjectType<PersonDetailSensibleData>
{
    protected override void Configure(IObjectTypeDescriptor<PersonDetailSensibleData> descriptor)
    {
        descriptor.Field(t => t.PltNationalityID).IsProjected(true);
        descriptor.Field(t => t.PltNogaID).IsProjected(true);
    }
}
