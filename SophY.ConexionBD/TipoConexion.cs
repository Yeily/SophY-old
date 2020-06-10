using System;
namespace SophY.ConexionBD
{
    /// <summary>
    /// Tipo de conexión
    /// </summary>
    public class TipoConexion
    {
        /// <summary>
        /// Tipo ODBC = 0
        /// </summary>
        public const byte ODBC = 0;

        /// <summary>
        /// Tipo OleDB = 1
        /// </summary>
        public const byte OleDB = 1;

        /// <summary>
        /// Tipo SqlClient = 2
        /// </summary>
        public const byte SqlClient = 2;

        /// <summary>
        /// Tipo SQLite = 3
        /// </summary>
        public const byte SQLite = 3;
    }
}