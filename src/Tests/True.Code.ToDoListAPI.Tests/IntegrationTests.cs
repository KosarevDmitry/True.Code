using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Xunit;
using True.Code.ToDoListAPI.Models;
using True.Code.ToDoListAPI.Models.CTOs;
using True.Code.ToDoListAPI.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using True.Code.ToDoListAPI.Infrastructure.Filters;
using True.Code.ToDoListAPI.Infrastructure.Exceptions;
using True.Code.ToDoListAPI.Infrastructure.Repositories;
using True.Code.ToDoListAPI.Infrastructure.ActionResults;
using True.Code.ToDoListAPI.Extensions;
using System.Net;

namespace True.Code.ToDoListAPI.Tests;

    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            SetUpClient();// инициализирует HttpClient с помощью  _factory
        }

		private void SetUpClient()
        {
            Client = _factory.WithWebHostBuilder(builder =>
                builder.UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    var context = new TestProjectContext(new DbContextOptionsBuilder<TestProjectContext>()
                        .UseSqlite("DataSource=:memory:")
                        .EnableSensitiveDataLogging()
                        .Options);

                    services.RemoveAll(typeof(TestProjectContext));
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                })).CreateClient();
        }
		
		// отправляет постом 4 модели библиотеки
        private async Task SeedLibraries()
        {
            var createForm0 = GenerateLibraryCreateForm("Library Name 1");
            var response0 = await Client.PostAsync("/api/libraries", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            var createForm1 = GenerateLibraryCreateForm("Library Name 2");
            var response1 = await Client.PostAsync("/api/libraries", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));

            var createForm2 = GenerateLibraryCreateForm("Library Name 3");
            var response2 = await Client.PostAsync("/api/libraries", new StringContent(JsonConvert.SerializeObject(createForm2), Encoding.UTF8, "application/json"));

            var createForm3 = GenerateLibraryCreateForm("Library Name 4");
            var response3 = await Client.PostAsync("/api/libraries", new StringContent(JsonConvert.SerializeObject(createForm3), Encoding.UTF8, "application/json"));
        }
		
		  private LibraryForm GenerateLibraryCreateForm(string libraryName)
        {
            return new LibraryForm
            {
                Name = libraryName,
            };
        }
		
       // отправляет постом одну модель книги
        public async Task SeedBook(string bookName, int libraryId)
        {
            var bookForm = new BookForm
            {
                Name = bookName,
                LibraryId = libraryId
            };
            var response1 = await Client.PostAsync($"/api/libraries/{libraryId}/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));
        }

      

        // TEST NAME - addBookToLibrary
        // TEST DESCRIPTION - It adds book to a library
        [Fact]
        public async Task TestAddBook_Ok_GetBook_NotFound()
        {
            await SeedLibraries();

            var bookForm = new BookForm
            {
                Name = "Test book 1",
                LibraryId = 1
            };

            var response1 = await Client.PostAsync($"/api/libraries/1/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));

            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status201Created);

            bookForm = new BookForm
            {
                Name = "Test book 2",
                LibraryId = 100
            };

            var response2 = await Client.PostAsync($"/api/libraries/100/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));

            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }

        // TEST NAME - getBooksInALibrary
        // TEST DESCRIPTION - It finds all books in a library by ID
        [Fact]
        public async Task TestGetBooks_Ok_NotFound()
        {
            await SeedLibraries();

            await SeedBook("test book 1", 1);
            await SeedBook("test book 2", 1);

            var response1 = await Client.GetAsync($"/api/libraries/2/books");
            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(response1.Content.ReadAsStringAsync().Result).ToList();
            books.Count.Should().Be(0);
            
            var response2 = await Client.GetAsync($"/api/libraries/1/books");
            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            var books2 = JsonConvert.DeserializeObject<IEnumerable<Book>>(response2.Content.ReadAsStringAsync().Result).ToList();
            books2.Count.Should().Be(2);

            var response3 = await Client.GetAsync($"/api/libraries/31232/books");
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }

        // TEST NAME - deleteLibraryById
        // TEST DESCRIPTION - Check delete library web api end point
        [Fact]
        public async Task TestDeleteLibrary()
        {
            await SeedLibraries();

            var response0 = await Client.DeleteAsync("/api/libraries/1");
            response0.StatusCode.Should().BeEquivalentTo(StatusCodes.Status204NoContent);

            // Verify that delete is successful
            var response1 = await Client.GetAsync("/api/libraries/1/books");
            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);

            var response2 = await Client.DeleteAsync("/api/libraries/1");
            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }

       
    }

