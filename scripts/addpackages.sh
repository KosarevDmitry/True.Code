#api
ver=7.0.20

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version  $ver
dotnet add package Microsoft.EntityFrameworkCore.Design --version  $ver
dotnet add package Microsoft.EntityFrameworkCore.Proxies --version  $ver
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version  $ver
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version $ver
dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet --version  2.0.3
dotnet add package Microsoft.Extensions.Configuration.UserSecrets
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Enrichers.Environment
dotnet add package  Serilog.Settings.Configuration
dotnet add package  Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Settings.Configuration
dotnet add package Swashbuckle.AspNetCore.Annotations




#test

	dotnet add package  FluentAssertions
	dotnet add package  Microsoft.AspNetCore.Mvc.Testing --version  6.0.32
	dotnet add package  Microsoft.EntityFrameworkCore.InMemory --version  $ver
	dotnet add package  Microsoft.EntityFrameworkCore.Tools --version  $ver
	dotnet add package  Microsoft.NET.Test.Sdk
	dotnet add package  XunitXml.TestLogger