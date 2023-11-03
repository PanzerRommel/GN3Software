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
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.CatalogoDepartamentos.FromSqlRaw("DepartamentoGetAll").ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.CatalogoDepartamentos catalogoDepartamentos = new ML.CatalogoDepartamentos()
                            {
                                ClaveDepartamento = obj.ClaveDepartamento,
                                Descripcion = obj.Descripcion
                            };
                            result.Objects.Add(catalogoDepartamentos);
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
        public static ML.Result GetById(int ClaveDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var objquery = context.CatalogoDepartamentos.FromSqlRaw($"DepartamentoGetById {ClaveDepartamento}").AsEnumerable().FirstOrDefault();
                    if(objquery != null)
                    {
                        ML.CatalogoDepartamentos catalogoDepartamentos = new ML.CatalogoDepartamentos();
                        catalogoDepartamentos.ClaveDepartamento = objquery.ClaveDepartamento;
                        catalogoDepartamentos.Descripcion = objquery.Descripcion;

                        result.Object = catalogoDepartamentos;
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
        public static ML.Result Delete(ML.CatalogoDepartamentos departamentos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoDelete {departamentos.ClaveDepartamento}");
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
        public static ML.Result Update(ML.CatalogoDepartamentos catalogoDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoUpdate {catalogoDepartamento.ClaveDepartamento} , '{catalogoDepartamento.Descripcion}'");
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
        public static ML.Result Add(ML.CatalogoDepartamentos departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaGn3Context context = new DL.FlunaGn3Context())
                {
                    var departamentoToAdd = new DL.CatalogoDepartamento
                    {
                        // Mapea las propiedades desde ML.CatalogoDepartamentos
                        ClaveDepartamento = departamento.ClaveDepartamento,
                        Descripcion = departamento.Descripcion
                    };

                    context.CatalogoDepartamentos.Add(departamentoToAdd);
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
