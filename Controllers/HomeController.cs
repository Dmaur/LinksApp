using System;
using Microsoft.AspNetCore.Mvc;
using LinksApp.Models;
using LinksApp.Data;
using System.Security.Permissions;

namespace LinksApp.Controllers
{

    public class HomeController : Controller
    {
        private LinkContext _context;

        public HomeController(LinkContext linkContext)
        {
            _context = linkContext;
        }


        public IActionResult Index()
        {
            var linkCatModel = new LinkCatModel
            {
                LinkModels = _context.tblLinks.ToList(),
                CatModels = _context.tblCategories.ToList()
            };
            return View(linkCatModel);
        }

        public IActionResult login()
        {
           return RedirectToAction("Index", "Login");
        }

    }
}
