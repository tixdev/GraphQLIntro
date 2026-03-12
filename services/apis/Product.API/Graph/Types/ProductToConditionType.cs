using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductToConditionType : ObjectType<ProductToCondition>
{
    protected override void Configure(IObjectTypeDescriptor<ProductToCondition> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductToConditionId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ConditionId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
    }
}
