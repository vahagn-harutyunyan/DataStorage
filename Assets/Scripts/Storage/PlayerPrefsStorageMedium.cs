using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Storage
{
    public class PlayerPrefsStorageMedium : IStorageMedium
    {
        public bool HasValue<T>(string name)
        {
            var type = typeof(T);
            if (type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(bool) ||
                type == typeof(string))
            {
                return PlayerPrefs.HasKey(name);
            }

            throw new ArgumentException($"Invalid data type {type}.");
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }

            throw CreateException(key);
        }

        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float LoadFloat(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetFloat(key);
            }

            throw CreateException(key);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }

            throw CreateException(key);
        }

        public void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public bool LoadBool(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) == 1;
            }

            throw CreateException(key);
        }

        public void Store()
        {
            PlayerPrefs.Save();
        }

        public Task StoreAsync()
        {
            Store();
            return Task.CompletedTask;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        private static ArgumentException CreateException(string key)
        {
            return new ArgumentException($"No value is set for the key {key}.");
        }
    }
}