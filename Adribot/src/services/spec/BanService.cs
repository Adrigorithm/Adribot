using Adribot.src.entities;
using Adribot.src.entities.source;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adribot.src.services.spec
{
    class BanService : IBackgroundService
    {
        Timer _timer;
        DBController _banController = new DBController();
        List<Ban> _bans;
        public DiscordClient Client { get; set; }

        public void StartTimer(int interval = 10000) {
            GetBans();
            _timer = new Timer(new TimerCallback(TimerProc));
            _ = _timer.Change(0, interval);
        }

        private void GetBans() {
            _bans = _banController.Bans
                .OrderBy(x => x.BanExpired)
                .Take(10)
                .ToList();
        }

        private void TimerProc(object state) {
            Work();
        }

        private void Work() {
            if(_bans.Count() == 0)
                GetBans();

            if(_bans[0].BanExpired.CompareTo(DateTime.Now) < 0)
                Task.Run(() => UnbanAsync(_bans[0]));
        }

        private async Task UnbanAsync(Ban ban) {
            if(Client != null) {
                await (await Client.GetGuildAsync(ban.GuildId)).UnbanMemberAsync(ban.UserId);
                if(_bans.Count() > 1) {
                    _banController.Remove(_bans[0]);
                    await _banController.SaveChangesAsync();
                    _bans.RemoveAt(0);

                }
            }
        }

        public async Task BanAsync(DiscordMember member, DateTime banExpired, string reason = null) {
            await member.Guild.BanMemberAsync(member);

            var ban = new Ban {
                GuildId = member.Guild.Id,
                UserId = member.Id,
                BanExpired = banExpired,
                Reason = reason
            };

            _banController.Add(ban);
            await _banController.SaveChangesAsync();

            if(_bans.Count() == 0) {
                _bans.Add(ban);
            } else {
                if(ban.BanExpired.CompareTo(_bans[^1].BanExpired) < 0) {
                    _bans.Add(ban);
                    _bans = _bans.OrderBy(x => x.BanExpired).ToList();
                }
            }
        }
    }
}
