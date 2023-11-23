FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .

# Set the ASP.NET Core environment variable
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port
EXPOSE 80

# Run the application
CMD ["dotnet", "BackEnd.dll"]
