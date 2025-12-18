{ pkgs ? import <nixpkgs> {} }:

let
  dotnet = pkgs.buildPackages.dotnet-sdk_10;
in

pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnet dotnet-ef sqlcmd ];
  DOTNET_ROOT="${dotnet}/share/dotnet/";
}

