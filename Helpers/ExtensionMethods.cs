using CaseManagmentAPI.Models;

namespace CaseManagmentAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<CMUser> WithoutPasswords(this IEnumerable<CMUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static CMUser WithoutPassword(this CMUser user)
        {
            user.Password = null;
            return user;
        }
    }
}
