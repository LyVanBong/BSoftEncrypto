using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BSoftEncryptor.Class
{
    public class HashFunction
    {
        private string FilePath;
        private HashAlgorithm[] hashAl = { new MD5CryptoServiceProvider(), new SHA1CryptoServiceProvider(), new SHA256CryptoServiceProvider(), new SHA512CryptoServiceProvider() };

        public HashFunction(string path)
        {
            FilePath = path;
        }

        #region hàm băm
        public string gethashcodes(int i)
        {
            byte[] hashValue;
            using (FileStream file = File.OpenRead(FilePath))
            {
                hashValue = hashAl[i].ComputeHash(file);
            }
            StringBuilder sBuilder = new StringBuilder();

            //chuyển đổi từ byte sang hex
            for (int j = 0; j < hashValue.Length; j++)
            {
                sBuilder.Append(hashValue[j].ToString("x2"));
            }
            return sBuilder.ToString();

        }
        #endregion

        #region xác thực hash code
        public bool VeryfyHash(string hashData, string hash)
        {
            //chuyển 2 chuối cần so sanh thành kỹ tự viết hoa rồi đem so sánh
            if (hash.ToUpper().Equals(hashData.ToUpper()))
                return true;
            return false;
        }
        #endregion
    }
}