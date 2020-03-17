using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace KVN.Providers
{
    public class JsonProvider<T> : IDataProvider<T> where T : struct
    {
        public Dictionary<string, T> GetData(string name)
        {
            var key = Utils.Locks.GetOrAdd(name, _ => new object());
            lock (key)
            {
                using var stream = File.OpenRead(name);
                var buffer = new byte[stream.Length];
                var bytes = stream.Read(buffer, 0, (int) stream.Length);
                return JsonSerializer.Deserialize<Dictionary<string, T>>(buffer);
            }
        }

        public void SetData(string name, Dictionary<string, T> value)
        {
            var key = Utils.Locks.GetOrAdd(name, _ => new object());
            lock (key)
            {
                var bytes = JsonSerializer.SerializeToUtf8Bytes(value);
                using var stream = File.OpenWrite(name);
                stream.Write(bytes);
            }
        }
    }
}