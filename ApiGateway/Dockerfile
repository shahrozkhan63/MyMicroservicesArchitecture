# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY ApiGateway.csproj ApiGateway/
RUN dotnet restore ApiGateway/ApiGateway.csproj

# Copy the entire project
COPY ApiGateway/ ApiGateway/

# Set the working directory for the build
WORKDIR /app/ApiGateway

# Build the application
RUN dotnet build -c Release -o out

# Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory for the runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/ApiGateway/out ./

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
