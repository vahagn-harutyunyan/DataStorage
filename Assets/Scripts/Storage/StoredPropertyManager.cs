using System.Threading.Tasks;

namespace Storage
{
    public class StoredPropertyManager
    {
        private readonly StorageManager _storageManager;

        public StringStoredProperty PlayerName { get; private set; }
        public IntStoredProperty PlayerLevel { get; private set; }

        public StoredPropertyManager()
        {
            _storageManager = new StorageManager(new FileStorageMedium());
            PlayerName = new StringStoredProperty("PlayerName", "Unset", _storageManager, false);
            PlayerLevel = new IntStoredProperty("PlayerLevel", 0, _storageManager, true);
        }

        public async Task StoreProperties()
        {
            await _storageManager.StoreAsync();
        }
    }
}