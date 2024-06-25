# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /src

# Install Node.js and npm
RUN apt-get update -yq && \
    apt-get install -yq curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -yq nodejs
    
# Copy csproj and restore as distinct layers
COPY ["SimpleCrm.WebApi/SimpleCrm.WebApi.csproj", "SimpleCrm.WebApi/"]
COPY ["SimpleCrm.SqlDbServices/SimpleCrm.SqlDbServices.csproj", "SimpleCrm.SqlDbServices/"]
COPY ["SimpleCrm/SimpleCrm.csproj", "SimpleCrm/"]
RUN dotnet restore "SimpleCrm.WebApi/SimpleCrm.WebApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/SimpleCrm.WebApi"
RUN dotnet build "SimpleCrm.WebApi.csproj" -c Release -o /app/build

# Build the Angular application
WORKDIR /src/simple-crm-cli
RUN npm install

RUN npm run build

# Publish the application
FROM builder AS publish
WORKDIR /src/SimpleCrm.WebApi
RUN dotnet publish "SimpleCrm.WebApi.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/simple-crm-cli/dist/simple-crm-cli /app/ClientApp/dist
ENTRYPOINT ["dotnet", "SimpleCrm.WebApi.dll"]
