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

        public string AssignNewKey(string filePathKey)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string publicKey;
            string privateKey;
            string filePathPublicKey = $"{filePathKey}\\{fileInfo.Name}.PublicKey.xml";
            string filePathPrivateKey = $"{filePathKey}\\{fileInfo.Name}.PrivateKey.xml";

            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ToXmlString(false);
                File.WriteAllText(filePathPublicKey, publicKey);
                privateKey = rsa.ToXmlString(true);
                File.WriteAllText(filePathPrivateKey, privateKey);
            }
            return filePathPublicKey;
        }

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

    }
}
