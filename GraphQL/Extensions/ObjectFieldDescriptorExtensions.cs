using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {

        // Where asp meets gQl. We get the service from the Asp di, and creaate a this objectefieldDescriptor for the type.
        // It's then used in the ApplicationDbContextAttribute.
        // I dont't like it. But How else can tou manage lifetimes of mlutiple contexts. It doesn't easily fir into the http paradigm.
        public static IObjectFieldDescriptor UseDbExtensionContext<TDbContext>(this IObjectFieldDescriptor @this) where TDbContext : DbContext
        {
            return @this.UseScopedService<TDbContext>(
                create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                disposeAsync: (s, c) => c.DisposeAsync());
        }
    }
}
