using System;
using System.IO;
using System.Windows;
using System.Security.Cryptography;

namespace BSoftEncryptor.ViewModels
{
    class DigitalSignatures
    {
        public byte[] SignByRSA(string filePathKey, string filePthData)
        {
            byte[] sign;
            byte[] hashData;
            using (SHA512Cng shaCng = new SHA512Cng())
            {
                hashData = shaCng.ComputeHash(File.ReadAllBytes(filePthData));
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(filePathKey));
                RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
                signatureFormatter.SetHashAlgorithm("SHA512");
                sign = signatureFormatter.CreateSignature(hashData);
            }
            return sign;
        }

        public bool VerifyByRSA(string filePathData, string filePathKey, string filePathCer)
        {
            bool verify = false;
            byte[] hashData;
            byte[] cer = File.ReadAllBytes(filePathCer);
            using (SHA512Cng shaCng = new SHA512Cng())
            {
                hashData = shaCng.ComputeHash(File.ReadAllBytes(filePathData));
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(filePathKey));
                RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                signatureDeformatter.SetHashAlgorithm("SHA512");
                verify = signatureDeformatter.VerifySignature(hashData, cer);
            }

            return verify;
        }

        public void SaveSign(string filePathCer, byte[] data, string filePathData)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePathData);
                string nameFile = (fileInfo.Name).Replace(fileInfo.Extension, "");
                FileStream fileStream = new FileStream($"{filePathCer}\\{nameFile}.RSA.bsign", FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Write(data, 0, data.Length);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


    }
}
