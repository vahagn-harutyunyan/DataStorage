using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Storage.Editor.Tests
{
    [TestFixture(typeof(PlayerPrefsStorageMedium))]
    [TestFixture(typeof(InMemoryStorageMedium))]
    [TestFixture(typeof(FileStorageMedium))]
    public class StorageMediumTests<T> where T : IStorageMedium, new()
    {
        private IStorageMedium _storage;

        [SetUp]
        public void SetUp()
        {
            _storage = new T();
            _storage.Clear();
        }

        #region Int Tests

        [Test]
        public async Task SaveAndLoadInt_StoresAndRetrievesValue()
        {
            const string key = "TestInt";
            const int valueToSave = 42;
            _storage.SaveInt(key, valueToSave);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadInt(key);
            Assert.AreEqual(valueToSave, loadedValue);
        }

        [Test]
        public void LoadInt_NonExistentKey_ThrowsException()
        {
            const string key = "NonExistentIntKey";
            Assert.Throws<ArgumentException>(() => _storage.LoadInt(key));
        }

        [Test]
        public async Task OverwriteExistingIntKey()
        {
            const string key = "OverwrittenIntKey";
            _storage.SaveInt(key, 100);
            await _storage.StoreAsync();
            _storage.SaveInt(key, 200);
            await _storage.StoreAsync();
            Assert.AreEqual(200, _storage.LoadInt(key));
        }

        #endregion

        #region Float Tests

        [Test]
        public async Task SaveAndLoadFloat_StoresAndRetrievesValue()
        {
            const string key = "TestFloat";
            const float valueToSave = 3.14159f;
            _storage.SaveFloat(key, valueToSave);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadFloat(key);
            Assert.AreEqual(valueToSave, loadedValue, 0.0001f);
        }

        [Test]
        public void LoadFloat_NonExistentKey_ThrowsException()
        {
            const string key = "NonExistentFloatKey";
            Assert.Throws<ArgumentException>(() => _storage.LoadFloat(key));
        }

        [Test]
        public async Task OverwriteExistingFloatKey()
        {
            const string key = "OverwrittenFloatKey";
            _storage.SaveFloat(key, 1.23f);
            await _storage.StoreAsync();
            _storage.SaveFloat(key, 4.56f);
            await _storage.StoreAsync();
            Assert.AreEqual(4.56f, _storage.LoadFloat(key), 0.0001f);
        }

        #endregion

        #region String Tests

        [Test]
        public async Task SaveAndLoadString_StoresAndRetrievesValue()
        {
            const string key = "TestString";
            const string valueToSave = "Hello, World!";
            _storage.SaveString(key, valueToSave);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadString(key);
            Assert.AreEqual(valueToSave, loadedValue);
        }

        [Test]
        public void LoadString_NonExistentKey_ThrowsException()
        {
            const string key = "NonExistentStringKey";
            Assert.Throws<ArgumentException>(() => _storage.LoadString(key));
        }

        [Test]
        public async Task OverwriteExistingStringKey()
        {
            const string key = "OverwrittenStringKey";
            _storage.SaveString(key, "FirstValue");
            await _storage.StoreAsync();
            _storage.SaveString(key, "SecondValue");
            await _storage.StoreAsync();
            Assert.AreEqual("SecondValue", _storage.LoadString(key));
        }

        [Test]
        public async Task SaveAndLoadEmptyString_StoresAndRetrievesValue()
        {
            const string key = "EmptyStringKey";
            var valueToSave = string.Empty;
            _storage.SaveString(key, valueToSave);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadString(key);
            Assert.AreEqual(valueToSave, loadedValue);
        }

        #endregion

        #region Bool Tests

        [Test]
        public async Task SaveAndLoadBool_TrueValue()
        {
            const string key = "TestBoolTrue";
            _storage.SaveBool(key, true);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadBool(key);
            Assert.IsTrue(loadedValue);
        }

        [Test]
        public async Task SaveAndLoadBool_FalseValue()
        {
            const string key = "TestBoolFalse";
            _storage.SaveBool(key, false);
            await _storage.StoreAsync();
            var loadedValue = _storage.LoadBool(key);
            Assert.IsFalse(loadedValue);
        }

        [Test]
        public void LoadBool_NonExistentKey_ThrowsException()
        {
            const string key = "NonExistentBoolKey";
            Assert.Throws<ArgumentException>(() => _storage.LoadBool(key));
        }

        [Test]
        public async Task OverwriteExistingBoolKey()
        {
            const string key = "OverwrittenBoolKey";
            _storage.SaveBool(key, true);
            await _storage.StoreAsync();
            _storage.SaveBool(key, false);
            await _storage.StoreAsync();
            Assert.IsFalse(_storage.LoadBool(key));
        }

        #endregion
    }
}