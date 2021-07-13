using System;
using System.Linq;
using System.Text;

namespace AtomicCore
{
    /// <summary>
    /// URL编码类
    /// </summary>
    public static class UrlEncoder
    {
        /// <summary>
        /// Url Encoder编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string UrlEncode(string str, Encoding e = null) =>
            str == null ? null : Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));

        /// <summary>
        /// Url Encoder编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static byte[] UrlEncodeToBytes(string str, Encoding e = null)
        {
            if (null == str)
                return null;
            if (null == e)
                e = Encoding.UTF8;

            byte[] bytes = e.GetBytes(str);
            return UrlEncode(bytes, 0, bytes.Length, alwaysCreateNewReturnValue: false);
        }

        /// <summary>
        /// Url Encoder编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="alwaysCreateNewReturnValue"></param>
        /// <returns></returns>
        public static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
        {
            byte[] encoded = UrlEncode(bytes, offset, count);

            return (alwaysCreateNewReturnValue && (encoded != null) && (encoded == bytes))
                ? (byte[])encoded.Clone()
                : encoded;
        }

        /// <summary>
        /// Url Encoder编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static byte[] UrlEncode(byte[] bytes, int offset, int count)
        {
            if (!ValidateUrlEncodingParameters(bytes, offset, count))
                return null;

            int cSpaces = 0;
            int cUnsafe = 0;

            // count them first
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];

                if (ch == ' ')
                    cSpaces++;
                else if (!IsUrlSafeChar(ch))
                    cUnsafe++;
            }

            // nothing to expand?
            if (cSpaces == 0 && cUnsafe == 0)
            {
                // DevDiv 912606: respect "offset" and "count"
                if (0 == offset && bytes.Length == count)
                    return bytes;
                else
                {
                    byte[] subarray = new byte[count];
                    Buffer.BlockCopy(bytes, offset, subarray, 0, count);
                    return subarray;
                }
            }

            // expand not 'safe' characters into %XX, spaces to +s
            byte[] expandedBytes = new byte[count + cUnsafe * 2];
            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];
                char ch = (char)b;

                if (IsUrlSafeChar(ch))
                    expandedBytes[pos++] = b;
                else if (ch == ' ')
                    expandedBytes[pos++] = (byte)'+';
                else
                {
                    expandedBytes[pos++] = (byte)'%';
                    expandedBytes[pos++] = (byte)ToCharLower(b >> 4);
                    expandedBytes[pos++] = (byte)ToCharLower(b);
                }
            }

            return expandedBytes;
        }

        private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if (bytes == null && count == 0)
                return false;

            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (offset < 0 || offset > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0 || offset + count > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            return true;
        }

        private static bool IsUrlSafeChar(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
            {
                return true;
            }

            switch (ch)
            {
                case '-':
                case '_':
                case '.':
                case '!':
                case '*':
                case '(':
                case ')':
                    return true;
            }

            return false;
        }

        private static string UrlEncodeSpaces(string str) => str != null && str.Contains(' ') ? str.Replace(" ", "%20") : str;

        private static char ToCharLower(int value)
        {
            value &= 0xF;
            value += '0';

            if (value > '9')
                value += ('a' - ('9' + 1));

            return (char)value;
        }
    }
}
