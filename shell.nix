{ pkgs ? import <nixpkgs> {} }:

pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_9_0 dotnet-ef sqlcmd jetbrains.rider ];
}
