using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Adribot.src.constants.strings;
using Adribot.src.entities.minecraft;
using Discord;
using Discord.Interactions;

namespace Adribot.src.commands.utilities;

public class MinecraftCommands : InteractionModuleBase
{
    private const string DatapackPath = "./temp/";

    [SlashCommand("datapack", "Compiles Emojiful datapacks from supplied DiscordEmoji")]
    public async Task CreateDatapackAsync(InteractionContext ctx, [Summary("category", "A name to categorise this emoji collection")] string category, [Summary("emojis", "A chain of DiscordEmoji")] string emojiList)
    {
        MatchCollection emojiMatches = Regex.Matches(emojiList, ConstantStrings.EmojiRegex);

        if (emojiMatches.Count > 0)
        {
            foreach (var filePath in Directory.GetFiles(DatapackPath, "*?.zip"))
                File.Delete(filePath);

            Directory.Delete(DatapackPath + "datapack/data/emojiful/recipes/", true);
            Directory.CreateDirectory(DatapackPath + "datapack/data/emojiful/recipes/");

            for (var i = 0; i < emojiMatches.Count; i++)
            {
                var emoji = new EmojifulEmoji
                {
                    Category = category,
                    Name = emojiMatches[i].Groups[2].Value,
                    Url = emojiMatches[i].Groups[1].Value.Contains('a') ? "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".gif" : "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".png",
                    Type = "emojiful:emoji_recipe"
                };

                FileStream fs = File.Create(DatapackPath + $"datapack/data/emojiful/recipes/{emojiMatches[i].Groups[2].Value.ToLower()}.json");

                await JsonSerializer.SerializeAsync(fs, emoji);
                await fs.DisposeAsync();
            }
            var fileName = $"{ctx.User.Username}-" + category + "-emojiful-datapack.zip";
            ZipFile.CreateFromDirectory(DatapackPath + "datapack/", DatapackPath + fileName);

            await RespondWithFileAsync(new FileAttachment(File.OpenRead(DatapackPath + fileName), fileName), ephemeral: true);
        }
    }
}
