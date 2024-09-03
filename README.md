# Web API for creating, viewing, editing and deleting tasks, as well as managing related data.

## Implementation
- Apply concurrency code execution (async/await)
- Input data validation
- Error handling using  global exception filter
- Task filtering by status (completed/not completed) and priority
 etc
I added a `RabbitMQ` tests as example of a parallel code execution and  thread synchronization using CountdownEvent  class.  
It is assumed that Rabbitmq instance will be running when tests start.
  
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
```

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


