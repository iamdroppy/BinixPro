FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Binix.Api/Binix.Api.csproj", "Binix.Api/"]
RUN dotnet restore "Binix.Api/Binix.Api.csproj"
COPY . .
WORKDIR "/src/Binix.Api"
RUN dotnet build "Binix.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Binix.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Binix.Api.dll"]
