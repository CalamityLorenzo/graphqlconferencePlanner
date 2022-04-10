using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Sessions
{
    public class SessionPayloadBase : Payload
    {
        public SessionPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        protected SessionPayloadBase(Session session)
        {
            Session = session;
        }

        public Session? Session { get; }
    }
}
