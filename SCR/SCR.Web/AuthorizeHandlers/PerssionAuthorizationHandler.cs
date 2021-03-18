using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCR.Web.AuthorizeHanders
{
    public class PermissionAuthorizationHandler : IAuthorizationHandler
    {


        public Task HandleAsync(AuthorizationHandlerContext context)
        {
        
            HttpContext httpContext = filterContext.HttpContext;

            var controllerName = httpContext.Request.RouteValues["controller"].ToString();
            var actionName = httpContext.Request.RouteValues["action"].ToString();

            
            //如果已经登录
            if (context.HasSucceeded)
            {
                //过期需要吗？
                //验证下权限


            }

            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
