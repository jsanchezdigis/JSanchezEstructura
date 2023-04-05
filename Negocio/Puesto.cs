using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Puestos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    var query = context.Puestos().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            Puesto puesto = new Puesto();

                            puesto.PuestoID = obj.PuestoID;
                            puesto.Descripcion = obj.Puestos;

                            result.Objects.Add(puesto);
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
