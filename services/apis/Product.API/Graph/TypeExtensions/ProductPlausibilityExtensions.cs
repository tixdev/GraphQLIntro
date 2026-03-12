using Product.API.Graph.FederationStubs;
using Product.API.Models;

namespace Product.API.Graph.TypeExtensions;

[ExtendObjectType(typeof(Models.Product))]
public class ProductPlausibilityExtensions
{
    public GroupBankFederationStub GetGroupBank([Parent] Models.Product model)
        => new GroupBankFederationStub { GroupBankId = model.GroupBankId };
}

[ExtendObjectType(typeof(ProductLifeCycleStatus))]
public class ProductLifeCycleStatusPlausibilityExtensions
{
    public ProductStatusFederationStub GetProductStatus([Parent] ProductLifeCycleStatus model)
        => new ProductStatusFederationStub { ProductStatusId = model.PltProductStatusId };
}
