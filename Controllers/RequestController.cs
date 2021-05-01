using FinalProject.Context;
using FinalProject.Context.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRequestViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var request = new Request()
            {
                Aim = model.Aim,
                Prepayment = model.Prepayment,
                CreditSumm = model.CreditSumm,
                CreditTerm = model.CreditTerm,
                HomeId = model.HomeId,
                PhoneNumber = model.PhoneNumber,
                PurchaseType = model.PurchaseType,
                RequestDate = DateTime.Now,
                CurrencyTypeId = model.CurrencyTypeId,
                FIO = model.FIO
            };

            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
