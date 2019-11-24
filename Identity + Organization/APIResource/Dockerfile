FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["APIResource/APIResource.csproj", "APIResource/"]
RUN dotnet restore "APIResource/APIResource.csproj"
COPY . .
WORKDIR "/src/APIResource"
RUN dotnet build "APIResource.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "APIResource.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "APIResource.dll"]