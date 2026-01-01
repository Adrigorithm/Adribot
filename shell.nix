{ pkgs ? import <nixpkgs> {} }:

let
  dotnet = pkgs.buildPackages.dotnet-sdk_10;
in

pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnet dotnet-ef sqlcmd podman-compose nixd nil ];
  DOTNET_ROOT="${dotnet}/share/dotnet/";

  shellHook = 
  ''
    dotnet restore ./Adribot.App/Adribot.csproj 
  '';
}

