using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleados
    {
        public int ClaveEmpleado {  get; set; }
        public string? NombreEmpleado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Departamento { get; set; }

        public List<object>? Empleadoos { get; set; }
    }
}
