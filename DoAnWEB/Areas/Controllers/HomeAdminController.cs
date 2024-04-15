using DoAnWEB.Areas.Identity.Data;
using DoAnWEB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWEB.Areas.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("Admin")]
	[Route("Admin/HomeAdmin")]
	public class HomeAdminController : Controller
	{
        [Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
