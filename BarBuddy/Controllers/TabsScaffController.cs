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
    public class TabsScaffController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TabsScaff
        public ActionResult Index()
        {
            var tabs = db.Tabs.Include(t => t.Bartender);
            return View(tabs.ToList());
        }

        // GET: TabsScaff/Details/5
        public ActionResult Details(int? id)
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

        // GET: TabsScaff/Create
        public ActionResult Create()
        {
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "ApplicationUserId");
            return View();
        }

        // POST: TabsScaff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TabId,Total,Name,CheckOut,WorkerId")] Tab tab)
        {
            if (ModelState.IsValid)
            {
                db.Tabs.Add(tab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "ApplicationUserId", tab.WorkerId);
            return View(tab);
        }

        // GET: TabsScaff/Edit/5
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
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "ApplicationUserId", tab.WorkerId);
            return View(tab);
        }

        // POST: TabsScaff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TabId,Total,Name,CheckOut,WorkerId")] Tab tab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "ApplicationUserId", tab.WorkerId);
            return View(tab);
        }

        // GET: TabsScaff/Delete/5
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

        // POST: TabsScaff/Delete/5
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
