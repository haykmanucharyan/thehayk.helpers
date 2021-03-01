using System.Security.Cryptography;
using System.Text;

namespace thehayk.helpers
{
    public static class SecureRandomHelper
    {
        public static byte[] GetRandomBytes(int quantity)
        {
            byte[] bytes = new byte[quantity];

            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetBytes(bytes);

            return bytes;
        }

        public static int GetRandomInt32()
        {
            byte[] bytes = GetRandomBytes(4);
            int num = 0;

            for (int i = 0; i < 4; i++)
                num |= bytes[i] << (24 - i * 8);

            return num;
        }

        public static long GetRandomInt64()
        {
            byte[] bytes = GetRandomBytes(8);
            long num = 0;

            for (int i = 0; i < 8; i++)
                num |= (long)bytes[i] << (56 - i * 8);

            return num;
        }

        public static string GetRandomString(int length)
        {
            byte[] bytes = GetRandomBytes(length);

            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < bytes.Length; i++)
                sb.Append((char)bytes[i]);

            return sb.ToString();
        }
    }
}
