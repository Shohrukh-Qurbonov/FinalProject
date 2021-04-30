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

        [Authorize(Roles = "Company")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var home = await context.Homes.FindAsync(id);

            var homeImages = await context.HomeImages.Where(x => x.HomeId == home.Id).ToDictionaryAsync(x => x.Id, x=>x.ImagePath);

            var editHomeViewModel = new EditHomeViewModel
            {
                Id = id,
                Address = home.Address,
                BuildDate = home.BuildDate,
                CategoryId = home.CategoryId,
                CityId = home.CityId,
                Description = home.Description,
                PrevImages = homeImages,
                Price = home.Price,
                Rooms = home.Rooms,
                Categories = await context.Categories.ToDictionaryAsync(x=>x.Id, x=>x.Name),
                Cities = await context.Cities.ToDictionaryAsync(x=>x.Id,x=>x.Name)
            };

            return View(editHomeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditHomeViewModel model)
        {
            var home = await context.Homes.FindAsync(model.Id);
            home.Price = model.Price;
            home.Rooms = model.Rooms;
            home.Address = model.Address;
            home.BuildDate = model.BuildDate;
            home.CategoryId = model.CategoryId;
            home.CityId = model.CityId;
            home.Description = model.Description;

            if (model.Images != null)
            {
                foreach (var file in model.Images)
                {
                    string fileName = await AddFile(file);
                    await context.HomeImages.AddAsync(new HomeImage { HomeId = home.Id, ImagePath = fileName });
                }
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditForAdmin(int id)
        {
            var home = await context.Homes.FindAsync(id);

            var editHomeViewModel = new EditHomeViewModelForAdmin
            {
                Id = id,
                CreditTerm = home.CreditTerm,
                YearlyPercent = home.YearlyPercent
            };

            return View(editHomeViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditForAdmin(EditHomeViewModelForAdmin model)
        {
            var home = await context.Homes.FindAsync(model.Id);
            home.YearlyPercent = model.YearlyPercent;
            home.CreditTerm = model.CreditTerm;

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HomeDetails(int id)
        {
            var home = await context.Homes.FindAsync(id);

            var categoryName = context.Categories.Find(home.CategoryId).Name;
            var cityName = context.Cities.Find(home.CityId).Name;

            var homeDetails = new HomeDetailsViewModel
            {
                Address = home.Address,
                BuildDate = home.BuildDate,
                CategoryName = categoryName,
                CityName = cityName,
                CreditTerm = home.CreditTerm,
                Description = home.Description,
                Id = id,
                Price = home.Price,
                Rooms = home.Rooms,
                YearlyPercent = home.YearlyPercent
            };

            return View(homeDetails);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var home = await context.Homes.FindAsync(id);

            var homeImages = await context.HomeImages.Where(x => x.HomeId == home.Id).ToListAsync();

            foreach(var image in homeImages)
            {
                await DeleteOneAsync(image);
            }

            context.Homes.Remove(home);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
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

            if(model.Images != null)
            {
                foreach(var file in model.Images)
                {
                    string fileName = await AddFile(file);
                    await context.HomeImages.AddAsync(new HomeImage { HomeId = home.Id, ImagePath = fileName });
                }
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task DeleteOneAsync(HomeImage homeImage)
        {
            string imagePath = environment.WebRootPath + homeImage.ImagePath;

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            context.HomeImages.Remove(homeImage);

            await context.SaveChangesAsync();
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            var homeImage = await context.HomeImages.FindAsync(id);

            await DeleteOneAsync(homeImage);

            return RedirectToAction("Edit", new { id = homeImage.HomeId });
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