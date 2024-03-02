
# Adribot

A feature rich general purpose discord bot written in C# using a discord api wrapper.




## Acknowledgements

 - [D#+ Library](https://github.com/DSharpPlus/DSharpPlus)
 - [Discord API](https://discord.com/developers/docs/intro)


## Deployment

There are currently no official releases. In order to deploy the bot you will need to build the solution yourself following these steps (follow these carefully unless you know exactly what you're doing).

- Clone the project
- Create `Adribot/secret/config.json` (it should look something like this)

    ```json
    {
        "botToken": "getThisFromDiscordDevPortalIAmRandom3489407",
        "catToken": "getThisFromTheCatApiDeezNuts38907432",
        "sqlConnectionString": "Server=YourMomsBasement;Database=VisitorLogs;User Id=YourMom;Password=AVerySecretPassword",
        "embedColour": "#191970",
        "devUserId": 123456789010111213
    }
    ```
    The embedColour is used to colourise the left border of embeds generated by the bot.
    
    devUserId is your discord user Id (This is used so the developer can perform certain admin functions (for testing) without having the required permissions)

- Get all the required dependencies (run `dotnet restore` if not done automatically)
- Create the database model using EF Core: `dotnet ef migrations add "InitialCreate"`
- Creete the actual database (make sure the database can be reached using the above connectionString): `dotnet ef database update`
- Provided everything went well you should now be able to build/run the bot. Since this is a dotnet core console app, you can run this on any OS (containers word fine too), just make sure you have a .net7 capable runtime and a mssql database)
## Contributing

I'm not expecting any contributions but if you're interested, you're free to do so. The first thing you should do is follow the steps in the `Deploy` section.

Wait where are all the branches? Yes that's right there's only one branch. Due to the above I will only adapt to more branches if people will actually contribute - it's just more convenient this way -, so yes if commit your changes you should use the feature branching pattern.
## FAQ

#### Can I create user bots with this?

No you cannot. It is against Discord TOS. Do not scam/spam/... or avoid paying for nitro, it's just morally/ethically unacceptable.

#### Can we get this feature or bug fixed?

Probably, create an issue for it.

### Todo
- [x] Check for chached guilds on Tag operations
- [ ] Add automatic online ical sync
- [ ] Provide clearer error messages / feedback for end user
- [ ] Unit testing
- [ ] Manually fully test all services

#### Do you like cats?

Yes.
## Support

Please craete an issue. If not applicable or your question is too specific you can contact me through discord. My pomelo name is `adrigorithm`.

ps: Pomelo is a fruit 🍊
