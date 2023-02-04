using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebUI.Infrastructure.Cache
{
    public interface ICache
    {
        bool TryGetValue(string key, out object value);
        void Set(string key, object value, int minutesToCache);
        void Remove(string key);
        
    }
}
