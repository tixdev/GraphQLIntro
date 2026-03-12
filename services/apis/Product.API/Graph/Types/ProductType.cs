using Product.API.Graph.DataLoaders;
using Product.API.Models;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Product.API.Graph.Types;

public class ProductType : ObjectType<Models.Product>
{
    protected override void Configure(IObjectTypeDescriptor<Models.Product> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        var method = typeof(ProductType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("productId").ResolveReferenceWith(method);

        descriptor.Field(t => t.ProductId).Type<NonNullType<IdType>>().IsProjected();
        descriptor.Field(t => t.ProductCode).Type<NonNullType<StringType>>().IsProjected();
        descriptor.Field(t => t.GroupBankId).Type<NonNullType<IntType>>().IsProjected();

        // Relations
        descriptor.Field(t => t.ProductDescription)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductDescriptionAsync(default!, default!, default!))
            .Type<ListType<ProductDescriptionType>>();
            
        descriptor.Field(t => t.ProductDetail)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductDetailAsync(default!, default!, default!))
            .Type<ListType<ProductDetailType>>();
            
        descriptor.Field(t => t.ProductToProductProduct)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductToProductAsync(default!, default!, default!))
            .Type<ListType<ProductToProductType>>();

        descriptor.Field(t => t.ProductDocument)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductDocumentAsync(default!, default!, default!))
            .Type<ListType<ProductDocumentType>>();

        descriptor.Field(t => t.ProductRoleAllowed)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductRoleAllowedAsync(default!, default!, default!))
            .Type<ListType<ProductRoleAllowedType>>();

        descriptor.Field(t => t.ProductToCondition)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductToConditionAsync(default!, default!, default!))
            .Type<ListType<ProductToConditionType>>();

        descriptor.Field(t => t.ProductLifeCycleStatus)
            .IsProjected(false)
            .ResolveWith<Resolvers.ProductResolvers>(r => r.GetProductLifeCycleStatusAsync(default!, default!, default!))
            .Type<ListType<ProductLifeCycleStatusType>>();
    }

    [ReferenceResolver]
    public static async Task<Models.Product?> GetByIdAsync(
        int productId,
        ProductByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(productId);
    }
}
