using System.IO;
using System.Security.Cryptography;

namespace BSoftEncryptor.ViewModels
{
    class AsymmetricAlgorithms
    {
        public string filePath { get; set; }

        public AsymmetricAlgorithms(string filePath)
        {
            this.filePath = filePath;
        }
        #region mã hóa
        public void EncryptData(string publicKeyPath)
        {

            byte[] dataToEncrypt = File.ReadAllBytes(filePath);

            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                //rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            FileInfo fileInfo = new FileInfo(filePath);
            File.WriteAllBytes(fileInfo.Directory + "\\" + fileInfo.Name + ".EncryptData" + fileInfo.Extension, cipherbytes);
        }
        #endregion

        #region giải mã
        public void DecryptData(string privateKeyPath)
        {
            byte[] dataToEncrypt = File.ReadAllBytes(filePath);
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                //rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                plain = rsa.Decrypt(dataToEncrypt, false);
            }
            FileInfo fileInfo = new FileInfo(filePath);
            File.WriteAllBytes(fileInfo.Directory + "\\" + fileInfo.Name + ".DecryptData" + fileInfo.Extension, plain);
        }
        #endregion
    }
}