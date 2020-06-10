using System;
using System.Collections.Generic;
using System.Data;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Clase genérica, representa un manipulador básico de bases de datos tradicionales (OBDC, OleDB, SQL Client)
    /// </summary>
    public class AdministradorBD : BaseAdministradorBD
    {
        #region Constructores
        /// <summary>
        /// Representa una conexión a una base de datos, pero debe completar sus parametros atraves de sus propiedades
        /// </summary>
        public AdministradorBD() { }

        /// <summary>
        /// Representa una conexión a una base de datossegún el parametro especificado
        /// </summary>
        /// <param name="conexion">Conexión con los parametros a la base de datos que desea manipular</param>
        public AdministradorBD(ConexionBD conexion)
        {
            ConexionGenerica = conexion;
        }

        #endregion

        #region Métodos públicos sobreescritos
        /// <summary>
        /// Ejecuta una sentencia SQL de consulta
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la consulta que desea realizar</param>
        /// <returns>Devuelve un conjunto de datos en un DataTable</returns>
        public override IEnumerable<string> ConsultaSQL(string sentenciaSQL, string[] parametros, string[] valores)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();
            IDbDataAdapter adp = AdaptadorDatos();

            cmd.CommandText = sentenciaSQL;
            cmd.Connection = cxn;
            adp.SelectCommand = cmd;

            foreach(string parametro in parametros)
            {
                cmd.Parameters.AddWithValues(parametro, "");
            }

            try
            {
                using (var datos = cmd.ExecuteReader())
                {
                    return (IEnumerable<string>)datos;
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Dispose();
            }

            //DataSet ds = new DataSet();

            //try
            //{
            //    adp.Fill(ds);
            //    var result = ds.Tables[0];

            //    return (IEnumerable<string>)result;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    this.Dispose();
            //}
        }

        /// <summary>
        /// Ejecuta una acción SQL como CREATE, INSERT, UPDATE, DELETE, DROP, ALTER, etc
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la acción que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la sentencia SQL se ejecuta correctamente</returns>
        public override bool AccionSQL(string sentenciaSQL, string[] parametros, string[] valores)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();

            try
            {
                cxn.Open();
                cmd.CommandText = sentenciaSQL;
                cmd.Connection = cxn;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        public override string ValorCampo(string Tabla, string Campo)
        {
            return ValorCampo(Tabla, Campo, string.Empty);
        }

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <param name="Condicion">Argumentos de la cláusula WHERE; ej: campo = '????'...</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        public override string ValorCampo(string Tabla, string Campo, string Condicion)
        {
            string result;
            string cond = string.Empty;
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();
            IDbDataAdapter adp = AdaptadorDatos();

            if (!string.IsNullOrEmpty(Condicion))
            {
                cond = " WHERE " + Condicion;
            }
            cmd.CommandText = "SELECT " + Campo + " FROM " + Tabla + cond;
            cmd.Connection = cxn;
            adp.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        #endregion

        #region Métodos públicos virtuales

        /// <summary>
        /// Intenta ejecutar una lista de transacciones SQL
        /// </summary>
        /// <param name="sentenciasSQL">Lista de sentencias SQL de las acciones que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la transacción se ejecuta correctamente, o bien, realiza el roll back en su defecto</returns>
        public virtual bool TransaccionesSQL(List<string> sentenciasSQL)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();
            IDbTransaction trans;

            int c = 0;
            string sentenciaError = "";

            cxn.Open();
            trans = cxn.BeginTransaction();

            try
            {
                foreach (string sentencia in sentenciasSQL)
                {
                    c += 1;
                    sentenciaError = sentencia;
                    cmd.CommandText = sentencia;
                    cmd.Connection = cxn;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Error en la sentencia: " + c + "\n" + ex.Message);
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }

                this.Dispose();
            }
        }

        /// <summary>
        /// Intenta ejecutar una o varias transacciones SQL separadas por ";"
        /// </summary>
        /// <param name="sentenciaSQL">Sentencias SQL de las acciones que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la transacción se ejecuta correctamente, o bien, realiza el roll back en su defecto</returns>
        public virtual bool TransaccionesSQL(string sentenciaSQL)
        {
            List<string> trans = new List<string>();

            trans.Add(sentenciaSQL);

            return TransaccionesSQL(trans);
        }

        /// <summary>
        /// Utilice esta función si su motor de base de datos no soporta las transacciones SQL
        /// </summary>
        /// <param name="sentenciaSQL">Sentencias SQL de las acciónes que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la transacción se ejecuta correctamente, o bien, debe identificar cuales sentencias no se pudieron ejecutar</returns>
        public virtual bool TransaccionesSQLsinRollback(List<string> sentenciaSQL)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();

            int numeroError = 0;
            string sentenciaError = "";

            try
            {
                cxn.Open();
                foreach (string sentencia in sentenciaSQL)
                {
                    if (sentencia != null)
                    {
                        sentenciaError = sentencia;
                        cmd.CommandText = sentencia;
                        cmd.Connection = cxn;
                        cmd.ExecuteNonQuery();
                    }

                    numeroError += 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la sentencia: " + numeroError + "\n" + sentenciaError + "\n" + ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado con la acción SQL
        /// </summary>
        /// <param name="nombreSP">Nombre del procedimiento almacenado</param>
        /// <returns>Devuelve <c>true</c> el procedimiento almacenado se ejecuta correctamente</returns>
        public virtual bool EjecutarStoredProcedure(string nombreSP)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();

            try
            {
                cxn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nombreSP;
                cmd.Connection = cxn;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado con la acción SQL
        /// </summary>
        /// <param name="nombreSP">Nombre del procedimiento almacenado</param>
        /// <param name="parametros">Nombres de los parametros</param>
        /// <param name="valores">Valores de los parametros respectivamente</param>
        /// <returns>Devuelve <c>true</c> el procedimiento almacenado se ejecuta correctamente</returns>
        public virtual bool EjecutarStoredProcedure(string nombreSP, string[] parametros, string[] valores)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();

            try
            {
                cxn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nombreSP;
                cmd.Connection = cxn;

                int i = 0;
                foreach (string prm in parametros)
                {
                    IDbDataParameter param = ParametrosDatos(prm, valores[i]);
                    cmd.Parameters.Add(param);

                    i += 1;
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve datos como una consulta
        /// </summary>
        /// <param name="nombreSP">Nombre del procedimiento almacenado</param>
        /// <returns>Devuelve un conjunto de datos como un DataTable</returns>
        public virtual DataTable ConsultaStoredProcedure(string nombreSP)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();
            IDbDataAdapter adp = AdaptadorDatos();

            cmd.CommandText = nombreSP;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cxn;
            adp.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve datos como una consulta
        /// </summary>
        /// <param name="nombreSP">Nombre del procedimiento almacenado</param>
        /// <param name="parametros">Nombres de los parametros</param>
        /// <param name="valores">Valores de los parametros respectivamente</param>
        /// <returns>Devuelve un conjunto de datos como un DataTable</returns>
        public virtual DataTable ConsultaStoredProcedure(string nombreSP, string[] parametros, string[] valores)
        {
            IDbConnection cxn = ConexionDatos();
            IDbCommand cmd = ComandoDatos();
            IDbDataAdapter adp = AdaptadorDatos();

            cmd.CommandText = nombreSP;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cxn;

            int i = 0;
            foreach (string prm in parametros)
            {
                IDbDataParameter param = ParametrosDatos(prm, valores[i]);
                cmd.Parameters.Add(param);

                i += 1;
            }

            adp.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
        }

        #endregion

        #region Propiedades públicas
        /// <summary>
        /// Devuelve o establece la conexión a la ase de datos
        /// </summary>
        public ConexionBD Conexion
        {
            get { return base.ConexionGenerica; }
            set { base.ConexionGenerica = value; }
        }

        /// <summary>
        /// Propiedad de ayuda. Devuelve un mensaje con ejemplos de lenguaje SQL
        /// </summary>
        public string Ayuda
        {
            get
            {
                string a;

                a = "CREATE TABLE SEGOC.dbo.Puestos (ID INT NOT NULL IDENTITY(1,1),[Puesto] VARCHAR(50) NULL,PRIMARY KEY(Id));" +
                    " \r\n " +
                    "## Cambiar nombre a una tabla ## \r\n" +
                    "#EXEC sprename 'SEGOC.dbo.Empleados', 'SEGOC.dbo.Empleadosviejos'; \r\n " +
                    " \r\n " +
                    "## Resetear el campo autonumérico, pero esto podria provocar registros duplicados (mejor cerciorarse de cual es el ultimo registro activo) ## \r\n " +
                    "#DBCC CHECKIDENT ('SEGOC.dbo.Registros', RESEED,0) \r\n " +
                    " \r\n " +
                    "## Eliminar y volver a crear una tabla ## \r\n " +
                    "#TRUNCATE TABLE SEGOC.dbo.Empleados \r\n " +
                    "  \r\n " +
                    "## Importar de Excel ## \r\n " +
                    "#select * into Empresa FROM OPENROWSET('Microsoft.Jet.OLEDB.4.0', 'Excel 8.0;Database=C:\\Resultados.xls;HDR=YES', 'SELECT * FROM [Hoja1$]') \r\n " +
                    " \r\n " +
                    "## Cambiar tipo de datos de un campo ## \r\n " +
                    "#alter table SEGOC.dbo.Empleados \r\n " +
                    "alter column fechaingreso datetime not null; \r\n " +
                    " \r\n " +
                    "## Sentencias comunes ## \r\n " +
                    "INSERT INTO bodegas (campos) VALUES ('valores') \r\n " +
                    "SELECT campos INTO (nuevatabla) FROM tabla1 INNER JOIN tabla2 ON camporelacionado1 = camporelacionado2 \r\n " +
                    "DELETE FROM tabla WHERE condicion \r\n " +
                    "DELETE * FROM tabla \r\n " +
                    "SELECT * FROM tablas WHERE condicion ORDER BY campos [UNION] SELECT ... \r\n " +
                    "SELECT campos FROM tabla1 INNER JOIN tabla2 WHERE tabla1.campo = tabla2.campo \r\n " +
                    "UPDATE tabla SET campo1 = 'valor1' WHERE campo = 'valor'";

                return a;
            }
        }

        #endregion
    }
}
