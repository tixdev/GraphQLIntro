using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductGroupLifeCycleStatusType : ObjectType<ProductGroupLifeCycleStatus>
{
    protected override void Configure(IObjectTypeDescriptor<ProductGroupLifeCycleStatus> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductGroupLifeCycleStatusId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductGroupId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltProductGroupStatusId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
