using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Speakers
{
    [ExtendObjectType("Query")]
    public class SpeakersQueries
    {
        [UseApplicationDbContext]
        public Task<List<SpeakerDb>> GetSpeakers([ScopedService] ApplicationDbContext context) => context.Speakers.ToListAsync();

        public Task<SpeakerDb> GetSpeakerbyIdAsync([ID(nameof(SpeakerDb))] int id,
                                            [DataLoader] SpeakerByIdDataLoader dataloader,
                                            CancellationToken ct) =>
                                            dataloader.LoadAsync(id, ct);

        public async Task<IEnumerable<SpeakerDb>> GetSpeakersById([ID(nameof(SpeakerDb))] int[] ids,
                 SpeakerByIdDataLoader dataloader,
                CancellationToken ct) => await dataloader.LoadAsync(ids, ct);
    }
}
