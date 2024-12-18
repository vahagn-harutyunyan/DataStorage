using System.Collections.Generic;

namespace Storage
{
    public class InMemoryStorageMedium : IStorageMedium
    {
        private readonly Dictionary<string, int> _intData = new Dictionary<string, int>();
        private readonly Dictionary<string, float> _floatData = new Dictionary<string, float>();
        private readonly Dictionary<string, string> _stringData = new Dictionary<string, string>();
        private readonly Dictionary<string, bool> _boolData = new Dictionary<string, bool>();

        public void SaveInt(string key, int value)
        {
            _intData[key] = value;
        }

        public int LoadInt(string key)
        {
            return _intData.Load(key);
        }

        public void SaveFloat(string key, float value)
        {
            _floatData[key] = value;
        }

        public float LoadFloat(string key)
        {
            return _floatData.Load(key);
        }

        public void SaveString(string key, string value)
        {
            _stringData[key] = value;
        }

        public string LoadString(string key)
        {
            return _stringData.Load(key);
        }

        public void SaveBool(string key, bool value)
        {
            _boolData[key] = value;
        }

        public bool LoadBool(string key)
        {
            return _boolData.Load(key);
        }

        public void Store()
        {
            //
        }

        public void Clear()
        {
            _intData.Clear();
            _floatData.Clear();
            _stringData.Clear();
            _boolData.Clear();
        }
    }
}