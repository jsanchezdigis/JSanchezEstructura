using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public Puesto PuestoID { get; set; }
        public Departamento DepartamentoID { get; set; }
        public List<object> Empleados { get; set; }

        public static Result Add(Empleado empleado)
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    int query = context.EmpleadoAdd(
                        empleado.Nombre,
                        empleado.PuestoID.PuestoID,
                        empleado.DepartamentoID.DepartamentoID
                        );
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

        public static Result Update(Empleado empleado)
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    int query = context.EmpleadoUpdate(
                        empleado.EmpleadoID,
                        empleado.Nombre,
                        empleado.PuestoID.PuestoID,
                        empleado.DepartamentoID.DepartamentoID
                        );
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
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

        public static Result Delete(Empleado empleado)
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    int query = context.EmpleadoDelete(empleado.EmpleadoID);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
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

        public static Result GetAll(Empleado empleado)
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    var query = context.PuestoDepartamento(empleado.Nombre,empleado.PuestoID.PuestoID,empleado.DepartamentoID.DepartamentoID).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            empleado = new Empleado();

                            empleado.EmpleadoID = obj.EmpleadoID;
                            empleado.Nombre = obj.Nombre;

                            empleado.PuestoID = new Puesto();
                            empleado.PuestoID.PuestoID = obj.PuestoID;
                            empleado.PuestoID.Descripcion = obj.Puesto;

                            empleado.DepartamentoID = new Departamento();
                            empleado.DepartamentoID.DepartamentoID = obj.DepartamentoID;
                            empleado.DepartamentoID.Descripcion = obj.Departamento;

                            result.Objects.Add(empleado);
                        }
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

        public static Result GetById(int EmpleadoID)
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    var query = context.EmpleadoGetById(EmpleadoID).FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            Empleado empleado = new Empleado();

                            empleado.EmpleadoID = obj.EmpleadoID;
                            empleado.Nombre = obj.Nombre;

                            empleado.PuestoID = new Puesto();
                            empleado.PuestoID.PuestoID = obj.PuestoID;
                            empleado.PuestoID.Descripcion = obj.Puesto;

                            empleado.DepartamentoID = new Departamento();
                            empleado.DepartamentoID.DepartamentoID = obj.DepartamentoID;
                            empleado.DepartamentoID.Descripcion = obj.Departamento;

                            result.Object = empleado;
                        }
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
