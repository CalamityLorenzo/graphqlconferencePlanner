using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ConferencePlanner.GraphQL.Tracks
{
    [ExtendObjectType("Query")]
    public class TrackQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Track>> GetTrackAsync(
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct)
        {
            FakeClasss fc = new FakeClasss
            {
                FakeName = "pumpkin spice"
            };
            Debug.WriteLine(fc.FakeName);
            return await ctx.Tracks.ToListAsync(ct);

        }

        [UseApplicationDbContext]
        public Task<Track> GetTrackByNameAsync(
            string name,
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct) => ctx.Tracks.FirstAsync(t => t.Name == name);

        public Task<Track> GetTrackByIdAsync(
            [ID(nameof(Track))] int id,
            TrackByIdDataLoader trackById,
            CancellationToken ct) => trackById.LoadAsync(id, ct);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync(
                                        [ID(nameof(Track))] int[] ids,
                                        TrackByIdDataLoader trackById,
                                        CancellationToken ct) => await trackById.LoadAsync(ids, ct);
    }
}
