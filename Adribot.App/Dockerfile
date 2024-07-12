FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY src/ temp/ Adribot.csproj Program.cs /app/

RUN dotnet restore
RUN dotnet publish -c Release -o release

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/release .

ENTRYPOINT ["dotnet", "Adribot.App.dll"]
