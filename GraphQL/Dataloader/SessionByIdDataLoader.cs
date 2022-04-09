using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Dataloader
{
    public class SessionByIdDataLoader : BatchDataLoader<int, Session>
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbCtxFactory;

        public SessionByIdDataLoader(IBatchScheduler batchScheduler,
                                    IDbContextFactory<ApplicationDbContext> dnCtxFactory) : base(batchScheduler) => this.dbCtxFactory = dnCtxFactory?? throw new ArgumentNullException(nameof(dbCtxFactory));   

        protected override async Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using ApplicationDbContext ctx  = dbCtxFactory.CreateDbContext();

            return await ctx.Sessions.Where(a => keys.Contains(a.Id)).ToDictionaryAsync(a => a.Id, cancellationToken);

        }
    }
}
