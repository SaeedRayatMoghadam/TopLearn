namespace TopLearn.Core.Utils;

public class Generator
{
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}