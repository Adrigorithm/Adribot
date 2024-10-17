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
