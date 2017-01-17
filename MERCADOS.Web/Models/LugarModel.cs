using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_LUGAR")]
    public class LugarModel
    {
        public LugarModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_lugar { get; set; }

        [Required(ErrorMessage = "Nombre de lugar es requerido")]
        [Display(Name = "Nombre de lugar")]
        public string nom_lugar { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }

        public string estado_lugar { get; set; }

        [Display(Name = "Estado")]
        [NotMapped]
        public bool EsActivo
        {
            get { return estado_lugar == "1"; }
            set { estado_lugar = value ? "1" : "0"; }
        }
    }

    [Table("VW_MAESTRO_LUGAR")]
    public class LugarViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_lugar { get; set; }

        [Required]
        [Display(Name = "Nombre de lugar")]
        public string nom_lugar { get; set; }

        [Display(Name = "Usuario modificación")]
        public string ult_usuario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public System.DateTime ult_fecha { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }
    }

}
