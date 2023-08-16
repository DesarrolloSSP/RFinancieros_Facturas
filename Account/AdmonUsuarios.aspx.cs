using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Account
{
    public partial class AdmonUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            //Prueba
            {
                try
                {
                    string opcion = Request.QueryString["opcion"];
                    if (opcion == "B" && Request.QueryString["usuario"] != null)
                    {
                        if (Membership.DeleteUser(Convert.ToString(Request.QueryString["usuario"])))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Usuario eliminado correctamente','info')", true);
                            EliminaUsuario(Convert.ToString(Request.QueryString["usuario"]));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('No se pudo eliminar al usuario','warning')", true);
                        }
                    }
                    if (opcion == "S" && Request.QueryString["iduser"] != null && Request.QueryString["status"] != null)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        SqlConnection conexion = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand("upd_status_membership", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iduser", Request.QueryString["iduser"]);
                        cmd.Parameters.AddWithValue("@op", Request.QueryString["status"]);
                        conexion.Open();
                        cmd.ExecuteScalar();
                        cmd.Dispose();
                    }
                    if (opcion == "BL" && Request.QueryString["iduser"] != null && Request.QueryString["bloqueo"] != null)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        SqlConnection conexion = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand("upd_bloqueo_membership", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iduser", Request.QueryString["iduser"]);
                        cmd.Parameters.AddWithValue("@op", Request.QueryString["bloqueo"]);
                        conexion.Open();
                        cmd.ExecuteScalar();
                        cmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
                LlenarUsuarios();
            }
        }

        private void LlenarUsuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conexion = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("[sel_usuarios]", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@valor", tbBuscarx.Text.Trim());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgUsuarios.DataSource = dt;
            dgUsuarios.DataBind();
            conexion.Close();
        }

        protected void ImgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            LlenarUsuarios();
        }

        protected void DgUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgUsuarios.PageIndex = e.NewPageIndex;
            LlenarUsuarios();
        }

        protected void DdlpageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgUsuarios.PageSize = Convert.ToInt32(ddlpageSize.SelectedValue);
            LlenarUsuarios();
        }

        protected void Rd1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            foreach (GridViewRow rw in dgUsuarios.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("rd1") as RadioButton;
                        rd.Checked = false;
                    }
                }
            }
        }



        protected void DgUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Imgimprimeres_Command(object sender, CommandEventArgs e)
        {
            var usuario = ((ImageButton)sender).CommandArgument;
            //generaResguardo(usuario);
        }

        protected void Imgcancelar_Command(object sender, CommandEventArgs e)
        {
            var usuario = e.CommandArgument.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "confirma('Cerrar cuenta de " + usuario + " ¿Esta seguro?','AdmonUsuarios.aspx?opcion=B&Usuario=" + usuario + "')", true);
        }


        protected void Imgcambiarcontra_Command(object sender, CommandEventArgs e)
        {
            var usuario = e.CommandArgument.ToString();
            Session["usuario"] = usuario;
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "AbrirModal();", true);
        }
      
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario.aspx");
            //ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "AbrirNuevo();", true);
        }

        protected void DgUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string status;
            string bloqueo;
            foreach (GridViewRow rw in dgUsuarios.Rows)
            {
                CheckBox cbstatus = rw.FindControl("chkstatus") as CheckBox;
                CheckBox cbbloqueo = rw.FindControl("chkbloq") as CheckBox;
                //ImageButton img = rw.FindControl("Imgimprimeres") as ImageButton;
                //ScriptManager.GetCurrent(Page).RegisterPostBackControl(img);
                status = Convert.ToString(dgUsuarios.DataKeys[rw.RowIndex]["Status"]);
                bloqueo = Convert.ToString(dgUsuarios.DataKeys[rw.RowIndex]["Bloqueo"]);
                if (status == "S")
                {
                    cbstatus.Checked = true;
                }
                else
                {
                    cbstatus.Checked = false;
                }
                if (bloqueo == "True")
                {
                    cbbloqueo.Checked = true;
                }
                else
                {
                    cbbloqueo.Checked = false;
                }
            }
        }

        protected void Chkstatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox valorcheck = (CheckBox)sender;
            GridViewRow row = (GridViewRow)valorcheck.Parent.Parent;
            int a = row.RowIndex;
            string status = Convert.ToString(dgUsuarios.DataKeys[a]["Status"]);
            string userid = Convert.ToString(dgUsuarios.DataKeys[a]["UserId"]);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "confirma('Se cambiará el status de la cuenta ¿Esta seguro?','AdmonUsuarios.aspx?opcion=S&iduser=" + userid + "&status=" + status + "')", true);
        }

        public void EliminaUsuario(string nombre)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Datos"].ConnectionString;
                SqlConnection conexion = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("del_resguardo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", nombre);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void Chkbloq_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox valorcheck = (CheckBox)sender;
            GridViewRow row = (GridViewRow)valorcheck.Parent.Parent;
            int a = row.RowIndex;
            string bloqueo = "";
            //string bloqueo = Convert.ToString(dgUsuarios.DataKeys[a]["Bloqueo"]);
            if (valorcheck.Checked)
            {
                bloqueo = "1";
            }
            else
            {
                bloqueo = "0";
            }
            string userid = Convert.ToString(dgUsuarios.DataKeys[a]["UserId"]);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "confirma('Se cambiará el status del bloqueo ¿Esta seguro?','AdmonUsuarios.aspx?opcion=BL&iduser=" + userid + "&bloqueo=" + bloqueo + "')", true);
        }
    }
}