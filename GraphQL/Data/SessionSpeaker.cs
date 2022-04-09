namespace ConferencePlanner.GraphQL.Data
{
    public class SessionSpeaker
    {
        public int SessionId { get; set; }

        public Session? Session { get; set; }

        public int SpeakerId { get; set; }

        public SpeakerDb? Speaker { get; set; }
    }
}