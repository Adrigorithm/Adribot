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
    class MuteService : IBackgroundService
    {
        Timer _timer;
        DBController _muteController = new DBController();
        List<Mute> _mutes;
        public DiscordClient Client { get; set; }

        public void StartTimer(int interval = 5000) {
            GetMutes();
            _timer = new Timer(new TimerCallback(TimerProc));
            _ = _timer.Change(0, interval);
        }

        private void GetMutes() {
            _mutes = _muteController.Mutes
                .OrderBy(x => x.ExpiryDate)
                .Take(10)
                .ToList();
        }

        private void TimerProc(object state) {
            Work();
        }

        private void Work() {
            if(_mutes.Count() == 0)
                GetMutes();

            if(_mutes[0].ExpiryDate.CompareTo(DateTime.Now) < 0)
                Task.Run(() => UnmuteAsync(_mutes[0]));
        }

        private async Task UnmuteAsync(Mute mute) {
            if(Client != null) {
                var guild = await Client.GetGuildAsync(mute.Member.GuildId);
                await guild.Members[mute.Member.UserId].SetMuteAsync(false, mute.Reason + " -> Mute expired.");
                if(_mutes.Count() > 1) {
                    _muteController.Remove(_mutes[0]);
                    await _muteController.SaveChangesAsync();
                    _mutes.RemoveAt(0);

                }
            }
        }

        public async Task MuteAsync(DiscordMember member, DateTime expiryDate, string reason = null) {
            await member.Guild.BanMemberAsync(member);

            var mute = new Mute {
                MemberId = _muteController.Members.First(x => x.UserId == member.Id).MemberId,
                ExpiryDate = expiryDate,
                Reason = reason
            };

            _muteController.Add(mute);
            await _muteController.SaveChangesAsync();

            if(_mutes.Count() == 0) {
                _mutes.Add(mute);
            } else {
                if(mute.ExpiryDate.CompareTo(_mutes[^1].ExpiryDate) < 0) {
                    _mutes.Add(mute);
                    _mutes = _mutes.OrderBy(x => x.ExpiryDate).ToList();
                }
            }
        }
    }
}
