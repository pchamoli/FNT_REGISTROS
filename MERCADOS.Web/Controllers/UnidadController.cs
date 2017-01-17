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
    public class UnidadController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Unidad
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_unidad" : "";
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

            var unidades = from s in db.UnidadViewModels
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                unidades = unidades.Where(s => s.nom_unidad.Contains(searchString)
                                            || s.ult_usuario.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nom_unidad":
                    unidades = unidades.OrderByDescending(s => s.nom_unidad);
                    break;
                case "Usuario":
                    unidades = unidades.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    unidades = unidades.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    unidades = unidades.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    unidades = unidades.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    unidades = unidades.OrderBy(s => s.estado);
                    break;
                case "estado":
                    unidades = unidades.OrderByDescending(s => s.estado);
                    break;
                default:
                    unidades = unidades.OrderBy(s => s.nom_unidad);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(unidades.ToPagedList(pageNumber, pageSize));
        }

        // GET: Unidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadViewModel unidadModel = db.UnidadViewModels.Find(id);
            if (unidadModel == null)
            {
                return HttpNotFound();
            }
            return View(unidadModel);
        }

        // GET: Unidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_unidad,nom_unidad,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_unidad,EsActivo")] UnidadModel unidadModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unidadModel.usuario_creacion = "RTELLO1";
                    unidadModel.fecha_creacion = DateTime.Now;
                    unidadModel.estado_unidad = (unidadModel.EsActivo ? "1" : "0");

                    db.UnidadModels.Add(unidadModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            return View(unidadModel);
        }

        // GET: Unidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadModel unidadModel = db.UnidadModels.Find(id);
            if (unidadModel == null)
            {
                return HttpNotFound();
            }
            return View(unidadModel);
        }

        // POST: Unidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_unidad,nom_unidad,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_unidad,EsActivo")] UnidadModel unidadModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unidadModel.usuario_modificacion = "RTELLO1";
                    unidadModel.fecha_modificacion = DateTime.Now;

                    db.Entry(unidadModel).State = EntityState.Modified;
                    db.Entry(unidadModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(unidadModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError(e.Message, "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(unidadModel);
        }

        // GET: Unidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadViewModel unidadModel = db.UnidadViewModels.Find(id);
            if (unidadModel == null)
            {
                return HttpNotFound();
            }
            return View(unidadModel);
        }

        // POST: Unidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                UnidadModel unidadModel = db.UnidadModels.Find(id);
                db.UnidadModels.Remove(unidadModel);
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
