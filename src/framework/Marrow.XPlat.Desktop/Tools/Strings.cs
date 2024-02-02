using System.Text;

namespace Marrow.XPlat.Tools
{
    public static class Strings
    {
        public static string Space(string text)
        {
            var bld = new StringBuilder();
            char last = default;
            foreach (var letter in text)
            {
                if ((char.IsLower(last) && (char.IsUpper(letter) || char.IsDigit(letter))) ||
                    (char.IsDigit(last) && char.IsUpper(letter)))
                    bld.Append(' ');
                bld.Append(last = letter);
            }
            return bld.ToString().Trim();
        }
    }
}