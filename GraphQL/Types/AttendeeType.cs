using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Types
{
    public class AttendeeType : ObjectType<Attendee>
    {
        protected override void Configure(IObjectTypeDescriptor<Attendee> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(a => a.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<AttendeeByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }

        private class AttendeeResolvers
        {
            public async Task<IEnumerable<Session>> GetSessionAsync(
                Attendee attendee,
                [ScopedService] ApplicationDbContext ctx,
                SessionByIdDataLoader sessionById,
                CancellationToken ct)
            {
                int[] speakersIds = await ctx.Attendees
                                .Where(a => a.Id == attendee.Id)
                                .Include(a => a.SessionsAttendees)
                                .SelectMany(a => a.SessionsAttendees.Select(t => t.SessionId))
                                .ToArrayAsync();
                return await sessionById.LoadAsync(speakersIds, ct);
            }
        }
    }
}
