//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MERCADOS.Request
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstadoRequest
    {
        public int id_estado { get; set; }
        public string nom_estado { get; set; }
        public string estado { get; set; }
        public string usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
    
        public virtual List<InstFmtMayoristaRequest> inst_fmt_mayorista { get; set; }
        public virtual List<InstFmtMinoristaRequest> inst_fmt_minorista { get; set; }
    }
}