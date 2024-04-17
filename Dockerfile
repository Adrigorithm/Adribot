# Use the .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/core/sdk AS build-env
WORKDIR /Adribot.App

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/core/runtime
WORKDIR /Adribot.App
COPY --from=build-env /app/out ./

# Set the entry point for the container
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
