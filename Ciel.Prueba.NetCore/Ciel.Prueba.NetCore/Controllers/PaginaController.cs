using Ciel.Prueba.NetCore.Entidades;
using Ciel.Prueba.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciel.Prueba.NetCore.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index()
        {
            List<PaginaE> listaPaginas = new List<PaginaE>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                listaPaginas = (from pagina in db.Paginas
                                where pagina.Bhabilitado == 1
                                select new PaginaE
                                {
                                    IdPagina = pagina.Iidpagina,
                                    Mensaje = pagina.Mensaje,
                                    Controlador = pagina.Controlador,
                                    Accion = pagina.Accion,
                                    Bhabilitado = Convert.ToBoolean(pagina.Bhabilitado)
                                }).ToList();
            }
            return View(listaPaginas);
        }
    }
}
