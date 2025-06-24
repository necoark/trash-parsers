FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProbnickRuParser/ProbnickRuParser.csproj", "ProbnickRuParser/"]
RUN dotnet restore "ProbnickRuParser/ProbnickRuParser.csproj"
COPY . .
WORKDIR "/src/ProbnickRuParser"
RUN dotnet build "ProbnickRuParser.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProbnickRuParser.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProbnickRuParser.dll"]
