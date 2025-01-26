# Adribot

A feature rich general purpose discord bot written in C# using a discord api wrapper.

## Features

This list showcases a few of the more **visual** features and does in no way cover everything this bot does.

### Fetch pictures of your favourite animals (currently supports cat, fox and dog)!
![api floof](https://github.com/Adrigorithm/Adribot/assets/12832161/5e09df1a-19fd-4453-a8ba-f40b9d13a341)

### Translate discord emoji's into an emoji format understandable by Minecraft
![emojiful datapack](https://github.com/Adrigorithm/Adribot/assets/12832161/6428781c-eef9-442a-abca-9a2d7051cdca)

### Schedules input from a calendar and notifies you beforehand (supports a specific format only for now because my University creates horrendous calendar files - everything in one string with newlines)
![schedule](https://github.com/Adrigorithm/Adribot/assets/12832161/69856c2d-b4f0-4578-8d79-31bc4fd9947d)

## Acknowledgements

 - [Discord.Net Library](https://github.com/discord-net/Discord.Net)
 - [Discord API](https://discord.com/developers/docs/intro)

## Development
I recommend using the [Nix](https://nixos.org/download/) package manager (NixOS is lovely, but just Nix will do). If you do not wish to use either, you can obviously still use this software, just reverse engineer the shell.nix file and you will alright.

- Clone it
- `cd Adribot`
- `nix-shell` (edit the shell.nix file if you want a custom configurtion (VSCodium instead of Rider for example))
- `mkdir Secrets`
- `cd Secrets`
- Create a bot account on the [discord developer platform](https://discord.com/developers/applications) and take note of the bot token (let's call it **bot_token**)
- Now do the same for the [cat api](https://thecatapi.com/). We will call this **cat_token**

Now inside the **Secrets** directory, create 4 plain text files with just the secrets inside:
- `echo "User ID=SA;Password=YourSecretPassword123*;Database=Adribot;Server=172.18.0.2;Encrypt=False" > db_connection.txt`
- `echo YourSecretPassword123 > db_password.txt`
- `echo cat_token > cat_token.txt`
- `echo bot_token > bot_token.txt`
- `sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourSecretPassword123*" -p 1433:1433 --name sqlserver_dev --hostname sqlserver_dev -d mcr.microsoft.com/mssql/server:2022-latest` You can use other methods to install the database, but you need to make sure you can connect to it. I use docker here because it is convenient.
- `cd ..`
- `rider &>/dev/null &` (or start whatever IDE or editor you prefer)

To verify the setup is done properly, we can do a few things:
- `sudo docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' adribot_db_dev` if you use docker, check output against the host you set for the connection string, they need to match.
- Run (obviously)

Oh and if you change domain entities that are to be stored in the database, you'll need to use the EF tool
- `dotnet-ef migrations add MigrationNameHere` (Whilst in **Adribot/Adribot.App**)

## Deployment

- Clone it
- `cd Adribot`
- `mkdir Secrets`
- `cd Secrets`
- Create a bot account on the [discord developer platform](https://discord.com/developers/applications) and take note of the bot token (let's call it **bot_token**)
- Now do the same for the [cat api](https://thecatapi.com/). We will call this **cat_token**

Now inside the **Secrets** directory, create 4 plain text files with just the secrets inside:
- `echo "User ID=SA;Password=YourSecretPassword123*;Database=Adribot;Server=adribot_db;Encrypt=False" > db_connection.txt`
If you are going to use this in production **PLEASE** do not do this. Instead, make sure to enable encryption by getting a valid certificate. Also refrain from using the SA user.
- `echo YourSecretPassword123 > db_password.txt`
- `echo cat_token > cat_token.txt`
- `echo bot_token > bot_token.txt`
Now it is time to build the app. Make sure you have docker installed.
- `docker compose up`

secrets:
  db_password:
    file: ./Secrets/db_password.txt
  db_connection:
    file: ./Secrets/db_connection.txt
  cat_token:
    file: ./Secrets/cat_token.txt
  bot_token:
    file: ./Secrets/bot_token.txt

## Contributing

I'm not expecting any contributions but if you're interested, you're free to do so. The first thing you should do is follow the steps in the `Deploy` section.

Wait where are all the branches? Yes that's right there's only one branch. Due to the above I will only adapt to more branches if people will actually contribute - it's just more convenient this way -, so yes if commit your changes you should use the feature branching pattern.

## FAQ

#### Can I create user bots with this?

No you cannot. It is against Discord TOS. Do not scam/spam/... or avoid paying for nitro, it's just morally/ethically unacceptable.

#### Can we get this feature or bug fixed?

Probably, create an issue for it.

#### Do you like cats?

Yes.

## Todo

- [ ] Add automatic online ical sync
- [ ] Provide clearer error messages / feedback for end user (in progress)
- [ ] Add NMBS data access
- [ ] ~~Add DeLijn data access~~ (impossible without scraping)

## Support

Please create an issue. If not applicable or your question is too specific you can contact me through discord. My pomelo name is `adrigorithm`.

ps: Pomelo is a fruit 🍊
