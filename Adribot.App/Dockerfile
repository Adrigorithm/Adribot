FROM mcr.microsoft.com/dotnet/nightly/sdk:10.0-preview-alpine-amd64 AS build

WORKDIR /home/adri/adribot

COPY Migrations/ src/ temp/ Adribot.csproj Program.cs ./

RUN dotnet new tool-manifest && dotnet tool install dotnet-ef
RUN dotnet tool run dotnet-ef migrations add AdribotDbScaffold
RUN dotnet restore
RUN dotnet publish -c Release -o release

FROM mcr.microsoft.com/dotnet/nightly/runtime:10.0-preview-alpine-amd64

RUN apk add icu-libs

WORKDIR /home/adri/adribot

COPY --from=build /home/adri/adribot/release .

ENTRYPOINT ["dotnet", "Adribot.dll"]
