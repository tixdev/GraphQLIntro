using HotChocolate.Types;
using Person.API.Models;

namespace Person.API.Graph.Types;

public class PersonNameType : ObjectType<PersonName>
{
    protected override void Configure(IObjectTypeDescriptor<PersonName> descriptor)
    {
        descriptor.Field(t => t.PltPersonNameTypeID).IsProjected(true);
        descriptor.Field(t => t.Person).Ignore();
    }
}
