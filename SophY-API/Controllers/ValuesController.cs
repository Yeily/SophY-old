using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SophY.ConexionBD;

namespace SophY_API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            using (ControlBD cxn = new ControlBD(new ConexionBD(TipoConexion.SqlClient, CadenaConexion.CLIENTE_SQL, servidor: "local", nombreBD: "PruebaY", usuario: "sa", password: "Yeily123")))
            {
                var s = cxn.Consulta("select * from Tabla_Prueba");
                return s;
            }

            //return s;
            //using (SqlConnection bdSql = new SqlConnection(_cadenaConexion))

            //{

            //    using (SqlCommand bdComando = new SqlCommand("pa_Servicios", bdSql))

            //    {

            //        bdComando.CommandType = CommandType.StoredProcedure;
            //        bdComando.Parameters.Add(new SqlParameter("@vIdCliente", idCliente));
            //        var servicios = new List();
            //        await bdSql.OpenAsync();

            //        using (var recordset = await bdComando.ExecuteReaderAsync())
            //        {
            //            while (await recordset.ReadAsync())
            //            {
            //                // asignamos los valores del recordset mediante un 
            //                // método en el que formateamos los valores recibidos
            //                servicios.Add(RecordsetServicios(recordset));
            //            }
            //        }

            //        return servicios;

            //    }

            //}
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
