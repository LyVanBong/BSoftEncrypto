using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool VerifyByRSA(string filedata, string priveteKey, string fileCer)
        {
            bool verify = false;
            byte[] hashData;
            byte[] cer = File.ReadAllBytes(fileCer);
            using (SHA512Cng shaCng = new SHA512Cng())
            {
                hashData = shaCng.ComputeHash(File.ReadAllBytes(filedata));
            }

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(priveteKey));
                RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                signatureDeformatter.SetHashAlgorithm("SHA512");
                verify = signatureDeformatter.VerifySignature(hashData, cer);
            }

            return verify;
        }

        public void SaveSign(string filePathCer, byte[] data, string filePathData)
        {
            FileInfo fileInfo = new FileInfo(filePathData);
            string nameFile = (fileInfo.Name).Replace(fileInfo.Extension, "");
            FileStream fileStream = new FileStream($"{filePathCer}\\{nameFile}.RSA.bsign", FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }


    }
}
