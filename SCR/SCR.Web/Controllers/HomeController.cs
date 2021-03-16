using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCR.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SCR.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!HttpContext.AuthenticateAsync().Result.Succeeded) {
                //必须使用有参构造函数
                //用户登录需要一个主身份
                var indentity = new ClaimsIdentity("main");
                //身份内容包含 名字，email,角色
                indentity.AddClaim(new Claim(ClaimTypes.Name, "Solo"));
                indentity.AddClaim(new Claim(ClaimTypes.Email, "446683009@qq.com"));
                indentity.AddClaim(new Claim(ClaimTypes.Role, "System"));
                indentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                indentity.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(80).ToString()));
                //次要身份可以是多个
                var subIndentity = new ClaimsIdentity("sub");
                subIndentity.AddClaim(new Claim("studentNo", "20150817001"));//学号
                subIndentity.AddClaim(new Claim("className", "计算机应用技术班"));//学号

                //创建以indentity为主身份的用户信息
                var principal = new ClaimsPrincipal(indentity);
                principal.AddIdentity(subIndentity);
                //以Cookies的方式登录 
      
                //接下来获取用户权限信息，写入缓存中
                //例如在数据库中存储角色或者用户所允许访问的controller 的名字和action的名字，在这里取出，然后写入redis 缓存中，使用用户组件来标识
                //同一个用户不同浏览器登录如何解决？貌似不用解决，权限信息不用对应每次对话
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

        

            }
            return View();

        }
        [Authorize()]
        public IActionResult Privacy()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Login() {

            return Content("登录成功");
        }

       
    }
}
