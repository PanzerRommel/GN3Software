using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadosController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleados empleados = new ML.Empleados();
            ML.Result result = BL.Empleados.GetAll();
            if (result.Correct)
            {
                empleados.Empleadoos = result.Objects;
            }
            return View(empleados);
        }
        [HttpGet]
        public ActionResult Form(int? ClaveEmpleado)
        {
            ML.Empleados empleados = new ML.Empleados();
            if (ClaveEmpleado == null)
            {
                return View(empleados);
            }
            else
            {
                ML.Result result = BL.Empleados.GetById(ClaveEmpleado.Value);

                if (result.Correct)
                {
                    empleados = ((ML.Empleados)result.Object);

                    return View(empleados);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Empleados empleados)
        {
            ML.Result result = new ML.Result();
            if (empleados.ClaveEmpleado == 0)
            {
                result = BL.Empleados.Add(empleados);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro de manera exitosa";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al agregar el registro";
                    return View("Modal");
                }
            }
            else
            {
                result = BL.Empleados.Update(empleados);
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
            return View(empleados);
        }

        public ActionResult Delete(ML.Empleados empleados)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleados.Delete(empleados);

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
            return View(empleados);
        }
    }
}
