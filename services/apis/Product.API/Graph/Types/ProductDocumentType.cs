using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductDocumentType : ObjectType<ProductDocument>
{
    protected override void Configure(IObjectTypeDescriptor<ProductDocument> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductDocumentId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltFormId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
