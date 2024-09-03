# Web API for creating, viewing, editing and deleting tasks, as well as managing related data.

## Implementation
- Apply concurrency code execution (async/await)
- Input data validation
- Error handling using  HttpGlobalExceptionFilter
- Task filtering by status (completed/not completed) and priority
 etc

## Requirement:
.NET 8 ASP.NET & EF
Database (any SQL)

## Run project

```bash
cd  ./src/True.Code.ToDoListAPI/
dotnet restore && dotnet build && dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

## Open swagger page

```bash
explorer https://localhost:7210/swagger
```

## Run tests

```bash
rm -rf reports && dotnet build && dotnet test --logger xunit --results-directory ./reports/
```


