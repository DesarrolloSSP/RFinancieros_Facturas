using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas
{
    class ConectarSqlServer
    {
        public SqlConnection conectarSQL()
        {
            try
            {
                string servidor = ConfigurationManager.AppSettings["servidor"];
                string bd = ConfigurationManager.AppSettings["bd"];
                string usuario = ConfigurationManager.AppSettings["usuario"];
                string clave = ConfigurationManager.AppSettings["clave"];
                string cadenaDeconeccion = "DATA SOURCE=" + servidor + ";INITIAL CATALOG=" + bd + ";USER= " + usuario + ";PASSWORD=" + clave + ";";
                SqlConnection conectar = new SqlConnection(cadenaDeconeccion);
                conectar.Open();
                return conectar;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}