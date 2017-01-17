using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_FORMATO")]
    public class FormatoModel
    {
        public FormatoModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_formato { get; set; }

        [Required(ErrorMessage = "Tipo de formato es requerido")]
        [Display(Name = "Tipo de formato")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un tipo de formato")]
        public int tipo_formato { get; set; }

        [Required(ErrorMessage = "Nombre de formato es requerido")]
        [Display(Name = "Nombre de formato")]
        public string nom_formato { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }

        public string estado { get; set; }

        [Display(Name = "Estado")]
        [NotMapped]
        public bool EsActivo
        {
            get { return estado == "1"; }
            set { estado = value ? "1" : "0"; }
        }

        //public SeccionModel Seccion { get; set; }
    }

    [Table("VW_MAESTRO_FORMATO")]
    public class FormatoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_formato { get; set; }

        [Required]
        [Display(Name = "Tipo de formato")]
        public string tipo_formato { get; set; }

        [Required]
        [Display(Name = "Nombre de formato")]
        public string nom_formato { get; set; }

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
