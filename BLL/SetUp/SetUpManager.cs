using System;
using System.Configuration;
using BLL.Interfaces.SetUp;

namespace BLL.Configuration
{
    public class SetUpManager : ISetUpManager
    {
        /// <inheritdoc/>
        public string ReadSetting(string key)
        {
            string result = null;

            try
            {
                result = ConfigurationManager.AppSettings[key];
            }
            catch (ConfigurationErrorsException ex)
            {
                new Exception("Can't read app settings", ex);
            }

            return result;
        }

        /// <inheritdoc/>
        public void AddUpdateSetting(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                new Exception("Can't write app settings", ex);
            }
        }
    }
}
