using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Extensions;

namespace ConferencePlanner.GraphQL.Tracks
{
    [ExtendObjectType("Mutation")]
    public class TracksMutation
    {
        [UseApplicationDbContext]
        public async Task<AddTrackPayload> AddTrackAsync(
            AddTrackInput input,
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct)
        {
            var track = new Track { Name = input.Name };
            ctx.Tracks.Add(track);
            await ctx.SaveChangesAsync(ct);
            return new AddTrackPayload(track);
        }

        [UseApplicationDbContext]
        public async Task<RenameTrackPayload> RenameTrackAsync(
            RenameTrackInput input,
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct)
        {
            Track track = await ctx.Tracks.FindAsync(input.Id);
            track.Name = input.Name;

            await ctx.SaveChangesAsync(ct);

            return new RenameTrackPayload(track);
        }
    }
}
