using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductGroupDescriptionType : ObjectType<ProductGroupDescription>
{
    protected override void Configure(IObjectTypeDescriptor<ProductGroupDescription> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductGroupDescriptionId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductGroupId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.LanguageId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.Description).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
