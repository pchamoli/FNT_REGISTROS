//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MERCADOS.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DAT_INST_FMT_MAYORISTA
    {
        [Key, Column(Order = 0)]
        public int id_formato { get; set; }

        [Key, Column(Order = 1)]
        public int id_instancia { get; set; }

        public int cantidad { get; set; }
        public decimal volumen { get; set; }
        public decimal precio_min { get; set; }
        public decimal precio_max { get; set; }
        public string observaciones { get; set; }
        public string usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public int id_especie { get; set; }
        public int id_unidad { get; set; }
        public int id_lugar { get; set; }
        public int id_estado { get; set; }
        public int id_presentacion { get; set; }
        public decimal peso_unidad { get; set; }
        public decimal tamanio { get; set; }

        [ForeignKey("id_especie")]
        public virtual DAT_ESPECIE DAT_ESPECIE { get; set; }

        [ForeignKey("id_estado")]
        public virtual DAT_ESTADO DAT_ESTADO { get; set; }

        [ForeignKey("id_unidad")]
        public virtual DAT_UNIDAD DAT_UNIDAD { get; set; }

        [ForeignKey("id_lugar")]
        public virtual DAT_LUGAR DAT_LUGAR { get; set; }

        [ForeignKey("id_presentacion")]
        public virtual DAT_PRESENTACION DAT_PRESENTACION { get; set; }

        [ForeignKey("id_formato, id_instancia")]
        public virtual DAT_INST_FORMATO DAT_INST_FORMATO { get; set; }
    }
}
