using Product.API.Models;
using HotChocolate.ApolloFederation.Types;

namespace Product.API.Graph.Types;

public class ProductGroupType : ObjectType<ProductGroup>
{
    protected override void Configure(IObjectTypeDescriptor<ProductGroup> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.ProductGroupId).Type<NonNullType<IdType>>().IsProjected();
        descriptor.Field(t => t.ProductGroupCode).Type<NonNullType<StringType>>().IsProjected();
        descriptor.Field(t => t.GroupBankId).Type<NonNullType<IntType>>().IsProjected();

        descriptor.Field(t => t.ProductGroupDescription)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductGroupResolvers>(r => r.GetProductGroupDescriptionAsync(default!, default!, default!))
            .Type<ListType<ProductGroupDescriptionType>>();

        descriptor.Field(t => t.ProductGroupDetail)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductGroupResolvers>(r => r.GetProductGroupDetailAsync(default!, default!, default!))
            .Type<ListType<ProductGroupDetailType>>();

        descriptor.Field(t => t.ProductGroupLifeCycleStatus)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductGroupResolvers>(r => r.GetProductGroupLifeCycleStatusAsync(default!, default!, default!))
            .Type<ListType<ProductGroupLifeCycleStatusType>>();

        descriptor.Field(t => t.ProductGroupToProduct)
            .IsProjected(false)
            .Type<ListType<ProductGroupToProductType>>();
    }
}
