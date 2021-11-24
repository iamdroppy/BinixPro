#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BinixPro.Node/BinixPro.Node.csproj", "BinixPro.Node/"]
RUN dotnet restore "BinixPro.Node/BinixPro.Node.csproj"
COPY . .
WORKDIR "/src/BinixPro.Node"
RUN dotnet build "BinixPro.Node.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BinixPro.Node.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinixPro.Node.dll"]