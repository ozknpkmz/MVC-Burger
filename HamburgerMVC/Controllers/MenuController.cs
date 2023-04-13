using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class MenuController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly Context context;

        public MenuController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, Context _context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            context = _context;
        }
        MenuVM menuVM = new MenuVM();
        public IActionResult MenuList()
        {
            menuVM.mList = context.Menus.Select(x => new MenuDTO()
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                MenuPrice = x.MenuPrice,
                //Image = x.Image
            }).ToList();
            return View(menuVM);
        }
        public IActionResult MenuAdd()
        {
            return View(menuVM);
        }
        [HttpPost]
        public IActionResult MenuAdd(MenuVM menuVM)
        {
            context.Menus.Add(menuVM.Menu);
            context.SaveChanges();
            TempData["result"] = "A new menu" + menuVM.Menu.MenuName + "has been added!";
            return RedirectToAction("MenuList");
        }
        public IActionResult MenuEdit(int id)
        {
            Menu menu = context.Menus.FirstOrDefault(x => x.MenuId.Equals(id));
            return View(menuVM);
        }
        [HttpPost]
        public IActionResult MenuEdit(int id, MenuVM menuVM)
        {//Todo: Add image
            Menu menu = context.Menus.Find(id);
            menu.MenuName = menuVM.Menu.MenuName;
            menu.MenuPrice = menuVM.Menu.MenuPrice;
            //menu.Image = menuVM.Menu.Image;
            context.SaveChanges();
            return RedirectToAction("MenuList");
        }
        public IActionResult MenuDelete(int id)
        {
            context.Menus.Remove(context.Menus.Find(id));
            context.SaveChanges();
            return RedirectToAction("MenuList");
        }
    }
}
