namespace ConferencePlanner.GraphQL.Common
{
    public class UserError
    {

        public UserError(string message, string code)
        {
            Message = message;
            this.Code = code;
        }

        public string Code { get; }
        public string Message { get; }
    }
}
