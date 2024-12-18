using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Storage.Editor.Tests
{
    public class StorageManagerTests
    {
        private StorageManager _storageManager;
        private InMemoryStorageMedium _inMemoryStorage;

        [SetUp]
        public void SetUp()
        {
            _inMemoryStorage = new InMemoryStorageMedium();
            _storageManager = new StorageManager(_inMemoryStorage);
        }

        [Test]
        public async Task SaveAndLoadInt()
        {
            _storageManager.SaveInt("testInt", 42);
            await _storageManager.StoreAsync();
            Assert.AreEqual(42, _storageManager.LoadInt("testInt"));
        }

        [Test]
        public async Task SaveAndLoadFloat()
        {
            _storageManager.SaveFloat("testFloat", 3.14f);
            await _storageManager.StoreAsync();
            Assert.AreEqual(3.14f, _storageManager.LoadFloat("testFloat"));
        }

        [Test]
        public async Task SaveAndLoadString()
        {
            _storageManager.SaveString("testString", "Hello");
            await _storageManager.StoreAsync();
            Assert.AreEqual("Hello", _storageManager.LoadString("testString"));
        }

        [Test]
        public async Task SaveAndLoadBool()
        {
            _storageManager.SaveBool("testBool", true);
            await _storageManager.StoreAsync();
            Assert.IsTrue(_storageManager.LoadBool("testBool"));

            _storageManager.SaveBool("testBoolFalse", false);
            await _storageManager.StoreAsync();
            Assert.IsFalse(_storageManager.LoadBool("testBoolFalse"));
        }

        [Test]
        public async Task SaveAndLoadObject()
        {
            var complexObject = new TestData { Id = 123, Name = "TestName", Active = true };
            _storageManager.SaveObject("testObject", complexObject);
            await _storageManager.StoreAsync();
            var loadedObject = _storageManager.LoadObject<TestData>("testObject");
            Assert.NotNull(loadedObject);
            Assert.AreEqual(complexObject.Id, loadedObject.Id);
            Assert.AreEqual(complexObject.Name, loadedObject.Name);
            Assert.AreEqual(complexObject.Active, loadedObject.Active);
        }

        [Test]
        public void LoadNonExistentKey_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _storageManager.LoadInt("nonExistentKey"));
        }

        [Test]
        public async Task Clear_RemovesAllEntries()
        {
            _storageManager.SaveInt("toClear", 100);
            await _storageManager.StoreAsync();
            _storageManager.Clear();
            Assert.Throws<ArgumentException>(() => _storageManager.LoadInt("toClear"));
        }

        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }
}