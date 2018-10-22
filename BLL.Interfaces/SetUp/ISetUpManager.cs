namespace BLL.Interfaces.SetUp
{
    public interface ISetUpManager
    {
        string ReadSetting(string key);

        void AddUpdateSetting(string key, string value);
    }
}
