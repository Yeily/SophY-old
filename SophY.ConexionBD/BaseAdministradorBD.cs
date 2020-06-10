using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Clase abstracta, base del administrador de bases de datos (OBDC, OleDB, SQL Client)
    /// </summary>
    public abstract class BaseAdministradorBD : IAdministradorBD
    {
        private IDbConnection pConexion;
        private IDbCommand pComando;
        private IDbDataAdapter pAdaptador;
        private IDbDataParameter pParametros;

        #region Propiedades
        /// <summary>
        /// Conexión a la base de datos
        /// </summary>
        protected ConexionBD ConexionGenerica { get; set; }

        #endregion

        #region Métodos protegidos
        /// <summary>
        /// Especifica la clase Connection que debe instanciarse según el tipo 
        /// </summary>
        /// <returns>Devuelve la <c>Connection</c> instanciada</returns>
        protected virtual IDbConnection ConexionDatos()
        {
            switch (ConexionGenerica.TipoConexion)
            {
                case TipoConexion.ODBC:
                    pConexion = new OdbcConnection(ConexionGenerica.CadenaConexion);
                    break;
                case TipoConexion.OleDB:
                    pConexion = new OleDbConnection(ConexionGenerica.CadenaConexion);
                    break;
                case TipoConexion.SqlClient:
                    pConexion = new SqlConnection(ConexionGenerica.CadenaConexion);
                    break;
                default:
                    pConexion = null;
                    break;
            }

            return pConexion;
        }

        /// <summary>
        /// Especifica la clase DataAdapter que debe instanciarse según el tipo 
        /// </summary>
        /// <returns>Devuelve el <c>DataAdapter</c> instanciado</returns>
        protected virtual IDbDataAdapter AdaptadorDatos()
        {
            switch (ConexionGenerica.TipoConexion)
            {
                case TipoConexion.ODBC:
                    pAdaptador = new OdbcDataAdapter();
                    break;
                case TipoConexion.OleDB:
                    pAdaptador = new OleDbDataAdapter();
                    break;
                case TipoConexion.SqlClient:
                    pAdaptador = new SqlDataAdapter();
                    break;
                default:
                    pAdaptador = null;
                    break;
            }

            return pAdaptador;
        }

        /// <summary>
        /// Especifica la clase Command que debe instanciarse según el tipo 
        /// </summary>
        /// <returns>Devuelve el <c>Command</c> instanciado</returns>
        protected virtual IDbCommand ComandoDatos()
        {
            switch (ConexionGenerica.TipoConexion)
            {
                case TipoConexion.ODBC:
                    pComando = new OdbcCommand();
                    break;
                case TipoConexion.OleDB:
                    pComando = new OleDbCommand();
                    break;
                case TipoConexion.SqlClient:
                    pComando = new SqlCommand();
                    break;
                default:
                    pComando = null;
                    break;
            }

            return pComando;
        }

        /// <summary>
        /// Especifica la clase DataParameter que debe instanciarse según el tipo 
        /// </summary>
        /// <param name="nombreParametro">Nombre del parametro a utilizar</param>
        /// <param name="valor">Valor de dicho parametro</param>
        /// <returns>Devuelve el <c>DataParameter</c> instanciado</returns>
        protected virtual IDbDataParameter ParametrosDatos(string nombreParametro, object valor)
        {
            switch (ConexionGenerica.TipoConexion)
            {
                case TipoConexion.ODBC:
                    pParametros = new OdbcParameter(nombreParametro, valor);
                    break;
                case TipoConexion.OleDB:
                    pParametros = new OleDbParameter(nombreParametro, valor);
                    break;
                case TipoConexion.SqlClient:
                    pParametros = new SqlParameter(nombreParametro, valor);
                    break;
                default:
                    pParametros = null;
                    break;
            }

            return pParametros;
        }

        #endregion

        #region Métodos públicos abstractos
        /// <summary>
        /// Ejecuta una sentencia SQL de consulta
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la consulta que desea realizar</param>
        /// <returns>Devuelve un conjunto de datos en un DataTable</returns>
        public abstract IEnumerable<string> ConsultaSQL(string sentenciaSQL, string[] parametros, string[] valores);

        /// <summary>
        /// Ejecuta una acción SQL como CREATE, INSERT, UPDATE, DELETE, DROP, ALTER, etc
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la acción que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la sentencia SQL se ejecuta correctamente</returns>
        public abstract bool AccionSQL(string sentenciaSQL, string[] parametros, string[] valores);

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        public abstract string ValorCampo(string Tabla, string Campo);

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <param name="Condicion">Argumentos de la clausula WHERE; ej: campo = '????'...</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        public abstract string ValorCampo(string Tabla, string Campo, string Condicion);

        #endregion

        #region Dispose
        /// <summary>
        /// Libera todos los recursos consumidos en la conexión
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera todos los recursos consumidos en la conexión
        /// </summary>
        /// <param name="disposing">¿Liberar los recursos administrados?</param>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            if (pComando != null)
            {
                pComando.Dispose();
            }

            if (pConexion != null)
            {
                if (pConexion.State == ConnectionState.Open)
                {
                    pConexion.Close();
                }

                pConexion.Dispose();
            }

            if (pAdaptador != null)
            {
                pAdaptador = null;
            }

            pComando = null;
            pConexion = null;
            //}
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~BaseAdministradorBD()
        {
            Dispose(false);
        }

        #endregion
    }
}