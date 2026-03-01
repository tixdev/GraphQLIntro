using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class GroupPersonType : ObjectType<GroupPerson>
{
    protected override void Configure(IObjectTypeDescriptor<GroupPerson> descriptor)
    {
        descriptor.Field(t => t.PltJoinTypeID).IsProjected(true);
    }
}
