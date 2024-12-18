using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class SerializableDictionary
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();
        public List<string> types = new List<string>();
    }

    public class FileStorageMedium : IStorageMedium
    {
        private const string TypeInt = "int";
        private const string TypeFloat = "float";
        private const string TypeBool = "bool";
        private const string TypeString = "string";

        private readonly string _filePath;

        private readonly Dictionary<string, int> _intData = new Dictionary<string, int>();
        private readonly Dictionary<string, float> _floatData = new Dictionary<string, float>();
        private readonly Dictionary<string, bool> _boolData = new Dictionary<string, bool>();
        private readonly Dictionary<string, string> _stringData = new Dictionary<string, string>();
        
        private readonly SemaphoreSlim _storeSemaphore = new SemaphoreSlim(1, 1);

        public FileStorageMedium(string filePath)
        {
            _filePath = filePath;
            LoadFromFile();
        }

        public FileStorageMedium() : this(Path.Combine(Application.dataPath, "DataStorage.txt"))
        {
        }

        public bool HasValue<T>(string name)
        {
            var type = typeof(T);
            if (type ==  typeof(int))
            {
                return _intData.ContainsKey(name);
            }

            if (type ==  typeof(float))
            {
                return _floatData.ContainsKey(name);
            }

            if (type ==  typeof(bool))
            {
                return _boolData.ContainsKey(name);
            }

            if (type ==  typeof(string))
            {
                return _stringData.ContainsKey(name);
            }

            throw new ArgumentException($"Invalid data type {type}.");
        }

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

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, string.Empty);
            }

            var json = File.ReadAllText(_filePath);

            var dict = JsonUtility.FromJson<SerializableDictionary>(json) ?? new SerializableDictionary();

            for (var i = 0; i < dict.keys.Count; i++)
            {
                var key = dict.keys[i];
                var type = dict.types[i];
                var valueStr = dict.values[i];

                switch (type)
                {
                    case TypeInt:
                        _intData[key] = int.Parse(valueStr);
                        break;
                    case TypeFloat:
                        _floatData[key] = float.Parse(valueStr);
                        break;
                    case TypeBool:
                        _boolData[key] = bool.Parse(valueStr);
                        break;
                    case TypeString:
                        _stringData[key] = valueStr;
                        break;
                    default:
                        throw new ArgumentException($"Invalid data type {type}.");
                }
            }
        }

        public void Store()
        {
            Task.Run(async () => await StoreAsync()).Wait();
        }

        public async Task StoreAsync()
        {
            var dict = new SerializableDictionary();
            foreach (var kvp in _intData)
            {
                dict.keys.Add(kvp.Key);
                dict.types.Add(TypeInt);
                dict.values.Add(kvp.Value.ToString());
            }

            foreach (var kvp in _floatData)
            {
                dict.keys.Add(kvp.Key);
                dict.types.Add(TypeFloat);
                dict.values.Add(kvp.Value.ToString(CultureInfo.InvariantCulture));
            }

            foreach (var kvp in _boolData)
            {
                dict.keys.Add(kvp.Key);
                dict.types.Add(TypeBool);
                dict.values.Add(kvp.Value.ToString());
            }

            foreach (var kvp in _stringData)
            {
                dict.keys.Add(kvp.Key);
                dict.types.Add(TypeString);
                dict.values.Add(kvp.Value);
            }

            var json = JsonUtility.ToJson(dict, true);
            await _storeSemaphore.WaitAsync();
            await File.WriteAllTextAsync(_filePath, json);
            _storeSemaphore.Release();
        }

        public void Clear()
        {
            _intData.Clear();
            _floatData.Clear();
            _boolData.Clear();
            _stringData.Clear();
        }
    }
}