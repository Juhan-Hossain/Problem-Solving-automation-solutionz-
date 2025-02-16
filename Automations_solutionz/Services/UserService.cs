using Automations_solutionz.Data;
using Automations_solutionz.Entity;
using Automations_solutionz.Helpers;
using Automations_solutionz.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Automations_solutionz.Services
{
    public class UserService : IUserService
    {
        private readonly IUserService _userService;
        private readonly ILogger<IUserService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public UserService(ILogger<IUserService> logger, ApplicationDbContext context, IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }
        public async Task<IEnumerable<User>> GetUserAsync()
        {
            try
            {
                if(_cache.TryGetValue(CacheKeys.UsersList, out IEnumerable<User> users))
                {
                    _logger.LogDebug("Cache hit for users list");
                    return users;
                }

                // if cache miss:
                _logger.LogDebug("Cache miss for users list");
                users = await _context
                               .Users.AsNoTracking()
                               .ToListAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                               .SetSlidingExpiration(CacheConfig.SlidingExpiration)
                               .SetAbsoluteExpiration(CacheConfig.AbsoluteExpiration)
                               .RegisterPostEvictionCallback((key, value, reason, state) =>
                               {
                                   _logger.LogInformation(
                                       "Users list cache entry was evicted. Reason: {Reason}",
                                       reason);
                               });

                _cache.Set(CacheKeys.UsersList, users, cacheOptions);
                return users;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error retriving users from database");
                throw;
            }
        } 
    }
}
