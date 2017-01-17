//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MERCADOS.Response
{
    using System;
    using System.Collections.Generic;
    
    public partial class InstFmtMinoristaResponse
    {
        public int id_formato { get; set; }
        public int id_instancia { get; set; }
        public Nullable<int> id_especie { get; set; }
        public Nullable<int> puesto { get; set; }
        public Nullable<int> id_unidad { get; set; }
        public Nullable<decimal> precio_min { get; set; }
        public Nullable<decimal> precio_max { get; set; }
        public Nullable<int> id_lugar { get; set; }
        public string observaciones { get; set; }
        public string usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public Nullable<decimal> tamanio { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_presentacion { get; set; }
    
        public virtual EspecieResponse especie { get; set; }
        public virtual EstadoResponse estado { get; set; }
        public virtual UnidadResponse unidad { get; set; }
        public virtual LugarResponse lugar { get; set; }
        public virtual PresentacionResponse presentacion { get; set; }
        public virtual InstFormatoResponse inst_formato { get; set; }
    }
}
