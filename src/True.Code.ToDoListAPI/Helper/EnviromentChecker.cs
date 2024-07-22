namespace True.Code.ToDoListAPI.Helper;

public class EnviromentChecker
{
    public static bool IsDebugEnviroment =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
}