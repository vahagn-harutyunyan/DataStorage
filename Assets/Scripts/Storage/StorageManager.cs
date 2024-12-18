using Newtonsoft.Json;

namespace Storage
{
    public class StorageManager
    {
        private readonly IStorageMedium _storageMedium;

        public StorageManager(IStorageMedium storageMedium)
        {
            _storageMedium = storageMedium;
        }

        public void SaveInt(string key, int value)
        {
            _storageMedium.SaveInt(key, value);
        }

        public int LoadInt(string key)
        {
            return _storageMedium.LoadInt(key);
        }

        public void SaveFloat(string key, float value)
        {
            _storageMedium.SaveFloat(key, value);
        }

        public float LoadFloat(string key)
        {
            return _storageMedium.LoadFloat(key);
        }

        public void SaveString(string key, string value)
        {
            _storageMedium.SaveString(key, value);
        }

        public string LoadString(string key)
        {
            return _storageMedium.LoadString(key);
        }

        public void SaveBool(string key, bool value)
        {
            _storageMedium.SaveBool(key, value);
        }

        public bool LoadBool(string key)
        {
            return _storageMedium.LoadBool(key);
        }

        public void SaveObject<T>(string key, T value)
        {
            SaveString(key, JsonConvert.SerializeObject(value));
        }

        public T LoadObject<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(LoadString(key));
        }

        public void Clear()
        {
            _storageMedium.Clear();
        }
    }
}