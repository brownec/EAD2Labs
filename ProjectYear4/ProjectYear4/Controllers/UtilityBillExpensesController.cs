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
    public class UtilityBillExpensesController : Controller
    {
        private MyDBConnection db = new MyDBConnection();

        // GET: UtilityBillExpenses
        public ActionResult Index()
        {
            var utilityBillExpense = db.UtilityBillExpense.Include(u => u.Budget);
            return View(utilityBillExpense.ToList());
        }

        // GET: UtilityBillExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilityBillExpense utilityBillExpense = db.UtilityBillExpense.Find(id);
            if (utilityBillExpense == null)
            {
                return HttpNotFound();
            }
            return View(utilityBillExpense);
        }

        // GET: UtilityBillExpenses/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budget, "BudgetId", "BudgetId");
            return View();
        }

        // POST: UtilityBillExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UtilityBillExpenseId,Electricity,Gas,RefuseCollection,IrishWater,BudgetId")] UtilityBillExpense utilityBillExpense)
        {
            if (ModelState.IsValid)
            {
                db.UtilityBillExpense.Add(utilityBillExpense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BudgetId = new SelectList(db.Budget, "BudgetId", "BudgetId", utilityBillExpense.BudgetId);
            return View(utilityBillExpense);
        }

        // GET: UtilityBillExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilityBillExpense utilityBillExpense = db.UtilityBillExpense.Find(id);
            if (utilityBillExpense == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budget, "BudgetId", "BudgetId", utilityBillExpense.BudgetId);
            return View(utilityBillExpense);
        }

        // POST: UtilityBillExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UtilityBillExpenseId,Electricity,Gas,RefuseCollection,IrishWater,BudgetId")] UtilityBillExpense utilityBillExpense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilityBillExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetId = new SelectList(db.Budget, "BudgetId", "BudgetId", utilityBillExpense.BudgetId);
            return View(utilityBillExpense);
        }

        // GET: UtilityBillExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilityBillExpense utilityBillExpense = db.UtilityBillExpense.Find(id);
            if (utilityBillExpense == null)
            {
                return HttpNotFound();
            }
            return View(utilityBillExpense);
        }

        // POST: UtilityBillExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UtilityBillExpense utilityBillExpense = db.UtilityBillExpense.Find(id);
            db.UtilityBillExpense.Remove(utilityBillExpense);
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
