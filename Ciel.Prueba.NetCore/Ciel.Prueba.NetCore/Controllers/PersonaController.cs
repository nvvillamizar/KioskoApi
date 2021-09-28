using Ciel.Prueba.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Ciel.Prueba.NetCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ciel.Prueba.NetCore.Controllers
{
    public class PersonaController : Controller
    {
        public void LlenarSexo()
        {
            List<SelectListItem> listaSexo = new();
            using BDHospitalContext db = new();
            listaSexo = (from sexo in db.Sexos
                         where sexo.Bhabilitado == 1
                         select new SelectListItem
                         {
                             Text = sexo.Nombre,
                             Value = sexo.Iidsexo.ToString()
                         }).ToList();

            listaSexo.Insert(0, new SelectListItem { Text = "Seleccione el sexo", Value = "" });

            ViewBag.ListaSexo = listaSexo;
        }
        public IActionResult Index(PersonaE obtenerPersona)
        {
            LlenarSexo();
            List<PersonaE> listaPersona = new List<PersonaE>();
            using (BDHospitalContext db = new BDHospitalContext())
            {

                listaPersona = (from persona in db.Personas
                                join sexo in db.Sexos
                                on persona.Iidsexo equals sexo.Iidsexo
                                where persona.Bhabilitado == 1
                                select new PersonaE
                                {
                                    IdPersona = persona.Iidpersona,
                                    NombreCompleto = $"{persona.Nombre} {persona.Appaterno} {persona.Apmaterno}",
                                    CorreoElectronico = persona.Email,
                                    NombreSexo = sexo.Nombre,
                                    IdSexo = sexo.Iidsexo
                                }).ToList();

                if (obtenerPersona.IdSexo != 0) listaPersona = listaPersona.Where(persona => persona.IdSexo == obtenerPersona.IdSexo).ToList();
                      
            }
            return View(listaPersona);
        }
    }
}
