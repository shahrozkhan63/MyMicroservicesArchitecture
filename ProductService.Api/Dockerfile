# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ["ProductService/ProductService.Api/ProductService.Api.csproj", "ProductService/ProductService.Api/"]
RUN dotnet restore "ProductService/ProductService.Api/ProductService.Api.csproj"

# Copy the rest of the application source code
COPY . .
WORKDIR "/app/ProductService/ProductService.Api"
RUN dotnet build "ProductService.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage for the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
