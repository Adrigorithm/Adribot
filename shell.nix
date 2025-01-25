{ pkgs ? import <nixpkgs> {} }:

pkgs.mkShell {
  nativeBuildInputs = with pkgs.buildPackages; [ dotnetCorePackages.sdk_9_0 dotnet-ef sqlcmd jetbrains.rider ];

  BOT_TOKEN="../../../../Secrets/bot_token.txt";
  CAT_TOKEN="../../../../Secrets/cat_token.txt";
  DB_CONNECTION="../../../../Secrets/db_connection.txt";
  DB_PASSWORD="../../../../Secrets/db_password.txt";
  DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false;
  DEV_ID=135081249017430016;
  DISCORD_EMBED_COLOUR="FF191970";
}
