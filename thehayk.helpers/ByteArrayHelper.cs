using System;
using System.Text;

namespace thehayk.helpers
{
    public static class ByteArrayHelper
    {
        public static byte[] ConcatArrays(params byte[][] arrays)
        {
            int L = 0;
            foreach (byte[] array in arrays)
                L += array.Length;

            byte[] concat = new byte[L];
            int index = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, concat, index, array.Length);
                index += array.Length;
            }

            return concat;
        }

        public static string ToBase64(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] ToArrayFromBase64(this string data)
        {
            return Convert.FromBase64String(data);
        }

        public static string ToHexString(this byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 2);

            foreach (byte b in data)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public static byte[] ToArrayFromHexString(this string data)
        {
            byte[] bytes = new byte[data.Length / 2];

            for (int i = 0; i < data.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);

            return bytes;
        }

        public static byte[] ToByteArray(this int num)
        {
            byte[] array = new byte[4];
            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static int ToInt32(this byte[] array)
        {
            int num = 0;
            for (int i = 0; i < array.Length; i++)
                num |= array[i] << i * 8;

            return num;
        }

        public static byte[] ToByteArray(this long num)
        {
            byte[] array = new byte[8];
            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static long ToInt64(this byte[] array)
        {
            long num = 0;
            for (int i = 0; i < array.Length; i++)
                num |= (long)array[i] << i * 8;

            return num;
        }

        public static bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1 == null && array2 == null)
                return true;

            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
                if (array1[i] != array2[i])
                    return false;

            return true;
        }

        public static bool IsEqualTo(this byte[] array1, byte[] array2)
        {
            return ArraysAreEqual(array1, array2);
        }
    }
}
