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
    
    public partial class EspecieResponse
    {
        public int id_especie { get; set; }
        public string nom_especie { get; set; }
        public string estado_especie { get; set; }
        public string usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
    
        public virtual List<InstFmtMayoristaResponse> inst_fmt_mayorista { get; set; }
        public virtual List<InstFmtMinoristaResponse> inst_fmt_minorista { get; set; }
    }
}
