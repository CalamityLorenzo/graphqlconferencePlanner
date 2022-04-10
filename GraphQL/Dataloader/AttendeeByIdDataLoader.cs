using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Dataloader
{
    public class AttendeeByIdDataLoader : BatchDataLoader<int, Attendee>
    {
        private readonly IDbContextFactory<ApplicationDbContext> ctxFactory;

        public AttendeeByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<ApplicationDbContext> ctxFactory) : base(batchScheduler)
        {
            this.ctxFactory = ctxFactory ?? throw new ArgumentNullException(nameof(ctxFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using ApplicationDbContext ctx = ctxFactory.CreateDbContext();

            return await ctx.Attendees.Where(a => keys.Contains(a.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }


    }
}
