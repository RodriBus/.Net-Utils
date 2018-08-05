using System;
using System.Text;

namespace Rodribus.Utils
{
    public static class StringUtils
    {
        public static string Base64Decode(string encodedText)
        {
            var encodedTextBytes = Convert.FromBase64String(encodedText);
            var plainText = Encoding.UTF8.GetString(encodedTextBytes);
            return plainText;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var encodedText = Convert.ToBase64String(plainTextBytes);
            return encodedText;
        }
    }
}
