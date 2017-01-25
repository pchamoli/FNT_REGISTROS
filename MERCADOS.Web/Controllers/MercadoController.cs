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
    public class MercadoController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Mercado
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_mercado" : "";
            ViewBag.DptoSortParm = sortOrder == "Departamento" ? "departamento" : "Departamento";
            ViewBag.ProvSortParm = sortOrder == "Provincia" ? "provincia" : "Provincia";
            ViewBag.DistSortParm = sortOrder == "Distrito" ? "distrito" : "Distrito";
            ViewBag.TipoSortParm = sortOrder == "Tipo" ? "tipo" : "Tipo";
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

            var mercados = from s in db.MercadoViewModels
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mercados = mercados.Where(s => s.nom_mercado.Contains(searchString.ToUpper())
                                            || s.distrito.Contains(searchString.ToUpper())
                                            || s.tipo.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_mercado":
                    mercados = mercados.OrderByDescending(s => s.nom_mercado);
                    break;
                case "Departamento":
                    mercados = mercados.OrderBy(s => s.departamento);
                    break;
                case "departamento":
                    mercados = mercados.OrderByDescending(s => s.departamento);
                    break;
                case "Provincia":
                    mercados = mercados.OrderBy(s => s.provincia);
                    break;
                case "provincia":
                    mercados = mercados.OrderByDescending(s => s.provincia);
                    break;
                case "Distrito":
                    mercados = mercados.OrderBy(s => s.distrito);
                    break;
                case "distrito":
                    mercados = mercados.OrderByDescending(s => s.distrito);
                    break;
                case "Tipo":
                    mercados = mercados.OrderBy(s => s.tipo);
                    break;
                case "tipo":
                    mercados = mercados.OrderByDescending(s => s.tipo);
                    break;
                case "Usuario":
                    mercados = mercados.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    mercados = mercados.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    mercados = mercados.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    mercados = mercados.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    mercados = mercados.OrderBy(s => s.estado);
                    break;
                case "estado":
                    mercados = mercados.OrderByDescending(s => s.estado);
                    break;
                default:
                    mercados = mercados.OrderBy(s => s.nom_mercado);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(mercados.ToPagedList(pageNumber, pageSize));
        }

        // GET: Mercado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MercadoViewModel mercadoModel = db.MercadoViewModels.Find(id);
            if (mercadoModel == null)
            {
                return HttpNotFound();
            }
            return View(mercadoModel);
        }

        public IEnumerable<SelectListItem> GetDepartamentos()
        {
            var sql = "select * from departamento";
            var dptos = db.Database.SqlQuery<DepartamentoModel>(sql).ToList().Select(f => new SelectListItem
            {
                Value = f.codigo_departamento,
                Text = f.departamento
            });

            return dptos;
        }

        //private IEnumerable<SelectListItem> PrimerNodoDpto
        //{
        //    get
        //    {
        //        return Enumerable.Repeat(new SelectListItem
        //        {
        //            Value = "0",
        //            Text = "Seleccione un departamento"
        //        }, count: 1);
        //    }
        //}

        public IEnumerable<SelectListItem> GetProvincias(string dpto)
        {
            var sql = "select * from provincia where codigo_departamento='" + dpto + "'";
            var provs = db.Database.SqlQuery<ProvinciaModel>(sql, dpto).ToList().Select(f => new SelectListItem
            {
                Value = f.codigo_provincia,
                Text = f.provincia
            });

            return provs;
        }

        //private IEnumerable<SelectListItem> PrimerNodoProv
        //{
        //    get
        //    {
        //        return Enumerable.Repeat(new SelectListItem
        //        {
        //            Value = "0",
        //            Text = "Seleccione una provincia"
        //        }, count: 1);
        //    }
        //}

        public IEnumerable<SelectListItem> GetDistritos(string dpto, string prov)
        {
            var sql = "select * from distrito where codigo_departamento='" + dpto + "' and codigo_provincia='" + @prov + "'";
            var dists = db.Database.SqlQuery<DistritoModel>(sql, dpto, prov).ToList().Select(f => new SelectListItem
            {
                Value = f.codigo_distrito,
                Text = f.distrito
            });

            return dists;
        }

        public JsonResult CargarProvincias(string dpto)
        {
            var sql = "select * from provincia where codigo_departamento='" + dpto + "'";
            var provincias = db.Database.SqlQuery<ProvinciaModel>(sql).ToList();

            return Json(provincias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarDistritos(string dpto, string prov)
        {
            var sql = "select * from distrito where codigo_departamento='" + dpto + "' and codigo_provincia='" + @prov + "'";
            var dists = db.Database.SqlQuery<DistritoModel>(sql, dpto, prov).ToList();
            
            return Json(dists, JsonRequestBehavior.AllowGet);
        }

        // GET: Mercado/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoList = new SelectList(GetDepartamentos(), "value", "text");
            ViewBag.ProvinciaList = new SelectList(GetProvincias("00"), "value", "text");
            ViewBag.DistritoList = new SelectList(GetDistritos("00", "00"), "value", "text");
            return View();
        }

        // POST: Mercado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mercado,nom_mercado,desc_mercado,dpto_mercado,prov_mercado,distrito_mercado,tipo_mercado,estado_mercado,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo")] MercadoModel mercadoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mercadoModel.usuario_creacion = "RTELLO1";
                    mercadoModel.fecha_creacion = DateTime.Now;
                    mercadoModel.estado_mercado = (mercadoModel.EsActivo ? "1" : "0");
                    mercadoModel.nom_mercado = mercadoModel.nom_mercado.ToUpper();
                    mercadoModel.desc_mercado = mercadoModel.desc_mercado?.ToUpper();

                    db.MercadoModels.Add(mercadoModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.DepartamentoList = new SelectList(GetDepartamentos(), "value", "text");
            ViewBag.ProvinciaList = new SelectList(GetProvincias(mercadoModel.dpto_mercado), "value", "text");
            ViewBag.DistritoList = new SelectList(GetDistritos(mercadoModel.dpto_mercado, mercadoModel.prov_mercado), "value", "text");
            return View(mercadoModel);
        }

        // GET: Mercado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MercadoModel mercadoModel = db.MercadoModels.Find(id);
            if (mercadoModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartamentoList = new SelectList(GetDepartamentos(), "value", "text", mercadoModel.dpto_mercado);
            ViewBag.ProvinciaList = new SelectList(GetProvincias(mercadoModel.dpto_mercado), "value", "text", mercadoModel.prov_mercado);
            ViewBag.DistritoList = new SelectList(GetDistritos(mercadoModel.dpto_mercado, mercadoModel.prov_mercado), "value", "text", mercadoModel.distrito_mercado);
            return View(mercadoModel);
        }

        // POST: Mercado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mercado,nom_mercado,desc_mercado,dpto_mercado,prov_mercado,distrito_mercado,tipo_mercado,estado_mercado,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo")] MercadoModel mercadoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mercadoModel.usuario_modificacion = "RTELLO1";
                    mercadoModel.fecha_modificacion = DateTime.Now;
                    mercadoModel.nom_mercado = mercadoModel.nom_mercado.ToUpper();
                    mercadoModel.desc_mercado = mercadoModel.desc_mercado?.ToUpper();

                    db.Entry(mercadoModel).State = EntityState.Modified;
                    db.Entry(mercadoModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(mercadoModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.DepartamentoList = new SelectList(GetDepartamentos(), "value", "text", mercadoModel.dpto_mercado);
            ViewBag.ProvinciaList = new SelectList(GetProvincias(mercadoModel.dpto_mercado), "value", "text", mercadoModel.prov_mercado);
            ViewBag.DistritoList = new SelectList(GetDistritos(mercadoModel.dpto_mercado, mercadoModel.prov_mercado), "value", "text", mercadoModel.distrito_mercado);
            return View(mercadoModel);
        }

        // GET: Mercado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MercadoViewModel mercadoModel = db.MercadoViewModels.Find(id);
            if (mercadoModel == null)
            {
                return HttpNotFound();
            }
            return View(mercadoModel);
        }

        // POST: Mercado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MercadoModel mercadoModel = db.MercadoModels.Find(id);
            db.MercadoModels.Remove(mercadoModel);
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
