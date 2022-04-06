namespace TopLearn.Core.Utils;

public static class TextFixer
{
    public static string FixEmail(this string email)
    {
        return email.Trim().ToLower();
    }
}