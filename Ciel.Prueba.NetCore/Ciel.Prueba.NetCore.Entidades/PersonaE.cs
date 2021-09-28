using System.ComponentModel.DataAnnotations;

namespace Ciel.Prueba.NetCore.Entidades
{
    public class PersonaE
    {
        [Display(Name = "Id Persona")]
        public int IdPersona { get; set; }
        [Display(Name = "Nombres")]
        public string NombreCompleto { get; set; }
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo debe ser válido")]
        public string CorreoElectronico { get; set; }
        [Display(Name = "Sexo")]
        public string NombreSexo { get; set; }
        [Display(Name = "Id Sexo")]
        public int IdSexo { get; set; }
    }
}
