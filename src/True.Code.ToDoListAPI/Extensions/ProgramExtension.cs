using Serilog;

namespace True.Code.ToDoListAPI.Extensions
{
    public static class ProgramExtension
    {
        public static void CreateSerilogLogger(this WebApplicationBuilder builder)
        {
            const string appName = "ToDoListAPI";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
               // .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}