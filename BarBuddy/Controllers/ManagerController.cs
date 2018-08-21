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
    public class ManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            //var user = User.Identity.GetUserId();      
            //var loggedInUser = db.Customers.Include(g => g.ZipCode).Include(v => v.PickUpDay).Where(c => c.ApplicationUserId == user).Single();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            //ViewBag.ZipCodeId = new SelectList(db.ZipCodes, "ZipCodeId", "ZipCodeArea");
      
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,Balance,ZipCodeId,PickUpId")]*/ Manager manager)
        {
            if (ModelState.IsValid)
            {
                manager.ApplicationUserId = User.Identity.GetUserId();
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(manager);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
      
            return View(manager);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "FirstName,LastName,StreetAddress,ZipCodeId,PickUpId")]*/ Manager manager)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var loggedInCustomer = db.Managers.Where(e => e.ApplicationUserId == user).Single();

                //loggedInCustomer.FirstName = customer.FirstName;


                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
       
            return View(manager);
        }
    
        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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