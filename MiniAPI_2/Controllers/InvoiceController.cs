using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniAPI_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoice()
        {
            //Token ile giriş yapmış kullanıcının user Name ni almış bulunduk
            var userName = HttpContext.User.Identity.Name;

            //Token a verdiğimiz User Id bilgisine ulaştık
            var userId = User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value;

            return Ok($"User Name: {userName} & User Id: {userId} in Invoice");
        }
    }
}