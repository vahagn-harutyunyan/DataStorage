using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public FileStorageMedium(string filePath)
        {
            _filePath = filePath;
            LoadFromFile();
        }

        public FileStorageMedium() : this(Path.Combine(Application.dataPath, "DataStorage.txt"))
        {
        }

        public void SaveInt(string key, int value)
        {
            _intData[key] = value;
            SaveToFile();
        }

        public int LoadInt(string key)
        {
            return _intData.Load(key);
        }

        public void SaveFloat(string key, float value)
        {
            _floatData[key] = value;
            SaveToFile();
        }

        public float LoadFloat(string key)
        {
            return _floatData.Load(key);
        }

        public void SaveString(string key, string value)
        {
            _stringData[key] = value;
            SaveToFile();
        }

        public string LoadString(string key)
        {
            return _stringData.Load(key);
        }

        public void SaveBool(string key, bool value)
        {
            _boolData[key] = value;
            SaveToFile();
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

        private void SaveToFile()
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
            File.WriteAllText(_filePath, json);
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