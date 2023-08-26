FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/NBPExchangeRates.WebApp/NBPExchangeRates.WebApp.csproj", "src/NBPExchangeRates.WebApp/"]
RUN dotnet restore "src/NBPExchangeRates.WebApp/NBPExchangeRates.WebApp.csproj"
COPY . .
WORKDIR "/src/src/NBPExchangeRates.WebApp"
RUN dotnet build "NBPExchangeRates.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NBPExchangeRates.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NBPExchangeRates.WebApp.dll"]
