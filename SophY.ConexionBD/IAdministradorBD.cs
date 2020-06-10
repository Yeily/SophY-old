using System;
using System.Collections.Generic;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Interface para la administración SQL a una base de datos
    /// </summary>
    public interface IAdministradorBD : IDisposable
    {
        #region Métodos

        /// <summary>
        /// Ejecuta una sentencia SQL de consulta
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la consulta que desea realizar</param>
        /// <returns>Devuelve un conjunto de datos en un DataTable</returns>
        IEnumerable<string> ConsultaSQL(string sentenciaSQL, string[] parametros, string[] valores);

        /// <summary>
        /// Ejecuta una acción SQL como CREATE, INSERT, UPDATE, DELETE, DROP, ALTER, etc
        /// </summary>
        /// <param name="sentenciaSQL">Sentencia SQL de la acción que desea ejecutar</param>
        /// <returns>Devuelve <c>true</c> si la sentencia SQL se ejecuta correctamente</returns>
        bool AccionSQL(string sentenciaSQL, string[] parametros, string[] valores);

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        string ValorCampo(string Tabla, string Campo);

        /// <summary>
        /// Consulta de un valor específico
        /// </summary>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <param name="Campo">Campo que desea consultar su valor</param>
        /// <param name="Condicion">Argumentos de la clausula WHERE; ej: campo = '????'...</param>
        /// <returns>Devuelve un único valor de una consulta</returns>
        string ValorCampo(string Tabla, string Campo, string Condicion);

        #endregion
    }
}