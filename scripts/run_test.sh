rm -rf reports && dotnet build && dotnet test --logger xunit --results-directory ./reports/

dotnet test  --results-directory ./reports/