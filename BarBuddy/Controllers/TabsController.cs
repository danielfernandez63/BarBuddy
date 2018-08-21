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
    public class TabsController : Controller
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
            Tab tab = db.Tabs.Find(id);
            if (tab == null)
            {
                return HttpNotFound();
            }
            return View(tab);
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
        public ActionResult Create(/*[Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,Balance,ZipCodeId,PickUpId")]*/ Tab tab)
        {
            if (ModelState.IsValid)
            {
               
                db.Tabs.Add(tab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(tab);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tab tab = db.Tabs.Find(id);
            if (tab == null)
            {
                return HttpNotFound();
            }

            return View(tab);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "FirstName,LastName,StreetAddress,ZipCodeId,PickUpId")]*/ Tab tab)
        {
            if (ModelState.IsValid)
            {
            
                db.Entry(tab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tab);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tab tab = db.Tabs.Find(id);
            if (tab == null)
            {
                return HttpNotFound();
            }
            return View(tab);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tab tab = db.Tabs.Find(id);
            db.Tabs.Remove(tab);
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