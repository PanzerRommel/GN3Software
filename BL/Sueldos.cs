using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sueldos
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Sueldos.FromSqlRaw("SueldosGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Sueldos sueldos = new ML.Sueldos()
                            {
                                IDSueldo = obj.Idsueldo,
                                SueldoMensual = obj.SueldoMensual.Value,
                                FormaPago = obj.FormaPago,
                                Empleados = new ML.Empleados()
                                {
                                    ClaveEmpleado = obj.ClaveEmpleado.Value
                                }
                            };
                            result.Objects.Add(sueldos);
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
        public static ML.Result GetById(int IDSueldo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var objquery = context.Sueldos.FromSqlRaw($"SueldosGetById {IDSueldo}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Sueldos sueldos = new ML.Sueldos();
                        sueldos.IDSueldo = objquery.Idsueldo;
                        sueldos.SueldoMensual = objquery.SueldoMensual.Value;
                        sueldos.FormaPago = objquery.FormaPago;
                        sueldos.Empleados = new ML.Empleados();
                        sueldos.Empleados.ClaveEmpleado = objquery.ClaveEmpleado.Value;

                        result.Object = sueldos;

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
        public static ML.Result Delete(ML.Sueldos sueldos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"SueldosDelete {sueldos.IDSueldo}");
                    if(query >= 1)
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
        public static ML.Result Update(ML.Sueldos sueldos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"SueldosUpdate {sueldos.IDSueldo}  , {sueldos.SueldoMensual} , '{sueldos.FormaPago}' , {sueldos.Empleados.ClaveEmpleado}");
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
        public static ML.Result Add(ML.Sueldos sueldos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"SueldosAdd {sueldos.SueldoMensual} , '{sueldos.FormaPago}' , {sueldos.Empleados.ClaveEmpleado}");
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
