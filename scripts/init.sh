project=True.Code
api=$project.ToDoListApi
mkdir $project
cd ./$project
mkdir src
cd ./src
f=net6.0
dotnet new webapi -n  $api -f $f
dotnet new xunit -n $api.Tests -f $f
cd ./$api.Tests
dotnet  add reference ../${api}/${api}.csproj 

cd ../${api}/
mkdir Data && mkdir Services  && mkdir Models && mkdir Extensions

cd ../../

dotnet new sln -n $api
dotnet sln add ./src/${api}/${api}.csproj
dotnet sln add ./src/${api}.Tests/${api}.Tests.csproj

git init
dotnet new gitignore

explorer $api.sln


