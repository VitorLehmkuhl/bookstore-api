# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore BookStore.Api/BookStore.Api.csproj
RUN dotnet publish BookStore.Api/BookStore.Api.csproj -c Release -o /app/publish

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BookStore.Api.dll"]