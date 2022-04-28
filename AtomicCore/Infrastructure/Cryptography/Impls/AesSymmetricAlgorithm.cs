using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AtomicCore
{
    /// <summary>
    /// Advanced Encryption Standard，AES
    /// </summary>
    public class AesSymmetricAlgorithm : IDesSymmetricAlgorithm
    {
        #region Variable

        /// <summary>
        /// default key
        /// </summary>
        private const string def_consultKey = "gotogether";

        /// <summary>
        /// key min length
        /// </summary>
        private const int c_min_keyLenght = 16;

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
                    string confKey;
                    if (null == ConfigurationJsonManager.AppSettings)
                        confKey = def_consultKey;
                    else
                    {
                        confKey = ConfigurationJsonManager.AppSettings["symmetryKey"];
                        if (string.IsNullOrEmpty(confKey))
                            confKey = def_consultKey;
                    }

                    this._algorithmKey = MD5Handler.Generate(confKey, true).Top(c_min_keyLenght);
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

            string key_str;
            if (null == argumentParam || argumentParam.Length <= 0)
                key_str = this.AlgorithmKey;
            else
            {
                if (argumentParam.Length == 1)
                    key_str = argumentParam.First().ToString();
                else
                    key_str = this.AlgorithmKey;

                if (key_str.Length < c_min_keyLenght)
                    key_str = key_str.PadRight(c_min_keyLenght, char.MinValue);
            }

            #endregion

            #region 执行AES加密 

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key_str);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(origialText);

            using (var rDel = new RijndaelManaged())
            {
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.Key = keyArray;

                byte[] cipherBytes;
                using (ICryptoTransform transform = rDel.CreateEncryptor())
                    cipherBytes = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                result = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
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

            string key_str;
            if (null == argumentParam || argumentParam.Length <= 0)
                key_str = this.AlgorithmKey;
            else
            {
                if (argumentParam.Length == 1)
                    key_str = argumentParam.First().ToString();
                else
                    key_str = this.AlgorithmKey;

                if (key_str.Length < c_min_keyLenght)
                    key_str = key_str.PadRight(c_min_keyLenght, char.MinValue);
            }

            #endregion

            #region 执行AES解密

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key_str);
            byte[] toEncryptArray = Convert.FromBase64String(ciphertext);

            using (var rDel = new RijndaelManaged())
            {
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.Key = keyArray;

                byte[] plainText;
                using (ICryptoTransform transform = rDel.CreateDecryptor())
                    plainText = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                result = Encoding.UTF8.GetString(plainText);
            }

            #endregion

            return result;
        }

        #endregion
    }
}
