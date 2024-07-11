using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adribot.src.helpers;

public static class FakeExtensions
{
    public static string PrintFormat(IEnumerable<object> list)
    {
        StringBuilder sb = new("[");
        var listSize = list.Count();

        for (var i = 0; i < listSize; i++)
        {
            if (i == listSize - 1)
                sb.AppendLine($"{list.ElementAt(i)}{Environment.NewLine}]");
            else
                sb.AppendLine($"{list.ElementAt(i)}, ");
        }

        return sb.ToString();
    }

    public static string GetMarkdownCSV(string[] strings)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < strings.Length; i++)
        {
            if (i == strings.Length - 1)
                sb.Append($"`{strings[i]}`");
            else
                sb.Append($"`{strings[i]}`, ");
        }

        return sb.ToString();
    }

    public static bool AreAllNullOrWhiteSpace(params IEnumerable<string> strings) =>
        strings.All(s => string.IsNullOrWhiteSpace(s));
}
