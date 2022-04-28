using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AtomicCore.Tests
{
    [TestClass()]
    public class CBCPKCS5SymmetricAlgorithmTests
    {
        public CBCPKCS5SymmetricAlgorithmTests()
        {
            AtomicCore.AtomicKernel.Initialize();
        }

        [TestMethod()]
        public void HomeTest()
        {
            var cls = new ClsCrypto("123456");

            var encrypt = cls.Encrypt("1234afd");
            var dencrypt = cls.Decrypt(encrypt);

            Assert.IsNotNull(dencrypt);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            IEncryptAlgorithm encrypt = AtomicCore.AtomicKernel.Dependency.Resolve<IEncryptAlgorithm>(CryptoMethods.CBCPKCS5);

            var result = encrypt.Encrypt("1234afd", "123456");

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        public class ClsCrypto
        {
            private RijndaelManaged myRijndael = new RijndaelManaged();
            private int iterations = 1000;
            private byte[] salt = System.Text.Encoding.UTF8.GetBytes("insight123resultxyz");

            public ClsCrypto(string strPassword, string iv = "e84ad660c4721ae0e84ad660c4721ae0")
            {
                myRijndael.BlockSize = 128;
                myRijndael.KeySize = 128;
                myRijndael.IV = HexStringToByteArray(iv);

                myRijndael.Padding = PaddingMode.PKCS7;
                myRijndael.Mode = CipherMode.CBC;
                myRijndael.Key = GenerateKey(strPassword);
            }

            public string Encrypt(string strPlainText)
            {
                byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
                ICryptoTransform transform = myRijndael.CreateEncryptor();
                byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);

                return Convert.ToBase64String(cipherText);
            }

            public string Decrypt(string encryptedText)
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                var decryptor = myRijndael.CreateDecryptor(myRijndael.Key, myRijndael.IV);
                byte[] originalBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(originalBytes);
            }

            public static byte[] HexStringToByteArray(string strHex)
            {
                dynamic r = new byte[strHex.Length / 2];
                for (int i = 0; i <= strHex.Length - 1; i += 2)
                {
                    r[i / 2] = Convert.ToByte(Convert.ToInt32(strHex.Substring(i, 2), 16));
                }
                return r;
            }

            private byte[] GenerateKey(string strPassword)
            {
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(strPassword), salt, iterations);

                return rfc2898.GetBytes(128 / 8);
            }
        }
    }
}