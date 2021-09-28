using System.ComponentModel.DataAnnotations;

namespace Ciel.Prueba.NetCore.Entidades
{
    public class MedicamentoE
    {
        [Display(Name = "Id Medicamento")]
        public int IdMedicamento { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Ingrese el nombre del medicamento.")]
        public string Nombre { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Ingrese el precio del medicamento.")]
        public decimal? Precio { get; set; }
        [Display(Name = "Stock")]
        [Range(0, 1000, ErrorMessage = "Debe ser de rango 0 a 1000")]
        [Required(ErrorMessage = "Ingrese el stock del medicamento.")]
        public int? Stock { get; set; }
        [Display(Name = "Nombre Forma de la Farmaceutica")]
        public string NombreFarmaceutica { get; set; }
        [Display(Name = "Seleccione Forma Farmaceutica")]
        [Required(ErrorMessage = "Ingrese la forma de la farmaceutica.")]
        public int? IdFormaFarmaceutica { get; set; }

        [Display(Name = "Concentración")]
        public string Concentracion { get; set; }

        [Display(Name = "Presentación")]
        public string Presentacion { get; set; }
    }
}
