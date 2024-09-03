using Xunit.Abstractions;

namespace True.Code.ToDoListAPI.Tests.TestHelper;

public class TestBase
{
    protected readonly ITestOutputHelper Output;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        Output                           = testOutputHelper;
        TestOutputHelperExtension.output = testOutputHelper;
    }
}
