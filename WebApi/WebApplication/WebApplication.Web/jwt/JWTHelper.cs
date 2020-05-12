using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace WebApplication.Web.jwt
{
    public  static class JWTHelper
    {
        static IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//HMACSHA256加密
        static IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
        static IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
        static IDateTimeProvider provider = new UtcDateTimeProvider();//UTC时间获取
        public static string CreateJWT(Dictionary<string, object> payload,string secret)
        {
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }
        public static bool ValidateJWT(string token, string secret,out string payload, out string message)
        {
            bool isValidted = false;
            payload = "";
            try
            {

                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                payload = decoder.Decode(token, secret, verify: true);

                isValidted = true;

                message = "验证成功";
            }
            catch (TokenExpiredException)//当前时间大于负载过期时间（负荷中的exp），会引发Token过期异常
            {
                message = "过期了！";
            }
            catch (SignatureVerificationException)//如果签名不匹配，引发签名验证异常
            {
                message = "签名错误！";
            }
            return isValidted;
        }
    }
}
