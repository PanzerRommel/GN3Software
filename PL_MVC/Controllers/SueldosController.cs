using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace PL_MVC.Controllers
{
    public class SueldosController : Controller
    {
        [HttpGet]
        // GET: SueldosController
        public ActionResult GetAll()
        {
            ML.Sueldos sueldos = new ML.Sueldos();
            ML.Result result = BL.Sueldos.GetAll();
            if (result.Correct)
            {
                sueldos.Sueldo = result.Objects;
                return View(sueldos);
            }
           return View(sueldos);
        }
        [HttpGet]
        // GET: SueldosController/Details/5
        public ActionResult Form(int? IDSueldo)
        {
            ML.Sueldos sueldos = new ML.Sueldos();
            ML.Result result = BL.Empleados.GetAll();

            sueldos.Empleados = new ML.Empleados();
            if(IDSueldo == null)
            {
                sueldos.Empleados.Empleadoos = result.Objects;
                return View(sueldos);
            }
            else
            {
                ML.Result result1 = BL.Sueldos.GetById(IDSueldo.Value);

                if (result.Correct)
                {
                    sueldos = ((ML.Sueldos)result.Object);
                    sueldos.Empleados.Empleadoos = result1.Objects;
                    return View(sueldos);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        // GET: SueldosController/Create
        public ActionResult Form(ML.Sueldos sueldos)
        {
            ML.Result result = new ML.Result();
            if(sueldos.IDSueldo == 0)
            {
                result =BL.Sueldos.Add(sueldos);
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
                result = BL.Sueldos.Update(sueldos);
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
            return View(sueldos) ;
        }

        [HttpDelete]
        public ActionResult Delete(ML.Sueldos sueldos)
        {
           ML.Result result = new ML.Result() ;
            result = BL.Sueldos.Delete(sueldos) ;
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado Con Exitoso";
            }
            else
            {
                ViewBag.Mensaje = "Se A Producido Un Error" + result.ErrorMessage;

            }
            return PartialView("Modal");
        }
    }
}
