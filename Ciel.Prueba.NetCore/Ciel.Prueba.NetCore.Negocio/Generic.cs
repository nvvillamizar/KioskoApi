using Ciel.Prueba.NetCore.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ciel.Prueba.NetCore.Negocio
{
    public class Generic
    {

        public static string CifrarDatos(string dato)
        {
            SHA256Managed sha = new();
            byte[] datoSinCifrar = Encoding.Default.GetBytes(dato);
            byte[] datoCifrado = sha.ComputeHash(datoSinCifrar);

            return BitConverter.ToString(datoCifrado).Replace("-", "");
        }

        public static List<PaginaE> ListaPagina { get; set; } = new();
        public static List<string> ListaController { get; set; } = new();
        public static List<string> ListaMensaje { get; set; } = new();
        public static List<string> ListaAccion { get; set; } = new();

    }
}
