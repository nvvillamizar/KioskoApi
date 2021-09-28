﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Ciel.Prueba.NetCore.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            Personas = new HashSet<Persona>();
        }

        public int Iidsexo { get; set; }
        public string Nombre { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
