using System.Security.Cryptography;
using System.Text;

namespace thehayk.helpers
{
    public static class HashHelper
    {
        public enum HashAlgorithmType : byte
        {
            MD5 = 0,
            SHA1 = 1,
            SHA256 = 2,
            SHA512 = 3
        }

        private static HashAlgorithm GetAlgorithm(HashAlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case HashAlgorithmType.MD5:
                    return MD5.Create();

                case HashAlgorithmType.SHA1:
                    return SHA1.Create();

                case HashAlgorithmType.SHA256:
                    return SHA256.Create();

                default:
                    return SHA512.Create();
            }
        }

        public static byte[] ComputeHash(HashAlgorithmType algorithmType, byte[] data)
        {
            using (HashAlgorithm algorithm = GetAlgorithm(algorithmType))
                return algorithm.ComputeHash(data);
        }

        public static byte[] ComputeHash(HashAlgorithmType algorithmType, string data, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            return ComputeHash(algorithmType, encoding.GetBytes(data));
        }
    }
}
