using System;
using System.Text;
using static thehayk.helpers.HashHelper;

namespace thehayk.helpers
{
    public static class SecurityHelper
    {
        public static byte[] PasswordHash(HashAlgorithmType algorithm, string password, string salt, int saltCount, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            byte[] saltBytes = encoding.GetBytes(salt);
            byte[] passwordBytes = encoding.GetBytes(password);
            byte[] concatBytes = new byte[passwordBytes.Length + 2 * saltCount * saltBytes.Length];

            int bStride = saltBytes.Length * saltCount + passwordBytes.Length;
            Buffer.BlockCopy(passwordBytes, 0, concatBytes, bStride - passwordBytes.Length, passwordBytes.Length);

            for (int q = 0; q < saltCount; q++)
            {
                Buffer.BlockCopy(saltBytes, 0, concatBytes, q * saltBytes.Length, saltBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, concatBytes, bStride + q * saltBytes.Length, saltBytes.Length);
            }

            return ComputeHash(algorithm, concatBytes);
        }

        public static byte[] SessionHash(HashAlgorithmType algorithm, long uId, DateTimeOffset timestamp, int s1Length, int s2Length)
        {
            byte[] salt1Bytes = SecureRandomHelper.GetRandomBytes(s1Length);
            byte[] salt2Bytes = SecureRandomHelper.GetRandomBytes(s2Length);
            byte[] uIdBytes = uId.ToByteArray();
            byte[] timestampBytes = timestamp.Ticks.ToByteArray();

            byte[] concatBytes = new byte[salt1Bytes.Length + salt2Bytes.Length + uIdBytes.Length + timestampBytes.Length];

            Buffer.BlockCopy(salt1Bytes, 0, concatBytes, 0, salt1Bytes.Length);
            Buffer.BlockCopy(uIdBytes, 0, concatBytes, salt1Bytes.Length, uIdBytes.Length);
            Buffer.BlockCopy(timestampBytes, 0, concatBytes, salt1Bytes.Length + uIdBytes.Length, timestampBytes.Length);
            Buffer.BlockCopy(salt2Bytes, 0, concatBytes, concatBytes.Length - salt2Bytes.Length, salt2Bytes.Length);

            return ComputeHash(algorithm, concatBytes);
        }

        public static byte[] SessionHash(HashAlgorithmType algorithm, long uId, DateTime timestamp, int s1Length, int s2Length)
        {
            return SessionHash(algorithm, uId, new DateTimeOffset(timestamp), s1Length, s2Length);
        }

        public static byte[] SessionHash(HashAlgorithmType algorithm, long uId, int s1Length, int s2Length)
        {
            return SessionHash(algorithm, uId, DateTimeOffset.Now, s1Length, s2Length);
        }
    }
}
