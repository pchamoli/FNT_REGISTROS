using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_ESPECIE")]
    public class EspecieModel
    {
        public EspecieModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_especie { get; set; }

        [Required(ErrorMessage = "Nombre de especie es requerido")]
        [Display(Name = "Nombre de especie")]
        public string nom_especie { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }

        public string estado_especie { get; set; }

        [Display(Name = "Estado")]
        [NotMapped]
        public bool EsActivo
        {
            get { return estado_especie == "1"; }
            set { estado_especie = value ? "1" : "0"; }
        }
    }

    [Table("VW_MAESTRO_ESPECIE")]
    public class EspecieViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_especie { get; set; }

        [Required]
        [Display(Name = "Nombre de especie")]
        public string nom_especie { get; set; }

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
