{ pkgs ? import <nixpkgs> {} }:
  pkgs.mkShell {
    nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_9_0 dotnetCorePackages.sdk_8_0_2xx ];
}