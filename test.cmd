rm -rf reports 
dotnet build 
dotnet test --logger xunit --results-directory .\reports\
pause