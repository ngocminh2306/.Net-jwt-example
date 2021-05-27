using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [Route("GetToken")]
        public string GetToken()
        {
            return JwtService.GenerateSecurityToken();
        }
    }
}
