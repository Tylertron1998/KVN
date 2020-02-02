using System.Collections.Concurrent;

namespace KVN
{
    internal static class Utils
    {
        public static readonly ConcurrentDictionary<string, object> Locks = new ConcurrentDictionary<string, object>();
    }
}