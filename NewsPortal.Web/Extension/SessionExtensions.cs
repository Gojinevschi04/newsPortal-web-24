using System.Web;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Domain.Enums;

namespace NewsPortal.Web.Extension
{
    // public class SessionExtensions
    //
    // {
    //     public static UDbTable GetUser(this HttpSessionStateBase session)
    //     {
    //         return (UDbTable)session["__User"];
    //     }
    //
    //     public static void ClearUser(this HttpSessionStateBase session)
    //     {
    //         session.Remove("__User");
    //     }
    //
    //     public static void SetUser(this HttpSessionStateBase session, UDbTable user)
    //     {
    //         session["__User"] = user;
    //     }
    //
    //     public static bool IsUserLoggedIn(this HttpSessionStateBase session)
    //     {
    //         return session.GetUser() != null;
    //     }
    //
    //     public static bool UserHasRole(this HttpSessionStateBase session, URole role)
    //     {
    //         if (!session.IsUserLoggedIn())
    //             return false;
    //
    //         var user = session.GetUser();
    //         return user.AccessLevel >= role;
    //     }
    // }
}