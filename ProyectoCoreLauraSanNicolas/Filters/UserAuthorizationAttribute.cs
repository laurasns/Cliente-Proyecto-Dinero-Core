using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Filters
{
    public class UserAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                String action = context.RouteData.Values["action"].ToString();
                String controller = context.RouteData.Values["controller"].ToString();

                ITempDataProvider provider = (ITempDataProvider)context.HttpContext.RequestServices.GetService(typeof(ITempDataProvider));
                var TempData = provider.LoadTempData(context.HttpContext);
                TempData["action"] = action;
                TempData["controller"] = controller;
                provider.SaveTempData(context.HttpContext, TempData);

                RouteValueDictionary route = new RouteValueDictionary(new { controller = "Calculator", action = "Unauthorize" });
                context.Result = new RedirectToRouteResult(route);
            }
        }
    }
}
