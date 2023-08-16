<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EliminaFolio.aspx.cs" Inherits="RFinancieros_Facturas.EliminaFolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <asp:Label ID="lbfolñio" runat="server" Text="Folio" class="small" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="tbfolio" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />
    <div class="row">
        <asp:Button ID="BtnExportar" runat="server" CssClass="btn btn-warning" Text="Eliminar folio de la base de datos" OnClick="BtnExportar_Click" />
    </div>
</asp:Content>
