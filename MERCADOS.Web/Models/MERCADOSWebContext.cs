using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MERCADOS.Web.Models
{
    public class MERCADOSWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MERCADOSWebContext() : base("DB_REGISTROSEntities")
        {
        }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.FormatoModel> FormatoModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.FormatoViewModel> FormatoViewModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.EspecieModel> EspecieModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.EspecieViewModel> EspecieViewModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.EstadoModel> EstadoModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.EstadoViewModel> EstadoViewModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.LugarModel> LugarModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.LugarViewModel> LugarViewModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.PresentacionViewModel> PresentacionViewModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.PresentacionModel> PresentacionModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.UnidadModel> UnidadModels { get; set; }

        public System.Data.Entity.DbSet<MERCADOS.Web.Models.UnidadViewModel> UnidadViewModels { get; set; }

    }
}
