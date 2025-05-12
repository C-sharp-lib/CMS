using System.Text.RegularExpressions;

namespace backend.Areas.Utility.Services;

public class UtilityFunctions
{
    public UtilityFunctions()
    {
    }

    public static string GenerateSlug(string phrase)
    {
        string str = phrase.ToLowerInvariant();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", "-");
        return str;
    }

    public static string SaveFile(IFormFile file, string path)
    {
        var fileName = Path.GetFileName(file.FileName);
        var fullPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        file.CopyTo(stream);

        return fileName;
    }
    //Email Validator
    public static bool EmailIsValid(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}