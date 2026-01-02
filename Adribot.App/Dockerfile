FROM alpine:latest

WORKDIR /home/adri/adribot

COPY Migrations/ src/ temp/ Adribot.csproj Program.cs ./

RUN apk add dotnet10-sdk 
RUN dotnet restore
RUN dotnet tool install dotnet-ef
RUN dotnet tool run dotnet-ef migrations add AdribotDbScaffold
RUN dotnet publish -c Release -o release

ENTRYPOINT ["dotnet", "release/Adribot.dll"]
