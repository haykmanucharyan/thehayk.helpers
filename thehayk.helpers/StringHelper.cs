using System;

namespace thehayk.helpers
{
    public static class StringHelper
    {
        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        public static byte[] ToEncodingInvariantBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        public static string ToEncodingInvariantString(this byte[] buffer)
        {
            char[] chars = new char[buffer.Length / sizeof(char)];
            Buffer.BlockCopy(buffer, 0, chars, 0, buffer.Length);

            return new string(chars);
        }

        public static int WordFrequency(this string input, string word, bool caseSensitive = true)
        {
            int strt = 0;
            int cnt = 0;
            int idx = -1;
            
            while (strt != -1)
            {
                strt = caseSensitive ? input.IndexOf(word, idx + 1) :
                    input.IndexOf(word, idx + 1, StringComparison.CurrentCultureIgnoreCase);
                cnt += 1;
                idx = strt;
            }

            return cnt;
        }
    }
}
