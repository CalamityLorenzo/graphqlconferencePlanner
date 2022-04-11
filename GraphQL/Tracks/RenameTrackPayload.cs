using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Tracks
{
    public class RenameTrackPayload : TracksPayloadBase
    {
        public RenameTrackPayload(Track track) : base(track)
        {
        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
