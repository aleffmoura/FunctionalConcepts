namespace FunctionalConcepts.Tests.Common;
public record ExampleTest
{
    public ExampleTest()
    {
        Msg = string.Empty;
    }

    public ExampleTest(string msg)
    {
        Msg = msg;
    }

    public string Msg { get; init; }
}
