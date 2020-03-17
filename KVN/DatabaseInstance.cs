using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using KVN.Providers;

namespace KVN
{
    public sealed class DatabaseInstance<T> where T : struct
    {
        private Dictionary<string, T> _cache = new Dictionary<string, T>();
        private readonly DatabaseSettings _settings;
        private bool SaveOnUpdate => _settings.ShouldSaveOnUpdate;
        private string FileName => _settings.FileName;
        private IDataProvider<T> _provider;
        
        public DatabaseInstance(DatabaseSettings settings, IDataProvider<T> provider)
        {
            _settings = settings;
            _provider = provider;
            Load();
        }

        public T this[long key]
        {
            get => Get(key);
            set => Set(key, value);
        }
        
        public void SaveChanges()
        {
            _provider.SetData(FileName, _cache);
        }
        
        private void Load()
        {
            _cache = _provider.GetData(FileName);
        }

        private void Set(long id, T value)
        {
            var key = Utils.Locks.GetOrAdd(FileName, _ => new object());

            lock (key)
            {
                _cache[id.ToString()] = value;
            }
        }

        private T Get(long id)
        {
            var key = Utils.Locks.GetOrAdd(FileName, _ => new object());

            lock (key)
            {
                return _cache[id.ToString()];
            }
        }
    }
}