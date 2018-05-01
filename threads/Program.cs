using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace threads
{
    class Program
    {
        /// <summary>
        /// Equivalent to Ruby Digest::MD5.hexdigest(input)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UInt64 MD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToUInt64(bytes, 0);
            }
        }

        static void Main(string[] args)
        {
            UInt64 Maximum = UInt64.MaxValue;
            UInt64 Difficulty = 1000000;
            UInt64 Goal = Maximum / Difficulty;
            // double NumHashes = 5;

            var results = new ConcurrentDictionary<string, ulong>();
            var toBeHashed = "abc";

            while (results.Values.Count < 5)
            {
                var hash = MD5Hash(toBeHashed);
                if (hash < Goal)
                {
                    Console.WriteLine(hash);
                    results.TryAdd(toBeHashed, hash);
                }
                toBeHashed = hash.ToString();
            }
        }
    }
}
