using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions
{
    public class ScheduleSessionPayload : SessionPayloadBase
    {
        public ScheduleSessionPayload(UserError error) : base(new[] { error })
        {
        }
        public ScheduleSessionPayload(Session session) : base(session)
        {
        }

        public async Task<Track?> GetTrackAsync(
            TrackByIdDataLoader trackById,
            CancellationToken ct)
        {
            if (Session is null) return null;

            return await trackById.LoadAsync(Session.Id, ct);
        }
        [UseApplicationDbContext]
        public async Task<IEnumerable<SpeakerDb>?> GetSpeakersAsync(
            [ScopedService] ApplicationDbContext ctx,
            SpeakerByIdDataLoader speakerById,
            CancellationToken ct)
        {
            if (Session is null) return null;

            int[] speakerIds = await ctx.Sessions
                .Where(s => s.Id == Session.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(a => a.SessionSpeakers.Select(a => a.SpeakerId))
                .ToArrayAsync();

            return await speakerById.LoadAsync(speakerIds, ct);
        }


    }
}
