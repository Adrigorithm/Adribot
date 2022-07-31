using System;
using System.Collections.Generic;
using DSharpPlus.Entities;

class NoHoistService{
    private Dictionary<ulong, DateTime> _poopers = new();

    public async void HoistCheck(DiscordMember member){
        if ((byte)member.Nickname[0] < 48)
        {
            await member.ModifyAsync(m => m.Nickname = "💩");
            _poopers[member.Id] = DateTime.UtcNow;
        }else if (_poopers.ContainsKey(member.Id) && _poopers[member.Id].AddDays(3).CompareTo(DateTime.UtcNow) > 0)
        {
            await member.ModifyAsync(m => m.Nickname = "💩");
        }
    }
}