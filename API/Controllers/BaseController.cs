using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API.Controllers{
        public class BaseController : ControllerBase
    {
        public string Email {
            get
            {
                return this.User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            }
        }
    }
}