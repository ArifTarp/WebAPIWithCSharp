using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace EnginDemirog.WebApiDemo.Formatters
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));//text/vcard tipinde gelirse bunu destekle demiş oluyoruz
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        //burada gelen datayı vcard formatına getiriyoruz
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response; //gelen yanıtı aldık
            var stringBuilder = new StringBuilder();
            if (context.Object is List<ContactModel>) //liste için
            {
                foreach (ContactModel contactModel in context.Object as List<ContactModel>)
                {
                    FormatVcard(stringBuilder,contactModel);
                }
            }
            else//tek bir nesne için
            {
                ContactModel contactModel = context.Object as ContactModel;
                FormatVcard(stringBuilder,contactModel);
            }

            return response.WriteAsync(stringBuilder.ToString());
        }

        //dışarıya vereceğimiz datayı vcard formatına çevirmemiz lazım
        //normalde contactmodeli jsona çevircekti biz diyoruz ki hayır çevirme diyip aşağıda belirttik neye çevirmesini istediğimizi
        private static void FormatVcard(StringBuilder stringBuilder, ContactModel contactModel)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:2.1");
            stringBuilder.AppendLine($"N:{contactModel.LastName};{contactModel.FirstName}");
            stringBuilder.AppendLine($"FN:{contactModel.FirstName};{contactModel.LastName}");
            stringBuilder.AppendLine($"UID:{contactModel.Id}\r\n");
            stringBuilder.AppendLine("END:VCARD");
        }

        //gelen veri tipinin ContactModel olduğunu kontrol etmemiz gerekiyor
        //kontrol etmezsek farklı veri tipini vcarda dönüştürürken hata çıkabilir
        protected override bool CanWriteType(Type type)
        {
            //gelen data liste veya tek olabilir
            //gelen tipden, ContactModel tipine atama yapılabiliyor mu? Yani kısaca bu tipler birbiriyle aynı mı
            if (typeof(ContactModel).IsAssignableFrom(type) || typeof(List<ContactModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            else
            {
                return false;
            }
        }
    }
}
