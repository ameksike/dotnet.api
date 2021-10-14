# Demo Project


## Description 


## Install steps
- git clone ...
- dotnet build
- dotnet ef database update --project priberam
- dotnet run --project priberam
- dotnet test 

## Develop steps
- dotnet --version
- dotnet new sln -o api
- cd ./api
- dotnet new tool-manifest

### Develop .Net Core Command
- dotnet new --list
- dotnet new webapi --name priberam 
- dotnet sln add ./priberam/priberam.csproj
- cd ./priberam
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.AspNetCore.Identity
- dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
- dotnet add package System.IdentityModel.Tokens.Jwt
- dotnet add package Swashbuckle.AspNetCore

### Entity Framework Core CLI Command
- dotnet tool install --global dotnet-ef
- dotnet ef migrations add CreateDatabase --project priberam    
- dotnet ef database update --project priberam  

### Test
- dotnet new xunit -o priberam.test
- dotnet sln add ./priberam.test/priberam.test.csproj
- dotnet add ./priberam.test/priberam.test.csproj reference ./priberam/priberam.csproj  
- cd ./priberam.test/
- dotnet add package Microsoft.AspNetCore.Mvc.Testing
- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Microsoft.AspNetCore.Hosting
- dotnet add package Microsoft.EntityFrameworkCore.InMemory
- dotnet restore
- dotnet test 

### SQL LocalDB Command
- SqlLocalDB versions
- SqlLocalDB info
- SqlLocalDB info "MSSQLLocalDB"
- SqlLocalDB start "MSSQLLocalDB"

### Heroku
- https://elements.heroku.com/buildpacks/jincod/dotnetcore-buildpack
