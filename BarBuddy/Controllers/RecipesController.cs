using BarBuddy.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BarBuddy.Controllers
{
    public class RecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager
        public ActionResult Index()
        {
            var recipes = db.Recipe.Include(m => m.Restaurant).OrderBy(r => r.IsSeasonal);
            return View(recipes.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
                
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "Name");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,Balance,ZipCodeId,PickUpId")]*/ Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipe.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(recipe);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "Name");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "UserId", "UserId");
            return View(recipe);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "FirstName,LastName,StreetAddress,ZipCodeId,PickUpId")]*/ Recipe recipe)
        {
            if (ModelState.IsValid)
            {
           
                //db.Entry(recipe).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");

                int workingId = recipe.RecipeId;


                var recipeEdit = db.Recipe.Where(ord => ord.RecipeId == workingId).Single();

                recipeEdit.Name = recipe.Name;
                recipeEdit.Description = recipe.Description;
                recipeEdit.Type = recipe.Type;
                recipeEdit.Amount = recipe.Amount;
                recipeEdit.Description = recipe.Description;
                recipeEdit.Price = recipe.Price;
                recipeEdit.IsSeasonal = recipe.IsSeasonal;
                recipeEdit.ReducedFromInventory = recipe.ReducedFromInventory;
              

                db.Entry(recipeEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(recipe);
        }


        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            db.Recipe.Remove(recipe);
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