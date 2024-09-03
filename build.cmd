cd  src\True.Code.ToDoListAPI\
dotnet restore 
dotnet build dotnet ef migrations add InitialCreate
dotnet ef database update
pause