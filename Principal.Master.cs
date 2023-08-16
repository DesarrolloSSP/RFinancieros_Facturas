using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        MembershipUser u; 
        protected void Page_Load(object sender, EventArgs e)
        {
            u = Membership.GetUser(Context.User.Identity.Name);
            if (!IsPostBack)
            {
                lbnombre.Text = u.UserName; 
            }
        }
    }
}