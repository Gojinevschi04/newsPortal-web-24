using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Enums;
using NewsPortal.Web.Extension;

namespace NewsPortal.Web.Filters
{
     public class AdminModAttribute : ActionFilterAttribute
     {
          private readonly ISession _session;

          public AdminModAttribute()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
          }

          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var user = _session.GetUserByCookie(apiCookie.Value);
                    if (user != null && user.Level == URole.Admin)
                    {
                         HttpContext.Current.SetMySessionObject(user);
                         filterContext.Controller.ViewBag.AuthorizedUser = user;
                    }
                    else
                    {
                         filterContext.Result =
                             new RedirectToRouteResult(new RouteValueDictionary(new
                             { controller = "Login", action = "Index" }));
                    }
               }
               else
               {
                    filterContext.Result =
                        new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
               }
          }
     }
}