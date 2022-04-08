using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Extensions;

namespace ConferencePlanner.GraphQL
{
    public class Mutation
    {
        [UseApplicationDbContext]
        public async Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input, [ScopedService] ApplicationDbContext ctx)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite,

            };
            ctx.Speakers.Add(speaker);
            await ctx.SaveChangesAsync();
            return new AddSpeakerPayload(speaker);
        }
    }
}
