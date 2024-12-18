using System;
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
        public void SaveAndLoadInt()
        {
            _storageManager.SaveInt("testInt", 42);
            _storageManager.Store();
            Assert.AreEqual(42, _storageManager.LoadInt("testInt"));
        }

        [Test]
        public void SaveAndLoadFloat()
        {
            _storageManager.SaveFloat("testFloat", 3.14f);
            _storageManager.Store();
            Assert.AreEqual(3.14f, _storageManager.LoadFloat("testFloat"));
        }

        [Test]
        public void SaveAndLoadString()
        {
            _storageManager.SaveString("testString", "Hello");
            _storageManager.Store();
            Assert.AreEqual("Hello", _storageManager.LoadString("testString"));
        }

        [Test]
        public void SaveAndLoadBool()
        {
            _storageManager.SaveBool("testBool", true);
            _storageManager.Store();
            Assert.IsTrue(_storageManager.LoadBool("testBool"));

            _storageManager.SaveBool("testBoolFalse", false);
            _storageManager.Store();
            Assert.IsFalse(_storageManager.LoadBool("testBoolFalse"));
        }

        [Test]
        public void SaveAndLoadObject()
        {
            var complexObject = new TestData { Id = 123, Name = "TestName", Active = true };
            _storageManager.SaveObject("testObject", complexObject);
            _storageManager.Store();
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
        public void Clear_RemovesAllEntries()
        {
            _storageManager.SaveInt("toClear", 100);
            _storageManager.Store();
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