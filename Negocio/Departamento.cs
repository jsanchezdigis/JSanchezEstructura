using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Departamentos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.JSanchezEstructuraEntities context = new AccesoDatos.JSanchezEstructuraEntities())
                {
                    var query = context.Departamentos().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            Departamento departamento = new Departamento();

                            departamento.DepartamentoID = obj.DepartamentoID;
                            departamento.Descripcion = obj.Departamento;

                            result.Objects.Add(departamento);
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
