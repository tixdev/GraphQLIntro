using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductDescriptionType : ObjectType<ProductDescription>
{
    protected override void Configure(IObjectTypeDescriptor<ProductDescription> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.ProductDescriptionId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.Description).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.LanguageId).Type<NonNullType<IntType>>().IsProjected();
    }
}
