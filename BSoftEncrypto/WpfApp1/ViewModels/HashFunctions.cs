using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BSoftEncryptor.Class
{
    public class HashFunction
    {
        public string FilePath { get; set; }
        //public string[] hexCodes { get; set; }

        public HashFunction(string path)
        {
            FilePath = path;
        }

        public string gethashcodes(int i)
        {
            //khởi tạo các thuật toán băm dưỡi dạng mảng
            HashAlgorithm[] hashAl = { new MD5CryptoServiceProvider(), new SHA1CryptoServiceProvider(), new SHA256CryptoServiceProvider(), new SHA512CryptoServiceProvider() };

            //lấy ra mã băm
            byte[] data = hashAl[i].ComputeHash(GetBytes());

            StringBuilder sBuilder = new StringBuilder();

            //chuyển đổi từ byte sang hex
            for (int j = 0; j < data.Length; j++)
            {
                sBuilder.Append(data[j].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public byte[] GetBytes()
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            // Create a byte array of file stream length
            byte[] bytes = new byte[fs.Length];
            //Read block of bytes from stream into the byte array
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            //Close the File Stream
            fs.Close();
            return bytes;
        }

        //veryfy hash
        public bool VeryfyHash(string hashData, string hash)
        {
            //chuyển 2 chuối cần so sanh thành kỹ tự viết hoa rồi đem so sánh
            if (hash.ToUpper().Equals(hashData.ToUpper()))
                return true;
            return false;
        }
    }
}
