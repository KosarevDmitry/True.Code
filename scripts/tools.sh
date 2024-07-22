dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef


dotnet user-secrets init
dotnet user-secrets set ToDoItemDb:ConnectionString "Data Source=HOME-PC\MSSQLSERVER01;Initial Catalog=ToDoItemDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
dotnet user-secrets list 

dotnet ef migrations add InitialCreate
dotnet ef migrations  remove
dotnet ef database update 


dotnet ef database  drop
dotnet ef migrations add UpdateDueDate

dotnet ef migrations script --output efscript.sql



