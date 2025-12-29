FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SimpleFinance.WebApi/SimpleFinance.WebApi.csproj", "src/SimpleFinance.WebApi/"]
RUN dotnet restore "src/SimpleFinance.WebApi/SimpleFinance.WebApi.csproj"
COPY . .
WORKDIR "/src/src/SimpleFinance.WebApi"
RUN dotnet build "./SimpleFinance.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SimpleFinance.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleFinance.WebApi.dll"]
