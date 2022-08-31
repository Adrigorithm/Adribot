using System.Collections.Generic;
using DSharpPlus.Entities;

public static class DiscordObjectExtensions{
    public static DGuild ToDGuild(this DiscordGuild guild){
        return new(){
            GuildId = guild.Id
        };
    }

    public static List<DGuild> ToDGuild(this Dictionary<ulong, DiscordGuild> guilds){
        List<DGuild> dGuilds = new();

        foreach (var guild in guilds)
        {
            dGuilds.Add(new(){
                GuildId = guild.Key
            });
        }
        return dGuilds;
    }

    public static DMember DoDMember(this DiscordMember member){
        return new(){
            MemberId = member.Id
        };
    }

    public static List<DMember> DoDMember(this Dictionary<ulong, DiscordMember> members){
        List<DMember> dMembers = new();

        foreach (var user in members)
        {
            dMembers.Add(user.Value.DoDMember());
        }
        return dMembers;
    }
}