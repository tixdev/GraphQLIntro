using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class PersonDetailType : ObjectType<PersonDetail>
{
    protected override void Configure(IObjectTypeDescriptor<PersonDetail> descriptor)
    {
        descriptor.Field(t => t.PltPersonAcquisitionSourceID).IsProjected(true);
        descriptor.Field(t => t.PltPersonalityID).IsProjected(true);
    }
}
