using Ciel.Prueba.NetCore.Entidades;
using Ciel.Prueba.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using cm = System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Ciel.Prueba.NetCore.Filters;

namespace Ciel.Prueba.NetCore.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class EspecialidadController : Controller
    {

        public static List<EspecialidadE> lista;
        public FileResult Exportar(string[] propiedades, string tipoReporte)
        {
            string tipoDato = "";
            byte[] buffer = null;

            switch (tipoReporte)
            {
                case "excel":
                    buffer = ExportarExcelDatos(propiedades, lista);
                    tipoDato = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    
                    break;

                case "pdf":

                    buffer = ExportarPdfDatos(propiedades, lista);
                    tipoDato = "application/pdf";

                    break;

                case "word":

                    buffer = ExportarWordDatos(propiedades, lista);
                    tipoDato = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                    break;

                default:

                    return null;
            }

            return File(buffer, tipoDato);
        }

        public byte[] ExportarWordDatos<T>(string[] propiedades, List<T> lista)
        {
            Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);
            using MemoryStream ms = new();
            WordDocument document = new();
            WSection seccion = document.AddSection() as WSection;
            seccion.PageSetup.Margins.All = 72;
            seccion.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
            IWParagraph paragraph = seccion.AddParagraph();
            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            WTextRange textRange = paragraph.AppendText("Reporte en Word") as WTextRange;
            textRange.CharacterFormat.FontSize = 20f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.AliceBlue;

            IWTable table = seccion.AddTable();
            int numeroCabeceras = propiedades.Length;
            int numeroFilas = lista.Count;
            table.ResetCells(numeroFilas + 1, numeroCabeceras);

            for (int i = 0; i < numeroCabeceras; i++) table[0, i].AddParagraph().AppendText(diccionario[propiedades[i]]);
            

            int fila = 1;
            int columna = 0;

            foreach (object item in lista)
            {
                columna = 0;
                foreach (string propiedad in propiedades)
                {
                    table[fila, columna].AddParagraph().AppendText(item.GetType().GetProperty(propiedad).GetValue(item).ToString());
                    columna++;
                }
                fila++;
            }

            document.Save(ms, FormatType.Docx);

            return ms.ToArray();
        }

        public byte[] ExportarPdfDatos<T>(string[] propiedades, List<T> lista)
        {
            using MemoryStream ms = new();
            PdfWriter writer = new(ms);
            Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

            using var pdfDoc = new PdfDocument(writer);
            Document doc = new(pdfDoc);
            Paragraph c1 = new("Reporte Pdf");
            c1.SetFontSize(20);
            c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            doc.Add(c1);

            Table table = new(propiedades.Length);
            Cell celda;

            for (int i = 0; i < propiedades.Length; i++)
            {
                celda = new Cell();
                celda.Add(new Paragraph(diccionario[propiedades[i]]));
                table.AddHeaderCell(celda);
            }

            foreach(object item in lista)
            {
                foreach(string propiedad in propiedades)
                {
                    celda = new Cell();
                    celda.Add(new Paragraph(item.GetType().GetProperty(propiedad).GetValue(item).ToString()));
                    table.AddCell(celda);
                }
            }

            doc.Add(table);
            doc.Close();
            writer.Close();

            return ms.ToArray();
        }

        public byte[] ExportarExcelDatos<T>(string[] propiedades, List<T> lista)
        {

            using MemoryStream ms = new();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage ep = new();
            ep.Workbook.Worksheets.Add("Hoja");
            ExcelWorksheet ex = ep.Workbook.Worksheets[0];


            Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

            for (int i = 0; i < propiedades.Length; i++)
            {
                ex.Cells[1, i + 1].Value = diccionario[propiedades[i]];
                ex.Column(i + 1).Width = 50;
            }

            int fila = 2;
            int col = 1;

            foreach(object item in lista)
            {
                col = 1;
                foreach(string propiedad in propiedades)
                {
                    ex.Cells[fila, col].Value = item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                    col++;
                }
                fila++;
            }

            ep.SaveAs(ms);
            byte[] buffer = ms.ToArray();

            return buffer;

        }

        public List<EspecialidadE> BuscarEspecialidad(string nombreEspecialidad)
        {
            List<EspecialidadE> listaEspecialidad = new();

            using BDHospitalContext db = new();
            listaEspecialidad = (from especialidad in db.Especialidads
                                 where especialidad.Bhabilitado == 1
                                 select new EspecialidadE
                                 {
                                     IdEspecialidad = especialidad.Iidespecialidad,
                                     Nombre = especialidad.Nombre,
                                     Descripcion = especialidad.Descripcion
                                 }).ToList();

            if (nombreEspecialidad != "null") listaEspecialidad = listaEspecialidad.Where(especialidad => especialidad.Nombre.Contains(nombreEspecialidad)).ToList();

            lista = listaEspecialidad;

            return listaEspecialidad;
        }
        public IActionResult Index()
        {       
            return View();
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(EspecialidadE especialidadE)
        {
            try
            {              
                using BDHospitalContext db = new();

                int contarSiExiste = db.Especialidads.Where(nombre => nombre.Nombre.ToLower() == especialidadE.Nombre.ToLower()).Count();

                if (!ModelState.IsValid || contarSiExiste > 0)
                {
                    especialidadE.MensajeError = "El nombre de la especialidad ya existe registrada.";
                    return View(especialidadE);
                }

                Especialidad especialidad = new();

                especialidad.Nombre = especialidadE.Nombre;
                especialidad.Descripcion = especialidadE.Descripcion;
                especialidad.Bhabilitado = 1;
                db.Especialidads.Add(especialidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int idEspecialidad)
        {
            EspecialidadE especialidadE = new(); 
            using BDHospitalContext db = new();

            especialidadE = (from especialidad in db.Especialidads
                             where especialidad.Bhabilitado == 1
                             && especialidad.Iidespecialidad == idEspecialidad
                             select new EspecialidadE
                             {
                                 IdEspecialidad = especialidad.Iidespecialidad,
                                 Nombre = especialidad.Nombre,
                                 Descripcion = especialidad.Descripcion
                             }).FirstOrDefault();

            return View(especialidadE);
        }

        [HttpPost]
        public IActionResult Editar(EspecialidadE especialidadE)
        {
            using BDHospitalContext db = new();

            int contarSiExiste = db.Especialidads.Where(nombre => nombre.Nombre.ToLower() == especialidadE.Nombre.ToLower()).Count();

            if (!ModelState.IsValid || contarSiExiste > 0)
            {
                especialidadE.MensajeError = "El nombre de la especialidad ya existe registrada.";
                return View(especialidadE);
            }

            Especialidad obtenerEspecialidad = db.Especialidads.Where(e => e.Iidespecialidad == especialidadE.IdEspecialidad).FirstOrDefault();
            obtenerEspecialidad.Nombre = especialidadE.Nombre;
            obtenerEspecialidad.Descripcion = especialidadE.Descripcion;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public int Eliminar(int idEspecialidad)
        {
            int res = 0;
            try
            {
                using BDHospitalContext db = new();

                Especialidad obtenerEspecialidad = db.Especialidads.Where(e => e.Iidespecialidad == idEspecialidad).FirstOrDefault();
                obtenerEspecialidad.Bhabilitado = 0;
                db.SaveChanges();

                res = 1;
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
    }
}
