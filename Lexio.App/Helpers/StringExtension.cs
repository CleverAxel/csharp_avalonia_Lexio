using System.Text;
using System.Text.RegularExpressions;

namespace Lexio.App.Helpers;

public static class StringExtension {
    public static string TrimAndReduce(this string value) {
        return ConvertWhitespacesToSingleSpaces(value).Trim();
    }

    public static string ToAscii(this string value) {
        string normalized = value.Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();
        foreach (char c in normalized) {
            if ((int)c < 128)
                sb.Append(c);
        }

        return sb.ToString();
    }

    public static string ConvertWhitespacesToSingleSpaces(this string value) {
        return Regex.Replace(value, @"\s+", " ");
    }
}