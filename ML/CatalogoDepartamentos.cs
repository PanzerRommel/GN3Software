using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CatalogoDepartamentos
    {
        public int ClaveDepartamento { get; set; }
        public string? Descripcion { get; set; }

        public List<object> Departamento { get; set; }
    }
}
