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
	}
}
