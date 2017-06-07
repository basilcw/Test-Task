using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RIBA_Task.Models;
using System.Globalization;

namespace RIBA_Task.Controllers
{
    public class OrdersController : Controller
    {
        private OrderDBContext db = new OrderDBContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.Where(x => x.OrderDate.Year == 2017).OrderByDescending(Order => Order.OrderDate));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderDate,CustomerName,Quantity1,Quantity2,Quantity3,Quantity4,Cost")] Order order)
        {
            if (ModelState.IsValid)
            {
                //Defining order.OrderDate as DateTime.Now
                order.OrderDate = DateTime.Now;

                //Calculation for order.Cost
                //I've defined 4 unit prices (1, 2, 3 and 4) with corresponding quantities (Quantity1, Quantity2, ...) to make it more realistic
                //An improvement for a live version would be to write the Details action method so that it persisted the order breakdown
                //in terms of the quantities like an itemised bill.
                order.Cost = CalculateDiscount(1m, order.Quantity1) + CalculateDiscount(2m, order.Quantity2) + CalculateDiscount(3m, order.Quantity3) + CalculateDiscount(4m, order.Quantity4);
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }


        public decimal CalculateDiscount(decimal price, int quantity)
        {

            decimal cost = 0;
            if (quantity > 30)
            {
                cost = (30 * 0.85m * price) + (quantity - 30) * 0.80m * price;

            }

            if (quantity > 20 && quantity <= 30)
            {
                cost =  quantity * 0.90m * price;

            }

            if (quantity > 10 && quantity <= 20)
            {
                cost = quantity * 0.95m * price;

            }

            if (quantity < 10)
            {
                cost = quantity * price;

            }

            return cost;
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDate,CustomerName,Quantity1,Quantity2,Quantity3,Quantity4,Cost")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
