using Microsoft.AspNetCore.Mvc;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels.PanelAdmin;
using UserMng.Web.Common;
using UserMng.Web.Common.MessageBox;

namespace UserMng.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RoleController : Controller
    {

        private readonly IPanelService _panelService;
        private readonly ImsgBox _msgBox;

        public RoleController(ImsgBox msgBox, IPanelService panelService)
        {
            _msgBox = msgBox;
            _panelService = panelService;
        }
        public async Task<IActionResult> Index()
        {
            var _data = await _panelService.GetAllRoleForIndex();
            return View(_data);
        }


        public async Task<IActionResult> CreateRole()
        {

            ViewData["Permissions"] = await _panelService.GetAllPermission();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _panelService.CreateRole(roleModel);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "GotoList()");

            return _msgBox.FaildMsg(result.Message);

        }

        [HttpGet("/{Id}/EditRole")]
        public async Task<IActionResult> EditRole(int Id)
        {
            var _data = await _panelService.GetForEditRole(Id);
            
            ViewData["Permissions"] = await _panelService.GetAllPermission();
            return View(_data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _panelService.EditRole(roleModel);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "GotoList()");

            return _msgBox.FaildMsg(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(int Id)
        {
            var result = await _panelService.RemoveRole(Id);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "RefreshTbl()");

            return _msgBox.FaildMsg(result.Message);
        }
    }
}
