<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EliminarOp.aspx.cs" Inherits="RFinancieros_Facturas.EliminarOp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <asp:Label ID="lblop" runat="server" Text="Orden de Pago" class="small" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="tbop" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />
    <div class="row">
        <asp:Button ID="BtnExportar" runat="server" CssClass="btn btn-warning" Text="Eliminar Orden de pago de la base de datos" OnClick="BtnExportar_Click" />
    </div>
</asp:Content>
