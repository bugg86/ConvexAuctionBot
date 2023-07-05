FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ConvexAuctionBot/ConvexAuctionBot.csproj", "ConvexAuctionBot/"]
#COPY ["ConvexAuctionBot/token", "ConvexAuctionBot/"]
RUN dotnet restore "ConvexAuctionBot/ConvexAuctionBot.csproj"
COPY . .
WORKDIR "/src/ConvexAuctionBot"
RUN dotnet build "ConvexAuctionBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConvexAuctionBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConvexAuctionBot.dll"]
