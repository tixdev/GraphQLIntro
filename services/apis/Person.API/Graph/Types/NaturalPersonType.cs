using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Person.API.Models;
using Person.API.Graph.FederationStubs;

namespace Person.API.Graph.Types;

public class NaturalPersonType : ObjectType<NaturalPerson>
{
    protected override void Configure(IObjectTypeDescriptor<NaturalPerson> descriptor)
    {
        descriptor.Field(t => t.PltGenderCodeID).IsProjected(true);
        descriptor.Field(t => t.PltPersonMaritalStatusID).IsProjected(true);
        descriptor.Field(t => t.PltMaritalRegimeID).IsProjected(true);
        descriptor.Field(t => t.PltPersonDependentsNumberID).IsProjected(true);
        descriptor.Field(t => t.PltPersonProfessionID).IsProjected(true);
        descriptor.Field(t => t.PltPepID).IsProjected(true);
    }
}
