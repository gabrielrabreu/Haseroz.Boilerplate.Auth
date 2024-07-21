using Bogus.DataSets;

namespace Haseroz.Boilerplate.Auth.FunctionalTests.TestUtilities;

public static class BogusExtensions
{
    public static string PasswordCustom(this Internet internet)
    {
        var r = internet.Random;
        var number = r.Replace("#");
        var letter = r.Replace("?");
        var lowerLetter = letter.ToLower();
        var symbol = r.Char((char)33, (char)47);
        var padding = r.String2(r.Number(2, 6));
        return new string(r.Shuffle(number + letter + lowerLetter + symbol + padding).ToArray());
    }
}