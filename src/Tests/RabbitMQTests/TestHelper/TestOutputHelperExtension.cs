using Xunit.Abstractions;

namespace True.Code.ToDoListAPI.Tests.TestHelper;

public static class TestOutputHelperExtension
{
    public static ITestOutputHelper? output;

    public static void Dump(this object o)=> output?.WriteLine($"{o}");
}
