using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Account
{
    public partial class CrearUsuario : System.Web.UI.Page
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
                carga_roles();
            }
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {

                string usuario = txtUsuario.Text;
                string password = txtPassword.Text;
                //string nombre = txtNombreCompleto.Text;
                string email = txtEmail.Text;
                if (txtPassword.Text.Trim() != txtConfirmaPassword.Text.Trim())
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Las contraseñas no coinciden','warning')", true);
                    return;
                }
                if (!IsValidEmail(email))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El email es incorrecto','warning')", true);
                    return;
                }
                if (!Membership.ValidateUser(usuario, password) && (Membership.FindUsersByName(usuario) == null) || Membership.FindUsersByName(usuario).Count == 0 && IsValidEmail(email))
                {
                    MembershipUser usuarioCreado = Membership.CreateUser(usuario, password, email);
                    Roles.AddUserToRole(txtUsuario.Text,ddlrole.SelectedValue);
                    txtUsuario.Text = "";
                    txtPassword.Text = "";
                    txtConfirmaPassword.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('El usuario ha sido creado exitosamente','info')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Es probable que este usuario ya exista','info')", true);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('Se presento un problema al crear el usuario','info')", true);
            }
        }
        public void carga_roles()
        {
            try
            {
                string[] roles = Roles.GetAllRoles();
                ddlrole.DataSource = roles;
                ddlrole.DataBind();
                ddlrole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normaliza el dominio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                _ = ex.Message;
                return false;
            }
            catch (ArgumentException ex)
            {
                _ = ex.Message;
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}