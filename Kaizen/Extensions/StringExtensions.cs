using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Kaizen.Extensions
{
    public static class StringExtensions
    {
        public static string Base64ForUrlEncode(this string str)
        {
            byte[] encBuff = Encoding.UTF8.GetBytes(str);
            return WebEncoders.Base64UrlEncode(encBuff);
        }

        public static string Base64ForUrlDecode(this string str)
        {
            byte[] encBuff = WebEncoders.Base64UrlDecode(str);
            return Encoding.UTF8.GetString(encBuff);
        }
    }
}
