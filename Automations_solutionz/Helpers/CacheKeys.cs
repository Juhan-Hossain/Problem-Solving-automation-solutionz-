namespace Automations_solutionz.Helpers
{
    public static class CacheKeys
    {
        public const string UsersList = "Users_List";
        public static string UserById(int id) => $"User_{id}";
    }
}
