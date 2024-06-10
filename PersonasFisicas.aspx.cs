using RFinancieros_Facturas.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class PersonasFisicas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarAreas();
                llenarTipoGasto();
                ruta.Visible = false;
                tbcadena.Focus();
            }
            DateTime dt = DateTime.Now;
            tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
            //tbfechae.Text = String.Format("{0:yyyy-MM-dd}", dt);
        }

        public void llenarAreas()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("sel_areas", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                ddlareas.DataSource = cmd.ExecuteReader();
                ddlareas.DataTextField = "nombre";
                ddlareas.DataValueField = "id";
                ddlareas.DataBind();
                //ddlareas.Items.Insert(0, new ListItem("--Area--"));
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        private void llenarTipoGasto()
        {
            try
            {
                //SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                //SqlCommand cmd = new SqlCommand("[sel_tipogasto]", conectar);
                //cmd.CommandType = CommandType.StoredProcedure;
                //ddlconcepto.DataSource = cmd.ExecuteReader();
                //ddlconcepto.DataTextField = "nombre";
                //ddlconcepto.DataValueField = "id";
                //ddlconcepto.DataBind();
                //ddlconcepto.Items.Insert(0, new ListItem("--Concepto--"));
                //conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void Tbverificar_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = "";
                string liga = "";
                resultado = tbcadena.Text.Trim();
                resultado = resultado.Replace("HTTPS://VERIFICACFDI.FACTURAELECTRONICA.SAT.GOB.MX/DEFAULT.ASPX", "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?&");
                liga = resultado.Replace("Ñ--", "://");
                liga = liga.Replace("'", "-");
                liga = liga.Replace("¿¿", "==");
                liga = liga.Replace("¿", "&");
                liga = liga.Replace("_", "?");
                liga = liga.Replace("mx-", "mx/");
                liga = liga.Replace("&ID=", "&id=");
                liga = liga.Replace("&RE=", "&re=");
                liga = liga.Replace("&RR=", "&rr=");
                liga = liga.Replace("&TT=", "&tt=");
                liga = liga.Replace("&FE=", "&fe=");
                liga = liga.Replace("id&", "&id=");
                liga = liga.Replace("re&", "&re=");
                liga = liga.Replace("rr&", "&rr=");
                liga = liga.Replace("tt&", "&tt=");
                liga = liga.Replace("fe&", "&fe=");
                liga = liga.Replace("/&re", "&re");
                liga = liga.Replace("/&rr", "&rr");
                liga = liga.Replace("/&tt", "&tt");
                liga = liga.Replace("/&fe", "&fe");
                string[] subs = liga.Split('&');
                var xx = subs[1].Substring(3, 36).ToUpper();
                tbFolio.Text = Convert.ToString(xx);
                xx = subs[2].ToString().Substring(3, 13).ToUpper();
                tbrfc.Text = xx;
                xx = Convert.ToDecimal(subs[4].ToString().Substring(3, subs[4].ToString().Length - 3)).ToString("0.00");
                tbImporte.Text = xx;
                if (liga != "" || liga != null)
                {
                    ruta.Visible = true;
                    ruta.Src = liga;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public bool ValidaDatos()
        {
            bool x = true;
            if (BuscaFolio())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este folio ya existe en la base de datos','warning')", true);
                x = false;
            }
            if (ddlTipopago.SelectedIndex != 1 && tbfolsuj.Text.Trim() == "")
            {
                tbfolsuj.Text = "0";
            }
            if (tbFolio.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el folio Fiscal','warning')", true);
                x = false;
            }

            if (tbImporte.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el Importe','warning')", true);
                x = false;
            }

            if (tbrfc.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el RFC','warning')", true);
                x = false;
            }
            if (tbrazon.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture la razón social','warning')", true);
                x = false;
            }

            //if (ddlareas.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el área','warning')", true);
            //    x = false;
            //}
            if (ddlPartida.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture la partida','warning')", true);
                x = false;
            }
            if (tbordpag.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture Número de Oficio/Tarjeta','warning')", true);
                x = false;
            }
            //if (tbfolint.Text.Trim() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el folio interno','warning')", true);
            //    x = false;
            //}
            if (chkdevol.Checked && tbMotivo.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el motivo de la devolución','warning')", true);
                x = false;
            }
            if (ddlTipopago.SelectedIndex == 1 && tbfolsuj.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el folio del sujeto','warning')", true);
                x = false;
            }
            //if (!ValidaCp())
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El código postal es incorrecto','warning')", true);
            //    x = false;
            //}


            if (ddlTercerosInst.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario seleccionar un Tercero Institucional','warning')", true);
                x = false;
            }

            return x;
        }

        public bool ValidaCp()
        {
            bool x = true;
            if (tbcp.Text.Length == 5)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            return x;
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
                cmd.Parameters.AddWithValue("@folio", tbFolio.Text.Trim());
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

        protected void Btnguardar_Click(object sender, EventArgs e)

        {
            try
            {
                if (ValidaDatos())
                {
                    string sr;
                    if (chkdevol.Checked)
                    {
                        sr = "Devolucion";
                    }
                    else
                    {
                        sr = ddlestatus.SelectedItem.Text;
                    }

                    var date_ = DateTime.Now.Date;

                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[ins_persona_fisica]", conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@folio", tbFolio.Text.Length == 0 ? "" : tbFolio.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", sr.Trim());
                    cmd.Parameters.AddWithValue("@importe", Convert.ToDecimal(tbImporte.Text.Trim()));
                    cmd.Parameters.AddWithValue("@fechae", tbfechae.Text.Length > 0 ? Convert.ToDateTime(tbfechae.Text.Trim()) : date_);
                    cmd.Parameters.AddWithValue("@rfce", tbrfc.Text.Trim());
                    cmd.Parameters.AddWithValue("@rse", tbrazon.Text.Trim());
                    cmd.Parameters.AddWithValue("@idarea", ddlareas.SelectedValue.Length == 0 ? 0 : Convert.ToInt32(ddlareas.SelectedValue));
                    //cmd.Parameters.AddWithValue("@idpago", Convert.ToInt32(ddlTipopago.SelectedValue));
                    cmd.Parameters.AddWithValue("@idtctipopago", Convert.ToInt32(ddlTipopago.SelectedValue));
                    cmd.Parameters.AddWithValue("@no", tbordpag.Text.Trim());
                    cmd.Parameters.AddWithValue("@fif", tbfolint.Text.Length == 0 ? "" : tbfolint.Text.Trim()); ;
                    cmd.Parameters.AddWithValue("@motivo", tbMotivo.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecdev", Convert.ToDateTime(tbfecdev.Text.Trim()));
                    cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                    //cmd.Parameters.AddWithValue("@idconcepto", Convert.ToInt32(ddlTipopago.SelectedIndex));
                    cmd.Parameters.AddWithValue("@idconcepto", 0);
                    cmd.Parameters.AddWithValue("@foliosujeto", Convert.ToInt32(tbfolsuj.Text));
                    cmd.Parameters.AddWithValue("@cp", tbcp.Text.Length == 0 ? "" : tbcp.Text.ToString());
                    cmd.Parameters.AddWithValue("@idtercero", ddlTercerosInst.SelectedValue.Length == 0 ? 0 : Convert.ToDecimal(ddlTercerosInst.SelectedValue));
                    cmd.Parameters.AddWithValue("@idpartida", Convert.ToInt32(ddlPartida.SelectedValue));
                    object inserta = cmd.ExecuteScalar();
                    conectar.Close();
                    if (inserta == null)
                    {
                        LimpiaCampos();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El registro fué grabado exitosamente','info')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void Chkdevol_CheckedChanged(object sender, EventArgs e)
        {
            tbMotivo.Enabled = chkdevol.Checked;
            tbfecdev.Enabled = chkdevol.Checked;
            ddlestatus.Enabled = !chkdevol.Checked;
            if (chkdevol.Checked)
            {
                ddlestatus.SelectedIndex = 2;
                //btnguardar.Enabled = true;
            }
            else
            {
                ddlestatus.SelectedIndex = 0;
                tbMotivo.Text = "";
            }
        }

        private void LimpiaCampos()
        {
            tbcadena.Text = "";
            tbFolio.Text = "";
            tbImporte.Text = "";
            tbrfc.Text = "";
            tbrazon.Text = "";
            txtPartida.Text = "";
            DateTime dt = DateTime.Now;
            tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
            tbfechae.Text = String.Format("{0:yyyy-MM-dd}", dt);
            ddlareas.SelectedIndex = 0;
            ddlestatus.SelectedIndex = 0;
            //ddlconcepto.SelectedIndex = 0;
            ddlPartida.SelectedIndex = 0;
            ddlTipopago.SelectedIndex = 0;
            tbordpag.Text = "";
            tbfolint.Text = "";
            tbMotivo.Text = "";
            tbordpag.Text = "";
            tbfolint.Text = "";
            chkdevol.Checked = false;
            tbMotivo.Text = "";
        }

        protected void DdlTipopago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipopago.SelectedValue == "1")
            {
                tbfolsuj.Enabled = true;
            }
            else
            {
                tbfolsuj.Enabled = false;
            }
        }

        protected void chkrecibo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkrecibo.Checked)
            {
                tbcadena.Enabled = false;
                tbFolio.Enabled = false;
                tbverificar.Enabled = false;
                Guid id = Guid.NewGuid();
                tbFolio.Text = id.ToString().Trim().ToUpper();
                tbFolio.Enabled = false;
            }
            else
            {
                tbcadena.Enabled = true;
                tbFolio.Enabled = true;
                tbverificar.Enabled = true;
                tbFolio.Text = "";
            }
        }

        protected void ddlTercerosInst_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTercerosInst.SelectedValue == "8")//correspondiente a sin especificar
            {
                ddlareas.Enabled = true;
            }
            else
            {

                ddlareas.Enabled = false;
            }

        }


        protected void txtPartida_TextChanged(object sender, EventArgs e)
        {
            mostrarPartida();
        }

        private void mostrarPartida()
        {
            try
            {
                using (dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities())
                {

                    tcpartida partida = ctx.tcpartida.Where(x => x.clave_partida == txtPartida.Text.Trim()).FirstOrDefault();
                    if (partida != null)
                    {
                        ddlPartida.DataBind();
                        ddlPartida.SelectedValue = partida.idtcpartida.ToString();
                    }
                    else
                    {
                        ddlPartida.Items.Clear();
                        ddlPartida.Items.Add(new ListItem("--NO EXISTE PARTIDA--", "0"));
                        ddlPartida.SelectedValue = "0";
                        txtPartida.Text = "";
                    }


                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}