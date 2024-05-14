using RFinancieros_Facturas.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class CapturaAgrupada : System.Web.UI.Page
    {
        public int Id;
        dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string id = Request.QueryString["id"];
                    if (id != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El folio fiscal se ha eliminado correctamente!','info')", true);
                        EliminaFolio(Convert.ToInt32(id));
                    }
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
                LlenarAreas();
                LlenarTipoGasto();
                LlenarAgrupadas();
                //llenarOrdenes(); 
                //llenarstatus(); 
            }
        }       

        private void LlenarFacturas()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_facturas]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvfacturas.DataSource = dt;
                gvfacturas.DataBind();
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        //private void llenarOrdenes()
        //{
        //    try
        //    {
        //        SqlConnection conectar = new ConectarSqlServer().conectarSQL();
        //        SqlCommand cmd = new SqlCommand("[sel_ordenes]", conectar);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
        //        ddlordenes.DataSource = cmd.ExecuteReader();
        //        ddlordenes.DataTextField = "Noodp";
        //        ddlordenes.DataValueField = "revisor";
        //        ddlordenes.DataBind();
        //        //ddlconcepto.Items.Insert(0, new ListItem("--Concepto--"));
        //        conectar.Close();
        //        if (ddlordenes.Items.Count == 0 )
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este usuario no tiene asignadas ordenes de Pago','warning')", true);
        //            //Response.Redirect("Captura.aspx");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _ = ex.Message;
        //    }
        //}

        private void LlenarAgrupadas()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("sel_facturas_agrupadas", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dta = new DataTable();
                adapter.Fill(dta);
                dgvfolios.DataSource = dta;
                dgvfolios.DataBind();
                if (dgvfolios.Rows.Count != 0)
                {
                    Panel2.Visible = true;                    
                    btnfinalizar.Enabled = true;
                    tbordpag.Enabled = false;
                    //ddlordenes.Enabled = false;
                    ddlareas.Enabled = false;
                    //ddlconcepto.Enabled = false;
                    ddlTipopago.Enabled = false;
                    tbfolsuj.Enabled = false;
                    int id = Convert.ToInt32(dgvfolios.DataKeys[0]["Id"]);
                    var consulta = ctx.tdfacturas.Where(p => p.Id == id).FirstOrDefault();
                    if (consulta != null)
                    {
                        tbordpag.Text = consulta.NoOdp.ToString();
                        ddlareas.SelectedValue = consulta.Idarea.ToString();
                        //ddlconcepto.SelectedValue = consulta.IdPago.ToString();
                        ddlTipopago.SelectedValue = consulta.Idconcepto.ToString();
                        tbfolsuj.Text = consulta.FolioSujeto.ToString();
                        lblnumreg.Text = TotalImporteOdp(consulta.NoOdp.ToString());                        
                        tbcp.Text = consulta.CodigoPostal.ToString();
                        tbfolintf.Text  = consulta.FolioInternoFactura.ToString(); 
                    }
                }
                else
                {
                    Panel2.Visible = false;
                    btnfinalizar.Enabled = true;
                    tbordpag.Enabled = true ;
                    //ddlordenes.Enabled = true;
                    ddlareas.Enabled = true;
                    //ddlconcepto.Enabled = true;
                    ddlTipopago.Enabled = true;
                }
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        private void LlenarTipoGasto()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_tipogasto]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                //ddlconcepto.DataSource = cmd.ExecuteReader();
                //ddlconcepto.DataTextField = "nombre";
                //ddlconcepto.DataValueField = "id";
                //ddlconcepto.DataBind();
                //ddlconcepto.Items.Insert(0, new ListItem("--Concepto--"));
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public void LlenarAreas()
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

        //public void llenarstatus()
        //{
        //    try
        //    {
        //        SqlConnection conectar = new ConectarSqlServer().conectarSQL();
        //        SqlCommand cmd = new SqlCommand("[sel_status]", conectar);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adapter.Fill(dt);
        //        ddlestatus.DataSource = dt;
        //        ddlestatus.DataBind();
        //        conectar.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void Gvfacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].HasControls())
                {
                    foreach (var control in e.Row.Cells[0].Controls)
                    {
                        if (!(control is CheckBox)) continue;
                        var checkBox = (CheckBox)control;
                        checkBox.Attributes.Add("CheckedChanged", "errorCheckChanged");
                        return;
                    }
                }
            }
        }

        protected void Rd1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            Session["Id"] = Convert.ToInt32(gvfacturas.DataKeys[a]["Id"]);
            string estado = Convert.ToString(gvfacturas.DataKeys[a]["Estreg"]); 
            foreach (GridViewRow rw in gvfacturas.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("rd1") as RadioButton;
                        rd.Checked = false;
                    }
                    //Panel2.Visible = true;
                    Panel3.Visible = true;
                    if (estado != "P")
                    {
                        tbcp.Enabled = false;
                        tbfolintf.Enabled = false;
                    }
                    else
                    {
                        tbcp.Enabled = true;
                        tbfolintf.Enabled = true;
                    }
                    int id = Convert.ToInt32(Session["Id"]);
                }
                else
                {
                    Panel3.Visible = false;
                }
            }
        }

        public static string TotalImporteOdp(string Odp)
        {
            string total = "";
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("sel_total_odp", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@odp", Odp);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //total = Convert.ToString(reader["Total"]);
                    total = String.Format("{0:c}", reader["Total"]);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return "Importe Total por Odp(" + Odp + "): " + total;
        }

        protected void Gvfacturas_PageIndexChanged(object sender, EventArgs e)
        {
            if (tbbuscar.Text.Trim() != "")
            {
                BuscarFacturas();
            }
            else
            {
                LlenarFacturas();
            }
        }

        protected void Gvfacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvfacturas.PageIndex = e.NewPageIndex;
            gvfacturas.DataBind();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (ValidaDatosGrupo())
            {
                BuscarFacturas();
            }
        }

        private void BuscarFacturas()
        {
            try
            {
                Panel3.Visible = false;
                gvfacturas.Visible = true;
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("sel_facturas_bus", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@valor", tbbuscar.Text.Trim());
                cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvfacturas.DataSource = dt;
                gvfacturas.DataBind();
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public bool ValidaDatos()
        {
            bool y = true;
            if (tbfolintf.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario el folio interno factura','warning')", true);
                y = false;
            }

            if (!ValidaCp())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El código postal es incorrecto','warning')", true);
                y = false;
            }
            return y;
        }

        public bool ValidaDatosGrupo()
        {
            bool x = true;
            //Validar 
            if (tbordpag.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el número de orden de pago','warning')", true);
                x = false;
            }
            if (ddlareas.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el área que trámita','warning')", true);
                x = false;
            }
            //if (ddlconcepto.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el concepto','warning')", true);
            //    x = false;
            //}
            //if (ddlconcepto.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el concepto','warning')", true);
            //    x = false;
            //}
            if (ddlTipopago.SelectedIndex == 1 && tbfolsuj.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el folio del sujeto','warning')", true);
                x = false;
            }              
            return x;
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

        protected void Chkfolios_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox valorcheck = (CheckBox)sender;
            GridViewRow row = (GridViewRow)valorcheck.Parent.Parent;
            int a = row.RowIndex;
            string folio = Convert.ToString(dgvfolios.DataKeys[a]["Id"]);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "confirma('Se eliminará el folio fiscal de la orden de pago ¿Esta seguro?','CapturaAgrupada.aspx?id=" + folio + "')", true);
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaDatos())
                {
                    if (ddlTipopago.SelectedIndex != 1 && tbfolsuj.Text.Trim() == "")
                    {
                        tbfolsuj.Text = "0";
                    }
                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[upd_datos_factura_agrupada]", conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", Session["Id"]);
                    cmd.Parameters.AddWithValue("@idarea", Convert.ToInt32(ddlareas.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@idpago", Convert.ToInt32(ddlTipopago.SelectedValue));
                    cmd.Parameters.AddWithValue("@idconcepto", 0);
                    //cmd.Parameters.AddWithValue("@idconcepto", Convert.ToInt32(ddlTipopago.SelectedIndex));
                    cmd.Parameters.AddWithValue("@no", tbordpag.Text.Trim());
                    //cmd.Parameters.AddWithValue("@no", ddlordenes.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@foliosujeto", Convert.ToInt32(tbfolsuj.Text));
                    cmd.Parameters.AddWithValue("@fif", tbfolintf.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecrev", DateTime.Now);
                    cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@cp", tbcp.Text.Trim());
                    object inserta = cmd.ExecuteScalar();
                    conectar.Close();
                    if (inserta == null)
                    {
                        tbbuscar.Text = "";
                        gvfacturas.Visible = false;
                        Panel3.Visible = false;
                        LlenarAgrupadas();
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            }

        protected void Btnfinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[del_factura_agrupada]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                cmd.Parameters.AddWithValue("@no", tbordpag.Text.Trim());
                //cmd.Parameters.AddWithValue("@no", ddlordenes.SelectedItem.Text);
                object borrar = cmd.ExecuteScalar();
                conectar.Close();
                if (borrar== null)
                {
                    LlenarAgrupadas();
                    //llenarOrdenes();
                    tbbuscar.Text = "";
                    gvfacturas.Visible = false;
                    Panel3.Visible = false;
                    Panel2.Visible = false;                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('La orden de Pago ha sido guarda correctamente','info')", true);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
                ddlareas.SelectedIndex = 0;
                ddlTipopago.SelectedIndex = 0;
                //ddlconcepto.SelectedIndex = 0;
                tbordpag.Text = "";
                tbfolsuj.Text = "";
                tbfolintf.Text = "";
            }

        public void EliminaFolio(int folio)
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[del_datos_factura_agrupada]", conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", folio);
                cmd.ExecuteNonQuery();
                conectar.Close();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
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


        protected void txtPartida_TextChanged(object sender, EventArgs e)
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
                    //ddlPartida.Items.Add(new ListItem("--Seleccione--", "0"));
                    ddlPartida.Text = "NO EXISTE";
                }

            }
        }
    }    
}