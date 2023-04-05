using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        [HttpGet]
        public ActionResult GetAll()
        {
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.PuestoID = new Negocio.Puesto();
            empleado.DepartamentoID = new Negocio.Departamento();
            Negocio.Result result = Negocio.Empleado.GetAll(empleado);
            Negocio.Result resultPuesto = Negocio.Puesto.GetAll();
            Negocio.Result resultDepartamento = Negocio.Departamento.GetAll();

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                empleado.PuestoID.Puestos = resultPuesto.Objects;
                empleado.DepartamentoID.Departamentos = resultDepartamento.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }
        [HttpPost]
        public ActionResult GetAll(Negocio.Empleado empleado)
        {
            //empleado.PuestoID = new Negocio.Puesto();
            //empleado.DepartamentoID = new Negocio.Departamento();
            Negocio.Result result = Negocio.Empleado.GetAll(empleado);
            Negocio.Result resultPuesto = Negocio.Puesto.GetAll();
            Negocio.Result resultDepartamento = Negocio.Departamento.GetAll();

            if (result.Correct)
            {
                empleado.DepartamentoID.Departamentos = resultDepartamento.Objects;
                empleado.PuestoID.Puestos = resultPuesto.Objects;
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpGet]
        public ActionResult Form(int EmpleadoID)
        {
            Result resultPuesto = Puesto.GetAll();
            Result resultDepartamento = Departamento.GetAll();

            Empleado empleado = new Empleado();
            empleado.PuestoID = new Puesto();
            empleado.DepartamentoID = new Departamento();

            if (resultPuesto.Correct & resultDepartamento.Correct)
            {
                empleado.PuestoID.Puestos = resultPuesto.Objects;
                empleado.DepartamentoID.Departamentos = resultDepartamento.Objects;
            }
            //GetById
            Result result = Negocio.Empleado.GetById(EmpleadoID);
            if (result.Correct)
            {
                empleado = (Empleado)result.Object;
                empleado.PuestoID.Puestos = resultPuesto.Objects;
                empleado.DepartamentoID.Departamentos = resultDepartamento.Objects;
                return View("GetAll",empleado);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al agregar el registro";
            }
            return View("Modal");
            //return RedirectToAction("GetAll");
        }
        
        [HttpPost]
        public ActionResult Form(Negocio.Empleado empleado)
        {
            Result result = new Result();
            if (empleado.EmpleadoID == 0)
            {
                //Add
                result = Negocio.Empleado.Add(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Se Agrego el registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar el registro";
                }
                return View("Modal");
                //return RedirectToAction("GetAll");
            }
            else
            {
                //Update
                result = Negocio.Empleado.Update(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Se Actualizo el registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro";
                }
                return View("Modal");
                //return RedirectToAction("GetAll");
            }
        }

        [HttpGet]
        public ActionResult Delete(int EmpleadoID)
        {
            Result result = new Result();
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.EmpleadoID = EmpleadoID;
            result = Negocio.Empleado.Delete(empleado);
            if (result.Correct)
            {
                ViewBag.Message = "Se Elimino el registro";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
            //return RedirectToAction("GetAll");
        }
    }
}