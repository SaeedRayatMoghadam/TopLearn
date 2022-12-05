namespace TopLearn.Core.Utilities;

public class CodeGenerator
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}