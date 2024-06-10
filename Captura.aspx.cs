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
    public partial class Captura : System.Web.UI.Page
    {
        public int Id;
        dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
            if (!IsPostBack)
            {
                LlenarFacturas();
                LlenarAreas();
                LlenarTipoGasto();
                LlenarCapturistas();
                //llenarstatus(); 
            }
        }

        private void LlenarFacturas()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_facturas]", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
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

        private void LlenarCapturistas()
        {
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[sel_capturista]", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvcapturistas.DataSource = dt;
                gvcapturistas.DataBind();
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
                SqlCommand cmd = new SqlCommand("[sel_tipogasto]", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
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
                SqlCommand cmd = new SqlCommand("sel_areas", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
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


        public static string TotalImporteOdp(string Odp)
        {
            string total = "";
            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                using (SqlCommand cmd = new SqlCommand("sel_total_odp", conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@odp", Odp);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //total = Convert.ToString(reader["Total"]);         
                        total = String.Format("{0:c}", reader["Total"]);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return "Importe Total por Odp(" + Odp + "): " + total;
        }

        public void AcccionBotones(string status)
        {
            try
            {
                switch (status)
                {
                    case "P":
                        btnguardar.Enabled = true;
                        btnCaptura.Enabled = false;
                        btnReingreso.Enabled = false;
                        EdicionCampos(true);
                        Panel3.Visible = false;
                        break;
                    case "T":
                        btnCaptura.Enabled = true;
                        if (User.IsInRole("Administrador"))
                        {
                            btnguardar.Enabled = true;

                            EdicionCampos(true);
                        }
                        else
                        {
                            btnguardar.Enabled = false;
                            EdicionCampos(false);
                        }
                        Panel3.Visible = false;
                        break;
                    case "C":
                        btnCaptura.Enabled = false;
                        btnReingreso.Enabled = false;
                        if (User.IsInRole("Administrador"))
                        {
                            btnguardar.Enabled = true;

                            EdicionCampos(true);
                        }
                        else
                        {
                            btnguardar.Enabled = false;
                            EdicionCampos(false);
                        }
                        tbEgreso.Enabled = true;
                        tbEgreso.Text = "";
                        Panel3.Visible = true;
                        break;
                    case "D":
                        btnReingreso.Enabled = true;
                        btnguardar.Enabled = false;
                        btnCaptura.Enabled = false;
                        EdicionCampos(false);
                        Panel3.Visible = false;
                        break;
                    case "E":
                        btnReingreso.Enabled = false;
                        if (User.IsInRole("Administrador"))
                        {
                            Panel3.Visible = true;
                            btnguardar.Enabled = true;
                            EdicionCampos(true);
                            tbEgreso.Enabled = true;
                        }
                        else
                        {
                            Panel3.Visible = false;
                            btnguardar.Enabled = false;
                            EdicionCampos(false);
                            tbEgreso.Enabled = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public void EdicionCampos(bool bandera)
        {
            ddlareas.Enabled = bandera;
            //ddlconcepto.Enabled = bandera;
            tbordpag.Enabled = bandera;
            tbfolint.Enabled = bandera;
            tbMotivo.Enabled = bandera;
            tbfecdev.Enabled = bandera;
            tbfolsuj.Enabled = bandera;
            ddlestatus.Enabled = bandera;
            ddlTipopago.Enabled = bandera;
            //chkdevol.Enabled = bandera;
            if (bandera)
            {
                ddlareas.SelectedIndex = 0;
                //ddltipogasto.SelectedIndex = 0;
                tbordpag.Text = "";
                tbfolint.Text = "";
                tbMotivo.Text = "";
                DateTime dt = DateTime.Now;
                tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
                ddlestatus.SelectedIndex = 0;
                //chkdevol.Checked = false;
                tbMotivo.Enabled = false;
                tbfecdev.Enabled = false;
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
                btnguardar.Enabled = true;
                DateTime dt = DateTime.Now;
                tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
            }
            else
            {
                ddlestatus.SelectedIndex = 0;
                tbMotivo.Text = "";
            }
        }

        protected void gvfacturas_PageIndexChanged(object sender, EventArgs e)
        {
            //if (tbbuscar.Text.Trim() != "")
            if (!string.IsNullOrEmpty(tbbuscar.Text.Trim()))
            {
                BuscarFacturas();
            }
            //else
            //{
            //    LlenarFacturas();
            //}
        }

        protected void gvfacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvfacturas.PageIndex = e.NewPageIndex;
            gvfacturas.DataBind();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarFacturas();
            Panel1.Visible = false;
        }

        private void BuscarFacturas()
        {
            try
            {

                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("sel_facturas_bus", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
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

                    //int? idPartida = string.IsNullOrEmpty(ddlPartida.SelectedValue) ? (int?)null : Convert.ToInt32(ddlPartida.SelectedValue);

                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[upd_datos_factura]", conectar)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@id", Session["Id"]);
                    cmd.Parameters.AddWithValue("@idarea", Convert.ToInt32(ddlareas.SelectedValue));
                    //cmd.Parameters.AddWithValue("@idpago", Convert.ToInt32(ddlTipopago.SelectedValue)); ya no se usa
                    cmd.Parameters.AddWithValue("@idtctipopago", Convert.ToInt32(ddlTipopago.SelectedValue));
                    cmd.Parameters.AddWithValue("@no", tbordpag.Text.Trim());
                    cmd.Parameters.AddWithValue("@fif", tbfolint.Text.Length == 0 ? "" : tbfolint.Text.Trim());
                    cmd.Parameters.AddWithValue("@motivo", tbMotivo.Text.Length == 0 ? "" : tbMotivo.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecdev", Convert.ToDateTime(tbfecdev.Text.Trim()));
                    cmd.Parameters.AddWithValue("@fecrev", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", sr.Trim());
                    cmd.Parameters.AddWithValue("@foliosujeto", tbfolsuj.Text.Length == 0 ? 0 : Convert.ToInt32(tbfolsuj.Text));
                    cmd.Parameters.AddWithValue("@usuario", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@cp", tbcp.Text.Length == 0 ? "" : tbcp.Text.Trim()); ;
                    //cmd.Parameters.AddWithValue("@idpartida", Convert.ToInt32(ddlPartida.SelectedValue));

                    int? idPartida = string.IsNullOrEmpty(ddlPartida.SelectedValue) ? (int?)null : Convert.ToInt32(ddlPartida.SelectedValue);
                    cmd.Parameters.AddWithValue("@idpartida", (object)idPartida ?? 0);

                    cmd.ExecuteScalar();
                    conectar.Close();
                    ddlareas.SelectedIndex = 0;
                    ddlestatus.SelectedIndex = 0;
                    //ddlconcepto.SelectedIndex = 0;
                    tbMotivo.Text = "";
                    tbordpag.Text = "";
                    tbfolint.Text = "";

                    txtPartida.Text = "";
                    ddlPartida.SelectedIndex = 0;
                    //ddlTercerosInst.SelectedIndex = 0;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El registro ha sido actualizado: " + Session["Id"] + "','info')", true);
                    Panel1.Visible = false;
                    //LlenarFacturas();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void BtnReingreso_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["NoOdp"].ToString().Trim() != "")
                {
                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[upd_factura_reingreso]", conectar)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@no", Session["NoOdp"]);
                    cmd.Parameters.AddWithValue("@usuario", User.Identity.Name.Trim());
                    cmd.ExecuteScalar();
                    conectar.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('La factura ha sido reingresada','info')", true);
                    LlenarFacturas();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este folio fiscal no contiene una order de pago','warning')", true);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void BtnCaptura_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
        }

        protected void Gvcapturistas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                //throw;
            }

        }

        protected void Rd2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            foreach (GridViewRow rw in gvcapturistas.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("rd2") as RadioButton;
                        rd.Checked = false;
                        Session["Capturista"] = gvcapturistas.Rows[a].Cells[2].Text;
                    }
                }
            }
        }

        protected void Btnfinal_Click(object sender, EventArgs e)
        {
            if ((Session["Capturista"] == null))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se seleccione el capturista','info')", true);
                return;
            }
            if (Session["NoOdp"].ToString().Trim() != "")
            {
                try
                {
                    SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                    SqlCommand cmd = new SqlCommand("[upd_odp_captura]", conectar)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@no", Session["NoOdp"]);
                    cmd.Parameters.AddWithValue("@iniciales", Session["Capturista"]);
                    cmd.ExecuteScalar();
                    conectar.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El registro ha sido enviado a captura','info')", true);
                    Panel2.Visible = false;
                    Panel1.Visible = false;
                    if (tbbuscar.Text.Trim() != "")
                    {
                        BuscarFacturas();
                    }
                    else
                    {
                        LlenarFacturas();
                    }
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este folio fiscal no contiene una order de pago','warning')", true);
            }
        }

        protected void BtnTermina_Click(object sender, EventArgs e)
        {
            if (tbEgreso.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el egreso','info')", true);
                return;
            }

            //else if (txtPartida.Text.Trim() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture la clave de partida','info')", true);
            //    return;
            //}
            //else if (ddlTercerosInst.SelectedValue == "0")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario seleccionar a un Tercero Institucional','info')", true);
            //    return;
            //}

            try
            {
                SqlConnection conectar = new ConectarSqlServer().conectarSQL();
                SqlCommand cmd = new SqlCommand("[upd_captura_egreso2]", conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", Session["Id"]);
                cmd.Parameters.AddWithValue("@egreso", tbEgreso.Text.Trim());
                cmd.Parameters.AddWithValue("@usuario", tbEgreso.Text.Trim());
                cmd.Parameters.AddWithValue("@idpartida", ddlPartida.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@idterceros", ddlTercerosInst.SelectedValue.ToString());

                cmd.ExecuteScalar();
                conectar.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Se ha captura el egreso al folio fiscal, exitosamente','info')", true);
                Panel2.Visible = false;
                Panel1.Visible = false;
                Panel3.Visible = false;
                if (tbbuscar.Text.Trim() != "")
                {
                    BuscarFacturas();
                }
                else
                {
                    LlenarFacturas();
                }
                Panel2.Visible = false;
                Panel3.Visible = false;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public bool ValidaDatos()
        {
            bool x = true;
            if (ddlTipopago.SelectedIndex != 1 && tbfolsuj.Text.Trim() == "")
            {
                tbfolsuj.Text = "0";
            }
            if (User.IsInRole("Administrador") && ddlestatus.SelectedItem.Text == "Cancelado")
            {
                return x;
            }
            if (User.IsInRole("Administrador") && ddlestatus.SelectedItem.Text == "Sin efecto")
            {
                ddlareas.SelectedIndex = 0;
                ddlTipopago.SelectedIndex = 0;
                // ddlconcepto.SelectedIndex = 0;
                tbordpag.Text = "";
                tbfolint.Text = "";
                tbMotivo.Text = "";
                DateTime dt = DateTime.Now;
                tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", dt);
                tbMotivo.Enabled = false;
                tbfecdev.Enabled = false;
                return x;
            }

            //if (ddlareas.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el área','warning')", true);
            //    x = false;
            //}

            //if (ddlconcepto.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el concepto','warning')", true);
            //    x = false;
            //}

            if (tbordpag.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture la orden de pago','warning')", true);
                x = false;
            }
            //if (tbfolint.Text.Trim() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es necesario se capture el folio interno','warning')", true);
            //    x = false;
            //}
            if (!User.IsInRole("Administrador") && Session["Estado"].ToString() == "Cancelado")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Este registro se encuentra cancelado, no es posible capturarlo','warning')", true);
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

        protected void gvcapturistas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcapturistas.PageIndex = e.NewPageIndex;
            LlenarCapturistas();
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

        protected void ddlareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlareas.SelectedIndex == 0)
            {
                tbfolint.Enabled = false;
                tbcp.Enabled = false;
            }
        }

        protected void Rd1_CheckedChanged(object sender, EventArgs e)
        {
            //Here
            try
            {
                RadioButton selectButton = (RadioButton)sender;
                GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
                int a = row.RowIndex;
                Session["Id"] = Convert.ToInt32(gvfacturas.DataKeys[a]["Id"]);
                Session["Estado"] = gvfacturas.Rows[a].Cells[2].Text.Trim();
                Session["NoOdp"] = Convert.ToString(gvfacturas.DataKeys[a]["NoOdp"]).Trim();

                var valorrfc = gvfacturas.Rows[a].Cells[4].Text.Trim();

                bool existe = ctx.tcproovedor_bloqueado.Any(x => x.rfc_proovedor == valorrfc);
                if (!existe)
                {
                    string valor = Session["NoOdp"].ToString();
                    if (valor.Trim() != "")
                    {
                        lblnumreg.Text = TotalImporteOdp(valor);
                    }
                    else
                    {
                        lblnumreg.Text = "Importe Total por Odp(): N/A";
                    }

                    foreach (GridViewRow rw in gvfacturas.Rows)
                    {
                        if (selectButton.Checked)
                        {
                            Panel1.Visible = true;
                            string osito;
                            string estado = gvfacturas.Rows[a].Cells[9].Text;
                            AcccionBotones(estado);
                            if (rw.RowIndex != a)
                            {
                                RadioButton rd = rw.FindControl("rd1") as RadioButton;
                                rd.Checked = false;
                            }
                            if (estado == "T" || estado == "C" || estado == "D" || estado == "E")
                            {
                                int id = Convert.ToInt32(Session["Id"]);
                                var consulta = ctx.tdfacturas.Where(p => p.Id == id).FirstOrDefault();
                                if (consulta != null)
                                {
                                    ddlareas.SelectedValue = consulta.Idarea.ToString();
                                    ddlTipopago.SelectedValue = consulta.idtctipo_pago.ToString();
                                    tbordpag.Text = consulta.NoOdp;
                                    tbfolint.Text = consulta.FolioInternoFactura;
                                    tbMotivo.Text = consulta.ConceptoDevol.Trim();
                                    tbfolsuj.Text = consulta.FolioSujeto.ToString();
                                    tbcp.Text = consulta.CodigoPostal is null ? "" : consulta.CodigoPostal.ToString();
                                    tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", consulta.FechaDevol);
                                    osito = consulta.Estado;
                                    ddlestatus.DataBind();
                                    ddlestatus.SelectedItem.Text = osito;
                                    if (osito == "Sin efecto")
                                    {
                                        ddlestatus.DataBind();
                                        ddlestatus.SelectedItem.Text = "Vigente";
                                    }
                                    tbEgreso.Text = consulta.Egreso.ToString();

                                    ddlPartida.DataBind();
                                    ddlPartida.SelectedValue = consulta.idtcpartida == null ? "0" : consulta.idtcpartida.ToString();


                                    tcpartida buscar = ctx.tcpartida.Where(x => x.idtcpartida == consulta.idtcpartida).FirstOrDefault();
                                    if (buscar != null)
                                    {
                                        txtPartida.Text = buscar.clave_partida.Trim();
                                    }
                                    else
                                    {
                                        txtPartida.Text = "";
                                    }


                                }
                            }
                            else
                            {
                                ddlestatus.DataBind();
                                ddlestatus.SelectedItem.Text = "Vigente";
                            }
                        }
                    }

                }

                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El proovedor se encuentra bloqueado','warning')", true);
                }
            }
            catch (Exception ex)
            {

                //throw;
            }



        }

        protected void gvfacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "VerDetalle")
                {
                    int Id = 0;
                    Id = int.Parse(e.CommandArgument.ToString());

                    Panel1.Visible = true;
                    string osito;

                    using (dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities())
                    {
                        tdfacturas consulta = ctx.tdfacturas.Where(x => x.Id == Id).FirstOrDefault();
                        if (consulta != null)
                        {
                            string estado = consulta.Estreg;
                            AcccionBotones(estado);

                            if (estado == "T" || estado == "C" || estado == "D" || estado == "E" || estado == "P")
                            {
                                lblFolioSeleccionado.Text = "Folio seleccionado:" + " " + consulta.Folio;
                                ddlareas.SelectedValue = consulta.Idarea.ToString();
                                ddlTipopago.DataBind();
                                ddlTipopago.SelectedValue = consulta.idtctipo_pago == null ? "5" : consulta.idtctipo_pago.ToString();
                                tbordpag.Text = consulta.NoOdp;
                                tbfolint.Text = consulta.FolioInternoFactura;
                                tbMotivo.Text = consulta.ConceptoDevol.Trim();
                                tbfolsuj.Text = consulta.FolioSujeto.ToString();
                                tbcp.Text = consulta.CodigoPostal is null ? "" : consulta.CodigoPostal.ToString();
                                tbfecdev.Text = String.Format("{0:yyyy-MM-dd}", consulta.FechaDevol);
                                osito = consulta.Estado;
                                ddlestatus.DataBind();
                                ddlestatus.SelectedItem.Text = osito;

                                if (osito == "Sin efecto")
                                {
                                    ddlestatus.DataBind();
                                    ddlestatus.SelectedItem.Text = "Vigente";
                                }
                                tbEgreso.Text = consulta.Egreso.ToString();

                                ddlPartida.DataBind();
                                ddlPartida.SelectedValue = consulta.idtcpartida == null ? "0" : consulta.idtcpartida.ToString();

                                Session["Id"] = consulta.Id;
                                Session["Estado"] = consulta.Estado;
                                Session["NoOdp"] = consulta.NoOdp;

                                string valor = Session["NoOdp"].ToString();
                                if (valor.Trim() != "")
                                {
                                    lblnumreg.Text = TotalImporteOdp(valor);
                                }
                                else
                                {
                                    lblnumreg.Text = "Importe Total por Odp(): N/A";
                                }



                                tcpartida buscar = ctx.tcpartida.Where(x => x.idtcpartida == consulta.idtcpartida).FirstOrDefault();
                                if (buscar != null)
                                {
                                    txtPartida.Text = buscar.clave_partida.Trim();
                                }
                                else
                                {
                                    txtPartida.Text = "";
                                }

                            }

                            else
                            {
                                ddlestatus.DataBind();
                                ddlestatus.SelectedItem.Text = "Vigente";
                            }


                        }

                    }


                }

            }
            catch (Exception ex)
            {

                //throw;
            }

        }


    }
}