using System;
using System.IO;
using System.Security.Cryptography;

namespace MvvX.Plugins.CryptoTools.Platform
{
    public class PlatformCryptoTools : ICryptoTools
    {
        #region CheckSums

        public string CalculateFileCheckSum(string filePath, HashType hashType)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            switch (hashType)
            {
                case HashType.MD5:
                    return GetMD5Checksum(filePath);
                case HashType.SHA256:
                    return GetSHA256Checksum(filePath);
                case HashType.SHA512:
                    return GetSHA512Checksum(filePath);
                default:
                    return string.Empty;
            }
        }

        internal static string GetSHA256Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        internal static string GetSHA512Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA512Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        internal static string GetMD5Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var md5 = new MD5CryptoServiceProvider();
                byte[] checksum = md5.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        #endregion
    }
}