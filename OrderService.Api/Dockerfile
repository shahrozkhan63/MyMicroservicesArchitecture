# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore any dependencies
COPY OrderService.Api/OrderService.Api.csproj OrderService.Api/
#COPY OrderService.Api/OrderService.Infrastructure/OrderService.Infrastructure.csproj OrderService.Api/OrderService.Infrastructure/
RUN dotnet restore OrderService.Api/OrderService.Api.csproj

# Copy everything else and build
COPY . .  # This copies the entire context; ensure needed files are included
WORKDIR /app/OrderService.Api
RUN dotnet build -c Release -o out

# Use the ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/OrderService.Api/out ./
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]
