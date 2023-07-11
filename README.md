# net-simple

## installation
`sudo apt-get update && sudo apt-get install -y dotnet-sdk-7.0`

## mssql server
`docker run --name mssql-server -e ACCEPT_EULA="Y" -e MSSQL_SA_PASSWORD="TheStrong\(\!\)Password1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

