﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IDEIMusic.DAL;
using IDEIMusic.Models;

namespace IDEIMusic.Controllers
{
    public class SalesController : Controller
    {
        private LabelContext db = new LabelContext();

        private ISaleRepository repo;

        public SalesController()
            : this(new SaleRepository())
        { }

        public SalesController(ISaleRepository rep)
        {
            repo = rep;
        }

        // GET: Sales
        public ActionResult Index(string sortorder)
        {
            var sales = repo.GetSaleSummaries();

            float totalIncome = 0;
            foreach(SaleSummary s in sales)
                totalIncome += s.Income;

            ViewBag.IDSort = String.IsNullOrEmpty(sortorder) ? "id_desc" : "";
            ViewBag.DateSort = sortorder == "Date" ? "date_desc" : "Date";
            ViewBag.QuantitySort = sortorder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.IncomeSort = sortorder == "Income" ? "income_desc" : "Income";

            switch (sortorder)
            {
                case "id_desc":
                    sales = sales.OrderByDescending(s => s.ID);
                    break;
                case "Date":
                    sales = sales.OrderBy(s => s.PurchaseDate);
                    break;
                case "date_desc":
                    sales = sales.OrderByDescending(s => s.PurchaseDate);
                    break;
                case "Quantity":
                    sales = sales.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    sales = sales.OrderByDescending(s => s.Quantity);
                    break;
                case "Income":
                    sales = sales.OrderBy(s => s.Income);
                    break;
                case "income_desc":
                    sales = sales.OrderByDescending(s => s.Income);
                    break;
            }


            ViewBag.Total = totalIncome;
            ViewData["summaries"] = repo.GetSaleSummaries();
            ViewData["summariesByQuantity"] = repo.GetSaleSummariesBySales().ToList();
            ViewData["summariesByIncome"] = repo.GetSaleSummariesByIncome();

            return View(sales.ToList());
        }


        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,PurchaseDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,PurchaseDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
