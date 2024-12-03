using System;
using Microsoft.AspNetCore.Mvc;
using LinksApp.Models;
using LinksApp.Data;

namespace LinksApp.Controllers
{
    public class AdminController : Controller
    {
        private LinkContext _context;

        // Injects the database context for database operations.
        public AdminController(LinkContext linkContext)
        {
            _context = linkContext;
        }

        // Displays the admin dashboard; redirects to login if the user is not authenticated.
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("auth") != "true")
            {
                return RedirectToAction("index", "Login");
            }

            var linkCatModel = new LinkCatModel
            {
                LinkModels = _context.tblLinks.ToList(), // Fetches all links.
                CatModels = _context.tblCategories.ToList() // Fetches all categories.
            };

            return View(linkCatModel);
        }


        // Prepares the view for adding a new link with initial category values.
        public IActionResult AddLink(int id, string name)
        {
            if (HttpContext.Session.GetString("auth") != "true")
            {
                return RedirectToAction("index", "Login");
            }

            var addLinkModel = new LinkCatModel
            {
                linkModel = new LinkModel() { category_id = id }, // Sets the category ID.
                catModel = new CatModel() { name = name } // Sets the category name.
            };

            return View(addLinkModel);
        }

        // Processes the form to add a new link to the database.
        [HttpPost]
        public IActionResult submitlink(LinkCatModel addLinkModel)
        {
            _context.tblLinks.Add(addLinkModel.linkModel); // Adds the new link to the database.
            _context.SaveChanges(); // Saves changes to the database.

            return RedirectToAction("Index", "Admin");
        }

        // Prepares the view to edit a category using its ID and name.
        public IActionResult EditCat(int catId, string catName)
        {
            if (HttpContext.Session.GetString("auth") != "true")
            {
                return RedirectToAction("index", "Login");
            }

            var catModel = new CatModel
            {
                id = catId, // Assigns the category ID.
                name = catName // Assigns the category name.
            };

            return View(catModel);
        }

        // Processes the form to update an existing category in the database.
        [HttpPost]
        public IActionResult submitChangeCat(CatModel catModel)
        {
            var existingCategory = _context.tblCategories.First(c => c.id == catModel.id); // Finds the category by ID.
            existingCategory.name = catModel.name; // Updates the category name.

            _context.SaveChanges(); // Saves changes to the database.

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult DelLink(int linkid)
        {
            if (HttpContext.Session.GetString("auth") != "true")
            {
                return RedirectToAction("index", "Login");
            }


            var selectedLink = _context.tblLinks.First(l => l.id == linkid);
            return View(selectedLink);
        }

        [HttpPost]
        public IActionResult DelSubmit(int id)
        {
            var linkToDelete = _context.tblLinks.First(l => l.id == id);

            _context.tblLinks.Remove(linkToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult editLink(int linkid)
        {
            if (HttpContext.Session.GetString("auth") != "true")
            {
                return RedirectToAction("index", "Login");
            }

            var selectedLinkModel = _context.tblLinks.First(l => l.id == linkid);

            var linkCatModel = new LinkCatModel
            {
                linkModel = selectedLinkModel,
                CatModels = _context.tblCategories.ToList() // Fetches all categories.
            };




            return View(linkCatModel);
        }

        [HttpPost]
        public IActionResult submitEditLink(LinkCatModel linkCatModel)
        {

            var linkToEdit = _context.tblLinks.First(l => l.id == linkCatModel.linkModel.id);
            linkToEdit.label = linkCatModel.linkModel.label;
            linkToEdit.link = linkCatModel.linkModel.link;
            linkToEdit.pinned = linkCatModel.linkModel.pinned;
            linkToEdit.category_id = linkCatModel.linkModel.category_id;

            _context.SaveChanges();

            return RedirectToAction("index", "Admin");
        }


    }
}

