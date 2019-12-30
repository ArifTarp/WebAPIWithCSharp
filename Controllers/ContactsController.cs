using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnginDemiroğ.WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        [HttpGet("")]
        [Authorize(Roles = "Admin")] //Bu işlemi admin yapabilsin. AuthenticationMiddleware sınıfında claimslerde vermiştik admin rolünü
        public List<ContactModel> Get()
        {
            //veri tabanından geliyormuş gibi düşünelim
            return new List<ContactModel>
            {
                new ContactModel{Id = 1,FirstName = "Engin",LastName = "Demiroğ"}
            };
        }
    }
}