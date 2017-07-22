using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MvvX.Plugins.CryptoTools.Platform
{
    public class PlatformCryptoTools : ICryptoTools
    {
        #region Checksums

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

        internal string GetSHA256Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        internal string GetSHA512Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA512Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        internal string GetMD5Checksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var md5 = new MD5CryptoServiceProvider();
                byte[] checksum = md5.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        #endregion

        #region Encrypt / Decrypt Symetric

        public byte[] EncryptWithSymetric(string text, byte[] key, byte[] iv, SymetricAlgorithmType encryptionType)
        {
            switch (encryptionType)
            {
                case SymetricAlgorithmType.DES:
                    return EncryptSymetric<DESCryptoServiceProvider>(text, key, iv);
                case SymetricAlgorithmType.Aes:
                    return EncryptSymetric<AesManaged>(text, key, iv);
                case SymetricAlgorithmType.RC2:
                    return EncryptSymetric<RC2CryptoServiceProvider>(text, key, iv);
                case SymetricAlgorithmType.Rijndael:
                    return EncryptSymetric<RijndaelManaged>(text, key, iv);
                case SymetricAlgorithmType.TripleDES:
                    return EncryptSymetric<TripleDESCryptoServiceProvider>(text, key, iv);
                default:
                    return null;
            }
        }

        public string DecryptWithSymetric(byte[] cryptedTextAsByte, byte[] key, byte[] iv, SymetricAlgorithmType encryptionType)
        {
            switch (encryptionType)
            {
                case SymetricAlgorithmType.DES:
                    return DecryptSymetric<DESCryptoServiceProvider>(cryptedTextAsByte, key, iv);
                case SymetricAlgorithmType.Aes:
                    return DecryptSymetric<AesManaged>(cryptedTextAsByte, key, iv);
                case SymetricAlgorithmType.RC2:
                    return DecryptSymetric<RC2CryptoServiceProvider>(cryptedTextAsByte, key, iv);
                case SymetricAlgorithmType.Rijndael:
                    return DecryptSymetric<RijndaelManaged>(cryptedTextAsByte, key, iv);
                case SymetricAlgorithmType.TripleDES:
                    return DecryptSymetric<TripleDESCryptoServiceProvider>(cryptedTextAsByte, key, iv);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Encrypt a text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        internal byte[] EncryptSymetric<T>(string text, byte[] key, byte[] iv) where T : SymmetricAlgorithm, new()
        {
            byte[] textAsByte = Encoding.Default.GetBytes(text);
            
            // Cet objet est utilisé pour chiffrer les données.
            // Il reçoit en entrée les données en clair sous la forme d'un tableau de bytes.
            // Les données chiffrées sont également retournées sous la forme d'un tableau de bytes
            var encryptor = new T().CreateEncryptor(key, iv);

            byte[] cryptedTextAsByte = encryptor.TransformFinalBlock(textAsByte, 0, textAsByte.Length);

            return cryptedTextAsByte;
        }

        /// <summary>
        /// Decrypt a byte array
        /// </summary>
        /// <param name="cryptedTextAsByte"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        internal string DecryptSymetric<T>(byte[] cryptedTextAsByte, byte[] key, byte[] iv) where T : SymmetricAlgorithm, new()
        {
            // Cet objet est utilisé pour déchiffrer les données.
            // Il reçoit les données chiffrées sous la forme d'un tableau de bytes.
            // Les données déchiffrées sont également retournées sous la forme d'un tableau de bytes
            var decryptor = new T().CreateDecryptor(key, iv);

            byte[] decryptedTextAsByte = decryptor.TransformFinalBlock(cryptedTextAsByte, 0, cryptedTextAsByte.Length);

            return Encoding.Default.GetString(decryptedTextAsByte);
        }

        #endregion

        #region encrypt / Decrypt Asymetric
        
        /// <summary>
        /// Encrypt a text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public byte[] EncryptRSA(string value, RSAParameters rsaKeyInfo)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Récupère les infos de la clé publique
                rsa.ImportParameters(rsaKeyInfo);

                byte[] encodedData = Encoding.Default.GetBytes(value);

                // Chiffre les données.
                // Les données chiffrées sont retournées sous la forme d'un tableau de bytes
                byte[] encryptedData = rsa.Encrypt(encodedData, true);

                rsa.Clear();

                return encryptedData;
            }
        }
        public string DecryptRSA(byte[] encryptedData, RSAParameters rsaKeyInfo)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Récupère les infos de la clé privée
                rsa.ImportParameters(rsaKeyInfo);

                // Déchiffre les données.
                // Les données déchiffrées sont retournées sous la forme d'un tableau de bytes
                byte[] decryptedData = rsa.Decrypt(encryptedData, true);

                string decryptedValue = Encoding.Default.GetString(decryptedData);

                rsa.Clear();

                return decryptedValue;
            }
        }

        #endregion
    }
}