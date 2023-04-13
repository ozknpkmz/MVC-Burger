using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HamburgerMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ExtraIngredientController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly Context context;
        
        public ExtraIngredientController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, Context _context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            context = _context;
        }
        ExtraIngredientVM extraIngredientVM = new ExtraIngredientVM();
        public async Task<IActionResult> ExtraIngredientList()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            extraIngredientVM.eList = context.ExtraIngredients.Select(x => new ExtraIngredientDTO()
            {
                ExtraIngredientId = x.ExtraIngredientId,
                ExtraIngredientName = x.ExtraIngredientName,
                ExtraIngredientPrice = x.ExtraIngredientPrice
            }).ToList();
            return View(extraIngredientVM);
        }

        public IActionResult ExtraIngredientAdd()
        {
            return View(extraIngredientVM);
        }
        [HttpPost]
        public IActionResult ExtraIngredientAdd(ExtraIngredientVM extraIngredientVM)
        {
            context.ExtraIngredients.Add(extraIngredientVM.ExtraIngredient);
            context.SaveChanges();
            TempData["result"] = "A new addition" + extraIngredientVM.ExtraIngredient.ExtraIngredientName + " has been added!";
            return RedirectToAction("ExtraIngredientList");
        }
        
        public IActionResult ExtraIngredientEdit(int id)
        {
            ExtraIngredient extraIngredient = context.ExtraIngredients.FirstOrDefault(x=>x.ExtraIngredientId.Equals(id));
            return View(extraIngredientVM);
        }
        [HttpPost]
        public IActionResult ExtraIngredientEdit(int id,ExtraIngredientVM extraIngredientVM)
        {
            ExtraIngredient extraIngredient = context.ExtraIngredients.Find(id);
            extraIngredient.ExtraIngredientName = extraIngredientVM.ExtraIngredient.ExtraIngredientName;
            extraIngredient.ExtraIngredientPrice = extraIngredientVM.ExtraIngredient.ExtraIngredientPrice;
            context.SaveChanges();
            return RedirectToAction("ExtraIngredientList");
        }
        public IActionResult ExtraIngredientDelete(int id)
        {
            context.ExtraIngredients.Remove(context.ExtraIngredients.Find(id));
            context.SaveChanges();
            return RedirectToAction("ExtraIngredientList");
        }
    }
}
