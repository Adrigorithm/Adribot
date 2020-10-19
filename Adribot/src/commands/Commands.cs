using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Adribot.config;

namespace Adribot
{
    public class Commands : BaseCommandModule
    {
        [Command("clear"), Description("Clears chat history"), RequirePermissions(Permissions.ManageMessages)]
        public async Task ClearChat(CommandContext ctx, [Description("Amount of messages to be deleted (1-100 / -1)")] int amount = 100)
        {
            // Remove command
            await ctx.Message.DeleteAsync();
            
            // Check for validity
            if (amount < -1 | amount > 100 | amount == 0)
            {
                await ctx.RespondAsync(
                    $"Invalid amount: `{amount}` issue `{Config.Prefix}help` command for more information.");
            }
            else
            {
                DiscordMessage confirmation = null;
                
                if (amount == -1)
                {
                    var interactivity = ctx.Client.GetInteractivity();

                    await ctx.RespondAsync("Removing `all` messages. Continue? (answer with ´y´)");
                    var check = await interactivity.WaitForMessageAsync(m => m.Author == ctx.User && m.Content == "y",
                        TimeSpan.FromSeconds(5));

                    if (check.Result != null)
                    {
                        var channel = await ctx.Channel.CloneAsync($"Force deleted all messages. Moderator: {ctx.User}");
                        await ctx.Channel.DeleteAsync();
                        confirmation = await channel.SendMessageAsync($"{DiscordEmoji.FromUnicode("\\U00002705")} `all` Messages have been removed successfully!");
                    }
                }
                else
                {
                    var old = false;
                    
                    var messages = await ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, amount);
                    
                    if (messages[-1].Timestamp.AddDays(14).CompareTo(DateTimeOffset.UtcNow) < 1)
                    {
                        old = true;
                    }
                    await ctx.Channel.DeleteMessagesAsync(messages, $"Deleted `{amount}` messages. Moderator: {ctx.User}");

                    if (old)
                    {
                        confirmation = await ctx.RespondAsync($"{DiscordEmoji.FromUnicode("\\U0000274c")} Messages older than `14` days have been left out.\nDelete manually or use `{Config.Prefix}clear -1`.");
                    }
                    else
                    {
                        confirmation = await ctx.RespondAsync($"{DiscordEmoji.FromUnicode("\\U00002705")} `{amount}` Messages have been removed successfully!");
                    }
                }

                await Task.Delay(2000);
                if (confirmation != null) await confirmation.DeleteAsync();
            }
        }
    }
}
