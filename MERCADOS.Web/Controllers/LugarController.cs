using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MERCADOS.Web.Models;
using System.Data.SqlClient;
using PagedList;

namespace MERCADOS.Web.Controllers
{
    public class LugarController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Lugar
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_lugar" : "";
            ViewBag.UsuarioSortParm = sortOrder == "Usuario" ? "ult_usuario" : "Usuario";
            ViewBag.FechaSortParm = sortOrder == "Fecha" ? "ult_fecha" : "Fecha";
            ViewBag.EstadoSortParm = sortOrder == "Estado" ? "estado" : "Estado";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var lugares = from s in db.LugarViewModels
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                lugares = lugares.Where(s => s.nom_lugar.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_lugar":
                    lugares = lugares.OrderByDescending(s => s.nom_lugar);
                    break;
                case "Usuario":
                    lugares = lugares.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    lugares = lugares.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    lugares = lugares.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    lugares = lugares.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    lugares = lugares.OrderBy(s => s.estado);
                    break;
                case "estado":
                    lugares = lugares.OrderByDescending(s => s.estado);
                    break;
                default:
                    lugares = lugares.OrderBy(s => s.nom_lugar);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(lugares.ToPagedList(pageNumber, pageSize));
        }

        // GET: Lugar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarViewModel lugarModel = db.LugarViewModels.Find(id);
            if (lugarModel == null)
            {
                return HttpNotFound();
            }
            return View(lugarModel);
        }

        // GET: Lugar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lugar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_lugar,nom_lugar,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_lugar,EsActivo")] LugarModel lugarModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lugarModel.usuario_creacion = "RTELLO1";
                    lugarModel.fecha_creacion = DateTime.Now;
                    lugarModel.estado_lugar = (lugarModel.EsActivo ? "1" : "0");
                    lugarModel.nom_lugar = lugarModel.nom_lugar.ToUpper();

                    db.LugarModels.Add(lugarModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            return View(lugarModel);
        }

        // GET: Lugar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarModel lugarModel = db.LugarModels.Find(id);
            if (lugarModel == null)
            {
                return HttpNotFound();
            }
            return View(lugarModel);
        }

        // POST: Lugar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_lugar,nom_lugar,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_lugar,EsActivo")] LugarModel lugarModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lugarModel.usuario_modificacion = "RTELLO1";
                    lugarModel.fecha_modificacion = DateTime.Now;
                    lugarModel.nom_lugar = lugarModel.nom_lugar.ToUpper();

                    db.Entry(lugarModel).State = EntityState.Modified;
                    db.Entry(lugarModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(lugarModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError(e.Message, "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(lugarModel);
        }

        // GET: Lugar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarViewModel lugarModel = db.LugarViewModels.Find(id);
            if (lugarModel == null)
            {
                return HttpNotFound();
            }
            return View(lugarModel);
        }

        // POST: Lugar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LugarModel lugarModel = db.LugarModels.Find(id);
                db.LugarModels.Remove(lugarModel);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
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
