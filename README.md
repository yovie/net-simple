# net-simple

## versions
- Ubuntu 22.10
- Dotnet SDK 6 (LTS)
- https://dotnet.microsoft.com/en-us/download/dotnet

## packages
https://www.nuget.org/package

## installation
`sudo apt-get update && sudo apt-get install -y dotnet-sdk-6.0`

## mssql server
`docker run --name mssql-server -e ACCEPT_EULA="Y" -e MSSQL_SA_PASSWORD="TheStrong\(\!\)Password1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

## command
```
dotnet list packages
dotnet tool list
```

## nx
```
npm install --global nx@latest  // we got nx command

npm create nx-workspace // minimum with ts/js config
// enter the workspace
npm install --save-dev @nx-dotnet/core
nx g @nx-dotnet/core:app satu-app

nx build satu-app
nx serve satu-app
// one workspace can be multiple application (backend service / frontend)
```

## simple
```
dotnet new sln -n Test-Backend
dotnet new webapi -n Test-Backend
dotnet sln Test-Backend.sln add Test-Backend/Test-Backend.csproj

// Program.cs -> comment 'app.UseHttpsRedirection();'
dotnet run --project Test-Backend

GET http://localhost:5183/weatherforecast
```
## nv-template
```
dotnet new -i NV.Templates
dotnet new nv-backend -n NVTemplate -C Nodev --RestApi --Auth
dotnet build --project NVTemplate

// some error found
dotnet add package NSwag.Core --version 13.19.0
// or add manually to *csproj
    <PackageReference Include="NSwag.Core" Version="13.19.0" />

// do, on each project
dotnet build
```