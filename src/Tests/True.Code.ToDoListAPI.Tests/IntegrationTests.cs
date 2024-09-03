namespace True.Code.ToDoListAPI.Tests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HttpClient Client { get; private set; }

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Client   = SetUpClient();
    }


    private HttpClient SetUpClient()
    {
        return _factory.WithWebHostBuilder(builder =>
            builder
                .ConfigureServices(services =>
                {
                    var context = new ToDoItemDbContext(new DbContextOptionsBuilder<ToDoItemDbContext>()
                        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoItemDb;Trusted_Connection=True")
                        .EnableSensitiveDataLogging()
                        .Options);

                    services.RemoveAll(typeof(ToDoItemDbContext)); // for any case
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in
                             context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                })).CreateClient();
    }


    private async Task SeedUser()
    {
        string urlbase     = "/api/v1/User";
        var    createForm0 = GenerateUserCreateForm("Hulio");
        var response0 = await Client.PostAsync(urlbase,
            new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

        var createForm1 = GenerateUserCreateForm("Lopes");
        var response1 = await Client.PostAsync(urlbase,
            new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));

        var createForm2 = GenerateUserCreateForm("Marianna");
        var response2 = await Client.PostAsync(urlbase,
            new StringContent(JsonConvert.SerializeObject(createForm2), Encoding.UTF8, "application/json"));

        var createForm3 = GenerateUserCreateForm("Dusha");
        var response3 = await Client.PostAsync(urlbase,
            new StringContent(JsonConvert.SerializeObject(createForm3), Encoding.UTF8, "application/json"));
    }

    private User GenerateUserCreateForm(string UserName)
    {
        return new User { Name = UserName, };
    }

    private ToDoItemAddCTO GenerateToDoItemAddCto(string title, int userId)
    {
        var toDoItemAddCto = new ToDoItemAddCTO()
        {
            Title       = title,
            Description = "Desc",
            IsCompleted = false,
            DueDate     = DateTime.Today,
            UserId      = userId,
            Level       = 1,
        };
        return toDoItemAddCto;
    }

    [Fact]
    public async Task AddTodoItem_Ok()
    {
        var form = GenerateToDoItemAddCto("Serg", 1);
        var response1 = await Client.PostAsync("/api/v1/ToDoItem",
            new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json"));
        Assert.Equal(StatusCodes.Status201Created, (int)response1.StatusCode);
    }

    [Fact]
    public async Task AddTodoItem_with_inValidUserId_500()
    {
        var form = GenerateToDoItemAddCto("Serg", 150);
        var response = await Client.PostAsync($"/api/v1/ToDoItem",
            new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json"));
        Assert.Equal((int?)response?.StatusCode, StatusCodes.Status500InternalServerError);
    }
}
