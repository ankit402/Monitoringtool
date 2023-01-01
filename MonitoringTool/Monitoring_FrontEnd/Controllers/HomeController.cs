//using System.Diagnostics;
//using System.Threading.Tasks;
//using Monitoring_FrontEnd.Infrastructure;
//using Monitoring_FrontEnd.Infrastructure.ErrorHandling;
//using Microsoft.AspNetCore.Mvc;
//using Monitoring_FrontEnd.Models;
//using Monitoring_FrontEnd.Models.Identity;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Logging;

//namespace Monitoring_FrontEnd.Controllers
//{
//    [Authorize]
//    public class HomeController : BaseController
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        [TempData]
//        public string StatusMessage { get; set; }

//        public HomeController(
//            ILogger<HomeController> logger,
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager)
//        {
//            _logger = logger;
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        [HttpGet("/")]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet("/icons")]
//        public IActionResult Icons()
//        {
//            return View();
//        }

//        [HttpGet("/maps")]
//        public IActionResult Maps()
//        {
//            return View();
//        }

//        [ImportModelState]
//        [HttpGet("/profile")]
//        public async Task<IActionResult> Profile()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            return View(new ProfileViewModel
//            {
//                Username = user.UserName,
//                Email = user.Email,
//                FullName = user.FullName
//            });
//        }

//        [ExportModelState]
//        [HttpPost("/profile")]
//        public async Task<IActionResult> UpdateProfile(
//            [FromForm]
//            ProfileViewModel input)
//        {
//            if (!ModelState.IsValid)
//            { 
//                return RedirectToAction(nameof(Profile));
//            }

//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            var email = await _userManager.GetEmailAsync(user);
//            if (input.Email != email)
//            {
//                var setEmailResult = await _userManager.SetEmailAsync(user, input.Email);
//                if (!setEmailResult.Succeeded)
//                {
//                    foreach (var error in setEmailResult.Errors)
//                    {
//                        ModelState.AddModelError(string.Empty, error.Description);
//                    }
//                }
//            }

//            // Model state might not be valid anymore if we weren't able to change the e-mail address
//            // so we need to check for that before proceeding
//            if (ModelState.IsValid)
//            {
//                if (input.FullName != user.FullName)
//                {
//                    // If we receive an empty string, set a null full name instead
//                    user.FullName = string.IsNullOrWhiteSpace(input.FullName) ? null : input.FullName;
//                }

//                await _userManager.UpdateAsync(user);

//                await _signInManager.RefreshSignInAsync(user);

//                StatusMessage = "Your profile has been updated";
//            }

//            return RedirectToAction(nameof(Profile));
//        }

//        [HttpGet("/tables")]
//        public IActionResult Tables()
//        {
//            return View();
//        }

//        [HttpGet("/upgrade")]
//        public IActionResult Upgrade()
//        {
//            return View();
//        }

//        [HttpGet("/privacy")]
//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [HttpPost("/logout")]
//        public async Task<IActionResult> Logout(string returnUrl = null)
//        {
//            await _signInManager.SignOutAsync();
//            _logger.LogInformation("User logged out.");
//            if (returnUrl != null)
//            {
//                return LocalRedirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        [HttpGet("/error")]
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }

//        [HttpGet("/status-code")]
//        public IActionResult StatusCodeHandler(int code)
//        {
//            ViewBag.StatusCode = code;
//            ViewBag.StatusCodeDescription = ReasonPhrases.GetReasonPhrase(code);
//            ViewBag.OriginalUrl = null;


//            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
//            if (statusCodeReExecuteFeature != null)
//            {
//                ViewBag.OriginalUrl =
//                    statusCodeReExecuteFeature.OriginalPathBase
//                    + statusCodeReExecuteFeature.OriginalPath
//                    + statusCodeReExecuteFeature.OriginalQueryString;
//            }

//            if (code == 404)
//            {
//                return View("Status404");
//            }

//            return View("Status4xx");
//        }
//    }
//}

using System.Diagnostics;
using System.Threading.Tasks;
using Monitoring_FrontEnd.Infrastructure;
using Monitoring_FrontEnd.Infrastructure.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Monitoring_FrontEnd.Models;
using Monitoring_FrontEnd.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using Monitoring_FrontEnd.Data;
using System.Linq;
using Monitoring_FrontEnd.Functions;

namespace Monitoring_FrontEnd.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        //private readonly LogClass _logClass;
        [TempData]
        public string StatusMessage { get; set; }

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext)
          //  LogClass logClass)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            //_logClass = logClass;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            string clsName = "Home";
            try
            {
              //  _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Information", "home index called");
                double ct = checkKioskStatus(1);
                checkKioskCount();
                checkKioskCountReadyState();
                checkKioskCountUnavailableState();
                checkKioskCountPausedState();
                return View();
            }
            catch (Exception ex)
            {
              //  _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "index page Error" + ex.Message + "");
                throw ex;
            }
        }
        //[HttpPost]
        //public IActionResult Index(int currentPageIndex)
        //{
        //    double ct = checkKioskStatus(currentPageIndex);
        //    ViewBag.ct = (double)((decimal)ct / 5);
        //    checkKioskCount();
        //    checkKioskCountReadyState();
        //    return View();
        //}

        public int checkKioskStatus(int index)
        {
            string clsName = "Home";
            try
            {
                //  int maxRows = 5;
              //  _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Information", "checkKioskStatus called");
                var active = (from kiosk in _dbContext.Kiosk_Informations
                              select kiosk)
                    .OrderBy(kiosk => kiosk.Id).ToList();
                // .Skip((index - 1) * maxRows)
                //  .Take(maxRows).ToList();
                //_dbContext.Kiosk_Informations.Where(x => x.Id != 0).OrderBy(x => x.Id).Skip((index - 1) * maxRows).Take(maxRows)..ToList();
                ViewBag.active = active;
                var records = _dbContext.Kiosk_Informations.Where(x => x.Id != 0).ToList();
                return records.Count;

            }
            catch (Exception ex)
            {
             //   _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                return 0;
            }
        }
        public int checkKioskCount()
        {
            string clsName = "Home";
            try
            {

                var active = _dbContext.Kiosk_Informations.Where(x => x.SerialNo != "").Distinct().ToList();
                ViewBag.Count = active.Count;
                return active.Count;

            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                return 0;
            }
        }
        public int checkKioskCountReadyState()
        {
            string clsName = "Home";
            try
            {

                var active = _dbContext.Kiosk_Informations.Where(x => x.Status.ToUpper() == "READY").Distinct().ToList();
                ViewBag.ReadyCount = active.Count;
                return active.Count;

            }
            catch (Exception ex)
            {
              //  _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                return 0;
            }
        }
        public int checkKioskCountUnavailableState()
        {
            string clsName = "Home";
            try
            {

                var active = _dbContext.Kiosk_Informations.Where(x => x.Status.ToUpper() == "UNAVAILABLE").Distinct().ToList();
                ViewBag.UnavailableCount = active.Count;
                return active.Count;

            }
            catch (Exception ex)
            {
                //_logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                return 0;
            }
        }
        public int checkKioskCountPausedState()
        {
            string clsName = "Home";
            try
            {

                var active = _dbContext.Kiosk_Informations.Where(x => x.Status.ToUpper() == "PAUSED").Distinct().ToList();
                ViewBag.PausedCount = active.Count;
                return active.Count;

            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                return 0;
            }
        }

        [HttpGet("/printers")]
        public IActionResult Printers()
        {
            string clsName = "Home";

            try
            {
                var printerList = (from printer in _dbContext.printingModules
                                   select printer)
                       .OrderBy(printer => printer.PrinterSerialNumber).Distinct().ToList();
                ViewBag.printerList = printerList;
                return View();
            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }

        [HttpGet("/PrinterDetails")]
        public ActionResult PrinterDetails(string serialNo)
        {
            string clsName = "Home";
            try
            {
                var printerList = getPrinterDetails(serialNo);
                return PartialView("_AppPartialPrinterDetails", printerList);
            }
            catch (Exception ex)
            {
              //  _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }
        [HttpGet("/PrinterModules")]
        public ActionResult PrinterModules(string serialNo)
        {
            string clsName = "Home";
            try
            {
                var printerList = getPrinterDetails(serialNo);
                return PartialView("_AppPartialPrinterModules", printerList);
            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }


        [HttpGet("/PrinterSupplies")]
        public ActionResult PrinterSupplies(string serialNo)
        {
            string clsName = "Home";
            try
            {
                var printerList = getPrinterDetails(serialNo);
                return PartialView("_AppPartialPrinterSupplies", printerList);
            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }

        [HttpGet("/PrinterCounts")]
        public ActionResult PrinterCounts(string serialNo)
        {
            string clsName = "Home";
            try
            {
                var printerList = getPrinterDetails(serialNo);
                return PartialView("_AppPartialPrinterCounts", printerList);
            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }

        private PrintingModule getPrinterDetails(string serialNo)
        {
            string clsName = "Home";
            try
            {
                var printerList = (from printer in _dbContext.printingModules
                                   select printer)
                      .Where(printer => printer.PrinterSerialNumber == serialNo).FirstOrDefault();
                return printerList;
            }
            catch (Exception ex)
            {
               // _logClass.LogFile(getClientIP(), clsName, GetType().Name, "Error", "Error" + ex.Message + "");
                throw ex;
            }
        }




        [HttpGet("/maps")]
        public IActionResult Maps()
        {
            return View();
        }

        [ImportModelState]
        [HttpGet("/profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new ProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            });
        }

        [ExportModelState]
        [HttpPost("/profile")]
        public async Task<IActionResult> UpdateProfile(
            [FromForm]
            ProfileViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, input.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Model state might not be valid anymore if we weren't able to change the e-mail address
            // so we need to check for that before proceeding
            if (ModelState.IsValid)
            {
                if (input.FullName != user.FullName)
                {
                    // If we receive an empty string, set a null full name instead
                    user.FullName = string.IsNullOrWhiteSpace(input.FullName) ? null : input.FullName;
                }

                await _userManager.UpdateAsync(user);

                await _signInManager.RefreshSignInAsync(user);

                StatusMessage = "Your profile has been updated";
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpGet("/tables")]
        public IActionResult Tables()
        {
            return View();
        }

        [HttpGet("/upgrade")]
        public IActionResult Upgrade()
        {
            return View();
        }

        [HttpGet("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/status-code")]
        public IActionResult StatusCodeHandler(int code)
        {
            ViewBag.StatusCode = code;
            ViewBag.StatusCodeDescription = ReasonPhrases.GetReasonPhrase(code);
            ViewBag.OriginalUrl = null;


            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                ViewBag.OriginalUrl =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            if (code == 404)
            {
                return View("Status404");
            }

            return View("Status4xx");
        }

        public string getClientIP()
        {
            string IPAddress = "127.0.0.1"; // HttpContext.Current.Request.UserHostAddress;
            if (IPAddress == null || IPAddress == "::1")
            {
                IPAddress = "127.0.0.1";
            }
            return IPAddress;
        }
    }
}

