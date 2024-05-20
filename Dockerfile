FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /Adribot.App

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /Adribot.App
COPY --from=build-env /Adribot.App/out .

# Start the app
ENTRYPOINT ["dotnet", "Adribot.dll"]
