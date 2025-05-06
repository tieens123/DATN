using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace K21CNT2_2110900086_DATN.Extension
{
    public static class IdentityExtensions
    {
        // Lấy giá trị claim từ ClaimsPrincipal
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value ?? string.Empty;
        }

        // Lấy giá trị claim từ IIdentity
        public static string GetSpecificClaim(this IIdentity identity, string claimType)
        {
            return (identity as ClaimsIdentity)?.FindFirst(claimType)?.Value ?? string.Empty;
        }

        // Các phương thức lấy thông tin cụ thể từ claim
        public static string GetAccountID(this IIdentity identity) => identity.GetSpecificClaim("AccountId");
        public static string GetRoleID(this IIdentity identity) => identity.GetSpecificClaim("RoleId");
        public static string GetUserName(this IIdentity identity) => identity.GetSpecificClaim("UserName");
        //public static string GetAvatar(this IIdentity identity) => identity.GetSpecificClaim("Avatar");
    }
}
