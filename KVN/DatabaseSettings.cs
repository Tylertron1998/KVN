using System.IO;
using System.Text.Json;

namespace KVN
{
    public struct DatabaseSettings
    {
        public DatabaseSettings(string fileName, bool saveOnUpdate = false)
        {
            FileName = fileName;
            ShouldSaveOnUpdate = saveOnUpdate;
        }
        public string FileName { get; set; }
        public bool ShouldSaveOnUpdate { get; set; }
    }
}