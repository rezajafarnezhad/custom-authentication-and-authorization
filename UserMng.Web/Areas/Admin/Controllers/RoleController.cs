using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMng.Core.Common;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels.PanelAdmin;
using UserMng.Web.Authentication;
using UserMng.Web.Common;
using UserMng.Web.Common.MessageBox;

namespace UserMng.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    [PermissionChecker(Roles.CanViewRoleManager)]
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


        [PermissionChecker(Roles.CanViewAddRole)]
        public async Task<IActionResult> CreateRole()
        {

            ViewData["Permissions"] = await _panelService.GetAllPermission();
            return View();
        }

        [PermissionChecker(Roles.CanViewAddRole)]
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

        [PermissionChecker(Roles.CanViewEditRole)]
        [HttpGet("/{Id}/EditRole")]
        public async Task<IActionResult> EditRole(int Id)
        {
            var _data = await _panelService.GetForEditRole(Id);
            
            ViewData["Permissions"] = await _panelService.GetAllPermission();
            return View(_data);
        }

        [PermissionChecker(Roles.CanViewEditRole)]
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


        [PermissionChecker(Roles.CanViewRemoveRole)]
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
