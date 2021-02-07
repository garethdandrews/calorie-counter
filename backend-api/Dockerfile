FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /backend-api

EXPOSE 80
EXPOSE 443

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /backend-api
COPY --from=build-env /backend-api/out .
ENTRYPOINT ["dotnet", "backend-api.dll"]
