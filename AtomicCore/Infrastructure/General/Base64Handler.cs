using System;
using System.IO;

namespace AtomicCore
{
    /// <summary>
    /// Base64处理类
    /// </summary>
    public static class Base64Handler
    {
        /// <summary>
        /// 将普通文本转换成Base64编码的文本
        /// </summary>
        /// <param name="origText">普通文本</param>
        /// <returns></returns>
        public static string ConvertToBase64(string origText, System.Text.Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(origText))
                throw new ArgumentNullException("origText");
            if (null == encoding)
                encoding = System.Text.Encoding.UTF8;

            byte[] binBuffer = encoding.GetBytes(origText.Replace(' ', '+'));
            int len = (int)Math.Ceiling(binBuffer.Length / 3d) * 4;
            char[] charBuffer = new char[len];
            Convert.ToBase64CharArray(binBuffer, 0, binBuffer.Length, charBuffer, 0);
            string s = new string(charBuffer);

            return s;
        }

        /// <summary>
        /// 将Byte[]转换成Base64编码文本
        /// </summary>
        /// <param name="buffer">Byte[]</param>
        /// <returns></returns>
        public static string ConvertToBase64(byte[] buffer)
        {
            if (null == buffer || buffer.Length == 0)
                throw new ArgumentNullException("buffer");

            int len = (int)Math.Ceiling(buffer.Length / 3d) * 4;
            char[] charBuffer = new char[len];
            Convert.ToBase64CharArray(buffer, 0, buffer.Length, charBuffer, 0);
            string s = new string(charBuffer);

            return s;
        }

        /// <summary>
        /// 将Stream流转换成Base64编码文本
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static string ConvertToBase64(Stream stream)
        {
            if (null == stream || Stream.Null == stream)
                throw new ArgumentNullException("stream");

            byte[] buffer = stream.ToBuffer();

            return ConvertToBase64(buffer);
        }

        /// <summary>
        /// 将Base64编码的文本转换成普通文本
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ConvertToOriginal(string base64, System.Text.Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(base64))
                throw new ArgumentNullException("base64");
            if (null == encoding)
                encoding = System.Text.Encoding.UTF8;

            char[] charBuffer = base64.Replace('-', '+').Replace('_', '/').PadRight(4 * ((base64.Length + 3) / 4), '=').ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将Base64编码文本转换成Byte[]
        /// </summary>
        /// <param name="base64">Base64编码文本</param>
        /// <returns></returns>
        public static byte[] ConvertToBuffer(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                throw new ArgumentNullException("base64");

            char[] charBuffer = base64.Replace('-', '+').Replace('_', '/').PadRight(4 * ((base64.Length + 3) / 4), '=').ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);

            return bytes;
        }

        /// <summary>
        /// 将Base64编码文本转换成Byte[]
        /// </summary>
        /// <param name="base64">Base64编码文本</param>
        /// <returns></returns>
        public static Stream ConvertToStream(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                throw new ArgumentNullException("base64");

            char[] charBuffer = base64.Replace('-', '+').Replace('_', '/').PadRight(4 * ((base64.Length + 3) / 4), '=').ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            MemoryStream ms = new MemoryStream(bytes);

            return ms;
        }
    }
}
