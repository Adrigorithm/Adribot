{ pkgs ? import <nixpkgs> {} }:

let
  lib = pkgs.lib;
  vscodeExtensions = [
    "ms-dotnettools.csdevkit"
  ];
in
pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_8_0_2xx dotnetCorePackages.sdk_9_0 ];
  
  shellHook = ''
    for ext in ${lib.concatStringsSep " " vscodeExtensions}; do
      code --install-extension $ext || true
    done
  '';
}
