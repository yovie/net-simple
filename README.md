# net-simple

## versions
- Dotnet SDK 6 (LTS)
- https://dotnet.microsoft.com/en-us/download/dotnet

## installation
`sudo apt-get update && sudo apt-get install -y dotnet-sdk-6.0`

## mssql server
`docker run --name mssql-server -e ACCEPT_EULA="Y" -e MSSQL_SA_PASSWORD="TheStrong\(\!\)Password1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

## simple
```
dotnet new sln -n Test-Backend
dotnet new webapi -n Test-Backend
dotnet sln Test-Backend.sln add Test-Backend/Test-Backend.csproj

// Program.cs -> comment 'app.UseHttpsRedirection();'
dotnet run --project Test-Backend

GET http://localhost:5183/weatherforecast
```