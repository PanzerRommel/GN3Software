﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Sueldos
    {
        public int IDSueldos {  get; set; }
        public decimal SueldoMensual { get; set; }
        public string FormaPago { get; set; }

        public ML.Empleados Empleados { get; set; }
        public List<object> Empleadoos { get; set; }
    }
}
