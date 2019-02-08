# TodoApp

This is a simple to-do app.

In this project, I have used .NET Core v2.1 as back-end and Angular 7 as front-end. Sqlite is used to store data.

To run project:

Open a command line in TodoApp.API folder and run "dotnet run"

Open a command line in TodoAppSPA folder and run "npm install" and "ng serve" consecutively

Open "http://localhost:4200" in your browser

To see SonarQube test coverage results, in TodoApp folder run these commands consecutively: 

dotnet test TodoApp.TEST/TodoApp.TEST.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet build-server shutdown

dotnet sonarscanner begin /k:"TodoApp" /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths="TodoApp.TEST\coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs"

dotnet build

dotnet sonarscanner end
