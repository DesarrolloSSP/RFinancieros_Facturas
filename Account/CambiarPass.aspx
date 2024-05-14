<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CambiarPass.aspx.cs" Inherits="RFinancieros_Facturas.Account.CambiarPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblUsuario" runat="server" Text="USUARIO A MODIFICAR:" />
                <br />
                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="NUEVA CONTRASEÑA:" />
                <br />
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" OnClick="btnCambiar_Click" /></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
    </table>

</asp:Content>
