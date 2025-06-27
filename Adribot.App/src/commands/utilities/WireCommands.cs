using System;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Extensions;
using Adribot.Services;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class WireCommands(WireService wireService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("register emote", "")]
    public async Task ExecuteRemindTaskAsync([Summary("task", "What you should be reminded of")] string taskTodo, [Summary("unit", "Time unit to be muliplied by the next factor parameter")] TimeSpanType timeUnit, [Summary("factor", "Amount of instances of the specified time unit")] int factor, [Summary("channel", "Fallback for if you don't want the bot to dm you")] ITextChannel? altChannel = null)
    {
        
    }
}
