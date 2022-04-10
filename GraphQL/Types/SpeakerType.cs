using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Types
{
    // WE define the names 
    // implementations etc of anything to with the <T>
    // Includes how to fetch other parts of the type.
    // And changes to the schema.
    public class SpeakerType : ObjectType<SpeakerDb>
    {
        protected override void Configure(IObjectTypeDescriptor<SpeakerDb> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<SpeakerByIdDataLoader>()
                 .LoadAsync(id, ctx.RequestAborted));

            
            descriptor
                   .Field(t => t.SessionSpeakers)
                   .ResolveWith<SpeakerResolvers>(t => t.GetSessionAsync(default!, default!, default!, default))
                   .UseDbExtensionContext<ApplicationDbContext>()
                   .Name("sessions");
        }

        private class SpeakerResolvers
        {
            public async Task<IEnumerable<Session>> GetSessionAsync(
                [Parent] SpeakerDb speaker,
                [ScopedService] ApplicationDbContext ctx,
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await ctx.Speakers
                    .Where(s => s.Id == speaker.Id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
                    .ToArrayAsync();

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}
