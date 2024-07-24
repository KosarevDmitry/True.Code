using System.Reflection;
using True.Code.ToDoListAPI;
using True.Code.ToDoListAPI.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.CreateSerilogLogger();


services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
    options.Filters.Add(typeof(ValidateModelStateFilter));
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
/* если подключать через option, выполнить
 ```dotnet user-secrets init
 dotnet user-secrets set ToDoItemDb:ConnectionString "Server=(localdb)\mssqllocaldb;Database=ToDoItemDb;Trusted_Connection=True"
 ```
 и добавить константу в csproj
*/
services.AddToDoItemDb(builder.Configuration);

services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
services.AddScoped<IPriorityRepository, PriorityRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddHealthChecks();
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseProducesOrSwaggerResponseCheck(); // response validator
}

app.UseSerilogRequestLogging();
app.UseCors("CorsPolicy");
app.EnsureDbIsCreated(builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }