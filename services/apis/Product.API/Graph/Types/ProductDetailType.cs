using Product.API.Models;

namespace Product.API.Graph.Types;

public class ProductDetailType : ObjectType<ProductDetail>
{
    protected override void Configure(IObjectTypeDescriptor<ProductDetail> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.ProductDetailId).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.ProductId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltStructureId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltClassId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltSubclassId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltFamilyId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.PltMarketId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.GroupBankId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(t => t.SellStartDate).Type<DateTimeType>();
        descriptor.Field(t => t.ValidEndDate).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.Notes).Type<StringType>();
    }
}
