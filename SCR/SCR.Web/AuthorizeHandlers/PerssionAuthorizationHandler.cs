using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCR.Web.Rquirements;
namespace SCR.Web.AuthorizeHanders
{
    public class PermissionAuthorizationHandler : IAuthorizationHandler
    {


        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            
            //如果已经登录
            if (context.HasSucceeded)
            {
                //过期需要吗？
                //验证下权限
                var filterContext = context.Resource as AuthorizationFilterContext;
                var descriptor = filterContext?.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor == null)
                    return;
                var permission =
                    $"{descriptor.ControllerTypeInfo.Namespace}.{descriptor.ControllerTypeInfo.Name}.{descriptor.MethodInfo.Name}";
                context.Succeed(new PemissionRequirement(permission));
            }

            else
            {
                context.Fail();
            }
            
        }
    }
}
