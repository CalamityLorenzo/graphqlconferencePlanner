using ConferencePlanner.GraphQL.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace ConferencePlanner.GraphQL.Extensions
{
 // This is middleware as applied directly to c# objects.
 // Attributes allow interception.
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        => descriptor.UseDbExtensionContext<ApplicationDbContext>();

    }
}
