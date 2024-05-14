using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Account
{
    public partial class CambiarPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCambiar_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtLogin.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                string username = txtLogin.Text;
                string password = txtPassword.Text;
                MembershipUser mu = Membership.GetUser(username);
                //mu.LastLoginDate
                if (mu != null)
                {
                    mu.IsApproved = true;
                    if (mu.IsLockedOut)
                    {
                        mu.UnlockUser();
                    }
                    mu.ChangePassword(mu.ResetPassword(), password);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Contraseña modificada')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Verifique el nombre de usuario')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Ingrese usuario y contraseña')", true);
            }

        }

    }
}