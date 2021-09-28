using Ciel.Prueba.NetCore.Entidades;
using Ciel.Prueba.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciel.Prueba.NetCore.Controllers
{
    public class SedeController : Controller
    {
        public IActionResult Index(SedeE obtenerSede)
        {
            List<SedeE> listaSedes = new List<SedeE>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                if (String.IsNullOrEmpty(obtenerSede.Nombre))
                {
                    listaSedes = (from seded in db.Sedes
                                  where seded.Bhabilitado == 1
                                  select new SedeE
                                  {
                                      IdSede = seded.Iidsede,
                                      Nombre = seded.Nombre,
                                      Direccion = seded.Direccion
                                  }).ToList();
                    ViewBag.NombreSede = "";
                }
                else
                {
                    listaSedes = (from sede in db.Sedes
                                  where sede.Bhabilitado == 1
                                  && sede.Nombre.Contains(obtenerSede.Nombre)
                                  select new SedeE
                                  {
                                      IdSede = sede.Iidsede,
                                      Nombre = sede.Nombre,
                                      Direccion = sede.Direccion
                                  }).ToList();
                    ViewBag.NombreSede = obtenerSede.Nombre;
                }
                
            }
            return View(listaSedes);
        }
    }
}
