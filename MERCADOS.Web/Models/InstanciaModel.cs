using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_INST_FORMATO")]
    public class InstanciaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_instancia { get; set; }

        [Required(ErrorMessage = "Nombre de formato es requerido")]
        [Display(Name = "Nombre formato")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione un formato válido")]
        public int id_formato { get; set; }

        [Required(ErrorMessage = "Nombre de mercado es requerido")]
        [Display(Name = "Nombre mercado")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione un mercado válido")]
        public int id_mercado { get; set; }

        [Display(Name = "Usuario solicitante")]
        public string user_instancia { get; set; }

        public string estado_instancia { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha de formato es requerida")]
        [Display(Name = "Fecha formato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha_formato { get; set; }

        [Display(Name = "Estado")]
        [NotMapped]
        public bool EsActivo
        {
            get { return estado_instancia == "1"; }
            set { estado_instancia = value ? "1" : "0"; }
        }
    }

    [Table("VW_LISTADO_INSTANCIAS")]
    public class InstanciaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_instancia { get; set; }

        public int id_formato { get; set; }

        [Required]
        [Display(Name = "Nombre de formato")]
        public string nom_formato { get; set; }

        public int id_mercado { get; set; }

        [Required]
        [Display(Name = "Nombre de mercado")]
        public string nom_mercado { get; set; }

        [Required]
        [Display(Name = "Usuario solicitante")]
        public string usuario { get; set; }

        [Required]
        [Display(Name = "Fecha formato")]
        public System.DateTime fecha_formato { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Usuario modificación")]
        public string ult_usuario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public System.DateTime ult_fecha { get; set; }

    }
}