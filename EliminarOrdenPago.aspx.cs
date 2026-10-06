using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace RFinancieros_Facturas
{
    public partial class EliminarOrdenPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!User.IsInRole("Administrador"))
            //{
            //    FormsAuthentication.SignOut();
            //    Session.Abandon();
            //    Response.Redirect(Request.RawUrl, false);
            //    Response.Redirect("~Inicio/Login.aspx");
            //}
        }

        private bool ExisteOrdenPago(string op)
        {
            using (SqlConnection conexion = new ConectarSqlServer().conectarSQL())
            using (SqlCommand cmd = new SqlCommand("sel_op", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@op", op);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }

        private bool EliminarOP(string op)
        {
            using (SqlConnection conexion = new ConectarSqlServer().conectarSQL())
            using (SqlCommand cmd = new SqlCommand("del_op", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@op", op);

                // Próximamente...
                //
                // cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);

                object resultado = cmd.ExecuteScalar();

                int afectados = 0;

                if (resultado != null)
                    int.TryParse(resultado.ToString(), out afectados);

                return afectados > 0;
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            mensaje = mensaje.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(
                this,
                GetType(),
                Guid.NewGuid().ToString(),
                $"alertame('{mensaje}','{tipo}')",
                true);
        }

        protected void BtnEliminarOP_Click(object sender, EventArgs e)
        {
            string op = tbop.Text.Trim();

            if (string.IsNullOrWhiteSpace(op))
            {
                MostrarMensaje("Es necesario proporcionar la Orden de Pago.", "warning");
                return;
            }

            try
            {
                if (!ExisteOrdenPago(op))
                {
                    MostrarMensaje("La Orden de Pago no existe en la base de datos.", "warning");
                    return;
                }

                if (EliminarOP(op))
                {
                    MostrarMensaje("La Orden de Pago fue dada de baja correctamente.", "success");
                }
                else
                {
                    MostrarMensaje("No fue posible realizar la operación.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error: " + ex.Message.Replace("'", ""), "error");
            }
        }

        public bool BuscaOp()
        {
            bool x = true;
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_op]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@op", tbop.Text.Trim());
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