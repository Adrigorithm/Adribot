{ pkgs ? import <nixpkgs> {} }:

pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_9_0 rider dotnet-ef sqlcmd ];
}
