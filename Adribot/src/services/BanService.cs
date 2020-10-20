using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Adribot.src.entities;
using Adribot.src.services.dbGateway;
using Adribot.timers;
using DSharpPlus;

namespace Adribot.services
{
    public static class BanService
    {
        private static DiscordClient _client;
        private static BaseTimer _timer;
        private static List<Ban> _bans;

        public static void SetupBanService(DiscordClient client) {
            _client = client;

            _timer = new BaseTimer();
            _timer.SetupTimer(10000, true, true);
            _timer.GlobalTimer.Elapsed += GlobalTimerOnElapsedAsync;
            GetBans(10);
        }

        private static async void GlobalTimerOnElapsedAsync(object sender, ElapsedEventArgs e) {
            if(_bans.Count == 0) {
                GetBans(10);
            }

            Ban currentBan = _bans[0] ?? null;

            if(currentBan is null) {
                return;
            }

            if(currentBan.BanExpired < DateTime.UtcNow) {
                await UnbanMemberAsync(currentBan);
            }
        }

        private static void GetBans(int amount) {
            Database database = new Database();
            _bans = database.GetObjectList(typeof(Ban), amount).Cast<Ban>().ToList();
        }

        public static void AddBan(Ban ban) {
            Database database = new Database();
            database.InsertObjectList(new [] { ban });

            if(ban.BanExpired > _bans[_bans.Count - 1].BanExpired) {
                GetBans(10);
            }
        }

        private static async Task UnbanMemberAsync(Ban ban) {
            // Implement logging / Embed
            try {
                var guild = await _client.GetGuildAsync(ban.GuildId);
                await guild.UnbanMemberAsync(ban.UserId, "Tempban Expired.");
            } catch(Exception e) {
                Console.WriteLine($"Guild or User doesn't exist anymore: {e.Message}");
            }

            Database database = new Database();
            database.CheckUnbanned(ban.BanId);
        }
    }
}