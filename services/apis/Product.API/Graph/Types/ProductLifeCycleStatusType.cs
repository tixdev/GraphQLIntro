using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductLifeCycleStatusType : ObjectType<ProductLifeCycleStatus>
{
    protected override void Configure(IObjectTypeDescriptor<ProductLifeCycleStatus> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductLifeCycleStatusId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltProductStatusId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
