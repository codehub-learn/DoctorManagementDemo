# DoctorManagement





Running the SQL Server container:


docker run -e "ACCEPT_EULA=Y" ^
           -e "MSSQL_SA_PASSWORD=P@ssw0rd!@#$" ^
           -p 1433:1433 ^
           --name sqlserver2022 ^
           -v mssql_data:/var/opt/mssql ^
           -d mcr.microsoft.com/mssql/server:2022-latest


Running the RabbitMQ container:

docker run -d ^
  --hostname rabbit-host ^
  --name rabbitmq ^
  -p 5672:5672 ^
  -p 15672:15672 ^
  -e RABBITMQ_DEFAULT_USER=admin ^
  -e RABBITMQ_DEFAULT_PASS=admin123 ^
  rabbitmq:3-management

  Default credentials at port 15672  guest guest