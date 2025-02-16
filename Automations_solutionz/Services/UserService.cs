using Automations_solutionz.Data;
using Automations_solutionz.Entity;
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
                if(_cache.TryGetValue(UserCacheKey,out IEnumerable<User> users))
                {
                    return users;
                }

                // if cache miss:

                users = await _context
                               .Users.AsNoTracking().ToListAsync();

                var cacheOptions = new MemoryCacheOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)).SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(UserCacheKey, users, cacheOptions);
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
