using ConferencePlanner.GraphQL.Extensions;

namespace ConferencePlanner.GraphQL.Data
{
    public class FakeClasss
    {

        [UseUpperCase]
        public string FakeName { get; set; } = "";
        public int FakeId { get; set; }
    }
}
