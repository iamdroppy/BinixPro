FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Binix.Web/Binix.Web.csproj", "Binix.Web/"]
RUN dotnet restore "Binix.Web/Binix.Web.csproj"
COPY . .
WORKDIR "/src/Binix.Web"
RUN dotnet build "Binix.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Binix.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Binix.Web.dll"]
