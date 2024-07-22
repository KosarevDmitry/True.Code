using Microsoft.EntityFrameworkCore;
using True.Code.ToDoListAPI.Models;

namespace True.Code.ToDoListAPI.Data;

public static class DbContextExtensions
{
    static readonly string db = "ToDoItemDb";

    public static void AddToDoItemDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoItemDbContext>(options =>
        {
#if UseSQLServer
            options.UseSqlServer(configuration[$"{db}:ConnectionString"]);
#elif UseMemoryDB
            options.UseInMemoryDatabase(db);

#elif UseSQLite
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, db);
            options.UseSqlite($"Data Source={DbPath}");
#endif
        });
    }

    public static void EnsureDbIsCreated(this IApplicationBuilder app, IConfiguration configuration)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ToDoItemDbContext>();
        context.Database.EnsureCreated();

        DbSettings settings = configuration.GetSection(db).Get<DbSettings>();
        if (settings.Init) context.Initialize();

        context.Database.CloseConnection();
    }
}