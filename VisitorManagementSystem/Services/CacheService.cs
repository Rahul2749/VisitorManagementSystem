using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using VisitorManagementSystem.Data;

namespace VisitorManagementSystem.Services
{
   

    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly VMSDbContext _context;

        public CacheService(IMemoryCache cache, VMSDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task CacheDataAsync(string key, object data)
        {
            // Cache data for 1 hour
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));

            _cache.Set(key, data, cacheEntryOptions);
        }

        public async Task StoreDataInDatabaseAsync(string key)
        {
            // Get cached data
            var data = _cache.Get(key);

            // Store data in database using EF Core
            //await _dbContext.MyTable.AddAsync((MyTable)data);
            await _context.SaveChangesAsync();

            // Remove cached data after successful store
            _cache.Remove(key);
        }
    }
}
