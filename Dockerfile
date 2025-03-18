FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/FinanceApp.API/FinanceApp.API.csproj", "src/FinanceApp.API/"]
# COPY ["src/FinanceApp.Application/FinanceApp.Application.csproj", "src/FinanceApp.Application/"]
# COPY ["src/FinanceApp.Domain/FinanceApp.Domain.csproj", "src/FinanceApp.Domain/"]
# COPY ["src/FinanceApp.Infrastructure/FinanceApp.Infrastructure.csproj", "src/FinanceApp.Infrastructure/"]

RUN dotnet restore "src/FinanceApp.API/FinanceApp.API.csproj"

COPY . .
WORKDIR "/src/src/FinanceApp.API"
RUN dotnet build "FinanceApp.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "FinanceApp.API.csproj" -c Release -o /app/publish --no-build

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FinanceApp.API.dll"]