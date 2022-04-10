using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<SpeakerDb>> GetSpeakers([ScopedService] ApplicationDbContext context) => context.Speakers.ToListAsync();

        public Task<SpeakerDb> GetSpeakerAsync([ID(nameof(SpeakerDb))] int id,
                                            [DataLoader] SpeakerByIdDataLoader dataloader,
                                            CancellationToken ct) =>
                                            dataloader.LoadAsync(id, ct);
    }
}
