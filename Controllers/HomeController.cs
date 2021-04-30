using FinalProject.Context;
using FinalProject.Context.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.context = context;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var homes = await context.Homes.Include(x=>x.HomeImages)
                .Select(x => new HomeViewModel 
                { 
                    Address = x.Address, 
                    CreditTerm = x.CreditTerm, 
                    Price = x.Price, 
                    Description = x.Description, 
                    Id = x.Id ,
                    ImagePath = x.HomeImages.FirstOrDefault().ImagePath
                }).ToListAsync();
            return View(homes);
        }

        //[Authorize(Roles = "Company")]
        public async Task<IActionResult> Create()
        {
            var model = new CreateHomeViewModel
            {
                Categories = await context.Categories.ToDictionaryAsync(x => x.Id, x => x.Name),
                Cities = await context.Cities.ToDictionaryAsync(x => x.Id, x => x.Name)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateHomeViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var home = new Home
            {
                Address = model.Address,
                BuildDate = model.BuildDate,
                CategoryId = model.CategoryId,
                CityId = model.CityId,
                CreateDate = DateTime.Now,
                CreditTerm = 20,
                Description = model.Description,
                Price = model.Price,
                Rooms = model.Rooms,
                UserId = currentUser.Id,
                YearlyPercent = 30
            };

            await context.Homes.AddAsync(home);
            await context.SaveChangesAsync();


            foreach(var file in model.Images)
            {
                string fileName = await AddFile(file);
                await context.HomeImages.AddAsync(new HomeImage { HomeId = home.Id, ImagePath = fileName });
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        private async Task<string> AddFile(IFormFile file)
        {
            string directoryPath = environment.WebRootPath + "/images";
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string img = $"{ Guid.NewGuid().ToString() }-{ file.FileName}";

            string imagePath = directoryPath + $"/{img}";

            using(FileStream fs = new FileStream(imagePath,FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return $"/images/{img}";
        }
    }
}