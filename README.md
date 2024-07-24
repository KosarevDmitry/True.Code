 
## Список команд для запуска проекта

- run
- 
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

- логирование в консоль
- перехват ошибок в фильтре HttpGlobalExceptionFilter
- automapper не добавлял
