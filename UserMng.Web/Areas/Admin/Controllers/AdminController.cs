using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMng.Core.Common;
using UserMng.Core.Contracts;
using UserMng.Web.Authentication;

namespace UserMng.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(Roles.CanViewAdminPanel)]
    public class AdminController : Controller
    {

        private readonly IPanelService _panelService;

        public AdminController(IPanelService panelService)
        {
            _panelService = panelService;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
