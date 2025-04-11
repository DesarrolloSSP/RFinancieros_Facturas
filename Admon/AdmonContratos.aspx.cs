using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RFinancieros_Facturas.Datos;

namespace RFinancieros_Facturas.Admon
{
    public partial class AdmonContratos : System.Web.UI.Page
    {
        dbFacturasFinancierosEntities db = new dbFacturasFinancierosEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarContratos();
            }

        }

        private void CargarContratos()
        {
            var query = db.tcContrato.Where(x => x.idtc_contrato != 0).ToList();

            // Si no hay registros, agregamos un dummy sólo para mostrar el footer
            bool isEmpty = !query.Any();
            if (isEmpty)
            {
                query.Add(new tcContrato { idtc_contrato = -1, nocontrato = "", anio = 0 });
            }

            gvContratos.DataSource = query;
            gvContratos.DataBind();

            // Si era dummy, lo ocultamos después de que se cargue
            if (isEmpty)
            {
                gvContratos.Rows[0].Visible = false;
            }
        }


        protected void gvContratos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Fila de datos (modo edición)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    // 1. DropDownList de Año
                    DropDownList ddlAño = (DropDownList)e.Row.FindControl("ddlAñoEdit");
                    if (ddlAño != null)
                    {
                        LlenarAños(ddlAño);

                        // Obtener el valor del año desde el DataItem
                        string añoContrato = DataBinder.Eval(e.Row.DataItem, "anio").ToString();
                        ListItem item = ddlAño.Items.FindByValue(añoContrato);
                        if (item != null)
                        {
                            ddlAño.ClearSelection();
                            item.Selected = true;
                        }
                    }

                    // 2. TextBox de No. Contrato (limpia espacios)
                    TextBox txtNoContrato = (TextBox)e.Row.FindControl("txtNoContratoEdit");
                    if (txtNoContrato != null)
                    {
                        txtNoContrato.Text = txtNoContrato.Text.Trim();
                    }
                }

                // Confirmación en botón eliminar
                foreach (Control control in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
                {
                    if (control is LinkButton btn && btn.CommandName == "Delete")
                    {
                        btn.OnClientClick = "return confirm('¿Estás seguro de que deseas eliminar este contrato?');";
                    }
                }
            }

            // Fila de pie (footer)
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlAñoNuevo = (DropDownList)e.Row.FindControl("ddlAñoNuevo");
                if (ddlAñoNuevo != null)
                {
                    LlenarAños(ddlAñoNuevo);
                }
            }
        }





        private void LlenarAños(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("-- Seleccione --", ""));


            int añoActual = DateTime.Now.Year;
            for (int año = añoActual - 5; año <= añoActual; año++)
            {
                ddl.Items.Add(new ListItem(año.ToString(), año.ToString()));
            }
        }


        protected void gvContratos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvContratos.EditIndex = e.NewEditIndex;
            CargarContratos();
        }

        protected void gvContratos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvContratos.EditIndex = -1;
            CargarContratos();
        }

        protected void gvContratos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = (int)gvContratos.DataKeys[e.RowIndex].Value;
                GridViewRow row = gvContratos.Rows[e.RowIndex];

                // Buscar el TextBox del contrato
                TextBox txtNoContrato = (TextBox)row.FindControl("txtNoContratoEdit");
                string noContrato = txtNoContrato?.Text.Trim();


                // Buscar el DropDownList del año
                DropDownList ddlAño = (DropDownList)row.FindControl("ddlAñoEdit");

                if (string.IsNullOrEmpty(noContrato))
                {
                    // Puedes mostrar un mensaje aquí si usas ScriptManager
                    throw new Exception("El número de contrato no puede estar vacío.");
                }

                if (ddlAño == null || string.IsNullOrEmpty(ddlAño.SelectedValue))
                {
                    throw new Exception("Debe seleccionar un año.");
                }

                int anio = int.Parse(ddlAño.SelectedValue);

                tcContrato contrato = db.tcContrato.Find(id);
                if (contrato != null)
                {
                    contrato.nocontrato = noContrato;
                    contrato.anio = anio;

                    db.SaveChanges();
                }

                gvContratos.EditIndex = -1;
                CargarContratos();
            }
            catch (Exception ex)
            {
                
                // lblError.Text = ex.Message;
                
                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"alert('{ex.Message}');", true);
            }
        }


        protected void gvContratos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvContratos.DataKeys[e.RowIndex].Value;
            tcContrato contrato = db.tcContrato.Find(id);
            db.tcContrato.Remove(contrato);
            db.SaveChanges();
            CargarContratos();
        }


        protected void gvContratos_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Insert")
            {
                GridViewRow footer = gvContratos.FooterRow;
                TextBox txtNoContrato = (TextBox)footer.FindControl("txtNoContratoNuevo");
                DropDownList ddlAño = (DropDownList)footer.FindControl("ddlAñoNuevo");

                string noContrato = txtNoContrato.Text.Trim().ToUpper();
                int anio = int.Parse(ddlAño.SelectedValue);

                
                bool yaExiste = db.tcContrato.Any(c => c.nocontrato == noContrato && c.anio == anio);

                if (yaExiste)
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Ya existe un contrato con ese número y año.');", true);
                    return;
                }

                
                tcContrato nuevo = new tcContrato
                {
                    nocontrato = noContrato,
                    anio = anio
                };

                db.tcContrato.Add(nuevo);
                db.SaveChanges();

                CargarContratos();
            }

        }


    }

}