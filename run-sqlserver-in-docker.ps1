#pull image
docker pull mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04

#run container
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SecurePassword_1" `
   -p 1433:1433 --name sql1 `
   -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
   
#install Entity Framework tools (will do nothing if already installed)
dotnet tool install -g dotnet-ef

#Run migrations against DB
dotnet ef database update