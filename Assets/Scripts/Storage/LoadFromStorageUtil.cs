using System;
using System.Collections.Generic;

namespace Storage
{
    public static class LoadFromStorageUtil
    {
        public static T Load<T>(this Dictionary<string, T> storage, string key)
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