namespace Ciel.Prueba.NetCore.Entidades
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EspecialidadE
    {
        [Display(Name = "Id Especialidad")]
        [DisplayName("Id de la Especialidad")]
        public int IdEspecialidad { get; set;}
        [Display(Name = "Nombre de la Especialidad")]
        [Required(ErrorMessage = "Ingrese el nombre de la especialidad.")]
        [DisplayName("Nombre de la Especialidad")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Ingrese la descripción de la especialidad.")]
        [DisplayName("Descripción de la Especialidad")]
        public string Descripcion { get; set; }

        public string MensajeError { get; set; }
    }
}
