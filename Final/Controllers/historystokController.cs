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
    public class historystokController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: historystok
        public ActionResult Index()
        {
            return View(db.tabel_history_stok_buku.ToList());
        }

        // GET: historystok/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_history_stok_buku tabel_history_stok_buku = db.tabel_history_stok_buku.Find(id);
            if (tabel_history_stok_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_history_stok_buku);
        }

        // GET: historystok/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: historystok/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_histori_stok,nama_buku,penerbit_buku,tambahan_stok_buku")] tabel_history_stok_buku tabel_history_stok_buku)
        {
            tabel_buku buku = new tabel_buku();
            var contex = new perpustakaan3Entities();
            var add = (from a in db.tabel_buku where a.nama_buku == tabel_history_stok_buku.nama_buku && a.penerbit_buku == tabel_history_stok_buku.penerbit_buku select a).SingleOrDefault();
            if (add != null) {
                add.stok_buku += tabel_history_stok_buku.tambahan_stok_buku;
                db.tabel_history_stok_buku.Add(tabel_history_stok_buku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if(add == null)
            {
                buku.nama_buku = tabel_history_stok_buku.nama_buku;
                buku.penerbit_buku = tabel_history_stok_buku.penerbit_buku;
                buku.stok_buku = tabel_history_stok_buku.tambahan_stok_buku;
                db.tabel_buku.Add(buku);
                db.tabel_history_stok_buku.Add(tabel_history_stok_buku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*
            if (ModelState.IsValid)
            {
                db.tabel_history_stok_buku.Add(tabel_history_stok_buku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/

            return View(tabel_history_stok_buku);
        }

        // GET: historystok/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_history_stok_buku tabel_history_stok_buku = db.tabel_history_stok_buku.Find(id);
            if (tabel_history_stok_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_history_stok_buku);
        }

        // POST: historystok/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_histori_stok,nama_buku,penerbit_buku,tambahan_stok_buku")] tabel_history_stok_buku tabel_history_stok_buku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_history_stok_buku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabel_history_stok_buku);
        }

        // GET: historystok/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_history_stok_buku tabel_history_stok_buku = db.tabel_history_stok_buku.Find(id);
            if (tabel_history_stok_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_history_stok_buku);
        }

        // POST: historystok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_history_stok_buku tabel_history_stok_buku = db.tabel_history_stok_buku.Find(id);
            db.tabel_history_stok_buku.Remove(tabel_history_stok_buku);
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
