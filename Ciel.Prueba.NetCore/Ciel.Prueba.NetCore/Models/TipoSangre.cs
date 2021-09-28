﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Ciel.Prueba.NetCore.Models
{
    public partial class TipoSangre
    {
        public TipoSangre()
        {
            Pacientes = new HashSet<Paciente>();
        }

        public int Iidtiposangre { get; set; }
        public string Nombre { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
