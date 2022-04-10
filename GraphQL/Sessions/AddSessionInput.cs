using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Sessions
{
    public record AddSessionInput(
        string Title,
        string? Abstract, 
        [ID(nameof(SpeakerDb))] IReadOnlyList<int> SpeakerIds);
}
