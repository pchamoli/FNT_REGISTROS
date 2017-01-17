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
    public class EspecieController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Especie
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_especie" : "";
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

            var especies = from s in db.EspecieViewModels
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                especies = especies.Where(s => s.nom_especie.Contains(searchString)
                                            || s.ult_usuario.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nom_especie":
                    especies = especies.OrderByDescending(s => s.nom_especie);
                    break;
                case "Usuario":
                    especies = especies.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    especies = especies.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    especies = especies.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    especies = especies.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    especies = especies.OrderBy(s => s.estado);
                    break;
                case "estado":
                    especies = especies.OrderByDescending(s => s.estado);
                    break;
                default:
                    especies = especies.OrderBy(s => s.nom_especie);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(especies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Especie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecieViewModel especieModel = db.EspecieViewModels.Find(id);
            if (especieModel == null)
            {
                return HttpNotFound();
            }
            return View(especieModel);
        }

        // GET: Especie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especie/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_especie,nom_especie,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_especie,EsActivo")] EspecieModel especieModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    especieModel.usuario_creacion = "RTELLO1";
                    especieModel.fecha_creacion = DateTime.Now;
                    especieModel.estado_especie = (especieModel.EsActivo ? "1" : "0");

                    db.EspecieModels.Add(especieModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            return View(especieModel);
        }

        // GET: Especie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecieModel especieModel = db.EspecieModels.Find(id);
            if (especieModel == null)
            {
                return HttpNotFound();
            }
            return View(especieModel);
        }

        // POST: Especie/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_especie,nom_especie,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado_especie,EsActivo")] EspecieModel especieModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    especieModel.usuario_modificacion = "RTELLO1";
                    especieModel.fecha_modificacion = DateTime.Now;

                    db.Entry(especieModel).State = EntityState.Modified;
                    db.Entry(especieModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(especieModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(especieModel);
        }

        // GET: Especie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecieViewModel especieModel = db.EspecieViewModels.Find(id);
            if (especieModel == null)
            {
                return HttpNotFound();
            }
            return View(especieModel);
        }

        // POST: Especie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EspecieModel especieModel = db.EspecieModels.Find(id);
                db.EspecieModels.Remove(especieModel);
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
