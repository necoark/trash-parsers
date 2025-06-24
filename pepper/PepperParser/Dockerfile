FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PepperParser/PepperParser.csproj", "PepperParser/"]
RUN dotnet restore "PepperParser/PepperParser.csproj"
COPY . .
WORKDIR "/src/PepperParser"
RUN dotnet build "PepperParser.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PepperParser.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PepperParser.dll"]
