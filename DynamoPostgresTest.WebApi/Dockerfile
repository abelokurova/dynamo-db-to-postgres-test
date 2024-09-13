FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5267

ENV ASPNETCORE_URLS=http://+:5267

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DynamoPostgresTest.WebApi/DynamoPostgresTest.WebApi.csproj", "DynamoPostgresTest.WebApi/"]
COPY ["DynamoPostgresTest.Core/DynamoPostgresTest.Core.csproj", "DynamoPostgresTest.Core/"]
RUN dotnet restore "DynamoPostgresTest.WebApi/DynamoPostgresTest.WebApi.csproj"
COPY . .
WORKDIR "/src/DynamoPostgresTest.WebApi"
RUN dotnet build "DynamoPostgresTest.WebApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DynamoPostgresTest.WebApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamoPostgresTest.WebApi.dll"]
