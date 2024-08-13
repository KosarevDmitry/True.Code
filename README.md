 
##  Command list

- run:

```
dotnet restore
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet run
```

- test:
```
rm -rf reports && dotnet build && dotnet test --logger xunit --results-directory ./reports/
```

- logging in console
- Catch of Exception by HttpGlobalExceptionFilter
- automapper is not added for simplicity
