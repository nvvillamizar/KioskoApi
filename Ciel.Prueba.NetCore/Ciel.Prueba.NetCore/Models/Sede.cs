﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Ciel.Prueba.NetCore.Models
{
    public partial class Sede
    {
        public Sede()
        {
            Cita = new HashSet<Citum>();
            Doctors = new HashSet<Doctor>();
        }

        public int Iidsede { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
