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

        #region mã hóa
        public void EncryptSymmetric(int i)
        {
            byte[] encryptoData;

            string data = Encoding.UTF8.GetString(GetByteFile());

            FileInfo fileInfo = new FileInfo(filePath);
            string filePath2 = fileInfo.DirectoryName;
            string extentions = fileInfo.Extension;
            string fileName = fileInfo.Name;
            fileName = fileName.Replace(extentions, "");

            //key and iv
            byte[] keys = GetKeys(symmetricAlgorithms[i].Key.Length);
            byte[] ivs = GetIVs(symmetricAlgorithms[i].IV.Length);

            //Tạo một bộ mã hóa để thực hiện chuyển đổi luồng.
            ICryptoTransform transform = symmetricAlgorithms[i].CreateEncryptor(keys, ivs);

            //Tạo các luồng được sử dụng để mã hóa
            using (MemoryStream memory = new MemoryStream())
            {
                using (CryptoStream crypto = new CryptoStream(memory, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(crypto))
                    {
                        //viết tất cả dữ liệu vào luồng
                        writer.Write(data);
                    }
                    encryptoData = memory.ToArray();
                }

                //ghi dữ liệu đã mã hõa ra file
                File.WriteAllBytes(filePath2 + "\\Encode_" + fileName + extentions, encryptoData);
            }
        }
        #endregion

        #region giải mã
        public void DecryptSymmetric(int i)
        {
            string decryptoData;
            byte[] plainText;
            FileInfo fileInfo = new FileInfo(filePath);
            string filePath2 = fileInfo.DirectoryName;
            string extentions = fileInfo.Extension;
            string fileName = fileInfo.Name;
            fileName = fileName.Replace(extentions, "");

            //key and iv
            byte[] keys = GetKeys(symmetricAlgorithms[i].Key.Length);
            byte[] ivs = GetIVs(symmetricAlgorithms[i].IV.Length);

            //Tạo một bộ mã hóa để thực hiện chuyển đổi luồng.
            ICryptoTransform transform = symmetricAlgorithms[i].CreateDecryptor(keys, ivs);

            //tao luồng để giải mã
            using (MemoryStream memory = new MemoryStream(GetByteFile()))
            {
                using (CryptoStream crypto = new CryptoStream(memory, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(crypto))
                    {
                        //đọc các byte được giải mã từ luồng
                        decryptoData = reader.ReadToEnd();
                    }
                    plainText = Encoding.UTF8.GetBytes(decryptoData);
                }
                //ghi dữ liệu được giải mã vào file
                using (FileStream file = new FileStream(Path.Combine(filePath2 + "\\Decode_" + fileName + extentions), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    file.Write(plainText, 0, plainText.Length);
                }
            }
        }
        #endregion

        #region đọc file
        public byte[] GetByteFile()
        {
            return File.ReadAllBytes(filePath);
        }
        #endregion

        #region tạo khóa bi mật
        public byte[] GetKeys(int size)
        {
            byte[] keys = new byte[size];
            byte[] hashKeys;
            string pwd = pass + pass.Length;
            int pwdLen = pass.Length;
            HashAlgorithm hash = new SHA512CryptoServiceProvider();
            hashKeys = hash.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            for (int i = pwdLen, j = 0; i < size + pwdLen; i++, j++)
                keys[j] = hashKeys[i];
            return keys;
        }
        #endregion

        #region tạo vector khởi tạo
        public byte[] GetIVs(int size)
        {
            byte[] ivs = new byte[size];
            byte[] hashIVs;
            string psswds = pass + pass.Length;
            int pwdLen = pass.Length;
            HashAlgorithm hash = new SHA512CryptoServiceProvider();
            hashIVs = hash.ComputeHash(Encoding.UTF8.GetBytes(ReverseString(psswds)));
            for (int i = pwdLen, j = 0; i < pwdLen + size; i++, j++)
                ivs[j] = hashIVs[i];
            return ivs;
        }
        #endregion

        #region dảo ngược mật khẩu
        public string ReverseString(string s)
        {
            char[] arrays = s.ToCharArray();
            Array.Reverse(arrays);
            return new string(arrays);
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