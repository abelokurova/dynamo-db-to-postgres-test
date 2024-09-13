FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DynamoPostgresTest.Job/DynamoPostgresTest.Job.csproj", "DynamoPostgresTest.Job/"]
RUN dotnet restore "DynamoPostgresTest.Job/DynamoPostgresTest.Job.csproj"
COPY . .
WORKDIR "/src/DynamoPostgresTest.Job"
RUN dotnet build "DynamoPostgresTest.Job.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DynamoPostgresTest.Job.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamoPostgresTest.Job.dll"]
