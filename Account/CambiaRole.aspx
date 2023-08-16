<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CambiaRole.aspx.cs" Inherits="RFinancieros_Facturas.Account.CambiaRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
        <fieldset>
            <legend>Cambia Role del Usuario</legend>
            <asp:Label ID="lbusuario" runat="server" Text="Usuarios" class="small" Font-Bold="True"></asp:Label>
            <asp:DropDownList ID="ddlusuarios" runat="server" CssClass="form-control"></asp:DropDownList >
            <br />
            <asp:Label ID="lbrole" runat="server" Text="Roles" class="small" Font-Bold="True"></asp:Label>
            <asp:DropDownList ID="ddlroles" runat="server" CssClass="form-control"></asp:DropDownList>
            <br />
            <asp:Button ID="btncambiarRole" runat="server" Text="Cambiar Role" CausesValidation="true" OnClick="btncambiarRole_Click"/>
        </fieldset>
    </div>
</asp:Content>
