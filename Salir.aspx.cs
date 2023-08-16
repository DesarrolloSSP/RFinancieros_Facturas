using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class Salir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
            //    SqlCommand cmd = new SqlCommand("[del_factura_agrupada]", conectar);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
            //    cmd.ExecuteNonQuery();
            //    conectar.Close();
            //}
            //catch (Exception ex)
            //{
            //    _ = ex.Message;
            //}
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect(Request.RawUrl, false);
            Response.Redirect("~Inicio/Login.aspx");
        }
    }
}