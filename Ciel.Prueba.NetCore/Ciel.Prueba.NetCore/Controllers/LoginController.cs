using Ciel.Prueba.NetCore.Entidades;
using Ciel.Prueba.NetCore.Models;
using Ciel.Prueba.NetCore.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciel.Prueba.NetCore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Login(string username, string password)
        {
            using BDHospitalContext db = new();
            string respuesta = "";
            string claveCifrada = Generic.CifrarDatos(password);
            int numeroVeces = db.Usuarios.Where(p => p.Nombreusuario == username && p.Contraseña == claveCifrada).Count();
            if(numeroVeces == 1)
            {
                respuesta = "OK";
                Usuario usuarioE = db.Usuarios.Where(p => p.Nombreusuario == username && p.Contraseña == claveCifrada).First(); 
                HttpContext.Session.SetString("usuario", usuarioE.Iidusuario.ToString());
                int tipoUsuario = usuarioE.Iidtipousuario;
                List<PaginaE> lista = (from tipoPagina in db.TipoUsuarioPaginas 
                                      join pagina in db.Paginas on tipoPagina.Iidpagina equals pagina.Iidpagina
                                      where tipoPagina.Bhabilitado == 1 && tipoPagina.Iidtipousuario == tipoUsuario
                                      select new PaginaE
                                      {
                                          Mensaje = pagina.Mensaje,
                                          Controlador = pagina.Controlador,
                                          Accion = pagina.Accion
                                      }).ToList();

                Generic.ListaPagina = lista;
            }

            return respuesta;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Login");
        }
    }
}
