namespace TopLearn.Core.Utilities;

public class EmailFixer
{
    public static string Fix(string email)
    {
        return email.ToLower().Trim();
    }
}