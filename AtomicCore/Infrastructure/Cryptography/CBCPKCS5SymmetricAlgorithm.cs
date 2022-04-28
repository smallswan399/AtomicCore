using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AtomicCore
{
    /// <summary>
    /// AES - CBC/PKCS5Padding 对称加密/解密算法
    /// </summary>
    public class CBCPKCS5SymmetricAlgorithm : IDesSymmetricAlgorithm
    {
        #region Variable

        /// <summary>
        /// 默认协商KEY
        /// </summary>
        public const string def_consultKey = "gotogether";

        #endregion

        #region IDesSymmetricAlgorithm

        private string _algorithmKey = null;

        /// <summary>
        /// 获取算法KEY（会自动从conf文件中读取symmetryKey节点配置,或不存在则会启用默认值）
        /// </summary>
        public string AlgorithmKey
        {
            get
            {
                if (null == this._algorithmKey)
                {
                    string confKey = ConfigurationJsonManager.AppSettings["symmetryKey"];
                    if (string.IsNullOrEmpty(confKey))
                        confKey = def_consultKey;

                    this._algorithmKey = MD5Handler.Generate(confKey, true).Top(8);
                }

                return this._algorithmKey;
            }
        }

        /// <summary>
        /// 是否是标准的DES密文格式
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public bool IsCiphertext(string ciphertext)
        {
            return Base64Handler.IsBase64Format(ciphertext);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="origialText">明文字符串</param>
        /// <param name="argumentParam">0:key,1:iv</param>
        /// <returns></returns>
        public string Encrypt(string origialText, params object[] argumentParam)
        {
            string result = string.Empty;

            #region 加密参数选取

            string key_str, vi_str;
            if (null == argumentParam || argumentParam.Length <= 0)
            {
                key_str = this.AlgorithmKey;
                vi_str = this.AlgorithmKey;
            }
            if (argumentParam.Length == 1)
            {
                key_str = argumentParam.First().ToString();
                vi_str = argumentParam.First().ToString();
            }
            else if (argumentParam.Length >= 2)
            {
                key_str = argumentParam[0].ToString();
                vi_str = argumentParam[1].ToString();
            }
            else
            {
                key_str = this.AlgorithmKey;
                vi_str = this.AlgorithmKey;
            }

            #endregion

            #region 执行AES加密 

            using (var rijndaelCipher = new RijndaelManaged())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key_str);
                byte[] keyBytes = new byte[16];

                int len = pwdBytes.Length;
                if (len > keyBytes.Length) len = keyBytes.Length;
                System.Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;

                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(vi_str);
                rijndaelCipher.IV = ivBytes;

                byte[] plainText = Encoding.UTF8.GetBytes(origialText);

                byte[] cipherBytes;
                using (ICryptoTransform transform = rijndaelCipher.CreateEncryptor())
                    cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

                result = Convert.ToBase64String(cipherBytes);
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="ciphertext">加密字符串</param>
        /// <param name="argumentParam">0:key,1:iv</param>
        /// <returns></returns>
        public string Decrypt(string ciphertext, params object[] argumentParam)
        {
            string result = string.Empty;

            #region 解密参数选取

            string key_str, vi_str;
            if (null == argumentParam || argumentParam.Length <= 0)
            {
                key_str = this.AlgorithmKey;
                vi_str = this.AlgorithmKey;
            }
            if (argumentParam.Length == 1)
            {
                key_str = argumentParam.First().ToString();
                vi_str = argumentParam.First().ToString();
            }
            else if (argumentParam.Length >= 2)
            {
                key_str = argumentParam[0].ToString();
                vi_str = argumentParam[1].ToString();
            }
            else
            {
                key_str = this.AlgorithmKey;
                vi_str = this.AlgorithmKey;
            }

            #endregion

            #region 执行AES解密

            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] encryptedData = Convert.FromBase64String(ciphertext);
                byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key_str);
                byte[] keyBytes = new byte[16];

                int len = pwdBytes.Length;
                if (len > keyBytes.Length) len = keyBytes.Length;
                System.Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;

                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(vi_str);
                rijndaelCipher.IV = ivBytes;

                byte[] plainText;
                using (ICryptoTransform transform = rijndaelCipher.CreateDecryptor())
                    plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                result = Encoding.UTF8.GetString(plainText);
            }

            #endregion

            return result;
        }

        #endregion
    }
}
