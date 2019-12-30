using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.DataAccess;
using Microsoft.AspNetCore.Http;

namespace EnginDemirog.WebApiDemo.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        //middleware'ler arası geçiş için
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)//core tarafında injekte olacak
        {
            _next = next;
        }

        //middleware'i çalıştırmak için
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"]; //Authorization olan headerı yakaladık burada

            if (authHeader == null)
            {
                await _next(context);
                return;
            }

            //basic engin:12345
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase)) //authHeader boş olmayıp basic ile başıyor ise(basic büyük küçük fark etmez dedik)
            {
                var token = authHeader.Substring(6).Trim(); //basic'i ayırdık engin:12345 halinin base64lük encode'u kaldı
                var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token)); //base64'den stringe çevirdik. elimizde engin:12345 kaldı
                var credentials = credentialString.Split(":");//stringi böldük 2 elemanlı liste oldu elimizde
                if (credentials[0] == "engin" && credentials[1] == "12345")//ifdeki engin ile 12345 veritabanından aldığımızı düşün
                {//bu if sağlanırsa yani doğrulama true çıkarsa admin rolünü veriyoruz
                    //bu claim iddia etmek anlamında ve iddia doğruysa istenilen bilgileri encode edip token halinde veriyor
                    //mesela angulara client tarafına parola göndermeyiz ama user ve role gönderebiliriz
                    var claims = new[]
                    {
                        new Claim("name", credentials[0]),
                        new Claim(ClaimTypes.Role,"Admin")
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    context.Response.StatusCode = 401;//authanticated değil
                }

            }
            else
            {
                context.Response.StatusCode = 500;
            }

            await _next(context);
        }
    }
}
