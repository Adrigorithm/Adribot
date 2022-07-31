using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

class MinecraftCommands : ApplicationCommandModule
{
    private const string DatapackPath = "./temp/";
    private string _lastDatapackFile = "";

    [SlashCommand("Datapack", "Compiles Emojiful datapacks from supplied DiscordEmoji")]
    public async Task CreateDatapack(InteractionContext ctx, [Option("Category", "A name to categorise this emoji collection")] string category, [Option("Emojis", "A chain of DiscordEmoji")] string emojiList)
    {
        var emojiMatches = Regex.Matches(emojiList, "(<a?):(\\w+):(\\d{18})>");
        if (emojiMatches.Count > 0)
        {
            if(!String.IsNullOrEmpty(_lastDatapackFile)) File.Delete(DatapackPath + _lastDatapackFile);
            Directory.Delete(DatapackPath + $"datapack/data/emojiful/recipes/", true);
            Directory.CreateDirectory(DatapackPath + $"datapack/data/emojiful/recipes/");

            for (int i = 0; i < emojiMatches.Count; i++)
            {
                var emoji = new EmojifulEmoji
                {
                    Category = category,
                    Name = emojiMatches[i].Groups[2].Value,
                    Url = emojiMatches[i].Groups[1].Value.Contains('a') ? "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".gif" : "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".png",
                    Type = "emojiful:emoji_recipe"
                };

                FileStream fs = File.Create(DatapackPath + $"datapack/data/emojiful/recipes/{emojiMatches[i].Groups[2].Value}.json");
                await JsonSerializer.SerializeAsync(fs, emoji);
                await fs.DisposeAsync();
            }
            _lastDatapackFile = $"{ctx.User.Username}-" + category + "-emojiful-datapack.zip";
            ZipFile.CreateFromDirectory(DatapackPath + "datapack/", DatapackPath + _lastDatapackFile);

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddFile(_lastDatapackFile, File.OpenRead(DatapackPath + _lastDatapackFile)));
        }
    }
}