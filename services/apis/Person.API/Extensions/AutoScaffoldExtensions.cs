using System.Reflection;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;

namespace Person.API.Extensions;

public static class AutoScaffoldExtensions
{
    public static IRequestExecutorBuilder AddAutoScaffoldedTypes(this IRequestExecutorBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => 
                t.IsSubclassOf(typeof(ObjectType)) ||
                t.IsSubclassOf(typeof(InputObjectType)) ||
                t.IsSubclassOf(typeof(EnumType)) ||
                t.IsSubclassOf(typeof(UnionType)) ||
                t.IsSubclassOf(typeof(InterfaceType)) ||
                t.GetCustomAttribute<ExtendObjectTypeAttribute>() != null ||
                t.GetCustomAttribute<ObjectTypeAttribute>() != null 
            )
            .Where(t => t.Name != "Query" && t.Name != "Mutation" && t.Name != "Subscription")
            .ToList();

        foreach (var type in types)
        {
            var isExtension = type.GetCustomAttribute<ExtendObjectTypeAttribute>() != null ||
                              typeof(ObjectTypeExtension).IsAssignableFrom(type) ||
                              type.Name.EndsWith("Extensions");

            if (isExtension)
            {
                builder.AddTypeExtension(type);
            }
            else
            {
                builder.AddType(type);
            }
        }

        return builder;
    }
}
