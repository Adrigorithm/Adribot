using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adribot.src.helpers
{
    public static class FakeExtensions
    {
        public static string PrintFormat(IEnumerable<object> list)
        {
            StringBuilder sb = new("[");
            int listSize = list.Count();

            for (int i = 0; i < listSize; i++)
            {
                if (i == listSize - 1)
                    sb.AppendLine($"{list.ElementAt(i)}{Environment.NewLine}]");
                else
                    sb.AppendLine($"{list.ElementAt(i)}, ");
            }

            return sb.ToString();
        }
    }
}
