namespace Ciel.Prueba.NetCore.Controllers
{
    using Ciel.Prueba.NetCore.Entidades;
    using Ciel.Prueba.NetCore.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class TipoUsuarioController : Controller
    {
        public IActionResult Index(TipoUsuarioE obtenerTipoUsuario)
        {
            List<TipoUsuarioE> listaTipoUsuarios = new();
            using (BDHospitalContext db = new())
            {
                listaTipoUsuarios = (from tipoUsu in db.TipoUsuarios
                                     where tipoUsu.Bhabilitado == 1
                                     select new TipoUsuarioE
                                     {
                                         IdTipoUsuario = tipoUsu.Iidtipousuario,
                                         Nombre = tipoUsu.Nombre,
                                         Descripcion = tipoUsu.Descripcion
                                     }).ToList();

                if (obtenerTipoUsuario.Nombre == null && obtenerTipoUsuario.Descripcion == null && obtenerTipoUsuario.IdTipoUsuario == 0)
                {
                    ViewBag.Nombre = "";
                    ViewBag.Descripcion = "";
                    ViewBag.IdUsuario = 0;
                }
                else
                {
                    if (obtenerTipoUsuario.Nombre != null) listaTipoUsuarios = listaTipoUsuarios.Where(p => p.Nombre.Contains(obtenerTipoUsuario.Nombre)).ToList();
                    if (obtenerTipoUsuario.IdTipoUsuario != 0) listaTipoUsuarios = listaTipoUsuarios.Where(p => p.IdTipoUsuario == obtenerTipoUsuario.IdTipoUsuario).ToList();
                    if (obtenerTipoUsuario.Descripcion != null) listaTipoUsuarios = listaTipoUsuarios.Where(p => p.Descripcion.Contains(obtenerTipoUsuario.Descripcion)).ToList();
                    ViewBag.Nombre = obtenerTipoUsuario.Nombre;
                    ViewBag.Descripcion = obtenerTipoUsuario.Descripcion;
                    ViewBag.IdUsuario = obtenerTipoUsuario.IdTipoUsuario;
                }
            }
            return View(listaTipoUsuarios);
        }
    }
}
