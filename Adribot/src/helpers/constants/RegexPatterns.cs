using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.helpers.constants
{
    public static class RegexPatterns
    {
        public static string EmojiRegex { get; } = "<a?:([a-zA-Z0-9_]*):([0-9]*)>";
    }
}
