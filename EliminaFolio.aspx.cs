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
    public partial class EliminaFolio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Administrador"))
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect(Request.RawUrl, false);
                Response.Redirect("~Inicio/Login.aspx");
            }
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            if(tbfolio.Text.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario propocione el folio a Eliminar','warning')", true);
            }
            else
            {
                if (BuscaFolio())
                {
                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[del_folio]", conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@folio", tbfolio.Text.Trim());
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El folio ha sido borrado de la base de datos','info')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este folio no existe en la base de datos','warning')", true);
                }
            }

        }

        public bool BuscaFolio()
        {
            bool x = true;
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_folio]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@folio", tbfolio.Text.Trim());
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    x = false;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return x;
        }
    }
}