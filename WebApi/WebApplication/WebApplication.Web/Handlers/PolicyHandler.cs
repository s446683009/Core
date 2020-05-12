
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication.Web
{
    public class PolicyHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToUpperInvariant();
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //只有是登录了授权通过
                context.Succeed(null);
           }
            else {
              
               ///var ath=httpContext.Request.Headers["Authorization"];
               ///string token = ath.ToString().Substring("Bearer ".Length).Trim();
               /////验证token
               ///string payload =string.Empty;
               ///string message = string.Empty;
               ///var result=jwt.JWTHelper.ValidateJWT(token, "dkadladadadadaobngrgfgfgk",out payload,out message);
                //只有是登录了授权通过
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
