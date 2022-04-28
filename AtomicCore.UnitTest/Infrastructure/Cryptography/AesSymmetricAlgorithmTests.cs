using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtomicCore.Tests
{
    [TestClass()]
    public class AesSymmetricAlgorithmTests
    {
        public AesSymmetricAlgorithmTests()
        {
            AtomicCore.AtomicKernel.Initialize();
        }

        [TestMethod()]
        public void EncryptTest()
        {
            IEncryptAlgorithm encrypt = AtomicCore.AtomicKernel.Dependency.Resolve<IEncryptAlgorithm>(CryptoMethods.AES);

            var result = encrypt.Encrypt("1234afd", "123456");

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [TestMethod()]
        public void DecryptTest()
        {
            IEncryptAlgorithm encrypt = AtomicCore.AtomicKernel.Dependency.Resolve<IEncryptAlgorithm>(CryptoMethods.AES);
            IDecryptAlgorithm decrypt = AtomicCore.AtomicKernel.Dependency.Resolve<IDecryptAlgorithm>(CryptoMethods.AES);

            var encryptResult = encrypt.Encrypt("C#AES加密字符串", "ae125efkk4454eeff444ferfkny6oxi8");

            var result = decrypt.Decrypt(encryptResult, "ae125efkk4454eeff444ferfkny6oxi8");

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }
    }
}