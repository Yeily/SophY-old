using System;
using System.Collections.Generic;
using System.Xml;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Representa un archivo de configuración *.param con formato xml 
    /// </summary>
    public sealed class Parametro
    {
        private readonly string ELEMENTO_PRINCIPAL = "YEILY";
        private string pRuta;

        #region Constructores
        /// <summary>
        /// Inicializa un archivo de configuración .param
        /// </summary>
        public Parametro() { }

        /// <summary>
        /// Inicializa un archivo de configuración .param
        /// </summary>
        /// <param name="ruta">Ruta completa, incluye el nombre, del archivo de configuración</param>
        public Parametro(string ruta)
        {
            this.Ruta = ruta;
        }

        #endregion

        #region Métodos privados
        private XmlDocument CrearDocumento()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration protocolo = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(protocolo);

            return doc;
        }

        private XmlDocument AbrirDocumento()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pRuta);

            return doc;
        }

        private XmlElement ElementoDefault(XmlDocument documento)
        {
            XmlElement elemento = documento.CreateElement(ELEMENTO_PRINCIPAL);
            documento.AppendChild(elemento);

            return elemento;
        }

        #endregion

        #region Métodos públicos
        /// <summary>
        /// Crea un archivo .param vacío con la estructura básica para usar como archivo de configuración
        /// </summary>
        /// <returns>Devuelve <c>true</c> si el archivo se crea correctamente</returns>
        public bool Nuevo()
        {
            try
            {
                XmlDocument documento = CrearDocumento();
                XmlElement elemento = ElementoDefault(documento);
                documento.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Crea un archivo .param con la estructura básica para usar como archivo de configuración con una sección, una clave y su valor
        /// </summary>
        /// <param name="nuevaSeccion">Nombre de la sección que desea agregar</param>
        /// <param name="nuevaClave">Nombre de la clave que contendrá la sección</param>
        /// <param name="valorClave">Valor de la clave</param>
        /// <returns>Devuelve <c>true</c> si el archivo se crea correctamente</returns>
        public bool Nuevo(string nuevaSeccion, string nuevaClave, string valorClave = "")
        {
            try
            {
                XmlDocument documento = CrearDocumento();
                XmlElement elemento = ElementoDefault(documento);

                XmlNode nodoSeccion = documento.CreateElement(nuevaSeccion);
                elemento.AppendChild(nodoSeccion);

                XmlNode nodoClave = documento.CreateElement(nuevaClave);
                nodoClave.AppendChild(documento.CreateTextNode(valorClave));
                nodoSeccion.AppendChild(nodoClave);
                documento.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Elimina una clave en el archivo de configuración indicado
        /// </summary>
        /// <param name="seccion">Nombre de la sección donde se encuentra la clave a eliminar</param>
        /// <param name="clave">Nombre de la clave a eliminar</param>
        /// <returns>Devuelve <c>true</c> si la clave se elimina correctamente</returns>
        public bool EliminarClave(string seccion, string clave)
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode nodoClave = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion).SelectSingleNode(clave);
                XmlNode nodoSeccion = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion);

                nodoSeccion.RemoveChild(nodoClave);
                archivo.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una sección en el archivo de configuración indicado
        /// </summary>
        /// <param name="seccion">Nombre de la sección que desea eliminar</param>
        /// <returns>Devuelve <c>true</c> si la sección se elimina correctamente</returns>
        public bool EliminarSeccion(string seccion)
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode sec = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion);
                XmlNode nodo = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL);

                nodo.RemoveChild(sec);
                archivo.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el valor de una clave especificada
        /// </summary>
        /// <param name="seccion">Nombre de la sección donde se encuentra la clave a leer</param>
        /// <param name="clave">Nombre de la clave para obtener su valor</param>
        /// <returns>Devuelve el valor de una clave</returns>
        public string GetClave(string seccion, string clave)
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode palabraClave = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion).SelectSingleNode(clave);

                return palabraClave.InnerText;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las claves que contien e una sección especifica
        /// </summary>
        /// <param name="seccion">Nombre de la sección a leer</param>
        /// <returns>Devuelve una lista con los valores de las claves de una sección</returns>
        public List<string> GetClaves(string seccion)
        {
            List<string> lista = new List<string>();

            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNodeList nodos = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion).ChildNodes;

                foreach (XmlElement nodo in nodos)
                {
                    lista.Add(nodo.Name);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas la secciones que contiene un archivo de configuración
        /// </summary>
        /// <returns>Devuelve una lista con todas las secciones de un archivo de configuracion .xml</returns>
        public List<string> GetSecciones()
        {
            List<string> lista = new List<string>();

            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNodeList nodos = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).ChildNodes;

                foreach (XmlElement nodo in nodos)
                {
                    lista.Add(nodo.Name);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Agrega una clave nueva
        /// </summary>
        /// <param name="seccion">Nombre de la sección donde se creará la nueva clave.</param>
        /// <param name="nuevaClave">Nombre de la clave nueva</param>
        /// <param name="valorClave">Valor que tendrá la clave nueva</param>
        /// <returns>Devuelve <c>true</c> si la clave se agrega correctamente</returns>
        public bool AddClave(string seccion, string nuevaClave, string valorClave = "")
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode nClave = archivo.CreateElement(nuevaClave);

                nClave.InnerText = valorClave;
                archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion).AppendChild(nClave);
                archivo.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Agrega una sección nueva
        /// </summary>
        /// <param name="nuevaSeccion">Nombre de la sección nueva</param>
        /// <param name="nuevaClave">Nombre de la clave que se incluirá en la sección nueva</param>
        /// <param name="valorClave">Valor de la clave</param>
        /// <returns>Devuelve <c>true</c> si la sección se agrega correctamente</returns>
        public bool AddSeccion(string nuevaSeccion, string nuevaClave, string valorClave = "")
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode nSeccion = archivo.CreateElement(nuevaSeccion);
                XmlNode nClave = archivo.CreateElement(nuevaClave);

                nClave.InnerText = valorClave;
                nSeccion.AppendChild(nClave);
                archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).AppendChild(nSeccion);
                archivo.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edita el valor de una clave existente
        /// </summary>
        /// <param name="seccion">Nombre de la sección donde se encuentra la clave</param>
        /// <param name="clave">Nombre de la clave a editar</param>
        /// <param name="valor">Valor nuevo que tendrá la clave</param>
        /// <returns>Devuelte <c>true</c> si la clave se edita correctamente</returns>
        public bool SetClave(string seccion, string clave, string valor = "")
        {
            try
            {
                XmlDocument archivo = AbrirDocumento();
                XmlNode palabraClave = archivo.SelectSingleNode(ELEMENTO_PRINCIPAL).SelectSingleNode(seccion).SelectSingleNode(clave);

                palabraClave.InnerText = valor;
                archivo.Save(pRuta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Propiedades
        /// <summary>
        /// Devuelve o establese la ruta del del archivo .xml
        /// </summary>
        public string Ruta
        {
            get { return pRuta; }

            set
            {
                string rutaSinExt = value;
                int posExt = value.LastIndexOf('.');

                if (posExt > 0)
                {
                    int largoExt = value.Length - posExt;

                    rutaSinExt = value.Substring(0, posExt);
                }

                pRuta = rutaSinExt + ".param";
            }
        }

        #endregion
    }
}