using System.IO;
using System.Security.Cryptography;

namespace BSoftEncryptor.ViewModels
{
    class GenerateKeys
    {
        public string publicKey { get; set; }
        public string privateKey { get; set; }
        #region tạo khóa bí mật mà khóa công khai lưu ra file
        public void AssignNewKey()
        {

            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }

        }
        #endregion

        #region save keys
        public void SaveKeys(string filePathKey, string pub, string priv)
        {
            string filePathPublicKey = $"{filePathKey}\\RSA.PublicKey.xml";
            string filePathPrivateKey = $"{filePathKey}\\RSA.PrivateKey.xml";
            File.WriteAllText(filePathPublicKey, pub);
            File.WriteAllText(filePathPrivateKey, priv);
        }
        #endregion
    }
}
