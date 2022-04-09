using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Speakers
{
    public class SpeakerPayloadBase : Payload
    {
        public SpeakerPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {

        }

        protected SpeakerPayloadBase(SpeakerDb speaker)
        {
            Speaker = speaker;
        }

        

        public SpeakerDb? Speaker { get; }
    }
}
