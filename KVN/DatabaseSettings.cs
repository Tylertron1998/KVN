using System.IO;
using System.Text.Json;

namespace KVN
{
    public struct DatabaseSettings
    {
        public DatabaseSettings(string fileName, JsonSerializerOptions options = default, bool saveOnUpdate = false)
        {
            FileName = fileName;
            Options = options ?? new JsonSerializerOptions();
            ShouldSaveOnUpdate = saveOnUpdate;
        }
        public string FileName { get; set; }
        public JsonSerializerOptions Options { get; set; }
        public bool ShouldSaveOnUpdate { get; set; }
    }
}