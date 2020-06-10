using System;
using System.Collections.Generic;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Clase para la manipulación de registros en la base de datos (ODBC, OleDB o SQL Client)
    /// </summary>
    public class ControlBD : IDisposable
    {
        private AdministradorBD bd;

        #region constructores
        /// <summary>
        /// Crea un modelo nuevo de conexión y manipulación de registros
        /// </summary>
        public ControlBD()
        {
            bd = new AdministradorBD();
        }

        /// <summary>
        /// Crea un modelo nuevo de conexión y manipulación de registros
        /// </summary>
        /// <param name="conexion">Conexión a la base de datos</param>
        public ControlBD(ConexionBD conexion)
            : this()
        {
            bd.Conexion = conexion;
        }

        #endregion

        #region Propiedades
        /// <summary>
        /// Devuelve o establese la conexión a utilizar para realizar el negocio de datos
        /// </summary>
        public ConexionBD Conexion
        {
            get { return bd.Conexion; }
            set { bd.Conexion = value; }
        }

        #endregion

        #region Métodos públicos
        /// <summary>
        /// Ideal para insertar registros nuevos en la base de datos
        /// </summary>
        /// <param name="Transacciones">Lista de sentencas SQL</param>
        public virtual bool Insertar(List<string> Transacciones)
        {
            try
            {
                return bd.TransaccionesSQL(Transacciones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Ideal para insertar un registro nuevo en la base de datos
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="campos">Cadena contenida de los campos de la tabla que desea llenar ej: campo1, campo2, ...</param>
        /// <param name="valores">Cadena con todos los valores de los campos que se van a llenar ej: 'valor1', 'valor2', numero1, ...</param>
        public virtual bool Insertar(string tabla, string campos, string valores)
        {
            return Ejecutar(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tabla, campos, valores));
        }

        /// <summary>
        /// Equivalente al UPDATE de SQL
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="modificadores">Cadena con los modificadores del registro ej: campo1 = 'valor_editado', campo2 = 'valor_editado2', ...</param>
        public virtual bool Actualizar(string tabla, string modificadores)
        {
            return Ejecutar(string.Format("UPDATE {0} SET {1}", tabla, modificadores));
        }

        /// <summary>
        /// Equivalente al UPDATE de SQL
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="modificadores">Cadena con los modificadores del registro ej: campo1 = 'valor_editado', campo2 = 'valor_editado2', ...</param>
        /// <param name="condicion">Argumentos de la cláusula WHERE; ej: campo = '????' ...</param>
        public virtual bool Actualizar(string tabla, string modificadores, string condicion)
        {
            return Ejecutar(string.Format("UPDATE {0} SET {1} WHERE {2}", tabla, modificadores, condicion));
        }

        /// <summary>
        /// Equivalente al DELETE de SQL y elimina <c>todos los registros de la tabla</c>
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        public virtual bool Eliminar(string tabla)
        {
            return Ejecutar(string.Format("DELETE FROM {0}", tabla));
        }

        /// <summary>
        /// Equivalente al DELETE de SQL y elimina los registros filtrados según la condición
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="condicion">Argumentos de la clausula WHERE; ej: campo = '????'...</param>
        public virtual bool Eliminar(string tabla, string condicion)
        {
            return Ejecutar(string.Format("DELETE FROM {0} WHERE {1}", tabla, condicion));
        }

        /// <summary>
        /// Ejecuta una acción SQL como CREATE, INSERT, UPDATE, DELETE, DROP, ALTER, etc
        /// </summary>
        /// <param name="accionSQL">Sentencia SQL de la acción que desea ejecutar</param>
        public virtual bool Ejecutar(string accionSQL)
        {
            List<string> lista = new List<string>();
            lista.Add(accionSQL);

            return Ejecutar(lista);
        }

        /// <summary>
        /// Ejecuta una acción SQL como CREATE, INSERT, UPDATE, DELETE, DROP, ALTER, etc
        /// </summary>
        /// <param name="accionesSQL">Sentencias SQL de las acciónes que desea ejecutar</param>
        public virtual bool Ejecutar(List<string> accionesSQL)
        {
            try
            {
                return bd.TransaccionesSQL(accionesSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Realiza una consulta a la base de datos como SELECT
        /// </summary>
        /// <param name="consultaSQL">Sentencia SQL de la consulta a la base de datos</param>
        /// <returns>Devuelve un DataTable con los datos</returns>
        public virtual IEnumerable<string> Consulta(string consultaSQL)
        {
            try
            {
                return bd.ConsultaSQL(consultaSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Realiza una consulta a la base de datos como SELECT
        /// </summary>
        /// <param name="campos">Cadena de nombres de los campos que desea seleccionar</param>
        /// <param name="tabla">Nombre de la tabla donde realizar la consulta</param>
        /// <returns>Devuelve un DataTable con los datos</returns>
        public virtual IEnumerable<string> Consulta(string campos, string tabla)
        {
            return Consulta(campos, tabla, "");
        }

        /// <summary>
        /// Realiza una consulta a la base de datos como SELECT
        /// </summary>
        /// <param name="campos">Cadena de nombres de los campos que desea seleccionar</param>
        /// <param name="tabla">Nombre de la tabla donde realizar la consulta</param>
        /// <param name="condicion">Argumentos de la clausula WHERE; ej: campo = '????'...</param>
        /// <returns>Devuelve un DataTable con los datos</returns>
        public virtual IEnumerable<string> Consulta(string campos, string tabla, string condicion)
        {
            string cond = string.Empty;

            if (!string.IsNullOrEmpty(condicion))
            {
                cond = " WHERE " + condicion;
            }

            return Consulta(string.Format("SELECT {0} FROM {1}{2}", campos, tabla, cond));
        }

        /// <summary>
        /// Obtiene un valor en una tabla
        /// </summary>
        /// <param name="campo">Nombre del campo a consulta su valor</param>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns>Retorna una cadena con el valor consultado</returns>
        public virtual string GetValor(string campo, string tabla)
        {
            return GetValor(tabla, campo, string.Empty);
        }

        /// <summary>
        /// Obtiene un valor en una tabla
        /// </summary>
        /// <param name="campo">Nombre del campo a consulta su valor</param>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="condicion">Argumento de la clausula WHERE ej: campo = '???'...</param>
        /// <returns>Retorna una cadena con el valor consultado</returns>
        public virtual string GetValor(string campo, string tabla, string condicion)
        {
            return bd.ValorCampo(tabla, campo, condicion);
        }

        ///// <summary>
        ///// Selecciona la primer fila de una consulta SQL
        ///// </summary>
        ///// <param name="consultaSQL">Sentencia SQL de la consulta</param>
        ///// <returns>Devuelve el registro completo en forma de DataRow</returns>
        //public virtual DataRow Seleccionar(string consultaSQL)
        //{
        //    DataRow result;

        //    try
        //    {
        //        using (DataTable dtConsulta = Consulta(consultaSQL))
        //        {
        //            if (dtConsulta.Rows.Count > 0)
        //            {
        //                result = dtConsulta.Rows[0];
        //            }
        //            else
        //            {
        //                result = null;
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Selecciona la primer fila de una consulta SQL
        ///// </summary>
        ///// <param name="campos">Cadena de nombres de los campos que desea seleccionar</param>
        ///// <param name="tabla">Nombre de la tabla donde realizar la consulta</param>
        ///// <param name="condicion">Argumentos de la clausula WHERE; ej: campo = '????'...</param>
        ///// <returns>Devuelve el registro completo en forma de DataRow</returns>
        //public virtual DataRow Seleccionar(string campos, string tabla, string condicion)
        //{
        //    return Seleccionar(string.Format("SELECT {0} FROM {1} WHERE {2}", campos, tabla, condicion));
        //}

        ///// <summary>
        ///// Muestra un formulario para seleccionar una elección
        ///// </summary>
        ///// <param name="datos">Tabla con los datos para escoger</param>
        ///// <param name="titulo">Título del formulario de consulta</param>
        ///// <param name="columnasVisibles">Cantidad de columnas visibles en la consulta</param>
        ///// <returns>Devuelve el registro completo en forma de DataRow</returns>
        //public virtual DataRow Seleccionar(DataTable datos, string titulo, int columnasVisibles)
        //{
        //    return Seleccionar(datos, titulo, columnasVisibles, 400, 500);
        //}

        ///// <summary>
        ///// Muestra un formulario para seleccionar una elección
        ///// </summary>
        ///// <param name="datos">Tabla con los datos para escoger</param>
        ///// <param name="titulo">Título del formulario de consulta</param>
        ///// <param name="columnasVisibles">Cantidad de columnas visibles en la consulta</param>
        ///// <param name="ancho">Tamaño del ancho que desea darle al formulario de consulta</param>
        ///// <param name="alto">Tamaño del alto que desea darle al formulario de consulta</param>
        ///// <returns>Devuelve el registro completo en forma de DataRow</returns>
        //public virtual DataRow Seleccionar(DataTable datos, string titulo, int columnasVisibles, int ancho, int alto)
        //{
        //    using (Seleccionador frmC = new Seleccionador(datos, titulo, columnasVisibles, ancho, alto))
        //    {
        //        frmC.ShowDialog();

        //        if (frmC.IsOk)
        //        {
        //            return frmC.Seleccionado;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Lista los valores direfentes de un campo en una tabla
        ///// </summary>
        ///// <param name="campo">Nombre del campo con los valores</param>
        ///// <param name="tabla">Nombre de la tabla que contiene el campo</param>
        ///// <returns>Retorna la lista de los valores sin repetidos</returns>
        //public List<string> Listar(string campo, string tabla)
        //{
        //    return Listar(campo, tabla, string.Empty);
        //}

        ///// <summary>
        ///// Lista los valores direfentes de un campo en una tabla
        ///// </summary>
        ///// <param name="campo">Nombre del campo con los valores</param>
        ///// <param name="tabla">Nombre de la tabla que contiene el campo</param>
        ///// <param name="condicion">Filtro que desea aplicar</param>
        ///// <returns>Retorna la lista de los valores sin repetidos</returns>
        //public List<string> Listar(string campo, string tabla, string condicion)
        //{
        //    string cond = string.Empty;
        //    List<string> result = new List<string>();

        //    if (!string.IsNullOrEmpty(condicion))
        //    {
        //        cond = "WHERE " + condicion;
        //    }

        //    string sqlConsulta = string.Format("SELECT DISTINCT {0} FROM {1} {2}ORDER BY {0};", campo, tabla, cond);
        //    using (DataTable dtBods = bd.ConsultaSQL(sqlConsulta))
        //    {
        //        foreach (DataRow fila in dtBods.Rows)
        //        {
        //            result.Add(fila[campo].ToString());
        //        }
        //    }

        //    return result;
        //}

        #endregion

        #region Dispose
        /// <summary>
        /// Libera todos los recursos consumidos
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera todos los recursos consumidos
        /// </summary>
        /// <param name="disposing">¿Liberar los recursos administrados?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (bd != null)
            {
                bd.Dispose();
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ControlBD()
        {
            Dispose(false);
        }

        #endregion
    }
}