using System;
using System.Collections.Generic;

namespace DL;

public partial class Sueldo
{
    public int Idsueldo { get; set; }

    public decimal? SueldoMensual { get; set; }

    public string? FormaPago { get; set; }

    public int? ClaveEmpleado { get; set; }

    public virtual Empleado? ClaveEmpleadoNavigation { get; set; }
}
