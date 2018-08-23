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
    public class BartendersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
         

            ViewBag.displaymenu = "Yes";
      
            var bartenders = db.Bartenders.Include(m => m.Restaurant);
            return View(bartenders.ToList());
         
        }

        public ActionResult BartenderHome()
        {
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            var user = User.Identity.GetUserId();
            var loggedInUser = db.Bartenders.Include(wrk => wrk.Restaurant).Include(grt => grt.Manager).Where(c => c.ApplicationUserId == user).Single();
                          
            if (loggedInUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Bartender bartender = db.Bartenders.Find(id);
            if (loggedInUser == null)
            {
                return HttpNotFound();
            }
            return View(loggedInUser);
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
        public ActionResult Create(/*[Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,Balance,ZipCodeId,PickUpId")]*/ Bartender bartender)
        {
            if (ModelState.IsValid)
            {
                bartender.ApplicationUserId = User.Identity.GetUserId();
                db.Bartenders.Add(bartender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "Name");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");

            return View(bartender);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bartender bartender = db.Bartenders.Find(id);
            if (bartender == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "Name");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "UserId", "UserId");
            return View(bartender);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "FirstName,LastName,StreetAddress,ZipCodeId,PickUpId")]*/ Bartender bartender)
        {
            if (ModelState.IsValid)
            {
                //var user = User.Identity.GetUserId();
                //var loggedInCustomer = db.Bartenders.Where(e => e.ApplicationUserId == user).Single();

                //loggedInCustomer.FirstName = customer.FirstName;

                int workingId = bartender.WorkerId;


                var BartenderEdit = db.Bartenders.Where(ord => ord.WorkerId == workingId).Single();

                BartenderEdit.FirstName = bartender.FirstName;
                BartenderEdit.LastName = bartender.LastName;
                BartenderEdit.PhoneNumber = bartender.PhoneNumber;
                BartenderEdit.ManagerId = bartender.ManagerId;
                BartenderEdit.RestaurantId = bartender.RestaurantId;

                db.Entry(BartenderEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bartender);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bartender bartender = db.Bartenders.Find(id);
            if (bartender == null)
            {
                return HttpNotFound();
            }
            return View(bartender);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bartender bartender = db.Bartenders.Find(id);
            db.Bartenders.Remove(bartender);
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