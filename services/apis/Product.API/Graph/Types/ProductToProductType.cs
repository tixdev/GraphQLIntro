using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductToProductType : ObjectType<ProductToProduct>
{
    protected override void Configure(IObjectTypeDescriptor<ProductToProduct> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductToProductId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ParentProductId).Type<IntType>().IsProjected();
        descriptor.Field(t => t.ProductChildId).Type<IntType>().IsProjected();
        descriptor.Field(t => t.PltRoleId).Type<IntType>().IsProjected();
        descriptor.Field(t => t.PltModeId).Type<IntType>().IsProjected();

        descriptor.Field(t => t.Product).Type<ProductType>().IsProjected(false);
        descriptor.Field(t => t.ParentProduct).Type<ProductType>().IsProjected(false);
        descriptor.Field(t => t.ProductChild).Type<ProductType>().IsProjected(false);
    }
}
