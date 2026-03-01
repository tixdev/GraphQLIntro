using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("EmployeesRangeNumber")]
public class EmployeesRangeNumberFederationStub
{
    [Key]
    public int EmployeesRangeNumberID { get; set; }
}
