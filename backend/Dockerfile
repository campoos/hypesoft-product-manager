# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# copia tudo
COPY . ./

# entra na pasta do projeto
WORKDIR /app/src/Hypesoft.API

# restore e build
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Hypesoft.API.dll"]