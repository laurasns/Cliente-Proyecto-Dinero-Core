using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoCoreLauraSanNicolas.Filters;
using ProyectoCoreLauraSanNicolas.Services;
using Microsoft.AspNetCore.Http;
using ProyectoCoreLauraSanNicolas.Email;
using Microsoft.AspNetCore.Hosting;

namespace ProyectoCoreLauraSanNicolas.Controllers
{
    public class CalculatorController : Controller
    {
        private IWebHostEnvironment hostingEnvironment;
        IUserService userService;
        private readonly IEmailSender emailSender;

        public CalculatorController(IUserService userService, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            this.userService = userService;
            hostingEnvironment = environment;
            this.emailSender = emailSender;
        }

        [UserAuthorization]
        public IActionResult Calculator()
        {
            return RedirectToAction(nameof(FinancialFreedom));
        }

        [HttpGet]
        [UserAuthorization]
        public IActionResult FinancialFreedom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FinancialFreedom(int age, int expense, int saving, int currentMoney, double inflation, double profitability)
        {
            int currentDate = DateTime.Now.Year;
            int lifeExpectancy = 100;
            double savingSum = 0;
            double inflationRest = inflation / 100;
            double profitabilitySum = profitability / 100;
            int freedomAge = 0;
            int totalSavingYears = 0;
            double freedomCapital = 0;
            List<int> years = new List<int>();
            Dictionary<int, double> money = new Dictionary<int, double>();

            for (int i = age; i <= lifeExpectancy; i++)
            {

                if (savingSum >= (expense * (lifeExpectancy - i)))
                {
                    freedomCapital = Math.Round(savingSum, 2);
                    freedomAge = i;
                    totalSavingYears = i - age;
                    break;
                }

                if (i == age)
                {
                    savingSum = saving + currentMoney;
                }
                else
                {
                    savingSum += (saving - expense);
                    savingSum -= (savingSum * inflationRest);
                    savingSum += Math.Round(savingSum * profitabilitySum, 2);
                }

                years.Add(currentDate);
                money.Add(i, savingSum);
                currentDate = currentDate++;
            }
            String formattedCapital = freedomCapital.ToString("#,##0.00");
            ViewBag.Liberty = "Serías libre financieramente a la edad de " + freedomAge.ToString() + " años.";
            ViewBag.SavingYears = "Tardarías unos " + totalSavingYears.ToString() + " años en conseguirlo.";
            ViewBag.Capital = "Necesitarías acumular unos " + formattedCapital + "€ para ser libre financieramente y que no se te acabara el dinero hasta los 100 años, aproximadamente, manteniendo el mismo nivel de vida.";
            TempData["liberty"] = freedomAge.ToString();
            TempData["years"] = totalSavingYears.ToString();
            TempData["capital"] = formattedCapital.ToString();
            return View();
        }

        public IActionResult Unauthorize()
        {
            return View();
        }

        public ActionResult SendMail()
        {
            String msj = "";
            try
            {                
                String subject = "Resultado Calculadora Financiera";
                String body = Path.GetFullPath(@"../../ProyectoCoreLauraSanNicolas/ProyectoCoreLauraSanNicolas/wwwroot/emailTemplate/EmailTemplate.html");
                StreamReader str = new StreamReader(body);
                String mailBody = str.ReadToEnd();
                str.Close();

                string username = userService.GetUserById(HttpContext.Session.GetInt32("UserId").GetValueOrDefault()).Result.Name;
                string email = userService.GetUserById(HttpContext.Session.GetInt32("UserId").GetValueOrDefault()).Result.Email;
                mailBody = mailBody.Replace("{name}", username);
                mailBody = mailBody.Replace("{liberty}", TempData["liberty"].ToString());
                mailBody = mailBody.Replace("{years}", TempData["years"].ToString());
                mailBody = mailBody.Replace("{capital}", TempData["capital"].ToString());

                var message = new Message(new string[] { email }, subject, mailBody);
                emailSender.SendEmail(message);
                msj = "OK";
            }
            catch (Exception ex)
            {
                msj = ex.ToString();
            }
            TempData["emailSentMessage"] = msj;
            return RedirectToAction("FinancialFreedom");
        }
    }
}