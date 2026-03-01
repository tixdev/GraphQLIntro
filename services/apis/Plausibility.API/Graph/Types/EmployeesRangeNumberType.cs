using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class EmployeesRangeNumberType : ObjectType<EmployeesRangeNumber>
{
    protected override void Configure(IObjectTypeDescriptor<EmployeesRangeNumber> descriptor)
    {
        var method = typeof(EmployeesRangeNumberType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("employeesRangeNumberID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<EmployeesRangeNumber?> GetByIdAsync(int employeesRangeNumberID, EmployeesRangeNumberByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(employeesRangeNumberID);
    }
}
