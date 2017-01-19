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
    // Comentario CampoController
    public class CampoController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Campo
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_campo" : "";
            ViewBag.Name1SortParm = sortOrder == "Nombre1" ? "nom_seccion" : "Nombre1";
            ViewBag.Name2SortParm = sortOrder == "Nombre2" ? "nom_formato" : "Nombre2";
            ViewBag.ObligSortParm = sortOrder == "Oblig" ? "obligatorio" : "Oblig";
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

            var campos = from s in db.CampoViewModels
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                campos = campos.Where(s => s.nom_formato.Contains(searchString.ToUpper())
                                            || s.nom_seccion.Contains(searchString.ToUpper())
                                            || s.nom_campo.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_campo":
                    campos = campos.OrderByDescending(s => s.nom_campo);
                    break;
                case "Nombre1":
                    campos = campos.OrderBy(s => s.nom_seccion);
                    break;
                case "nom_seccion":
                    campos = campos.OrderByDescending(s => s.nom_seccion);
                    break;
                case "Nombre2":
                    campos = campos.OrderBy(s => s.nom_formato);
                    break;
                case "nom_formato":
                    campos = campos.OrderByDescending(s => s.nom_formato);
                    break;
                case "Oblig":
                    campos = campos.OrderBy(s => s.obligatorio);
                    break;
                case "obligatorio":
                    campos = campos.OrderByDescending(s => s.obligatorio);
                    break;
                case "Usuario":
                    campos = campos.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    campos = campos.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    campos = campos.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    campos = campos.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    campos = campos.OrderBy(s => s.estado);
                    break;
                case "estado":
                    campos = campos.OrderByDescending(s => s.estado);
                    break;
                default:
                    campos = campos.OrderBy(s => s.nom_campo);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(campos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Campo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampoViewModel campoModel = db.CampoViewModels.Find(id);
            if (campoModel == null)
            {
                return HttpNotFound();
            }
            return View(campoModel);
        }

        public IEnumerable<SelectListItem> GetFormatoList()
        {
            var formatos = db.FormatoModels.Select(f => new SelectListItem
            {
                Value = f.id_formato.ToString(),
                Text = f.nom_formato
            });

            return DefaultFlavorItem.Concat(formatos);
        }

        public IEnumerable<SelectListItem> DefaultFlavorItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione un formato"
                }, count: 1);
            }
        }

        public IEnumerable<SelectListItem> GetSeccionList(int id_formato)
        {
            // obtengo el listado de secciones asociadas a un formato
            var secciones = from s in db.SeccionModels
                            where s.id_formato == id_formato
                            select s; 

            var secciones1 = secciones.Select(f => new SelectListItem
            {
                Value = f.id_seccion.ToString(),
                Text = f.nom_seccion
            });

            return DefaultFlavorItem2.Concat(secciones1);
        }

        public IEnumerable<SelectListItem> DefaultFlavorItem2
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione una sección"
                }, count: 1);
            }
        }

        public int ObtenerFormatoID(int id_seccion)
        {
            SeccionModel seccion = db.SeccionModels.Find(id_seccion);

            if (seccion == null)
                return 0;
            else
                return seccion.id_formato;
        }

        public JsonResult CargarSecciones(int id_formato)
        {
            var secciones = from s in db.SeccionModels
                            where s.id_formato == id_formato
                            select s;

            return Json(secciones, JsonRequestBehavior.AllowGet);
        }

        // GET: Campo/Create
        public ActionResult Create()
        {
            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            ViewBag.SeccionList = new SelectList(GetSeccionList(0), "value", "text");
            return View();
        }

        // POST: Campo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_campo,id_seccion,nom_campo,desc_campo,estado_campo,oblig_campo,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo,EsObligatorio")] CampoModel campoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    campoModel.usuario_creacion = "RTELLO1";
                    campoModel.fecha_creacion = DateTime.Now;
                    campoModel.estado_campo = (campoModel.EsActivo ? "1" : "0");
                    campoModel.oblig_campo = (campoModel.EsObligatorio ? "1" : "0");
                    campoModel.nom_campo = campoModel.nom_campo.ToUpper();
                    campoModel.desc_campo = campoModel.desc_campo.ToUpper();

                    db.CampoModels.Add(campoModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            ViewBag.SeccionList = new SelectList(GetSeccionList(ObtenerFormatoID(campoModel.id_seccion)), "value", "text");
            return View(campoModel);
        }

        // GET: Campo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampoModel campoModel = db.CampoModels.Find(id);
            if (campoModel == null)
            {
                return HttpNotFound();
            }

            var id_formato = ObtenerFormatoID(campoModel.id_seccion);
            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text", id_formato);
            ViewBag.SeccionList = new SelectList(GetSeccionList(id_formato), "value", "text", campoModel.id_seccion);

            return View(campoModel);
        }

        // POST: Campo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_campo,id_seccion,nom_campo,desc_campo,estado_campo,oblig_campo,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo,EsObligatorio")] CampoModel campoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    campoModel.usuario_modificacion = "RTELLO1";
                    campoModel.fecha_modificacion = DateTime.Now;
                    campoModel.nom_campo = campoModel.nom_campo.ToUpper();
                    campoModel.desc_campo = campoModel.desc_campo.ToUpper();

                    db.Entry(campoModel).State = EntityState.Modified;
                    db.Entry(campoModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(campoModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            var id_formato = ObtenerFormatoID(campoModel.id_seccion);
            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text", id_formato);
            ViewBag.FormatoList = new SelectList(GetSeccionList(id_formato), "value", "text", campoModel.id_seccion);

            return View(campoModel);
        }

        // GET: Campo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampoViewModel campoModel = db.CampoViewModels.Find(id);
            if (campoModel == null)
            {
                return HttpNotFound();
            }
            return View(campoModel);
        }

        // POST: Campo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CampoModel campoModel = db.CampoModels.Find(id);
                db.CampoModels.Remove(campoModel);
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
