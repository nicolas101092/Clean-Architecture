# Use .NET SDK 6.0 image to build the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Expose ports
EXPOSE 80
EXPOSE 443

# Copy project files and publlish the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
RUN dotnet publish -c Release -o /app/out

# Use .NET runtime 6.0 image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Run the application
ENTRYPOINT ["dotnet", "CleanArchitecture.API.dll"]