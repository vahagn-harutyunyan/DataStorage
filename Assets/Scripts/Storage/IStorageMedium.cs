namespace Storage
{
    public interface IStorageMedium
    {
        bool HasValue<T>(string name);
        
        void SaveInt(string key, int value);
        int LoadInt(string key);

        void SaveFloat(string key, float value);
        float LoadFloat(string key);

        void SaveString(string key, string value);
        string LoadString(string key);

        void SaveBool(string key, bool value);
        bool LoadBool(string key);

        void Store();

        void Clear();
    }
}