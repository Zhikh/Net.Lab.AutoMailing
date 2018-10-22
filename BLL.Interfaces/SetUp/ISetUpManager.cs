namespace BLL.Interfaces.SetUp
{
    public interface ISetUpManager
    {
        /// <summary>
        /// Returns value for key
        /// </summary>
        /// <param name="key"> Key value of setting parameter </param>
        /// <returns> Value of setting parameter </returns>
        string ReadSetting(string key);

        /// <summary>
        /// Adds or updates pair of key and value in application settings 
        /// </summary>
        /// <param name="key"> Key </param>
        /// <param name="value"> Value </param>
        void AddUpdateSetting(string key, string value);
    }
}
