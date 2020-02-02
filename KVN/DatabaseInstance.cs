using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace KVN
{
    public sealed class DatabaseInstance<T> where T : struct
    {
        private Dictionary<string, T> _cache = new Dictionary<string, T>();
        private readonly DatabaseSettings _settings;

        private bool _saveOnUpdate => _settings.ShouldSaveOnUpdate;
        private string _fileName => _settings.FileName;
        
        public DatabaseInstance(DatabaseSettings settings)
        {
            _settings = settings;
            Load();
        }

        public T this[long key]
        {
            get => _cache[key.ToString()];
            set => _cache[key.ToString()] = value;
        }

        private void Load()
        {
            var key = Utils.Locks.GetOrAdd(_fileName, _ => new object());
            
            lock (key)
            {
                using var stream = File.OpenRead(_fileName);
                var buffer = new byte[stream.Length];
                var bytes = stream.Read(buffer, 0, (int)stream.Length);
                _cache = JsonSerializer.Deserialize<Dictionary<string, T>>(buffer, _settings.Options);
            }
        }
        
        public void SaveChanges()
        {
            var key = Utils.Locks.GetOrAdd(_fileName, _ => new object());
            
            lock (key)
            {
                var bytes = JsonSerializer.SerializeToUtf8Bytes(_cache, _settings.Options);
                using var stream = File.OpenWrite(_fileName);
                stream.Write(bytes);
            }
        }
    }
}