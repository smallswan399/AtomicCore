using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace AtomicCore
{
    /// <summary>
    /// Http协议(提供Get Post Download等方法函数)
    /// </summary>
    public static class HttpProtocol
    {
        #region Variables

        /// <summary>
        /// ContentType -> application/json
        /// </summary>
        public const string APPLICATIONJSON = "application/json";

        /// <summary>
        /// ContentType -> application/x-www-form-urlencoded
        /// </summary>
        public const string XWWWFORMURLENCODED = "application/x-www-form-urlencoded";

        #endregion

        #region Public Methods

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType">application/json | application/x-www-form-urlencoded | .....</param>
        /// <param name="heads"></param>
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string data, string contentType = APPLICATIONJSON, Dictionary<string, string> heads = null, Encoding chast = null)
        {
            if (null == chast)
                chast = Encoding.UTF8;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                SetCertificateValidationCallBack();//HTTPS证书验证

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";

            if (null != heads && heads.Count > 0)
                foreach (var kv in heads)
                    request.Headers.Add(kv.Key, kv.Value);

            if (!string.IsNullOrEmpty(data))
            {
                byte[] buffer = chast.GetBytes(data.ToString());
                using (var reqStream = request.GetRequestStream())
                {
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
            }

            string respText = string.Empty;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), chast))
                {
                    respText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != response)
                    response.Dispose();
            }

            return respText;
        }

        /// <summary>
        /// Http Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpGet(string url, string data = null, Dictionary<string, string> heads = null, Encoding chast = null)
        {
            if (null == chast)
                chast = Encoding.UTF8;

            string get_url;
            if (string.IsNullOrEmpty(data))
                get_url = url;
            else
                get_url = string.Format("{0}?{1}", url, UrlEnconde(data));

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                SetCertificateValidationCallBack();//HTTPS证书验证

            HttpWebRequest request = WebRequest.Create(get_url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";

            if (null != heads && heads.Count > 0)
                foreach (var kv in heads)
                    request.Headers.Add(kv.Key, kv.Value);

            string respText = string.Empty;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), chast))
                {
                    respText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != response)
                    response.Dispose();
            }

            return respText;
        }

        /// <summary>
        /// 下载图片流至内存缓冲字节数组中
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] DownImage(string url, ref string contenType)
        {
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri get_url))
                return null;

            string suffix = get_url.LocalPath.Substring(get_url.LocalPath.LastIndexOf('.'));
            if (string.IsNullOrEmpty(suffix))
                return null;
            else
                suffix = suffix.ToLower();

            if (!s_imgContentTypeDics.ContainsKey(suffix))
                return null;
            else
                contenType = s_imgContentTypeDics[suffix];

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                SetCertificateValidationCallBack();//HTTPS证书验证

            HttpWebRequest request = WebRequest.Create(get_url) as HttpWebRequest;
            request.ServicePoint.Expect100Continue = false;
            request.Method = "GET";
            request.KeepAlive = true;

            request.ContentType = contenType;

            byte[] bys = null;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream()))
                    {
                        img.Save(ms, img.RawFormat);
                    }

                    bys = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != response)
                    response.Dispose();
            }

            return bys;
        }

        #endregion

        #region SSL Certificate Validatrion

        /// <summary>
        /// 设置服务器证书验证回调
        /// </summary>
        private static void SetCertificateValidationCallBack()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationResult;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        ///  证书验证回调函数  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private static bool CertificateValidationResult(object obj, System.Security.Cryptography.X509Certificates.X509Certificate cer, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }

        #endregion

        #region URL Enconde

        /// <summary>
        /// Url参数编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlEnconde(string data)
        {
            StringBuilder queryBuilder = new StringBuilder();
            foreach (var param in data.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] kv_arr = param.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                queryBuilder.AppendFormat("{0}={1}&", kv_arr.First(), UrlEncode(kv_arr.Last()));
            }
            if (queryBuilder.Length > 1)
                queryBuilder.Remove(queryBuilder.Length - 1, 1);

            return queryBuilder.ToString();
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string UrlEncode(string str, Encoding e = null)
        {
            if (null == e)
                e = Encoding.UTF8;

            byte[] bytes = e.GetBytes(str);
            byte[] encodedBytes = UrlEncodeNonAscii(bytes, 0, bytes.Length);
            return Encoding.ASCII.GetString(encodedBytes);
        }

        /// <summary>
        /// URL Encode NonAscii
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static byte[] UrlEncodeNonAscii(byte[] bytes, int offset, int count)
        {
            int cNonAscii = 0;

            // count them first
            for (int i = 0; i < count; i++)
            {
                if (IsNonAsciiByte(bytes[offset + i]))
                {
                    cNonAscii++;
                }
            }

            // nothing to expand?
            if (cNonAscii == 0)
            {
                return bytes;
            }

            // expand not 'safe' characters into %XX, spaces to +s
            byte[] expandedBytes = new byte[count + cNonAscii * 2];
            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];

                if (IsNonAsciiByte(b))
                {
                    expandedBytes[pos++] = (byte)'%';
                    expandedBytes[pos++] = (byte)ToCharLower(b >> 4);
                    expandedBytes[pos++] = (byte)ToCharLower(b);
                }
                else
                {
                    expandedBytes[pos++] = b;
                }
            }

            return expandedBytes;
        }

        /// <summary>
        /// Char转小写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static char ToCharLower(int value)
        {
            value &= 0xF;
            value += '0';

            if (value > '9')
                value += ('a' - ('9' + 1));

            return (char)value;
        }

        /// <summary>
        /// 判断是否包含Ascii Byte
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool IsNonAsciiByte(byte b) => b >= 0x7F || b < 0x20;

        #endregion

        #region Private Methods

        /// <summary>
        /// 图片ContentType
        /// </summary>
        private static Dictionary<string, string> s_imgContentTypeDics = new Dictionary<string, string>()
        {
            {".fax"," image/fax " },
            {".gif","image/gif" },
            {".ico","image/x-icon" },
            {".jfif" ,"image/jpeg"},
            {".jpe","image/jpeg" },
            { ".jpeg","image/jpeg"},
            { ".jpg","image/jpg"},
            { ".net","image/pnetvue"},
            { ".png","image/png"},
            {".rp","image/vnd.rn-realpix" },
            {".tif","image/tiff" },
            { ".tiff","image/tiff"},
            {".wbmp"," image/vnd.wap.wbmp " }
        };

        #endregion
    }
}
