using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Timers;

namespace Adribot.timers.specTimers
{
    public class BanTimer : BaseTimer
    {
        private Bans _bans;
        private const string Banfile = "D:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Projects\\Adribot\\Adribot\\data\\bans.json";

        public BanTimer()
        {
            SetupTimer(10000, true, true);
            LoadBans();
            GlobalTimer.Elapsed += BanTimerOnElapsed;
            
            NewBan(10, DateTime.UtcNow.AddHours(2));
        }

        private void LoadBans()
        {
            try
            {
                using (var streamReader =
                    new StreamReader(Banfile, Encoding.UTF8))
                {
                    _bans = JsonSerializer.Deserialize<Bans>(streamReader.ReadToEnd());
                }

                TimerStatus();
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON file is corrupt or empty.\nRestoring...");
                CreateFile();
            }
            catch (FileNotFoundException)
            {
                CreateFile();
            }
        }

        private void CreateFile()
        {
            try
            {
                using (var streamReader =
                    new StreamReader(Banfile, Encoding.UTF8))
                {
                    _bans = JsonSerializer.Deserialize<Bans>(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Couldn't write to file: {Banfile}, make sure the location is accessible to the application.\nException thrown: {e.Message}");
                throw;
            }
        }

        private void NewBan(ulong user, DateTime unban)
        {
            var ban = new Ban {User = user, Unban = unban};
            _bans.Banlist.Add(ban);
            SaveBans();
            TimerStatus();
        }

        private void BanTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _bans.Banlist.RemoveAll(ban => ban.Unban.CompareTo(DateTime.UtcNow) < 0);
            SaveBans();
            
            Console.WriteLine(_bans.Banlist.Count);
            TimerStatus();
        }

        private void SaveBans()
        {
            using var streamWriter = new StreamWriter(Banfile, false, Encoding.UTF8);
            streamWriter.Write(JsonSerializer.Serialize<Bans>(_bans));
        }

        private void TimerStatus()
        {
            GlobalTimer.Enabled = !(_bans.Banlist is null);
        }
    }
    
    public struct Ban
    {
        public ulong User { get; set; }
        public DateTime Unban { get; set; }
    }

    public struct Bans
    {
        public List<Ban> Banlist { get; set; }
    }
}