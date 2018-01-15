namespace MvvX.Plugins.CryptoTools
{
	public interface ICryptoTools
	{
		/// <summary>
		/// Calculate the checksum of a file
		/// </summary>
		/// <param name="filePath">Full file path</param>
		/// <param name="hashType">Type of hash</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FileNotFoundException"></exception>
		string CalculateFileCheckSum(string filePath, HashType hashType);

        /// <summary>
        /// Generate ahash
        /// </summary>
        /// <param name="content">Full string content</param>
        /// <param name="hashType">Type of hash</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        string ComputeHash(string content, HashType hashType);

        /// <summary>
        /// Encrypt a string with a symetric algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        byte[] EncryptWithSymetric(string text, byte[] key, byte[] iv, SymetricAlgorithmType encryptionType);

        /// <summary>
        /// Decrypt a string with a symetric algorithm
        /// </summary>
        /// <param name="cryptedTextAsByte"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        string DecryptWithSymetric(byte[] cryptedTextAsByte, byte[] key, byte[] iv, SymetricAlgorithmType encryptionType);
    }
}
