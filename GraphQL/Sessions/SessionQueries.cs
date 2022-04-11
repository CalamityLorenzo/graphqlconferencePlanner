using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions
{
    [ExtendObjectType("Query")]
    public class SessionQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct) => await ctx.Sessions.ToListAsync(ct);

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader dataloader,
            CancellationToken ct) => dataloader.LoadAsync(id, ct);


        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader dataloader,
            CancellationToken ct) => await dataloader.LoadAsync(ids, ct);
    }
}
