{ pkgs ? import <nixpkgs> {} }:
pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_9_0 zed-editor ];
  
  DOTNET_ROOT = "/nix/store/xrbswlskla2qi3hqf2j2543k1qwq10l0-dotnet-sdk-9.0.100-rc.1.24452.12";
}
