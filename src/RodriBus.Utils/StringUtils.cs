using System;
using System.Text;

namespace Rodribus.Utils
{
    /// <summary>
    /// Contains utils related to common string operations.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Decodes a string from an UTF-8 base64 string.
        /// </summary>
        /// <param name="encodedText">The UTF-8 base64 encoded text</param>
        /// <returns>The decoded text</returns>
        public static string Base64Decode(string encodedText)
        {
            var encodedTextBytes = Convert.FromBase64String(encodedText);
            var plainText = Encoding.UTF8.GetString(encodedTextBytes);
            return plainText;
        }

        /// <summary>
        /// Encodes a string into a base64 string using UTF-8.
        /// </summary>
        /// <param name="plainText">The plain text to encode</param>
        /// <returns>The encoded text</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var encodedText = Convert.ToBase64String(plainTextBytes);
            return encodedText;
        }
    }
}
