using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Inicio
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Login1.Focus();
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Membership.ValidateUser(UserName.Text, Password.Text))
                {
                    string[] roles = Roles.GetRolesForUser(UserName.Text);
                    int myIndex = Array.IndexOf(roles, "Administrador");
                    FormsAuthentication.RedirectFromLoginPage(UserName.Text, false);
                    if (myIndex != -1)
                    {
                        Response.Redirect("../CargaDatos.aspx");
                    }
                    else
                    {
                        Response.Redirect("../CapturaAgrupada.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}