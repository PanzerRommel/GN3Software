using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int ClaveEmpleado { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string? Departamento { get; set; }

    public virtual ICollection<Sueldo> Sueldos { get; set; } = new List<Sueldo>();
}
