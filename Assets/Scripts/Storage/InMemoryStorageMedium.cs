using System;
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
            return Load(key, _intData);
        }

        public void SaveFloat(string key, float value)
        {
            _floatData[key] = value;
        }

        public float LoadFloat(string key)
        {
            return Load(key, _floatData);
        }

        public void SaveString(string key, string value)
        {
            _stringData[key] = value;
        }

        public string LoadString(string key)
        {
            return Load(key, _stringData);
        }

        public void SaveBool(string key, bool value)
        {
            _boolData[key] = value;
        }

        public bool LoadBool(string key)
        {
            return Load(key, _boolData);
        }

        public void Clear()
        {
            _intData.Clear();
            _floatData.Clear();
            _stringData.Clear();
            _boolData.Clear();
        }

        private static T Load<T>(string key, Dictionary<string, T> storage)
        {
            if (storage.TryGetValue(key, out var value))
            {
                return value;
            }

            throw CreateException(key);
        }

        private static ArgumentException CreateException(string key)
        {
            return new ArgumentException($"No value is set for the key {key}.");
        }
    }
}