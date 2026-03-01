using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class InternalPersonType : ObjectType<InternalPerson>
{
    protected override void Configure(IObjectTypeDescriptor<InternalPerson> descriptor)
    {
        descriptor.Field(t => t.PltPersonInternalTypeID).IsProjected(true);
    }
}
