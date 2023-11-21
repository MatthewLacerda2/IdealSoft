# IdealSoft
A Full .NET Core Solution with WPF as the Front-End for CRUDing Users

To run this you`ll need .NET Core 8.0 installed, as well as the Docker Engine up and running

From Windows` cmd, run these commands:

docker pull mysql
docker run -d --name mysql-container -e MYSQL_ROOT_PASSWORD=my-secret-pw -p 3306:3306 mysql

Than, from the BackEnd folder, run these commands to get the API up and running:

dotnet restore
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet build
dotnet run

Finally, from the FrontEnd folder, run these commands to use the WPF app:

cd ..\frontend\
dotnet restore
dotnet build
dotnet run

You can also use Swagger via the url
http://localhost:5293/swagger/index.html
When the API is running, that is