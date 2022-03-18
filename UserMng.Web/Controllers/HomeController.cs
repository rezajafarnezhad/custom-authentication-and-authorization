using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UserMng.Core.Common;
using UserMng.Core.Common.Email;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels;
using UserMng.Web.Common;
using UserMng.Web.Common.MessageBox;
using UserMng.Web.Models;

namespace UserMng.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImsgBox _msgBox;
        private readonly IuserService _userService;
        private readonly IViewRenderService _renderService;
        private readonly IEmailSender _emailSender;
        public HomeController(ILogger<HomeController> logger, ImsgBox msgBox, IuserService userService, IViewRenderService renderService, IEmailSender emailSender)
        {
            _logger = logger;
            _msgBox = msgBox;
            _userService = userService;
            _renderService = renderService;
            _emailSender = emailSender;
        }


        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid)
                return _msgBox.ModelStateMsg(ModelState.GetErrors());

            var result = await _userService.Register(registerViewModel);
            if (result.isSucceeded)
            {
                //Send Activation Email

                var _email = result.Data.Split("`")[0];
                var _activeCode = result.Data.Split("`")[1];

                string _Body = _renderService.RenderToStringAsync("_ActiveAccount", new ActiveAccountPageModel()
                {
                    UserName = _email,
                    ActiveCode = _activeCode,
                });

                await _emailSender.Send(_email, _Body, "فعال سازی حساب کاربری");

                return _msgBox.SuccessMsg(result.Message);
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.ModelStateMsg(ModelState.GetErrors());

            var result = await _userService.Login(loginViewModel);
            var _user = await _userService.GetUserBy(result.Data);
            if (result.isSucceeded)
            {

                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,_user.Id.ToString()),
                    new Claim(ClaimTypes.Name,_user.UserName),
                    new Claim(ClaimTypes.Email,_user.Email),
                };

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = loginViewModel.RemmemberMe,
                };

                await HttpContext.SignInAsync(principal, properties);
                return _msgBox.SuccessMsg(result.Message);
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }

        [HttpGet("/ActiveAccount/{Activecode}")]
        public async Task<IActionResult> ActiveAccount(string Activecode)
        {
            var result = await _userService.ActiveAccount(Activecode);
            if (result.isSucceeded)
            {
                ViewBag.Message = true;
            }
            else
            {
                ViewBag.Message = false;

            }
            return View();

        }

        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel passwordViewModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _userService.ForgotPassword(passwordViewModel);
            if (result.isSucceeded)
            {
                var _userName = result.Data.Split("`")[0];
                var _activeCode = result.Data.Split("`")[1];

                string _Body = _renderService.RenderToStringAsync("_ForgotPassword", new ActiveAccountPageModel()
                {
                    UserName = _userName,
                    ActiveCode = _activeCode,
                });
                await _emailSender.Send(passwordViewModel.Email, _Body, "بازیابی کلمه عبور");
                return _msgBox.SuccessMsg(result.Message);

            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }

        [HttpGet("/ResetPassword/{ActiveCode}")]
        public async Task<IActionResult> ResetPassword(string ActiveCode)
        {
            var _ResetPassword = await _userService.ResetPassword(ActiveCode);
            if (_ResetPassword is null)
            {
                return NotFound();
            }

            return View(_ResetPassword);
        }
        
        [HttpPost("/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel passwordViewModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _userService.ResetPasswordStep2(passwordViewModel);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message,"GotoLogin()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}