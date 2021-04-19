using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// Http Utils
    /// </summary>
    internal static class BizHttpUtils
    {
        /// <summary>
        /// Http Post application/json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpJson<T>(string url, T data, Encoding chast = null)
            where T : new()
        {
            if (null == chast)
                chast = Encoding.UTF8;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                SetCertificateValidationCallBack();//HTTPS证书验证

            //序列化
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            //构造请求
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
            request.Timeout = 30 * 60 * 1000;
            if (!string.IsNullOrEmpty(json))
            {
                byte[] buffer = chast.GetBytes(json.ToString());
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
                    respText = sr.ReadToEnd();
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
        /// HTTP POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="inputParams"></param>
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpPost(string url, IDictionary<string, string> inputParams, Encoding chast = null)
        {
            if (null == inputParams || inputParams.Count <= 0)
                return HttpPost(url, string.Empty, null);
            else
            {
                string data = string.Join("&", inputParams.Select(s => string.Format("{0}={1}", s.Key, s.Value)));
                return HttpPost(url, data, null);
            }
        }

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string data, Encoding chast = null)
        {
            if (null == chast)
                chast = Encoding.UTF8;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                SetCertificateValidationCallBack();//HTTPS证书验证

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
            request.Timeout = 30 * 60 * 1000;
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
                    respText = sr.ReadToEnd();
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
        /// <param name="chast"></param>
        /// <returns></returns>
        public static string HttpGet(string url, string data, Encoding chast = null)
        {
            if (null == chast)
                chast = Encoding.UTF8;

            string get_url;
            if (string.IsNullOrEmpty(data))
                get_url = url;
            else
                get_url = string.Format("{0}?{1}", url, UrlEnconde(data));

            HttpWebRequest request = WebRequest.Create(get_url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
            request.Timeout = 30 * 60 * 1000;

            string respText = string.Empty;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), chast))
                    respText = sr.ReadToEnd();
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
        /// Url参数编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string UrlEnconde(string data)
        {
            StringBuilder queryBuilder = new StringBuilder();
            foreach (var param in data.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] kv_arr = param.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                queryBuilder.AppendFormat("{0}={1}&", kv_arr.First(), System.Web.HttpUtility.UrlEncode(kv_arr.Last()));
            }
            if (queryBuilder.Length > 1)
                queryBuilder.Remove(queryBuilder.Length - 1, 1);

            return queryBuilder.ToString();
        }

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
    }
}
