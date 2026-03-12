using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductRoleAllowedType : ObjectType<ProductRoleAllowed>
{
    protected override void Configure(IObjectTypeDescriptor<ProductRoleAllowed> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductRoleAllowedId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltRoleAllowedId).Type<NonNullType<IntType>>().IsProjected();
    }
}
