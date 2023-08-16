using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Account
{
    public partial class CambiaRole : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                llenarUsuarios();
                llenarRoles();
            }
        }

        private void llenarUsuarios()
        {
            try
            {
                MembershipUserCollection users = Membership.GetAllUsers();
                ddlusuarios.DataSource = users;
                ddlusuarios.DataBind();                
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        private void llenarRoles()
        {
            try
            {
                string[] roles = Roles.GetAllRoles();
                ddlroles.DataSource = roles;
                ddlroles.DataBind();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        protected void btncambiarRole_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    string[] rolesUusario = Roles.GetRolesForUser(ddlusuarios.SelectedValue);
                    Roles.RemoveUserFromRoles(ddlusuarios.SelectedValue, rolesUusario);
                    Roles.AddUserToRole(ddlusuarios.SelectedValue, ddlroles.SelectedValue);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El rol ha sido cambiado exitosamente','info')", true);
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
        }
    }
}