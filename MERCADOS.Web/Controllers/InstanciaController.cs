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
using System.Configuration;

namespace MERCADOS.Web.Controllers
{
    public class InstanciaController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Instancia
        public ActionResult Index(string sortOrder, string currentFilter, string currentFilter1, string FechaDesde, string FechaHasta, int? page, string cbFormatos, string cbMercados, string currentFilter2, string currentFilter3)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FormatoSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_formato" : "";
            ViewBag.MercadoSortParm = sortOrder == "Mercado" ? "nom_mercado" : "Mercado";
            ViewBag.UsuarioSortParm = sortOrder == "Usuario" ? "ult_usuario" : "Usuario";
            ViewBag.FechaSortParm = sortOrder == "Fecha" ? "ult_fecha" : "Fecha";
            ViewBag.Fecha1SortParm = sortOrder == "Fecha1" ? "fecha_formato" : "Fecha1";
            ViewBag.EstadoSortParm = sortOrder == "Estado" ? "estado" : "Estado";

            ViewBag.FormatoList = new SelectList(GetFormatos(), "value", "text", currentFilter2);
            ViewBag.MercadoList = new SelectList(GetMercados((cbFormatos == null) ? 0 : Convert.ToInt32(cbFormatos)), "value", "text", currentFilter3);

            if ((FechaDesde!= null) && (FechaDesde != ""))
            {
                page = 1;
            }
            else
            {
                FechaDesde = currentFilter;
            }

            if ((FechaHasta != null) && (FechaHasta != ""))
            {
                page = 1;
            }
            else
            {
                FechaHasta = currentFilter1;
            }

            if ((cbFormatos != null) && (cbFormatos != ""))
            {
                page = 1;
            }
            else
            {
                cbFormatos = currentFilter2;
            }

            if ((cbMercados != null) && (cbMercados != ""))
            {
                page = 1;
            }
            else
            {
                cbMercados = currentFilter3;
            }

            ViewBag.CurrentFilter = FechaDesde;
            ViewBag.CurrentFilter1 = FechaHasta;
            ViewBag.CurrentFilter2 = cbFormatos;
            ViewBag.CurrentFilter3 = cbMercados;

            var instancias = from s in db.InstanciaViewModels
                           select s;

            var formatoID = Convert.ToInt32(cbFormatos);
            var mercadoID = Convert.ToInt32(cbMercados);

            if (FechaDesde == null && FechaHasta == null)
            {
                instancias = instancias.Where(s => s.id_formato == (formatoID != 0 ? formatoID : s.id_formato)
                                             && s.id_mercado == (mercadoID != 0 ? mercadoID : s.id_mercado));
            }
            else {
                if (FechaDesde != null && FechaHasta == null)
                {
                    var fechaIni = Convert.ToDateTime(FechaDesde);
                    instancias = instancias.Where(s => s.id_formato == (formatoID != 0 ? formatoID : s.id_formato)
                                                  && s.id_mercado == (mercadoID != 0 ? mercadoID : s.id_mercado)
                                                  && s.fecha_formato >= fechaIni);
                }
                else
                {
                    if (FechaDesde == null && FechaHasta != null)
                    {
                        var fechaFin = Convert.ToDateTime(FechaHasta);
                        instancias = instancias.Where(s => s.id_formato == (formatoID != 0 ? formatoID : s.id_formato)
                                                      && s.id_mercado == (mercadoID != 0 ? mercadoID : s.id_mercado)
                                                      && s.fecha_formato <= fechaFin);
                    }
                    else
                    {
                        var fechaIni = Convert.ToDateTime(FechaDesde);
                        var fechaFin = Convert.ToDateTime(FechaHasta);

                        instancias = instancias.Where(s => s.id_formato == (formatoID != 0 ? formatoID : s.id_formato)
                                                      && s.id_mercado == (mercadoID != 0 ? mercadoID : s.id_mercado)
                                                      && (s.fecha_formato >= fechaIni && s.fecha_formato <= fechaFin));
                    }
                }
            }

            switch (sortOrder)
            {
                case "nom_formato":
                    instancias = instancias.OrderByDescending(s => s.nom_formato);
                    break;
                case "Mercado":
                    instancias = instancias.OrderBy(s => s.nom_mercado);
                    break;
                case "nom_mercado":
                    instancias = instancias.OrderByDescending(s => s.nom_mercado);
                    break;
                case "Usuario":
                    instancias = instancias.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    instancias = instancias.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    instancias = instancias.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    instancias = instancias.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    instancias = instancias.OrderBy(s => s.estado);
                    break;
                case "estado":
                    instancias = instancias.OrderByDescending(s => s.estado);
                    break;
                case "Fecha1":
                    instancias = instancias.OrderBy(s => s.fecha_formato);
                    break;
                case "fecha_formato":
                    instancias = instancias.OrderByDescending(s => s.fecha_formato);
                    break;
                default:
                    instancias = instancias.OrderBy(s => s.nom_formato);
                    break;
            }

            ViewBag.TotalCount = instancias.Count();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(instancias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Instancia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstanciaViewModel instanciaModel = db.InstanciaViewModels.Find(id);
            if (instanciaModel == null)
            {
                return HttpNotFound();
            }
            return View(instanciaModel);
        }

        #region Utilitarios

        public IEnumerable<SelectListItem> GetFormatos()
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

        public JsonResult CargarMercados(int id_formato)
        {
            var mercados = from s in db.MercadoModels
                            where s.tipo_mercado == id_formato
                            select s;

            return Json(mercados, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetMercados(int id_formato)
        {
            var mercados = db.MercadoModels.Where(x => x.tipo_mercado == id_formato).
                Select(f => new SelectListItem
                {
                    Value = f.id_mercado.ToString(),
                    Text = f.nom_mercado
                });

            return DefaultFlavorItem2.Concat(mercados);
        }

        public IEnumerable<SelectListItem> DefaultFlavorItem2
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione un mercado"
                }, count: 1);
            }
        }

        #endregion

        // GET: Instancia/Create
        public ActionResult Create()
        {
            ViewBag.FormatoList = new SelectList(GetFormatos(), "value", "text");
            ViewBag.MercadoList = new SelectList(GetMercados(0), "value", "text");

            return View();
        }

        // POST: Instancia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_instancia,id_formato,id_mercado,user_instancia,estado_instancia,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo,fecha_formato")] InstanciaModel instanciaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    instanciaModel.usuario_creacion = "RTELLO1";
                    instanciaModel.fecha_creacion = DateTime.Now;
                    instanciaModel.estado_instancia = (instanciaModel.EsActivo ? "1" : "0");

                    db.InstanciaModels.Add(instanciaModel);
                    db.SaveChanges();

                    // Redirigimos a la pantalla de Formato Mayorista o Minorista dependiendo de lo registrado
                    if (instanciaModel.id_formato == Convert.ToInt32(ConfigurationManager.AppSettings["FMT_MAYORISTA"]))
                    {
                        return RedirectToAction("Index", "FmtMayorista", new { formatoID = instanciaModel.id_formato, instanciaID = instanciaModel.id_instancia });
                    }
                    else
                    {
                        return RedirectToAction("Index", "FmtMinorista", new { formatoID = instanciaModel.id_formato, instanciaID = instanciaModel.id_instancia });
                    }
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.FormatoList = new SelectList(GetFormatos(), "value", "text");
            ViewBag.MercadoList = new SelectList(GetMercados(instanciaModel.id_formato), "value", "text");
            return View(instanciaModel);
        }

        // GET: Instancia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstanciaModel instanciaModel = db.InstanciaModels.Find(id);
            if (instanciaModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.FormatoList = new SelectList(GetFormatos(), "value", "text", instanciaModel.id_formato);
            ViewBag.MercadoList = new SelectList(GetMercados(instanciaModel.id_formato), "value", "text", instanciaModel.id_mercado);

            return View(instanciaModel);
        }

        // POST: Instancia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_instancia,id_formato,id_mercado,user_instancia,estado_instancia,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo,fecha_formato")] InstanciaModel instanciaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    instanciaModel.usuario_modificacion = "RTELLO1";
                    instanciaModel.fecha_modificacion = DateTime.Now;

                    db.Entry(instanciaModel).State = EntityState.Modified;
                    db.Entry(instanciaModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(instanciaModel).Property("fecha_creacion").IsModified = false;
                    db.Entry(instanciaModel).Property("id_mercado").IsModified = false;
                    db.Entry(instanciaModel).Property("id_formato").IsModified = false;

                    db.SaveChanges();
                    //return RedirectToAction("Index");

                    // Redirigimos a la pantalla de Formato Mayorista o Minorista dependiendo de lo registrado
                    if (instanciaModel.id_formato == Convert.ToInt32(ConfigurationManager.AppSettings["FMT_MAYORISTA"]))
                    {
                        return RedirectToAction("Index", "FmtMayorista", new { formatoID = instanciaModel.id_formato, instanciaID = instanciaModel.id_instancia });
                    }
                    else
                    {
                        return RedirectToAction("Index", "FmtMinorista", new { formatoID = instanciaModel.id_formato, instanciaID = instanciaModel.id_instancia });
                    }
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.FormatoList = new SelectList(GetFormatos(), "value", "text", instanciaModel.id_formato);
            ViewBag.MercadoList = new SelectList(GetMercados(instanciaModel.id_formato), "value", "text", instanciaModel.id_mercado);
            return View(instanciaModel);
        }

        // GET: Instancia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstanciaViewModel instanciaModel = db.InstanciaViewModels.Find(id);
            if (instanciaModel == null)
            {
                return HttpNotFound();
            }
            return View(instanciaModel);
        }

        // POST: Instancia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                InstanciaModel instanciaModel = db.InstanciaModels.Find(id);
                db.InstanciaModels.Remove(instanciaModel);
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
