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

## Development & Deployment

I recommend using the [Nix](https://nixos.org/download/) package manager (NixOS is lovely, but just Nix will do). If you
do not wish to use either, you can obviously still use this software, just reverse engineer the shell.nix file and you
will alright.

- Clone it
- `cd Adribot`
- `nix-shell` (OR download packages some other way)
- Create a bot account on the [discord developer platform](https://discord.com/developers/applications) and take note of
  the bot token (let's call it **bot_token**)
- Now do the same for the [cat api](https://thecatapi.com/). We will call this **cat_token**

To store the secrets you can put these in an `.env` file. This way podman-compose can find it. 
If you want to run it without podman - easier for development - you'll need to set these environment variables yourself using `export` (or with nix).
- For the `.env` file: `printf 'db_connection="User ID=SA;Password=YourSecretPassword123*;Database=Adribot;Server=172.18.0.2;Encrypt=False"\ndb_password=YourSecretPassword123\ncat_token=cat_token\nbot_token=bot_token\n' > .env`
- For development you can manually set the variables using `export`. In this case the keys have to be capitalised (CAT_TOKEN=cat_token, ...)

In the case of **Deployment** you are done, you can now run `podman-compose up --detach` to run the bot and database.

The application also requires the MSSQL database. To set it up you can use a container like so (or install it another way): 
- `sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourSecretPassword123*" -p 1433:1433 --name sqlserver_dev --hostname sqlserver_dev -d mcr.microsoft.com/mssql/server:2022-latest`

Check the database connection:
- `sudo docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' adribot_db_dev` if you use the container as a database, check output against the host you set for the connection string, they need to match.

Done? Wodnerful. You can now launch a development bot by running the dotnet application.

Oh and if you change domain entities that are to be stored in the database, you'll need to use the EF tool
- `dotnet-ef migrations add MigrationNameHere` (Whilst in **Adribot/Adribot.App**)

## Contributing

I'm not expecting any contributions but if you're interested, you're free to do so. The first thing you should do is
follow the steps in the `Development` section.

Wait where are all the branches? Yes that's right there's only one branch. Due to the above I will only adapt to more
branches if people will actually contribute - it's just more convenient this way -, so yes if commit your changes you
should use the feature branching pattern.

## FAQ

#### Can I create user bots with this?

No you cannot. It is against Discord TOS. Do not scam/spam/... or avoid paying for nitro, it's just morally/ethically
unacceptable.

#### Can we get this feature or bug fixed?

Probably, create an issue for it.

#### Do you like cats?

Yes.

## Todo

- [ ] Add automatic online ical sync
- [ ] Provide clearer error messages / feedback for end user (in progress)
- [ ] Add NMBS data access
- [ ] ~~Add DeLijn data access~~ (impossible without scraping)
- [ ] Pokémons! (in progress)

## Support

Please create an issue. If not applicable or your question is too specific you can contact me through discord. My pomelo
name is `adrigorithm`.

ps: Pomelo is a fruit 🍊
