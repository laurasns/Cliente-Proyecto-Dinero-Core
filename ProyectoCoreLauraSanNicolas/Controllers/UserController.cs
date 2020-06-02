using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using ProyectoCoreLauraSanNicolas.Services;

namespace ProyectoCoreLauraSanNicolas.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public int? SessionUserId
        {
            get { return (int?)HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", value.GetValueOrDefault()); }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Home", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVMO loginVMO)
        {
            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Home", "Home");
            }
            String token = await userService.Login(loginVMO);
            if (token != null)
            {
                int userId = await userService.GetUserIdByUsername(loginVMO.Username);
                int roleId = await userService.GetRoleId(userId);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.NameIdentifier, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, roleId.ToString()));
                identity.AddClaim(new Claim("Token", token));
                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddMinutes(20) });
                SessionUserId = await userService.GetUserIdByUsername(loginVMO.Username);
                object controller = TempData["controller"];
                object action = TempData["action"];

                var redirect = (controller == null || action == null) ?
                    RedirectToAction("Home", "Home") :
                    RedirectToAction(action.ToString(), controller.ToString());

                TempData["controller"] = null;
                TempData["action"] = null;

                return redirect;
            }
            else
            {
                ViewBag.Error = "Usuario o Contraseña Incorrectos";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            if (SessionUserId.HasValue)
            {
                return RedirectToAction("Home", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserVMO userVMO)
        {
            ViewData["userData"] = userVMO;

            try
            {
                if (SessionUserId.HasValue)
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    bool passwordOk = userService.ValidatePassword(userVMO.Password);
                    bool emailOk = userService.ValidateEmail(userVMO.Email);

                    string errorMessage = null;
                    if (passwordOk && emailOk)
                    {
                        String result = await userService.SignUp(userVMO);
                        if (result == "OK")
                        {
                            return RedirectToAction("SignIn");
                        }
                        errorMessage = result;
                    }

                    ViewBag.Error = errorMessage ?? "La contraseña o el email no cumplen los requisitos";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserInformation()
        {
            if (!SessionUserId.HasValue)
            {
                return RedirectToAction("SignIn");
            }

            UserVMO userById = await userService.GetUserById(SessionUserId.Value);
            return View(userById);
        }

        [HttpPost]
        public async Task<IActionResult> UserInformation(String Name, String Surname, String Email, String Username)
        {
            await userService.EditUserInformation(Name, Surname, Email, Username);
            TempData["edited"] = "OK";
            return RedirectToAction("UserInformation");
        }

        [HttpGet]
        public async void DeleteUser(int id)
        {
            await userService.DeleteUser(id);
        }
    }
}