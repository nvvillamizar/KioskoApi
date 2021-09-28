using Ciel.Prueba.NetCore.Entidades;
using Ciel.Prueba.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciel.Prueba.NetCore.Controllers
{
    public class MedicamentoController : Controller
    {

        public List<SelectListItem> ListarFormaFarmaceutica()
        {
            List<SelectListItem> listarFormaFarmaceutica = new List<SelectListItem>();
            using BDHospitalContext db = new();

            listarFormaFarmaceutica = (from formaFarma in db.FormaFarmaceuticas
                                       where formaFarma.Bhabilitado == 1
                                       select new SelectListItem
                                       {
                                           Text = formaFarma.Nombre,
                                           Value = formaFarma.Iidformafarmaceutica.ToString()
                                       }).ToList();

            listarFormaFarmaceutica.Insert(0, new SelectListItem { Text = "Seleccione la Farmaceutica", Value = "" });

            return listarFormaFarmaceutica;

        }
        public IActionResult Index(MedicamentoE obtenerMedicamento)
        {
            ViewBag.ListaForma = ListarFormaFarmaceutica();
            List<MedicamentoE> listaMedicamentos = new();
            using (BDHospitalContext db = new())
            {
                listaMedicamentos = (from medicamento in db.Medicamentos
                                     join formaFama in db.FormaFarmaceuticas
                                     on medicamento.Iidformafarmaceutica equals formaFama.Iidformafarmaceutica
                                     where medicamento.Bhabilitado == 1
                                     select new MedicamentoE
                                     {
                                         IdMedicamento = medicamento.Iidmedicamento,
                                         Nombre = medicamento.Nombre,
                                         Precio = medicamento.Precio,
                                         Stock = medicamento.Stock,
                                         NombreFarmaceutica = formaFama.Nombre,
                                         IdFormaFarmaceutica = formaFama.Iidformafarmaceutica
                                     }).ToList();

                if (obtenerMedicamento.IdFormaFarmaceutica != null) listaMedicamentos = listaMedicamentos.Where(medicamento => medicamento.IdFormaFarmaceutica == obtenerMedicamento.IdFormaFarmaceutica).ToList();
            }
            return View(listaMedicamentos);
        }

        public IActionResult Agregar()
        {
            ViewBag.ListarFormaFarma = ListarFormaFarmaceutica();
            return View();
        }

        public IActionResult Editar(int idMedicamento)
        {
            MedicamentoE medicamentoE = new();
            using BDHospitalContext db = new();

            medicamentoE = (from medicamento in db.Medicamentos
                            where medicamento.Iidmedicamento == idMedicamento
                            select new MedicamentoE
                            {
                                IdMedicamento = medicamento.Iidmedicamento,
                                Nombre = medicamento.Nombre,
                                Concentracion = medicamento.Concentracion,
                                IdFormaFarmaceutica = medicamento.Iidformafarmaceutica,
                                Precio = medicamento.Precio,
                                Stock = medicamento.Stock,
                                Presentacion = medicamento.Presentacion
                            }).FirstOrDefault();

            ViewBag.ListarFormaFarma = ListarFormaFarmaceutica();
            return View(medicamentoE);
        }

        [HttpPost]
        public IActionResult Agregar(MedicamentoE medicamentoE)
        {
            try
            {
                using BDHospitalContext db = new();

                if (!ModelState.IsValid)
                {
                    ViewBag.ListarFormaFarma = ListarFormaFarmaceutica();
                    return View(medicamentoE);
                }

                Medicamento medicamento = new();

                medicamento.Nombre = medicamentoE.Nombre;
                medicamento.Concentracion = medicamentoE.Concentracion;
                medicamento.Iidformafarmaceutica = medicamentoE.IdFormaFarmaceutica;
                medicamento.Precio = medicamentoE.Precio;
                medicamento.Stock = medicamentoE.Stock;
                medicamento.Presentacion = medicamentoE.Presentacion;
                medicamento.Bhabilitado = 1;

                db.Medicamentos.Add(medicamento);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
