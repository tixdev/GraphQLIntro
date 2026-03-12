using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductGroupDetailType : ObjectType<ProductGroupDetail>
{
    protected override void Configure(IObjectTypeDescriptor<ProductGroupDetail> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductGroupDetailId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductGroupId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.GroupBankId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
