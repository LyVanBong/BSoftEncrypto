using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BSoftEncryptor.ViewModels
{
    public class SymmetricAlgorithms
    {
        public string filePath { get; set; }
        public string pass { get; set; }
        private SymmetricAlgorithm[] symmetricAlgorithms = { new AesCryptoServiceProvider(), new DESCryptoServiceProvider(), new RC2CryptoServiceProvider(), new TripleDESCryptoServiceProvider() };

        public SymmetricAlgorithms(string filePath, string pass)
        {
            this.filePath = filePath;
            this.pass = pass;
        }

        public string FilePath2(string nameFile)
        {
            string fullFilePath2 = "";
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fullFilePath2 = fileInfo.Directory + "\\" + nameFile + "_" + fileInfo.Name;
            }
            return fullFilePath2;
        }

        #region ma hoa
        public void EncryptSymmetric(int algorithm)
        {

            using (FileStream fileStream = new FileStream(FilePath2("EncryptSymmetric"), FileMode.OpenOrCreate, FileAccess.Write))
            {
                // thiet lap iv vao luu vao file
                symmetricAlgorithms[algorithm].GenerateIV();
                fileStream.Write(symmetricAlgorithms[algorithm].IV, 0, symmetricAlgorithms[algorithm].IV.Length);

                // thiết lập salt cho khóa dẫn suất lưu vào file
                RandomNumberGenerator numberGenerator = new RNGCryptoServiceProvider();
                byte[] salt = new byte[symmetricAlgorithms[algorithm].KeySize];
                numberGenerator.GetBytes(salt);
                fileStream.Write(salt, 0, salt.Length);

                // tạo khóa từ mật khẩu
                DeriveBytes deriveBytes = new Rfc2898DeriveBytes(pass, salt, 4096);
                symmetricAlgorithms[algorithm].Key = deriveBytes.GetBytes(symmetricAlgorithms[algorithm].Key.Length);

                // bắt đầu mã hõa và ghi bản mã ra file
                byte[] buf = new byte[2048];
                int count;
                using (CryptoStream cryptoStream = new CryptoStream(new FileStream(filePath, FileMode.Open, FileAccess.Read), symmetricAlgorithms[algorithm].CreateEncryptor(), CryptoStreamMode.Read))
                {
                    while ((count = cryptoStream.Read(buf, 0, buf.Length)) != 0)
                    {
                        fileStream.Write(buf, 0, count);
                    }
                }

            }


        }
        #endregion

        #region giai ma
        public void DecryptSymmetric(int algorithm)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // doc iv tu file ma hoa
                byte[] iv = new byte[symmetricAlgorithms[algorithm].IV.Length];
                fileStream.Read(iv, 0, symmetricAlgorithms[algorithm].IV.Length);
                symmetricAlgorithms[algorithm].IV = iv;

                // doc salt tu file ma hoa
                byte[] salt = new byte[symmetricAlgorithms[algorithm].KeySize];
                fileStream.Read(salt, 0, symmetricAlgorithms[algorithm].KeySize);

                // tạo khóa từ mật khẩu
                DeriveBytes deriveBytes = new Rfc2898DeriveBytes(pass, salt, 4096);
                symmetricAlgorithms[algorithm].Key = deriveBytes.GetBytes(symmetricAlgorithms[algorithm].Key.Length);

                // bắt đầu mã hõa và ghi bản mã ra file
                byte[] buf = new byte[2048];
                int count;
                using (CryptoStream cryptoStream = new CryptoStream(new FileStream(FilePath2("DecryptSymmetric"), FileMode.OpenOrCreate, FileAccess.Write), symmetricAlgorithms[algorithm].CreateDecryptor(), CryptoStreamMode.Write))
                {
                    while ((count = fileStream.Read(buf, 0, buf.Length)) != 0)
                    {
                        cryptoStream.Write(buf, 0, count);
                    }
                }

            }
        }
        #endregion

        #region tạo mật khẩu
        public static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_+{}\":<>?/.,;'[\\/]=-|`";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        #endregion
    }
}