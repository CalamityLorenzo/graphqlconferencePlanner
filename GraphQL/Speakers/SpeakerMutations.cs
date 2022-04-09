using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Extensions;

namespace ConferencePlanner.GraphQL.Speakers
{
    [ExtendObjectType("Mutation")]
    public class SpeakerMutations
    {
        // DI is on the method.
        // The way the di is handled for a particular argument can be mamnaged in the attributes
        // Notcing the scope is also an attribute.
        // this is a not good.
        [UseApplicationDbContext]
        public async Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input, [ScopedService] ApplicationDbContext ctx)
        {
            var speaker = new SpeakerDb
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
