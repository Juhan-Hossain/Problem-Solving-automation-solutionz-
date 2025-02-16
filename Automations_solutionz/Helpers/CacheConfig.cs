namespace Automations_solutionz.Helpers
{
    public static class CacheConfig
    {
        public static readonly TimeSpan SlidingExpiration = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan AbsoluteExpiration = TimeSpan.FromHours(1);
    }
}
