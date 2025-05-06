using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Core.Service.Contracts
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, Object response, TimeSpan expireTime);
        Task<string> GetCacheKeyAsync(string key);  
    }
}
