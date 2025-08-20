using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Constants.Strings;
using Adribot.Entities.Minecraft;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class MinecraftCommands : InteractionModuleBase<SocketInteractionContext>
{
    private readonly Dictionary<MinecraftVersion, string> _versionMapper = new()
    {
        {
            MinecraftVersion.Legacy, "Temp/datapack/data/emojiful/recipes/"
        },
        {
            MinecraftVersion.Modern, "Temp/datapack/data/emojiful/recipe/"
        }
    };

    public MinecraftCommands()
    {
        Directory.CreateDirectory("Temp/datapack/data/emojiful");

        StreamWriter writer = File.CreateText("Temp/datapack/pack.mcmeta");

        writer.Write("{\"pack\":{\"pack_format\":6,\"description\":\"Emojifulemojis!\"}}");
        writer.Close();
        writer.Dispose();
    }

    [SlashCommand("datapack", "Discord emojis -> Emojiful datapack!")]
    public async Task GenerateDatapackAsync([Summary("minecraft-version")] MinecraftVersion mcVersion, [Summary("category", "Name to group the emoji by in-game")] string category, [Summary("emojis", "List of emoji")] string emojiList, [Summary("hide", "hide this message from public view")] bool isHidden = false)
    {
        var datapackParent = "Temp/";
        MatchCollection emojiMatches = Regex.Matches(emojiList, ConstantStrings.EmojiRegex);

        if (emojiMatches.Count > 0)
        {
            foreach (var filePath in Directory.GetFiles(datapackParent, "*?.zip"))
                File.Delete(filePath);

            Directory.Delete(datapackParent + "datapack/data/emojiful", true);
            Directory.CreateDirectory(_versionMapper[mcVersion]);

            for (var i = 0; i < emojiMatches.Count; i++)
            {
                var emoji = new EmojifulEmoji
                {
                    Category = category, Name = emojiMatches[i].Groups[2].Value, Url = emojiMatches[i].Groups[1].Value.Contains('a') ? "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".gif" : "https://cdn.discordapp.com/emojis/" + emojiMatches[i].Groups[3].Value + ".png", Type = "emojiful:emoji_recipe"
                };

                FileStream fs = File.Create($"{_versionMapper[mcVersion]}{emojiMatches[i].Groups[2].Value.ToLower()}.json");

                await JsonSerializer.SerializeAsync(fs, emoji);
                await fs.DisposeAsync();
            }

            var fileName = $"{Context.User.GlobalName ?? "notch"}-{category}-emojiful-datapack.zip";

            ZipFile.CreateFromDirectory(datapackParent + "datapack/", datapackParent + fileName);

            await RespondWithFileAsync(File.OpenRead(datapackParent + fileName), fileName, ephemeral: isHidden);
        }
    }
}
