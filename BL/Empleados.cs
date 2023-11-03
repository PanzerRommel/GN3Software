using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleados
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();
                    result.Objects = new List<object>();
                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Empleados empleados = new ML.Empleados
                            {
                                ClaveEmpleado = obj.ClaveEmpleado,
                                NombreEmpleado = obj.NombreEmpleado,
                                FechaIngreso = obj.FechaIngreso,
                                FechaNacimiento = obj.FechaNacimiento,
                                Departamento = obj.Departamento,
                            };
                            result.Objects.Add(empleados);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int? ClaveEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var objquery = context.Empleados.FromSqlRaw($"EmpleadoGetById {ClaveEmpleado}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Empleados empleados = new ML.Empleados();
                        empleados.ClaveEmpleado = objquery.ClaveEmpleado;
                        empleados.NombreEmpleado = objquery.NombreEmpleado;
                        empleados.FechaIngreso = objquery.FechaIngreso;
                        empleados.FechaNacimiento = objquery.FechaNacimiento;
                        empleados.Departamento = objquery.Departamento;

                        result.Object = empleados;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(ML.Empleados empleados)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {empleados.ClaveEmpleado}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Eliminar Registro";
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Empleados empleados)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var fechaIngreso = empleados.FechaIngreso.ToString("yyyy-MM-dd"); // Formatear la fecha de ingreso
                    var fechaNacimiento = empleados.FechaNacimiento.ToString("yyyy-MM-dd"); // Formatear la fecha de nacimiento
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleados.NombreEmpleado}' , '{fechaIngreso}' , '{fechaNacimiento}' , '{empleados.Departamento}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Empleados empleados)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleados.ClaveEmpleado} , '{empleados.NombreEmpleado}' , '{empleados.FechaIngreso}' , '{empleados.FechaNacimiento}' , '{empleados.Departamento}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
