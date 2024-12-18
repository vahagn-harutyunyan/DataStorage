using System;

namespace Storage
{
    public abstract class StoredProperty<T>
    {
        protected readonly string Name;
        protected readonly StorageManager StorageManager;

        public Action Updated;
        public bool IsBatchedStorage { get; }

        protected StoredProperty(string name, T defaultValue, StorageManager storageManager, bool isBatchedStorage)
        {
            Name = name;
            StorageManager = storageManager;
            IsBatchedStorage = isBatchedStorage;

            if (!HasValue())
            {
                SetValue(Name, defaultValue);
            }
        }

        protected abstract bool HasValue();

        protected abstract T GetValue();

        private void SetValue(string name, T value)
        {
            SetValueInternal(name, value);
            Updated?.Invoke();
            if (!IsBatchedStorage)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                StorageManager.StoreAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        protected abstract void SetValueInternal(string name, T value);
    }

    public class IntStoredProperty : StoredProperty<int>
    {
        public IntStoredProperty(string name, int defaultValue, StorageManager storageManager, bool isBatchedStorage) :
            base(name, defaultValue, storageManager, isBatchedStorage)
        {
        }

        protected override bool HasValue()
        {
            return StorageManager.HasValue<int>(Name);
        }

        protected override int GetValue()
        {
            return StorageManager.LoadInt(Name);
        }

        protected override void SetValueInternal(string name, int value)
        {
            StorageManager.SaveInt(name, value);
        }
    }

    public class FloatStoredProperty : StoredProperty<float>
    {
        public FloatStoredProperty(string name, float defaultValue, StorageManager storageManager,
            bool isBatchedStorage) : base(name, defaultValue, storageManager, isBatchedStorage)
        {
        }

        protected override bool HasValue()
        {
            return StorageManager.HasValue<float>(Name);
        }

        protected override float GetValue()
        {
            return StorageManager.LoadFloat(Name);
        }

        protected override void SetValueInternal(string name, float value)
        {
            StorageManager.SaveFloat(name, value);
        }
    }

    public class BoolStoredProperty : StoredProperty<bool>
    {
        public BoolStoredProperty(string name, bool defaultValue, StorageManager storageManager,
            bool isBatchedStorage) : base(name, defaultValue, storageManager, isBatchedStorage)
        {
        }

        protected override bool HasValue()
        {
            return StorageManager.HasValue<bool>(Name);
        }

        protected override bool GetValue()
        {
            return StorageManager.LoadBool(Name);
        }

        protected override void SetValueInternal(string name, bool value)
        {
            StorageManager.SaveBool(name, value);
        }
    }

    public class StringStoredProperty : StoredProperty<string>
    {
        public StringStoredProperty(string name, string defaultValue, StorageManager storageManager,
            bool isBatchedStorage) : base(name, defaultValue, storageManager, isBatchedStorage)
        {
        }

        protected override bool HasValue()
        {
            return StorageManager.HasValue<string>(Name);
        }

        protected override string GetValue()
        {
            return StorageManager.LoadString(Name);
        }

        protected override void SetValueInternal(string name, string value)
        {
            StorageManager.SaveString(name, value);
        }
    }

    public class ObjectStoredProperty<T> : StoredProperty<object> where T : new()
    {
        public ObjectStoredProperty(string name, T defaultValue, StorageManager storageManager, bool isBatchedStorage) :
            base(name, defaultValue, storageManager, isBatchedStorage)
        {
        }

        protected override bool HasValue()
        {
            return StorageManager.HasValue<T>(Name);
        }

        protected override object GetValue()
        {
            return StorageManager.LoadObject<T>(Name);
        }

        protected override void SetValueInternal(string name, object value)
        {
            StorageManager.SaveObject(name, value);
        }
    }
}