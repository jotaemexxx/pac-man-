FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication1/PacMan.csproj", "WebApplication1/"]
RUN dotnet restore "WebApplication1/PacMan.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "PacMan.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PacMan.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PacMan.dll"]