using System.Collections.Generic;
using System.Threading.Tasks;

namespace KVN.Providers
{
    public interface IDataProvider<T> where T : struct
    {
        Dictionary<string, T> GetData(string name);
        void SetData(string name, Dictionary<string, T> value);
    }
}