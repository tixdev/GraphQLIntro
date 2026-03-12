using Product.API.Graph.FederationStubs;
using Product.API.Models;

namespace Product.API.Graph.TypeExtensions;

[ExtendObjectType(typeof(ProductDescription))]
public class ProductDescriptionPlausibilityExtensions
{
    public LanguageFederationStub GetLanguage([Parent] ProductDescription model)
        => new LanguageFederationStub { LanguageId = model.LanguageId };
}
