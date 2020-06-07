using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SophY.ConexionBD
{
    /// <summary>
    /// Clase estática que cifra o descifra cadenas de texto
    /// </summary>
    public static class Encriptado
    {

        /// <summary>
        /// Limpia una cadena de texto eliminando los caracteres null
        /// </summary>
        /// <param name="texto">Texto que desea limpiar</param>
        /// <returns>Devuelve el texto sin los caracteres <c>null</c></returns>
        private static string QuitarNullCadena(string texto)
        {
            int posicionNull = 1;
            string textoSinNull = texto;

            while (posicionNull > 0)
            {
                posicionNull = textoSinNull.IndexOf('\0', posicionNull);
                if (posicionNull > 0)
                {
                    textoSinNull = textoSinNull.Substring(0, posicionNull) + textoSinNull.Substring(posicionNull + 1, textoSinNull.Length - posicionNull - 1);
                }

                if (posicionNull > textoSinNull.Length)
                {
                    break;
                }
            }

            return textoSinNull;
        }

        /// <summary>
        /// Oculta cadenas de texto por medio de una clave
        /// </summary>
        /// <param name="cadena">Texto que desea encriptar</param>
        /// <param name="clave">Palabra clave para el encriptado</param>
        /// <returns>Devuelve una cadena encriptada equivalente un texto determinado</returns>
        public static string Cifrar(string cadena, string clave)
        {
            string claveEncriptacion = clave;
            byte[] bytValor;
            byte[] bytKey;
            byte[] bytCodigo = new byte[0];
            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };
            int intLargo;
            //int intRestante;
            MemoryStream objMemoria = new MemoryStream();
            CryptoStream objCrypto;
            //RijndaelManaged objRijndael;
            char vCaracter;
            char nCaracter;
            string encriptara = "";

            if (cadena == "")
            {
                return "";
            }
            else
            {
                for (int i = 0; i < cadena.Length; i++)
                {
                    vCaracter = Convert.ToChar(cadena.Substring(i, 1));
                    nCaracter = Convert.ToChar(((int)vCaracter) + 1);
                    encriptara += nCaracter;
                }

                //Quitar nulos en la cadena de texto a encriptar
                cadena = QuitarNullCadena(encriptara);

                bytValor = Encoding.ASCII.GetBytes(encriptara.ToCharArray());
                intLargo = clave.Length;

                //La clave de cifrado debe ser de 256 bits de longitud (32 bytes)
                //Si tiene más de 32 bytes se truncará
                //Si es menor de 32 bytes se rellenará con X
                if (intLargo >= 32)
                {
                    claveEncriptacion = claveEncriptacion.Substring(0, 32);
                }
                else
                {
                    //intLargo = claveEncriptacion.Length;
                    //intRestante = 32 - intLargo;
                    claveEncriptacion = claveEncriptacion.PadRight(32, Convert.ToChar("Y"));
                }

                bytKey = Encoding.ASCII.GetBytes(claveEncriptacion.ToCharArray());

                var objRijndael = Aes.Create();// new RijndaelManaged();

                try
                {
                    //Crear objeto Encryptor y escribir su valor 
                    //después de que se convierta en array de bytes
                    objCrypto = new CryptoStream(objMemoria, objRijndael.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write);
                    objCrypto.Write(bytValor, 0, bytValor.Length);
                    objCrypto.FlushFinalBlock();

                    bytCodigo = objMemoria.ToArray();
                    objMemoria.Dispose();
                    objCrypto.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                //Devolver el valor del texto encriptado
                //convertido de array de bytes a texto en base64
                return Convert.ToBase64String(bytCodigo);
            }
        }

        /// <summary>
        /// Muestra o resuelve el texto original de una cadena encriptada
        /// </summary>
        /// <param name="cadena">Cadena encriptada</param>
        /// <param name="clave">Palabra clave que utilizó en el encriptado</param>
        /// <returns>Devuelve la traducción de una cadena encriptada</returns>
        public static string Descifrar(string cadena, string clave)
        {
            string claveDesencriptacion = clave;
            byte[] bytDatoDesencriptar;
            byte[] bytTemp;
            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };
            //RijndaelManaged objRijndael = new RijndaelManaged();
            var objRijndael = Aes.Create();
            MemoryStream objMemory;
            CryptoStream objCrypto;
            byte[] bytClaveDesencriptado;
            int intLargo;
            int intRestante;
            string strRetorno = string.Empty;
            string Desencriptado;
            char vCaracter;
            char nCaracter;

            if (cadena == "")
            {
                return "";
            }
            else
            {
                //Convertir el valor encriptado base64 a array de bytes
                bytDatoDesencriptar = Convert.FromBase64String(cadena);

                //La clave de desencriptación debe ser de 256 bits de longitud (32 bytes)
                //Si tiene más de 32 bytes se truncará
                //Si es menor de 32 bytes se rellenará con Y
                intLargo = claveDesencriptacion.Length;

                if (intLargo >= 32)
                {
                    claveDesencriptacion = claveDesencriptacion.Substring(0, 32);
                }
                else
                {
                    intLargo = claveDesencriptacion.Length;
                    intRestante = 32 - intLargo;
                    claveDesencriptacion += new string('Y', intRestante);
                }

                bytClaveDesencriptado = Encoding.ASCII.GetBytes(claveDesencriptacion.ToCharArray());
                bytTemp = new byte[bytDatoDesencriptar.Length];
                objMemory = new MemoryStream(bytDatoDesencriptar);

                try
                {
                    //Crear objeto Dencryptor y escribir su valor 
                    //después de que se convierta en array de bytes
                    objCrypto = new CryptoStream(objMemory, objRijndael.CreateDecryptor(bytClaveDesencriptado, bytIV), CryptoStreamMode.Read);
                    objCrypto.Read(bytTemp, 0, bytTemp.Length);
                    objMemory.Dispose();
                    objCrypto.Dispose();

                    //Devolver la cadena de texto desencriptada
                    //convertida de array de bytes a cadena de texto ASCII
                    Desencriptado = QuitarNullCadena(Encoding.ASCII.GetString(bytTemp));

                    for (int i = 0; i < Desencriptado.Length; i++)
                    {
                        vCaracter = Convert.ToChar(Desencriptado.Substring(i, 1));
                        nCaracter = Convert.ToChar(((int)vCaracter) - 1);
                        strRetorno += nCaracter;
                    }

                    return strRetorno;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
