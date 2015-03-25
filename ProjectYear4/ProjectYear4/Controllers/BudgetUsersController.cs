using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectYear4.Models;

namespace ProjectYear4.Controllers
{
    public class BudgetUsersController : Controller
    {
        private BudgetUserDBContext db = new BudgetUserDBContext();

        // GET: BudgetUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: BudgetUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.Users.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // GET: BudgetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BudgetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,LastName,FirstName,DateOfBirth,AddressLine1,AddressLine2,Town,County,Country,PostCode,ContactNo")] BudgetUser budgetUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(budgetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budgetUser);
        }

        // GET: BudgetUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.Users.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // POST: BudgetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,LastName,FirstName,DateOfBirth,AddressLine1,AddressLine2,Town,County,Country,PostCode,ContactNo")] BudgetUser budgetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budgetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budgetUser);
        }

        // GET: BudgetUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.Users.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // POST: BudgetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetUser budgetUser = db.Users.Find(id);
            db.Users.Remove(budgetUser);
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
