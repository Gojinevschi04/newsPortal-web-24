using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Enums;
using Gastronique.Web.Extension;

namespace Gastronique.Web.Filters
{
     public class ReporterModAttribute : ActionFilterAttribute

     {
          private readonly ISession _session;

          public ReporterModAttribute()
          {
               var bl = new Gastronique.BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
          }

          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var user = _session.GetUserByCookie(apiCookie.Value);
                    if (user != null && user.Level == URole.Reporter)
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