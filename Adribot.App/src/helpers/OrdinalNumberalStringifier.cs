namespace Adribot.helpers;

public static class OrdinalNumberalStringifier
{
    public static string Short(int number)
    {
        var numberString = number.ToString();

        return numberString[^1] switch
        {
            '1' => $"{numberString}st",
            '2' => $"{numberString}nd",
            '3' => $"{numberString}rd",
            _ => $"{numberString}th"
        };
    }
}
