using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Types
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<SessionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(a => a.SessionSpeakers)
                .ResolveWith<SessionResolvers>(t => t.GetSpeakerAsync(default!, default!, default!, default))
                .UseDbExtensionContext<ApplicationDbContext>()
                .Name("attendees");
            descriptor
                .Field(t => t.Track)
                .ResolveWith<SessionResolvers>(t => t.GetTrackAsync(default!, default!, default));

            descriptor
                .Field(t => t.TrackId)
                .ID(nameof(Track));
        }

        private class SessionResolvers
        {
            public async Task<IEnumerable<SpeakerDb>> GetSpeakerAsync(Session session, [ScopedService] ApplicationDbContext dbContext, SpeakerByIdDataLoader speakerById, CancellationToken ct)
            {
                int[] speakerIds = await dbContext.Sessions
                        .Where(a => a.Id == session.Id)
                        .Include(s => s.SessionSpeakers)
                        .SelectMany(s => s.SessionSpeakers.Select(b => b.SpeakerId))
                        .ToArrayAsync();
                return await speakerById.LoadAsync(speakerIds, ct);
            }
            public async Task<IEnumerable<Attendee>> GetAttendeesAsync(
                    Session session,

                    [ScopedService] ApplicationDbContext dbContext,
                    AttendeeByIdDataLoader attendeeById,
                    CancellationToken cancellationToken)
            {
                int[] attendeeIds = await dbContext.Sessions
                    .Where(s => s.Id == session.Id)
                    .Include(session => session.SessionAttendees)
                    .SelectMany(session => session.SessionAttendees.Select(t => t.AttendeeId))
                    .ToArrayAsync();

                return await attendeeById.LoadAsync(attendeeIds, cancellationToken);
            }
            public async Task<Track?> GetTrackAsync(
                Session session,
                TrackByIdDataLoader trackById,
                CancellationToken ct)
            {
                if (session.TrackId is null) return null;

                return await trackById.LoadAsync(session.TrackId.Value, ct);
            }
        }
    }
}
