#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY [".editorconfig", "/"]
COPY ["Directory.Build.props", "/"]
COPY ["stylecop.json", "/"]
WORKDIR /src
COPY ["src/Host/WebAPI/WebAPI.csproj", "src/Host/WebAPI/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Contracts/Contracts.csproj", "src/Core/Contracts/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Shared/Shared.csproj", "src/Core/Shared/"]
COPY ["src/Migrators/Migrators.PostgreSQL/Migrators.PostgreSQL.csproj", "src/Migrators/Migrators.PostgreSQL/"]
RUN dotnet restore "src/Host/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Host/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amina.WebAPI.dll"]