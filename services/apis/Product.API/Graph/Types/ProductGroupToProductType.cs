using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductGroupToProductType : ObjectType<ProductGroupToProduct>
{
    protected override void Configure(IObjectTypeDescriptor<ProductGroupToProduct> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductGroupToProductId).Type<NonNullType<IdType>>().IsProjected();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ProductGroupId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.GroupBankId).Type<NonNullType<IntType>>().IsProjected();

        descriptor.Field(t => t.Product).Type<ProductType>().IsProjected(false);
        descriptor.Field(t => t.ProductGroup).Type<ProductGroupType>().IsProjected(false);
    }
}
