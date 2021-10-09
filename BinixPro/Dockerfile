FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BinixPro/BinixPro.csproj", "BinixPro/"]
RUN dotnet restore "BinixPro/BinixPro.csproj"
COPY . .
WORKDIR "/src/BinixPro"
RUN dotnet build "BinixPro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BinixPro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinixPro.dll"]
