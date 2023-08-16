<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RFinancieros_Facturas._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    asp:LoginView ID="LoginView2" runat="server">
        <anonymoustemplate>
            <div class="ClassAlto10"></div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Login ID="LoginPrincipal" runat="server" LoginButtonText="Entrar" BorderStyle="Solid" RememberMeText="" TitleText="" DisplayRememberMe="False" BackColor="#B7B7B7" FailureText="Error de usuario y/o contraseña." PasswordRequiredErrorMessage="El password es requerido " UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="Escriba el nombre del usuario" BorderColor="#666666" BorderWidth="1">
                            <LayoutTemplate>
                                <div class="ClassAlto10"></div>
                                <table class="ClassLogin" cellpadding="0" cellspacing="10">
                                    <tr>
                                        <td width="2%"></td>
                                        <td width="47%" align="right"><b>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label></b></td>
                                        <td width="2%"></td>
                                        <td width="47%" align="left">
                                            <asp:TextBox ID="UserName" runat="server" ForeColor="#333333" BackColor="#D9DADC" CssClass="ClassNombreUsuario"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Escriba el nombre del usuario" ToolTip="Escriba el nombre del usuario" ValidationGroup="ctl00$LoginPrincipal" Display="None">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td width="2%"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td align="right"><b>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></b></td>
                                        <td></td>
                                        <td align="left">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" ForeColor="#333333" BackColor="#D9DADC"></asp:TextBox>
                                            <%--onkeypress="capLock(event)"--%>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="El password es requerido " ToolTip="El password es requerido " ValidationGroup="ctl00$LoginPrincipal" Display="None">*</asp:RequiredFieldValidator>
                                            <%--<div id="divMayus" style="visibility:hidden">Mayúsculas activadas</div>--%>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <div align="center" style="color: Red; font-family: Flama, Arial;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></div>
                                <div align="center">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Entrar" ValidationGroup="ctl00$LoginPrincipal" /></div>
                                <div class="ClassAlto10"></div>
                            </LayoutTemplate>
                        </asp:Login>
                    </td>
                </tr>
            </table>
            asp:LoginView ID="LoginView2" runat="server">
        <anonymoustemplate>
            <div class="ClassAlto10"></div>
</asp:Content>
