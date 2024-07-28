FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0-preview-noble AS build

ARG DB_CONNECTION_FILE

ENV DB_CONNECTION=${DB_CONNECTION_FILE}

WORKDIR /home/ubuntu/app

COPY Migrations/ src/ temp/ Adribot.csproj Program.cs /home/ubuntu/app/

RUN dotnet tool install --global dotnet-ef --version 9.0.0-preview.6.24327.4

# Apparently dotnet installation doesn't do this by default, surprisingly
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet restore
RUN dotnet ef database update
RUN dotnet publish -c Release -o release

FROM mcr.microsoft.com/dotnet/nightly/runtime:9.0-preview-noble

WORKDIR /home/ubuntu/app

COPY --from=build /home/ubuntu/app/release .

ENTRYPOINT ["dotnet", "Adribot.dll"]
