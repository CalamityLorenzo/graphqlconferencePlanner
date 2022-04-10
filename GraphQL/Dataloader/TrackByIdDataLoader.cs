using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Dataloader
{
    public class TrackByIdDataLoader : BatchDataLoader<int, Track>
    {
        private readonly IDbContextFactory<ApplicationDbContext> ctxFactory;

        public TrackByIdDataLoader(IBatchScheduler batch, IDbContextFactory<ApplicationDbContext> ctxFactory) : base(batch)
        {
            this.ctxFactory = ctxFactory ?? throw new ArgumentNullException(nameof(ctxFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using ApplicationDbContext ctx = ctxFactory.CreateDbContext();
            return await ctx.Tracks.Where(a => keys.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, cancellationToken);
        }
    }
}
