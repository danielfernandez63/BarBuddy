﻿using BarBuddy.Models;
using Microsoft.AspNet.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BarBuddy.Controllers
{
    public class TabsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager
        public ActionResult Index()
        {
            var tabs = db.Tabs.Include(t => t.Bartender);
            return View(tabs.ToList());
        }

        public ActionResult IndexAllOrdersOnlyActive()
        {
            var tabs = db.Tabs.Include(t => t.Bartender).Where(tbs=> tbs.CheckOut == false);
            return View(tabs.ToList());
        }

        // GET: Manager
        public ActionResult Payment()
        {
            var tabs = db.Tabs.Include(t => t.Bartender);
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
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
            return View();
        }

        // POST: Customers/Create
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


                return RedirectToAction("Create", "TabRecipes");
            }

            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
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
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
            return View(tab);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "TabId,Total,Name,CheckOut,WorkerId")]*/ Tab tab)
        {
            if (ModelState.IsValid)
            {

                int workingId = tab.TabId;


                var tabEdit = db.Tabs.Where(ord => ord.TabId == workingId).Single();


                tabEdit.Name = tab.Name;
                tabEdit.WorkerId = tab.WorkerId;
                   

                db.Entry(tabEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAllOrdersOnlyActive");
            }
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
            return View(tab);
        }

        // GET: Customers/Edit/5
        public ActionResult Checkout(int? id)
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
            ViewBag.CustomerName = tab.Name;
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
            return View(tab);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(/*[Bind(Include = "TabId,Total,Name,CheckOut,WorkerId")]*/ Tab tab)
        {
            if (ModelState.IsValid)
            {

                int workingId = tab.TabId;
                var tabEdit = db.Tabs.Where(ord => ord.TabId == workingId).Single();
                tabEdit.CheckOut = tab.CheckOut;

                if (tabEdit.CheckOut == true)
                {
                    var morkerId = tabEdit.WorkerId;
                    var bartenderEdit = db.Bartenders.Where(wrk => wrk.WorkerId == morkerId).Single();

                    var restuarantFind = bartenderEdit.RestaurantId;
                    var restaurantItem = tabEdit.Total;

                    var rest = db.Restaurants.Where(rst => rst.RestaurantId == restuarantFind).Single();

                    rest.Balance += restaurantItem;
                    tabEdit.Total = 0;

                    db.Entry(tabEdit).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Entry(rest).State = EntityState.Modified;
                    db.SaveChanges();

                    var passedCost = (restaurantItem * 100);
                    int stripeCost = Convert.ToInt32(passedCost);

                    return RedirectToAction("Charge", new RouteValueDictionary(new { controller = "Tabs", action = "Charge", stripeCost }));
                }
            }
            ViewBag.WorkerId = new SelectList(db.Bartenders, "WorkerId", "FirstName");
            return View(tab);
        }


        //StripeCharging

        public ActionResult Charge(int stripeCost)
        {
            StripeConfiguration.SetApiKey("sk_test_CAtwmaT2le5Vw7iJfk9FlBSp");          
            var stripePublishKey ="pk_test_BkWKjB5Ie1Y51YhuFGt0OC5R";

            int AmountStripe = stripeCost / 100;

            ViewBag.AmountStripe = AmountStripe;
            ViewBag.StripePublishKey = stripePublishKey;
            ViewBag.StripeCost = stripeCost;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Charge(string stripeEmail, string stripeToken, int stripeCost)
        {
            var user = User.Identity.GetUserId();
            var loggedInUser = db.Bartenders.Where(c => c.ApplicationUserId == user).Single();

            int cost = stripeCost;


            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = cost,//charge in cents
                Description = "Tab For Bar",
                Currency = "usd",
                CustomerId = customer.Id
            });
        
            return RedirectToAction("Tabs", "Payment", "Tabs");
        }



        //{ controller = "Tabs", action = "Charge", stripeCost  }















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
            return RedirectToAction("IndexAllOrdersOnlyActive");
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