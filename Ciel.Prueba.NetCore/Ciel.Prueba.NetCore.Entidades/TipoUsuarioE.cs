namespace Ciel.Prueba.NetCore.Entidades
{
    using System.ComponentModel.DataAnnotations;

    public class TipoUsuarioE
    {
        [Display(Name = "Id Tipo Usuario")]
        public int IdTipoUsuario { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
