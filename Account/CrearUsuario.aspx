<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="RFinancieros_Facturas.Account.CrearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <fieldset>
            <legend>Formulario de Registro</legend>
            <ol>
                <li>
                    <asp:Label runat="server" class="small" Font-Bold="True" AssociatedControlID="txtnombre">Nombre</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtnombre"
                        CssClass="field-validation-error" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                    <br />
                    <asp:TextBox runat="server" ID="txtnombre" class="form-control" Width="300px" />
                </li>
                <li>
                    <asp:Label runat="server" class="small" Font-Bold="True" AssociatedControlID="txtUsuario">Usuario</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsuario"
                        CssClass="field-validation-error" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                    <br />
                    <asp:TextBox runat="server" ID="txtUsuario" class="form-control" Width="300px" />
                </li>
                <li>
                    <asp:Label runat="server" class="small" Font-Bold="True" AssociatedControlID="txtPassword">Password</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                        CssClass="field-validation-error" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                    <br />
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" class="form-control" Width="300px" />

                </li>
                <li>
                    <asp:Label runat="server" class="small" Font-Bold="True" AssociatedControlID="txtConfirmaPassword">Confirme password</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmaPassword"
                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                    <br />
                    <asp:TextBox runat="server" ID="txtConfirmaPassword" TextMode="Password" class="form-control" Width="300px" />
                    <br />
                    <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmaPassword"
                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La contraseña no coincide" />
                </li>
                <%--<li>
                        <asp:Label runat="server" AssociatedControlID="txtUsuario">Nombre completo de la Persona</asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreCompleto"
                            CssClass="field-validation-error" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                        <br />
                        <asp:TextBox runat="server" ID="txtNombreCompleto" />
                    </li>--%>
                <li>
                    <asp:Label runat="server" class="small" Font-Bold="True" AssociatedControlID="txtemail">email</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtemail"
                        CssClass="field-validation-error" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                    <br />
                    <asp:TextBox runat="server" ID="txtEmail" class="form-control" Width="300px" />
                </li>
                <li>
                    <asp:Label ID="lbrole" runat="server" Text="Role" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlrole" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlrole"
                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="CrearNuevoUsuario" ForeColor="Red" />
                </li>
                <%--<li>
                        <asp:Label runat="server" AssociatedControlID="txtUsuario">Cargo organizacional de la Persona (en caso de que aplique su cargo en la sección de firmas)</asp:Label>
                        <br />
                        <asp:TextBox runat="server" ID="txtCargoOrganizacional" />
                    </li>--%>
            </ol>
            <asp:Button ID="btnCrearUsuario" runat="server" Text="REGISTRAR" CausesValidation="true" ValidationGroup="CrearNuevoUsuario" OnClick="btnCrearUsuario_Click" />
        </fieldset>
    </div>
</asp:Content>
