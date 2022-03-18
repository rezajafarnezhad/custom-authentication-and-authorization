using Microsoft.AspNetCore.Mvc;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels.PanelAdmin;
using UserMng.Web.Common;
using UserMng.Web.Common.MessageBox;

namespace UserMng.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IPanelService _panelService;
        private readonly ImsgBox _msgBox;

        public UserController(ImsgBox msgBox, IPanelService panelService)
        {
            _msgBox = msgBox;
            _panelService = panelService;
        }

        public async Task<IActionResult> Index(int PageId= 1,int take=20 , string filter = "")
        {

            var _date = await _panelService.GetAllUser(PageId, take, filter);
            return View(_date);
        }

        [HttpPost("/Search")]
        public async Task<IActionResult> Search(int PageId, int take, string filter)
        {
            var _date = await _panelService.GetAllUser(PageId, take=20, filter);
            return View("_Users",_date);
        }

        public async Task<IActionResult> CreateUser()
        {
            var Roles= await _panelService.GetAllRole();
            ViewData["RolesData"] = Roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser _createUser)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());


            var result = await _panelService.AddUser(_createUser);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "GotoList()");

            return _msgBox.FaildMsg(result.Message);
        }


        [HttpGet("/{Id}/EditUser")]
        public async Task<IActionResult> EditUser(string Id)
        {
            var _data = await _panelService.GetForEdit(Id);

            if (_data is null)
                return NotFound();


            var Roles = await _panelService.GetAllRole();
            ViewData["RolesData"] = Roles;
            return View(_data);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUser editUser)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _panelService.EditUser(editUser);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "GotoList()");

            return _msgBox.FaildMsg(result.Message);
        }

        public async Task<IActionResult> changePassword(string Id)
        {
            var _data = await _panelService.ChangeUserPassword(Id);

            if (_data is null)
                return NotFound();

            return View(_data);
        }

        [HttpPost]
        public async Task<IActionResult> changePassword(ChangeUserPasswordModel changeUserPasswordModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _panelService.ChangeUserPassword(changeUserPasswordModel);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "GotoList()");

            return _msgBox.FaildMsg(result.Message);

        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            var result = await _panelService.ChangeStatus(Id);
            if (result.isSucceeded)
                return _msgBox.SuccessMsg(result.Message, "RefreshTbl()");

            return _msgBox.FaildMsg(result.Message);
        }
    }
}
