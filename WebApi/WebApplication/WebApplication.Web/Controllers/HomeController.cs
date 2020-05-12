﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace WebApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privity() {
            return Content("c");
        }
        [HttpPost]
        public IActionResult Login(string usid,string psw) {
            // push the user’s name into a claim, so we can identify the user later on.
            //这里可以随意加入自定义的参数，key可以自己随便起
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.NameIdentifier, usid),
                    new Claim("Role", "c")
                };
            //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            var token = new JwtSecurityToken(
                //颁发者
                issuer:"http://localhost:5000",
                    //接收者
                audience: "123",
                //过期时间
                expires: DateTime.Now.AddMinutes(60),
                //签名证书
                signingCredentials: creds,
                //自定义参数
                claims: claims
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
     

        }
        [HttpPost]
        public ActionResult Login2(string userAccount, string psw)
        {
            var payload = new Dictionary<string, object>
      {
        { "iss","流月无双"},//发行人
        { "exp", DateTimeOffset.UtcNow.AddSeconds(60).ToUnixTimeSeconds() },//到期时间
        { "sub", "testJWT" }, //主题
        { "aud", "USER" }, //用户
        { "iat", DateTime.Now.ToString() }, //发布时间 
        { "data" ,new { name="111",age=11,address="hubei"} }
      };
          var token=jwt.JWTHelper.CreateJWT(payload,"dkadladadadadaobngrgfgfgk");
            return Content(token);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
