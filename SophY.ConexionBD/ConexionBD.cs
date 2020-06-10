using System;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Representa una conexión a una bases de datos tradicional 
    /// </summary>
    public class ConexionBD
    {
        private byte pTipo;
        private string pCadenaConexion;

        private string pServidor = "{{SERVIDOR}}";
        private string pNombreBD = "{{NOMBREBD}}";
        private string pUsuarioBD = "{{USUARIO}}";
        private string pPassUsuarioBD = "{{PASSWORD}}";
        private string pRutaCarpetaBD = "{{RUTACARPETA}}";
        private string pRutaBD = "{{RUTABD}}";
        private string pRutaXLS = "{{RUTAXLS}}";
        private string pRutaMDW = "{{RUTAMDW}}";

        #region Constructores
        /// <summary>
        /// Representa una conexión a una base de datos, pero debe completar sus parametros atraves de sus propiedades
        /// </summary>
        public ConexionBD(byte tipo, string cadenaConexion)
        {
            pTipo = tipo;
            pCadenaConexion = cadenaConexion;
        }

        /// <summary>
        /// Representa una conexión a una base de datos. Sólo especifique los parametros necesarios según lo requiera
        /// </summary>
        /// <param name="tipo">Tipo de conexión que desea realizar</param>
        /// <param name="cadenaConexion">Cadena de conexión necesaria para conectrase a la base de datos</param>
        /// <param name="servidor">Nombre, ruta o IP del servidor donde se encuenta la base de datos</param>
        /// <param name="nombreBD">Nombre de la base de datos a la que se desea conectar</param>
        /// <param name="usuario">Nombre del usuario administrador de la base de datos</param>
        /// <param name="password">Contraseña del usuarios administrador de la base de datos</param>
        /// <param name="rutaCarpeta">Ruta de la carpeta donde se aloja la base de datos</param>
        /// <param name="rutaBD">Ruta completa, incluyendo el nombre de la base de datos</param>
        /// <param name="rutaXLS">Ruta completa, incluyendo el nombre del archivo de excel</param>
        /// <param name="rutaMDW">Ruta completa, incluyendo el nombre dedel archivo .mdw</param>
        public ConexionBD(byte tipo, string cadenaConexion, string servidor = "", string nombreBD = "", string usuario = "",
            string password = "", string rutaCarpeta = "", string rutaBD = "", string rutaXLS = "", string rutaMDW = "")
        {
            string strConexion = cadenaConexion.Replace(pServidor, servidor);
            strConexion = strConexion.Replace(pNombreBD, nombreBD);
            strConexion = strConexion.Replace(pUsuarioBD, usuario);
            strConexion = strConexion.Replace(pPassUsuarioBD, password);
            strConexion = strConexion.Replace(pRutaCarpetaBD, rutaCarpeta);
            strConexion = strConexion.Replace(pRutaBD, rutaBD);
            strConexion = strConexion.Replace(pRutaXLS, rutaXLS);
            strConexion = strConexion.Replace(pRutaMDW, rutaMDW);

            pTipo = tipo;
            pCadenaConexion = strConexion;
            pServidor = servidor;
            pNombreBD = nombreBD;
            pUsuarioBD = usuario;
            pPassUsuarioBD = password;
            pRutaCarpetaBD = rutaCarpeta;
            pRutaBD = rutaBD;
            pRutaXLS = rutaXLS;
            pRutaMDW = rutaMDW;
        }

        #endregion

        #region Propiedades
        /// <summary>
        /// Establese o regresa el tipo de conexión
        /// </summary>
        public byte TipoConexion
        {
            get { return pTipo; }
            set { pTipo = value; }
        }

        /// <summary>
        /// Establese o regresa la cadena de conexión actual
        /// </summary>
        public string CadenaConexion
        {
            get { return pCadenaConexion; }
        }

        /// <summary>
        /// Establese o regresa el servidor de la base de datos
        /// </summary>
        public string Servidor
        {
            get { return pServidor; }
            set
            {
                pServidor = value;
                pCadenaConexion = pCadenaConexion.Replace(pServidor, value);
            }
        }

        /// <summary>
        /// Establese o regresa el nombre de la base de datos
        /// </summary>
        public string NombreBD
        {
            get { return pNombreBD; }
            set
            {
                pNombreBD = value;
                pCadenaConexion = pCadenaConexion.Replace(pNombreBD, value);
            }
        }

        /// <summary>
        /// Establese o regresa el nombre del usuario administrador de la base de datos
        /// </summary>
        public string Usuario
        {
            get { return pUsuarioBD; }
            set
            {
                pUsuarioBD = value;
                pCadenaConexion = pCadenaConexion.Replace(pUsuarioBD, value);
            }
        }

        /// <summary>
        /// Establese la contraseña del usuario administrador de la base de datos
        /// </summary>
        public string PassUsuario
        {
            set
            {
                pPassUsuarioBD = value;
                pCadenaConexion = pCadenaConexion.Replace(pPassUsuarioBD, value);
            }
        }

        /// <summary>
        /// Establese o regresa la ruta de la carpeta donde se aloja la base de datos
        /// </summary>
        public string RutaCarpetaBD
        {
            get { return pRutaCarpetaBD; }
            set
            {
                pRutaCarpetaBD = value;
                pCadenaConexion = pCadenaConexion.Replace(pRutaCarpetaBD, value);
            }
        }

        /// <summary>
        /// Establese o regresa la ruta completa, incluyendo el nombre, de la base de datos
        /// </summary>
        public string RutaBD
        {
            get { return pRutaBD; }
            set
            {
                pRutaBD = value;
                pCadenaConexion = pCadenaConexion.Replace(pRutaBD, value);
            }
        }

        /// <summary>
        /// Establese o regresa la ruta completa, incluyendo el nombre, del archivo de excel
        /// </summary>
        public string RutaXLS
        {
            get { return pRutaXLS; }
            set
            {
                pRutaXLS = value;
                pCadenaConexion = pCadenaConexion.Replace(pRutaXLS, value);
            }
        }

        /// <summary>
        /// Establese o regresa la ruta completa, incluyendo el nombre, del archivo .mdw
        /// </summary>
        public string RutaMDW
        {
            get { return pRutaMDW; }
            set
            {
                pRutaMDW = value;
                pCadenaConexion = pCadenaConexion.Replace(pRutaMDW, value);
            }
        }

        #endregion
    }
}