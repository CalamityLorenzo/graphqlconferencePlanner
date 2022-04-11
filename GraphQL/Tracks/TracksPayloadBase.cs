using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Tracks
{
    public class TracksPayloadBase : Payload
    {
        public TracksPayloadBase(Track track) => this.Track = track;

        public TracksPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Track? Track { get; }


    }
}
