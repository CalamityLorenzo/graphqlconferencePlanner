using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
        {
            descriptor
                   .Field(t=>t.SessionSpeakers)
                   .ResolveWith<SpeakerResolvers>(t=>t.Get)
                   https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
        }
    }
}
