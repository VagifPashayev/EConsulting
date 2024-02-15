using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EConsult.Controllers
{
    [Route("conference")]
    [Authorize]
    public class ConferenceController : Controller
    {
        [HttpGet("/conference/{roomId}")]
        public IActionResult Chat(string roomId)
        {
            //!!!user check deleted for presentation!!!
            ViewBag.RoomId = roomId;
            return View();
        }
    }
}
