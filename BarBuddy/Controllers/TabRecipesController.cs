using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BarBuddy.Models;

namespace BarBuddy.Controllers
{
    public class TabRecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TabRecipes
        public ActionResult Index()
        {
            var tabRecipes = db.TabRecipes.Include(t => t.Recipe).Include(t => t.Tab);
            return View(tabRecipes.ToList());
        }

        // GET: TabRecipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TabRecipes tabRecipes = db.TabRecipes.Find(id);
            if (tabRecipes == null)
            {
                return HttpNotFound();
            }
            return View(tabRecipes);
        }

        // GET: TabRecipes/Create
        public ActionResult Create()
        {
            ViewBag.RecipeId = new SelectList(db.Recipe, "RecipeId", "Name");
            ViewBag.TabId = new SelectList(db.Tabs, "TabId", "Name");
            return View();
        }

        // POST: TabRecipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TabRecipe,RecipeId,TabId")] TabRecipes tabRecipes)
        {
            if (ModelState.IsValid)
            {
                db.TabRecipes.Add(tabRecipes);
                db.SaveChanges();


                var recipeItem = tabRecipes.RecipeId;
                var tabItem = tabRecipes.TabId;     
                
                var tab = db.Tabs.Where(ord => ord.TabId == tabItem).Single();
                var recipe = db.Recipe.Where(ord => ord.RecipeId == recipeItem).Single();

                tab.Total += recipe.Price;
                db.Entry(tab).State = EntityState.Modified;
                db.SaveChanges();

                var alcoholType = recipe.InventoryId;
                var alcoholAmount = recipe.ReducedFromInventory;
                var inventoryObject = db.Inventory.Where(itm => itm.InventoryId == alcoholType).Single();
                inventoryObject.Stock -= alcoholAmount;
                db.Entry(inventoryObject).State = EntityState.Modified;
                db.SaveChanges();



                return RedirectToAction("IndexAllOrdersOnlyActive", "Tabs");
            }

            ViewBag.RecipeId = new SelectList(db.Recipe, "RecipeId", "Name", tabRecipes.RecipeId);
            ViewBag.TabId = new SelectList(db.Tabs, "TabId", "Name", tabRecipes.TabId);
            return View(tabRecipes);
        }

        // GET: TabRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TabRecipes tabRecipes = db.TabRecipes.Find(id);
            if (tabRecipes == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipeId = new SelectList(db.Recipe, "RecipeId", "Name", tabRecipes.RecipeId);
            ViewBag.TabId = new SelectList(db.Tabs, "TabId", "Name", tabRecipes.TabId);
            return View(tabRecipes);
        }

        // POST: TabRecipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TabRecipe,RecipeId,TabId")] TabRecipes tabRecipes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabRecipes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipeId = new SelectList(db.Recipe, "RecipeId", "Name", tabRecipes.RecipeId);
            ViewBag.TabId = new SelectList(db.Tabs, "TabId", "Name", tabRecipes.TabId);
            return View(tabRecipes);
        }

        // GET: TabRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TabRecipes tabRecipes = db.TabRecipes.Find(id);
            if (tabRecipes == null)
            {
                return HttpNotFound();
            }
            return View(tabRecipes);
        }

        // POST: TabRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TabRecipes tabRecipes = db.TabRecipes.Find(id);
            db.TabRecipes.Remove(tabRecipes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
