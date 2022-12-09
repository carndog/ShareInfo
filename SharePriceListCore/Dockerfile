FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SharePriceListCore/SharePriceListCore.csproj", "SharePriceListCore/"]
RUN dotnet restore "SharePriceListCore/SharePriceListCore.csproj"
COPY . .
WORKDIR "/src/SharePriceListCore"
RUN dotnet build "SharePriceListCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharePriceListCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharePriceListCore.dll"]
