using System;

namespace SophY.ConexionBD
{
    public static class CadenaConexion
    {
        #region ODBC
        /// <summary>
        /// ODBC_SQL_SERVER = "Driver={SQL Server};Server={{SERVIDOR}};Database={{NOMBREBD}};Uid={{USUARIO}};Pwd={{PASSWORD}};"
        /// </summary>
        public static readonly string ODBC_SQL_SERVER = "Driver={SQL Server};Server={{SERVIDOR}};Database={{NOMBREBD}};Uid={{USUARIO}};Pwd={{PASSWORD}};";

        /// <summary>
        /// ODBC_SQL_NATIVE = "Driver={SQL Native Client};Server={{SERVIDOR}};Database={{NOMBREBD}};Uid={{USUARIO}};Pwd={{PASSWORD}};"
        /// </summary>
        public static readonly string ODBC_SQL_NATIVE = "Driver={SQL Native Client};Server={{SERVIDOR}};Database={{NOMBREBD}};Uid={{USUARIO}};Pwd={{PASSWORD}};";

        /// <summary>
        /// ODBC_TIMBERLINE = "Driver={Timberline Data};DBQ={{RUTACARPETA}};CODEPAGE=1252;DictionaryMode=0;StandardMode=1;MaxColSupport=1536;ShortenNames=0;DatabaseType=1;";
        /// </summary>
        public static readonly string ODBC_TIMBERLINE = "Driver={Timberline Data};DBQ={{RUTACARPETA}};CODEPAGE=1252;DictionaryMode=0;StandardMode=1;MaxColSupport=1536;ShortenNames=0;DatabaseType=1;";

        /// <summary>
        /// ODBC_MDF = "Integrated Security=SSPI;Initial Catalog={{RUTABD}};Data Source={{SERVIDOR}}\\{{NOMBREBD}};"
        /// </summary>
        public static readonly string ODBC_MDF = "Integrated Security=SSPI;Initial Catalog={{RUTABD}};Data Source={{SERVIDOR}}\\{{NOMBREBD}};";

        /// <summary>
        /// ODBC_ACCESS = "Driver={Microsoft Access Driver (*.mdb)};dbq={{RUTABD}};uid=admin;pwd="
        /// </summary>
        public static readonly string ODBC_ACCESS = "Driver={Microsoft Access Driver (*.mdb)};dbq={{RUTABD}};uid=admin;pwd=";

        /// <summary>
        /// ODBC_ACCESS_EXCLUSIVO = "Driver={Microsoft Access Driver (*.mdb)};Dbq={{RUTABD}};Exclusive=1;Uid=admin;Pwd=;"
        /// </summary>
        public static readonly string ODBC_ACCESS_EXCLUSIVO = "Driver={Microsoft Access Driver (*.mdb)};Dbq={{RUTABD}};Exclusive=1;Uid=admin;Pwd=;";

        /// <summary>
        /// ODBC_ACCESS_WORKGROUP = "Driver={Microsoft Access Driver (*.mdb)};Dbq={{RUTABD}};systemdb={{RUTAMDW}};"
        /// </summary>
        public static readonly string ODBC_ACCESS_WORKGROUP = "Driver={Microsoft Access Driver (*.mdb)};Dbq={{RUTABD}};systemdb={{RUTAMDW}};";

        /// <summary>
        /// ODBC_MYSQL = "Driver={MySQL ODBC 3.51 Driver};Server={{SERVIDOR}};Port=3306;Database={{NOMBREBD}};User={{USUARIO}};Password={{PASSWORD}};Option=3;"
        /// </summary>
        public static readonly string ODBC_MYSQL = "Driver={MySQL ODBC 3.51 Driver};Server={{SERVIDOR}};Port=3306;Database={{NOMBREBD}};User={{USUARIO}};Password={{PASSWORD}};Option=3;";

        /// <summary>
        /// ODBC_MYSQL_LOCAL = "Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database={{NOMBREBD}};User={{USUARIO}};Password={{PASSWORD}};Option=3;"
        /// </summary>
        public static readonly string ODBC_MYSQL_LOCAL = "Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database={{NOMBREBD}};User={{USUARIO}};Password={{PASSWORD}};Option=3;";

        /// <summary>
        /// ODBC_DBF = "Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq={{RUTACARPETA}};"
        /// </summary>
        public static readonly string ODBC_DBF = "Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq={{RUTACARPETA}};";

        /// <summary>
        /// ODBC_EXCEL = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq={{RUTAXLS}};defaultdir={{RUTACARPETA}}"
        /// </summary>
        public static readonly string ODBC_EXCEL = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq={{RUTAXLS}};defaultdir={{RUTACARPETA}}";

        /// <summary>
        /// ODBC_TXT = "DRIVER={Microsoft Text Driver (*.txt; *.csv)};DBQ={{RUTABD}};"
        /// </summary>
        public static readonly string ODBC_TXT = "DRIVER={Microsoft Text Driver (*.txt; *.csv)};DBQ={{RUTABD}};";

        #endregion

        #region OleDb
        /// <summary>
        /// OLEDB_ACCESS_PASS = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};Jet OLEDB:DataBase Password={{PASSWORD}}"
        /// </summary>
        public static readonly string OLEDB_ACCESS_PASS = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};Jet OLEDB:DataBase Password={{PASSWORD}}";

        /// <summary>
        /// OLEDB_ACCESS_WORKGROUP = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};Jet OLEDB:System DataBase={{RUTAMDW}}"
        /// </summary>
        public static readonly string OLEDB_ACCESS_WORKGROUP = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};Jet OLEDB:System DataBase={{RUTAMDW}}";

        /// <summary>
        /// OLEDB_ACCESS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};User Id=admin;Password="
        /// </summary>
        public static readonly string OLEDB_ACCESS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTABD}};User Id=admin;Password=";

        /// <summary>
        /// OLEDB_EXCEL12 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTAXLS}};Extended Properties=\"Excel 8.0 {{FORMATO}};HDR=YES;MaxScanRows=0\""
        /// </summary>
        public static readonly string OLEDB_EXCEL12 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTAXLS}};Extended Properties=\"Excel 8.0 {{FORMATO}};HDR=YES;MaxScanRows=0\"";

        /// <summary>
        /// OLEDB_EXCEL14 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTAXLS}};Extended Properties=\"Excel 12.0 {{FORMATO}};HDR=YES;MaxScanRows=0;IMEX=0\""
        /// </summary>
        public static readonly string OLEDB_EXCEL14 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTAXLS}};Extended Properties=\"Excel 12.0 {{FORMATO}};HDR=YES;MaxScanRows=0;IMEX=0\"";

        /// <summary>
        /// OLEDB_SQLITE = "Data Source={{RUTABD}};Pooling=true;FailIfMissing=false";
        /// </summary>
        public static readonly string OLEDB_SQLITE = "Data Source={{RUTABD}};Pooling=true;FailIfMissing=false";

        /// <summary>
        /// OLEDB_SQL_SERVER = "Provider=sqloledb;Data Source={{SERVIDOR}};Initial Catalog={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}}"
        /// </summary>
        public static readonly string OLEDB_SQL_SERVER = "Provider=sqloledb;Data Source={{SERVIDOR}};Initial Catalog={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}}";

        /// <summary>
        /// OLEDB_MYSQL = "Provider=MySQLProv;Data Source={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}}"
        /// </summary>
        public static readonly string OLEDB_MYSQL = "Provider=MySQLProv;Data Source={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}}";

        /// <summary>
        /// OLEDB_ACCESS_2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTABD}};Persist Security Info=False;"
        /// </summary>
        public static readonly string OLEDB_ACCESS_2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTABD}};Persist Security Info=False;";

        /// <summary>
        /// OLEDB_ACCESS_2007_PASS = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTABD}};Jet OLEDB:Database Password={{PASSWORD}};"
        /// </summary>
        public static readonly string OLEDB_ACCESS_2007_PASS = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={{RUTABD}};Jet OLEDB:Database Password={{PASSWORD}};";

        /// <summary>
        /// OLEDB_TXT = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTACARPETA}};Extended Properties=\"text;HDR=Yes;FMT=Delimited\""
        /// </summary>
        public static readonly string OLEDB_TXT = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTACARPETA}};Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";

        /// <summary>
        /// OLEDB_DBF = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTACARPETA}};Extended Properties=dBASE IV;User ID=Admin;Password=;"
        /// </summary>
        public static readonly string OLEDB_DBF = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={{RUTACARPETA}};Extended Properties=dBASE IV;User ID=Admin;Password=;";

        #endregion

        #region Otros
        /// <summary>
        /// SQLITE = "Data Source={{RUTABD}};Pooling=true;FailIfMissing=false"
        /// </summary>
        public static readonly string SQLITE = "Data Source={{RUTABD}};Pooling=true;FailIfMissing=false";

        /// <summary>
        /// SQLITE_PASS = "Data Source={{RUTABD}};Version=3;Password={{PASSWORD}};"
        /// </summary>
        public static readonly string SQLITE_PASS = "Data Source={{RUTABD}};Version=3;Password={{PASSWORD}};";

        /// <summary>
        /// CLIENTE_SQL = "Server={{SERVIDOR}};Database={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}};"
        /// </summary>
        public static readonly string CLIENTE_SQL = "Server={{SERVIDOR}};Database={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}};";

        /// <summary>
        /// Oracle_Standard = "Data Source={{NOMBREBD}};Integrated Security=yes;" (Sólo funciona con Oracle 8i release 3 o más reciente)
        /// </summary>
        public static readonly string Oracle_Standard = "Data Source={{NOMBREBD}};Integrated Security=yes;";

        /// <summary>
        /// Oracle = "Data Source={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}};Integrated Security = no;" (Sólo funciona con Oracle 8i release 3 o más reciente)
        /// </summary>
        public static readonly string Oracle = "Data Source={{NOMBREBD}};User Id={{USUARIO}};Password={{PASSWORD}};Integrated Security = no;";

        #endregion
    }
}