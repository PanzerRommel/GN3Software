using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PL_MVC.Controllers
{
    public class CatalogoDepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.CatalogoDepartamentos catalogoDepartamentos = new ML.CatalogoDepartamentos();
            ML.Result result = BL.Departamento.GetAll();

            if(result.Correct)
            {
                catalogoDepartamentos.Departamento = result.Objects;
                return View(catalogoDepartamentos);
            }
            return View(catalogoDepartamentos);
        }

        [HttpGet]
        public ActionResult Form(int? ClaveDepartamento)
        {
            ML.CatalogoDepartamentos catalogoDepartamentos = new ML.CatalogoDepartamentos();
            if(ClaveDepartamento == null)
            {
                return View(catalogoDepartamentos);
            }
            else
            {
                ML.Result result = BL.Departamento.GetById(ClaveDepartamento.Value);
                if(result.Correct)
                {
                    catalogoDepartamentos = ((ML.CatalogoDepartamentos)result.Object);
                    return View(catalogoDepartamentos);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.CatalogoDepartamentos departamento)
        {
            ML.Result result = new ML.Result();

            if (departamento.ClaveDepartamento == 0)
            {
                result = BL.Departamento.Add(departamento);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro de manera exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al agregar el registro";
                }
            }
            else
            {
                result = BL.Departamento.Update(departamento);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualización exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al actualizar el registro";
                }
                return PartialView("Modal");
            }

            return View(departamento);
        }


        // GET: CatalogoDepartamentoController/Delete/5
        public ActionResult Delete(ML.CatalogoDepartamentos catalogoDepartamentos)
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.Delete(catalogoDepartamentos);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado Con Exitoso";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "Se A Producido Un Error" + result.ErrorMessage;
                return PartialView("Modal");
            }
            
            return View(catalogoDepartamentos);
        }
        
        
    }
}
