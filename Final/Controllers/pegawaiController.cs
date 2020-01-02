using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.Models;

namespace Final.Controllers
{
    public class pegawaiController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: pegawai
        public ActionResult Index()
        {
            return View(db.tabel_pegawai.ToList());
        }

        // GET: pegawai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pegawai tabel_pegawai = db.tabel_pegawai.Find(id);
            if (tabel_pegawai == null)
            {
                return HttpNotFound();
            }
            return View(tabel_pegawai);
        }

        // GET: pegawai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pegawai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pegawai,nama_pegawai,password_pegawai")] tabel_pegawai tabel_pegawai)
        {
            if (ModelState.IsValid)
            {
                db.tabel_pegawai.Add(tabel_pegawai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tabel_pegawai);
        }

        // GET: pegawai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pegawai tabel_pegawai = db.tabel_pegawai.Find(id);
            if (tabel_pegawai == null)
            {
                return HttpNotFound();
            }
            return View(tabel_pegawai);
        }

        // POST: pegawai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pegawai,nama_pegawai,password_pegawai")] tabel_pegawai tabel_pegawai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_pegawai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabel_pegawai);
        }

        // GET: pegawai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pegawai tabel_pegawai = db.tabel_pegawai.Find(id);
            if (tabel_pegawai == null)
            {
                return HttpNotFound();
            }
            return View(tabel_pegawai);
        }

        // POST: pegawai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_pegawai tabel_pegawai = db.tabel_pegawai.Find(id);
            db.tabel_pegawai.Remove(tabel_pegawai);
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
