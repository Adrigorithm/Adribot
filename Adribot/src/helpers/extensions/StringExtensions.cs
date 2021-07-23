using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adribot.src.helpers.extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a DateTime representation of a string object consisting of a number (preceding zeros are omitted) followed by an indicator:
        /// 'm' = minutes
        /// 'h' = hours
        /// 'd' = days
        /// 'w' = weeks
        /// 'M' = months
        /// 'y' = years
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>DateTime representation of a string object or DateTime.Now if invalid string is provided</returns>
        public static DateTime ToFutureDate(this string s) {
            try {
                var match = Regex.Match(s, "^0*(\\d*)([mhdwMy]\\Z)");
                switch(match.Groups[1].Value[0]) {
                    case 'm':
                        return DateTime.Now.AddMinutes(Convert.ToDouble(match.Groups[0].Value));
                    case 'h':
                        return DateTime.Now.AddHours(Convert.ToDouble(match.Groups[0].Value));
                    case 'd':
                        return DateTime.Now.AddDays(Convert.ToDouble(match.Groups[0].Value));
                    case 'w':
                        return DateTime.Now.AddDays(Convert.ToDouble(match.Groups[0].Value) * 7);
                    case 'M':
                        return DateTime.Now.AddMonths(Convert.ToInt16(match.Groups[0].Value));
                    case 'y':
                        return DateTime.Now.AddYears(Convert.ToInt16(match.Groups[0].Value));
                    default:
                        return DateTime.Now;
                }
            } catch(Exception) {
                return DateTime.Now;
            }
        }
    }
}
