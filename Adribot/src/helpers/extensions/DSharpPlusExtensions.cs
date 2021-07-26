using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.helpers.extensions
{
    public static class DSharpPlusExtensions
    {
        /// <summary>
        /// This methods attempts to send a private message to a DiscordMember
        /// </summary>
        /// <param name="m">Member to send a pm to</param>
        /// <param name="content">Content of the message to send</param>
        /// <returns>true if the pm could reach the member, otherwise false.</returns>
        public static async Task<bool> TrySendMessageAsync(this DiscordMember m, string content) {
            try {
                await m.SendMessageAsync(content);
            } catch(Exception) {
                return false;
            }
            return true;
        }
    }
}
