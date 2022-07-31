using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

class AdminCommands : ApplicationCommandModule
{
    [SlashCommand("Clear", "Deletes given amount of messages")]
    [SlashRequirePermissions(Permissions.ManageMessages)]
    public async Task DeleteMessagesAsync(InteractionContext ctx, [Option("Amount", "Amount of messages to delete"), Minimum(2), Maximum(100)] long amount = 10)
    {
        var messages = await ctx.Channel.GetMessagesAsync((int)amount);
        int index = messages.Count - 1;
        while (index > -1)
        {
            if (messages[index].Timestamp.UtcDateTime.AddDays(14).CompareTo(DateTime.UtcNow) < 0)
            {
                index--;
                continue;
            }
            break;
        }

        await ctx.Channel.DeleteMessagesAsync(messages.Take(index + 1));
        if (messages.Count - index + 1 > 0)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Ink too dry! {messages.Count - index - 1} Messages could not be deleted."));
        }
        else
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        }
    }
}
